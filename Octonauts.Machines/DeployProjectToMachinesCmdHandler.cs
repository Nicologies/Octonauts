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
            using var client = await OctopusClientProvider.GetOctopusClient(options);
            var env = await GetEnvironment(client, options.Environment);
            var machines = await GetActiveMachines(client, env);
            var project = await client.Repository.Projects.FindByName(options.Project);
            var release = await client.Repository.Projects.GetReleaseByVersion(project, options.Version);

            foreach (var machine in machines)
            {
                if (machine.IsDisabled)
                {
                    Console.WriteLine($"Skipping disabled {machine.Name}, {machine.Id}");
                    continue;
                }

                if (machine.HealthStatus != MachineModelHealthStatus.Healthy &&
                    machine.HealthStatus != MachineModelHealthStatus.HasWarnings)
                {
                    Console.WriteLine($"Skipping unhealthy {machine.Name}, {machine.Id}");
                    continue;
                }

                if (!await CanDeployToMachineCheckingRoles(client, machine, release, env.Id))
                {
                    Console.WriteLine("Machine doesn't have required roles");
                    continue;
                }

                Console.WriteLine($"Deploying to {machine.Name}, {machine.Id}");
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

                try
                {
                    await client.Repository.Deployments.Create(depResource);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex);
                }

                if (options.SleepSeconds > 0)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(options.SleepSeconds));
                }
            }
        }

        public override string FeatureName => MachineFeature.StaticFeatureName;
        public override string CommandName => "deploy-project";
        public override string CommandDescription => "Individually deploy a project to machines in an environment";

        private async Task<bool> CanDeployToMachineCheckingRoles(IOctopusAsyncClient client, MachineResource machine, ReleaseResource release, string envId)
        {
            var deployTarget = await GetDeploymentTargetEnvironment(client, release, envId);
            var preview = await client.Repository.Releases.GetPreview(deployTarget);
            var stepsNeedRole = preview.StepsToExecute.Where(s => s.Roles.Any()).ToList();

            if (stepsNeedRole.Count < preview.StepsToExecute.Count)
            { // if any step doesn't require a role, then we can deploy the step to the machine.
                return true;
            }

            return stepsNeedRole.Any(step => step.Roles.Any(r => machine.Roles.Contains(r)));
        }

        private async Task<DeploymentPromotionTarget> GetDeploymentTargetEnvironment(
            IOctopusAsyncClient client, ReleaseResource release, string envId)
        {
            var deploymentTemplate = await client.Repository.Releases.GetTemplate(release);
            if (deploymentTemplate == null)
            {
                throw new Exception("Unable to retrieve template for release. Deployment failed");
            }

            return deploymentTemplate.PromoteTo.FirstOrDefault(x => string.Equals(x.Id, envId));
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