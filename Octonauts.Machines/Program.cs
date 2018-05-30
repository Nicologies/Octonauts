using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using coreArgs.Attributes;
using Octonauts.Core;
using Octonauts.Core.OctopusClient;
using Octopus.Client;
using Octopus.Client.Extensibility;
using Octopus.Client.Model;

namespace Octonauts.Machines
{
    internal class MachineInEnvParams : OctopusParams
    {
        [Option("env", "environment", required: true)]
        public string Environment { get; set; }

        [Option("sleep", "sleep couple of seconds after performed action for a machine", required: false)]
        public short SleepSeconds { get; set; } = 0;
    }

    internal class DeployParams : MachineInEnvParams
    {
        [Option("project", "project to deploy", required: true)]
        public string Project { get; set; }

        [Option("version", "version to deploy", required: true)]
        public string Version { get; set; }
    }

    internal class MachineParams : OctopusParams
    {
        [Option("disable-enable-in-env", "disable and then enable machines", required: false)]
        public bool DisableThenEnable { get; set; }

        [Option("deploy", "deploy a project to machines in env", required: false)]
        public bool Deploy { get; set; }
    }

    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var machineParams = CommandArgsParaser.Parse<MachineParams>(args);

            if (machineParams.DisableThenEnable)
            {
                await DisableThenEnableMachinesInEnv(args);
            }

            if (machineParams.Deploy)
            {
                await DeployProjectToMachinesInEnv(args);
            }
        }

        private static async Task DeployProjectToMachinesInEnv(string[] args)
        {
            var deployParams = CommandArgsParaser.Parse<DeployParams>(args);
            deployParams.FillOctopusParams();

            using (var client = await OctopusClientProvider.GetOctopusClient(deployParams))
            {
                var env = await GetEnvironment(client, deployParams.Environment);
                var machines = await GetActiveMachines(client, env);
                var project = await client.Repository.Projects.FindByName(deployParams.Project);
                var release = await client.Repository.Projects.GetReleaseByVersion(project, deployParams.Version);

                foreach (var machine in machines)
                {
                    var depResource = new DeploymentResource
                    {
                        Comments =
                            $"Individually deploy project to machines in environment {deployParams.Environment}",
                        TenantId = null,
                        EnvironmentId = env.Id,
                        SkipActions = new ReferenceCollection(),
                        ReleaseId = release.Id,
                        ForcePackageDownload = false,
                        UseGuidedFailure = false,
                        SpecificMachineIds = new ReferenceCollection(machine.Id),
                        ForcePackageRedeployment = false,
                        FormValues = new Dictionary<string, string>(),
                        QueueTime = null
                    };

                    await client.Repository.Deployments.Create(depResource);

                    if (deployParams.SleepSeconds > 0)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(deployParams.SleepSeconds));
                    }
                }
            }
        }

        private static async Task DisableThenEnableMachinesInEnv(string[] args)
        {
            var enableDisableMachineParams = CommandArgsParaser.Parse<MachineInEnvParams>(args);
            enableDisableMachineParams.FillOctopusParams();

            using (var client = await OctopusClientProvider.GetOctopusClient(enableDisableMachineParams))
            {
                var env = await GetEnvironment(client, enableDisableMachineParams.Environment);
                var machines = await GetActiveMachines(client, env);
                foreach (var machine in machines)
                {
                    Console.WriteLine($"Disabling {machine.Name}");
                    machine.IsDisabled = true;
                    await client.Repository.Machines.Modify(machine);

                    Console.WriteLine($"Enabling {machine.Name}");
                    machine.IsDisabled = false;
                    await client.Repository.Machines.Modify(machine);

                    if (enableDisableMachineParams.SleepSeconds > 0)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(enableDisableMachineParams.SleepSeconds));
                    }
                }
            }
        }

        private static async Task<List<MachineResource>> GetActiveMachines(IOctopusAsyncClient client, IResource env)
        {
            var machines = await client.Repository.Machines.List(environmentIds: env.Id, take: int.MaxValue, isDisabled: false);
            var badStatus = new[]
            {
                MachineModelStatus.Disabled,
                MachineModelStatus.Offline,
                MachineModelStatus.Unknown
            };

            var badHealthStatus = new[]
            {
                MachineModelHealthStatus.Unavailable,
                MachineModelHealthStatus.Unhealthy,
                MachineModelHealthStatus.Unknown
            };

            return machines.Items.Where(x => !badStatus.Contains(x.Status))
                .Where(x => !badHealthStatus.Contains(x.HealthStatus))
                .ToList();
        }

        private static async Task<EnvironmentResource> GetEnvironment(IOctopusAsyncClient client, string name)
        {
            var env = await client.Repository.Environments.FindByName(name);
            return env;
        }
    }
}
