using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using coreArgs;
using Octonauts.Core;
using Octonauts.Core.OctopusClient;
using Octopus.Client;
using Octopus.Client.Model;

namespace Octonauts.Environment
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            var options = ArgsParser.Parse<EnvironmentDeletionParams>(args);
            if (options.Errors.Count > 0)
            {
                Console.Write(ArgsParser.GetHelpText<EnvironmentDeletionParams>());
                System.Environment.Exit(-1);
                return;
            }

            options.Arguments.FillOctopusParams();
            using (var client = await OctopusClientProvider.GetOctopusClient(options.Arguments))
            {
                var environments = await client.Repository.Environments.FindMany(e =>
                    Regex.IsMatch(e.Name, options.Arguments.EnvironmentNameRegex, RegexOptions.IgnoreCase));
                var lifeCycles = await client.Repository.Lifecycles.FindAll();

                await RemoveEnvFromLifecycles(lifeCycles, environments, client, options.Arguments.DryRun);

                // ReSharper disable once AccessToDisposedClosure
                foreach (var env in environments)
                {
                    await DeleteEnvironment(client, env, options.Arguments.DryRun).ConfigureAwait(false);
                }
            }
        }

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
                        Console.Error.WriteLine($"Unable to delete {env.Name}, since it has machine and the machine has only this environment assgined.");
                        return;
                    }
                }

                await client.Repository.Environments.Delete(env);
            }
        }

        private static async Task RemoveEnvFromLifecycles(IReadOnlyCollection<LifecycleResource> lifeCycles,
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
