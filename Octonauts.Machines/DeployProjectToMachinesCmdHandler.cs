using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Octonauts.Core.CommandsFramework;
using Octonauts.Core.OctopusClient;
using Octopus.Client;
using Octopus.Client.Extensibility;
using Octopus.Client.Model;

namespace Octonauts.Machines
{
    internal class DeployProjectToMachinesCmdHandler : CommandHandler<DeployParams>
    {
        protected override async Task Execute(DeployParams options)
        {
            using (var client = await OctopusClientProvider.GetOctopusClient(options))
            {
                var env = await GetEnvironment(client, options.Environment);
                var machines = await GetActiveMachines(client, env);
                var project = await client.Repository.Projects.FindByName(options.Project);
                var release = await client.Repository.Projects.GetReleaseByVersion(project, options.Version);

                foreach (var machine in machines)
                {
                    var depResource = new DeploymentResource
                    {
                        Comments =
                            $"Individually deploy project to machines in environment {options.Environment}",
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

                    if (options.SleepSeconds > 0)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(options.SleepSeconds));
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