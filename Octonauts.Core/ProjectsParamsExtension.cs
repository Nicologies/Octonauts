using System.Collections.Generic;
using System.Linq;
using Octopus.Client;

namespace Nicologies.Octonauts.Core
{
    public static class ProjectsParamsExtension
    {
        public static List<string> GetEffectiveProjects(this ProjectsParams projectsParams,
            IOctopusAsyncClient client)
        {
            if (string.IsNullOrWhiteSpace(projectsParams.ProjectGroup))
            {
                return projectsParams.Projects;
            }

            var group = client.Repository.ProjectGroups.FindByName(projectsParams.ProjectGroup).Result;
            var projectsInGroup = client.Repository.ProjectGroups.GetProjects(group).Result.Select(x => x.Name);
            var ret = new HashSet<string>(projectsParams.Projects);
            foreach (var prj in projectsInGroup)
            {
                ret.Add(prj);
            }

            return ret.ToList();
        }
    }
}