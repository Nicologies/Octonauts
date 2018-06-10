using System;
using System.Linq;
using System.Threading.Tasks;
using Octonauts.Core.CommandsFramework;
using Octonauts.Core.OctopusClient;

namespace Octonauts.Machines
{
    internal class FindByThumbprintCmdHandler : CommandHandler<FindByThumbprintParams>
    {
        protected override async Task Execute(FindByThumbprintParams options)
        {
            using (var client = await OctopusClientProvider.GetOctopusClient(options))
            {
                var machines = await client.Repository.Machines.FindByThumbprint(options.Thumbprint);
                if (!machines.Any())
                {
                    Console.WriteLine("Machine not found");
                }

                foreach (var m in machines)
                {
                    Console.WriteLine(m.Name);
                }
            }
        }
    }
}