using System.Collections.Generic;
using System.Linq;
using Octopus.Client;
using System.Threading.Tasks;

namespace Octonauts.Core
{
    public static class ProjectsParamsExtension
    {
        public static async Task<List<string>> GetEffectiveProjects(this ProjectsParams projectsParams,
            IOctopusAsyncClient client)
        {
            if (string.IsNullOrWhiteSpace(projectsParams.ProjectGroup))
            {
                return projectsParams.Projects;
            }

            var group = await client.Repository.ProjectGroups.FindByName(projectsParams.ProjectGroup);
            var projectsInGroup = await client.Repository.ProjectGroups.GetProjects(group);
            var ret = new HashSet<string>(projectsParams.Projects);
            foreach (var prj in projectsInGroup.Select(x => x.Name))
            {
                ret.Add(prj);
            }

            return ret.Except(projectsParams.ExcludeProjects).ToList();
        }
    }
}