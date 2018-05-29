using System;
using System.Linq;
using System.Threading.Tasks;
using coreArgs.Attributes;
using Octonauts.Core;
using Octonauts.Core.OctopusClient;
using Octopus.Client.Model;

namespace Octonauts.Machines
{
    internal class EnableDisableMachinesFromEnvParams : OctopusParams
    {
        [Option("env", "environment", required: true)]
        public string Environment { get; set; }
    }

    internal class MachineParams : OctopusParams
    {
        [Option("disable-enable-in-env", "disable and then enable machines", required: false)]
        public bool DisableThenEnable { get; set; }
    }

    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var machineParams = CommandArgsParaser.Parse<MachineParams>(args);

            if (machineParams.DisableThenEnable)
            {
                var enableDisableMachineParams = CommandArgsParaser.Parse<EnableDisableMachinesFromEnvParams>(args);
                enableDisableMachineParams.FillOctopusParams();

                using (var client = await OctopusClientProvider.GetOctopusClient(enableDisableMachineParams))
                {
                    var env = await client.Repository.Environments.FindByName(enableDisableMachineParams.Environment);
                    var machines = await client.Repository.Machines.List(environmentIds: env.Id, take: int.MaxValue);
                    foreach (var machine in machines.Items)
                    {
                        var badStatus = new[]
                        {
                            MachineModelStatus.Disabled,
                            MachineModelStatus.Offline,
                            MachineModelStatus.Unknown
                        };

                        if (badStatus.Contains(machine.Status))
                        {
                            continue;
                        }

                        var badHealthStatus = new[]
                        {
                            MachineModelHealthStatus.Unavailable,
                            MachineModelHealthStatus.Unhealthy,
                            MachineModelHealthStatus.Unknown
                        };

                        if (badHealthStatus.Contains(machine.HealthStatus))
                        {
                            continue;
                        }

                        if (machine.IsDisabled)
                        {
                            continue;
                        }

                        Console.WriteLine($"Disabling {machine.Name}");
                        machine.IsDisabled = true;
                        await client.Repository.Machines.Modify(machine);

                        Console.WriteLine($"Enabling {machine.Name}");
                        machine.IsDisabled = false;
                        await client.Repository.Machines.Modify(machine);
                    }
                }
            }
        }
    }
}
