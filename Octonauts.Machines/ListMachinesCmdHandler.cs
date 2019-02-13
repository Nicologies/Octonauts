using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Octonauts.Core.CommandsFramework;
using Octonauts.Core.OctopusClient;
using Octopus.Client.Model;

namespace Octonauts.Machines
{
    internal class ListMachinesCmdHandler : CommandHandler<ListMachinesParams>
    {
        protected override async Task Execute(ListMachinesParams options)
        {
            using (var client = await OctopusClientProvider.GetOctopusClient(options))
            {
                List<MachineResource> machines;
                if (!string.IsNullOrWhiteSpace(options.Environment))
                {
                    var env = await client.Repository.Environments.FindByName(options.Environment);
                    if (env == null)
                    {
                        Console.Error.WriteLine($"Environment {options.Environment} not found");
                    }

                    machines = await client.Repository.Environments.GetMachines(env);
                }
                else
                {
                    machines = await client.Repository.Machines.FindAll();
                }

                var json = JsonConvert.SerializeObject(machines, Formatting.Indented);
                Console.WriteLine(json);
            }
        }
    }
}