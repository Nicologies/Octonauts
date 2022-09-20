using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octonauts.Core;
using Octonauts.Core.CommandsFramework;
using Octonauts.Core.OctopusClient;
using Octopus.Client.Model;

namespace Octonauts.Packages
{
    // ReSharper disable once InconsistentNaming
    internal class GetPackagesCmdHandler : CommandHandler<ProjectsParams>
    {
        protected override async Task Execute(ProjectsParams options)
        {
            using var client = await OctopusClientProvider.GetOctopusClient(options);
            var actions = await GetAllActionsFromProjects(options, client);
            var packages = GetPackagesFromActions(actions);
            OutputPackages(packages);
        }

        private static void OutputPackages(List<string> packages)
        {
            packages.ForEach(Console.WriteLine);
        }

        private static List<string> GetPackagesFromActions(IEnumerable<DeploymentActionResource> actions)
        {
            return actions.SelectMany(a => a.Properties)
                .Where(p => p.Key == "Octopus.Action.Package.PackageId")
                .Select(x => x.Value.Value).ToList();
        }

        private static async Task<List<DeploymentActionResource>> GetAllActionsFromProjects(ProjectsParams projectsParams,
            Octopus.Client.IOctopusAsyncClient client)
        {
            var actions = new List<DeploymentActionResource>();
            foreach (var projName in await projectsParams.GetEffectiveProjects(client))
            {
                var proj = await client.Repository.Projects.FindByName(projName);
                var deployProcess = await client.Repository.DeploymentProcesses.Get(proj.DeploymentProcessId);
                actions.AddRange(deployProcess.Steps.ToList().SelectMany(s => s.Actions));
            }

            return actions;
        }
    }
}