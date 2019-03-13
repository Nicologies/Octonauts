using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Octonauts.Core.CommandsFramework;
using Octonauts.Core.OctopusClient;
using Octopus.Client.Model;

namespace Octonauts.Machines
{
    internal class SetRolesCmdHandler : CommandHandler<SetRolesParams>
    {
        protected override async Task Execute(SetRolesParams options)
        {
            using (var client = await OctopusClientProvider.GetOctopusClient(options))
            {
                var machine = await client.Repository.Machines.Get(options.MachineId);
                var envs = await client.Repository.Environments.Get(machine.EnvironmentIds.ToArray());
                await client.Repository.Machines.CreateOrModify(machine.Name, machine.Endpoint,
                    envs.ToArray(), options.Roles.ToArray());
            }
        }
    }
}