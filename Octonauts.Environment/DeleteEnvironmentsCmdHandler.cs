using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Octonauts.Core.CommandsFramework;
using Octonauts.Core.OctopusClient;
using Octopus.Client;
using Octopus.Client.Model;

namespace Octonauts.Environment
{
    internal class DeleteEnvironmentsCmdHandler : CommandHandler<EnvironmentDeletionParams>
    {
        protected override async Task Execute(EnvironmentDeletionParams options)
        {
            using var client = await OctopusClientProvider.GetOctopusClient(options);
            var environments = await client.Repository.Environments.FindMany(e =>
                Regex.IsMatch(e.Name, options.EnvironmentNameRegex, RegexOptions.IgnoreCase));
            var lifeCycles = await client.Repository.Lifecycles.FindAll();

            await RemoveEnvFromLifeCycles(lifeCycles, environments, client, options.DryRun);

            // ReSharper disable once AccessToDisposedClosure
            foreach (var env in environments)
            {
                await DeleteEnvironment(client, env, options.DryRun).ConfigureAwait(false);
            }
        }

        public override string FeatureName => EnvironmentFeature.StaticFeatureName;
        public override string CommandName => "delete";
        public override string CommandDescription => "Delete environments that matches regex pattern";

        private static async Task DeleteEnvironment(IOctopusAsyncClient client, EnvironmentResource env, bool dryRun)
        {
            Console.WriteLine($"Deleting {env.Name}");
            if (!dryRun)
            {
                var machines = await client.Repository.Environments.GetMachines(env);
                foreach (var m in machines)
                {
                    m.EnvironmentIds.Remove(env.Id);
                    if (m.EnvironmentIds.Any())
                    {
                        await client.Repository.Machines.Modify(m);
                    }
                    else
                    {
                        await Console.Error.WriteLineAsync($"Unable to delete {env.Name}, since it has machine and the machine has only this environment assigned.");
                        return;
                    }
                }

                await client.Repository.Environments.Delete(env);
            }
        }

        private static async Task RemoveEnvFromLifeCycles(IReadOnlyCollection<LifecycleResource> lifeCycles,
            IEnumerable<EnvironmentResource> environments, IOctopusAsyncClient client, bool dryRun)
        {
            foreach (var env in environments)
            {
                foreach (var lifecycle in lifeCycles)
                {
                    foreach (var phase in lifecycle.Phases)
                    {
                        if (phase.OptionalDeploymentTargets.Contains(env.Id))
                        {
                            phase.OptionalDeploymentTargets.Remove(env.Id);
                        }
                    }
                }
            }

            if (dryRun)
            {
                return;
            }

            foreach (var lifecycle in lifeCycles)
            {
                await client.Repository.Lifecycles.Modify(lifecycle);
            }
        }
    }
}