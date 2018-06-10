namespace Octonauts.Packages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Core;
    using Core.OctopusClient;
    using Octopus.Client.Model;

    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var projectsParams = CommandArgsParaser.Parse<ProjectsParams>(args);
                projectsParams.FillOctopusParams();
            using (var client = await OctopusClientProvider.GetOctopusClient(projectsParams))
            {
                var actions = await GetAllActionsFromProjects(projectsParams, client);
                var packages = GetPackagesFromActions(actions);
                OutputPackages(packages);
            }
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
