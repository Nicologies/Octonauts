using coreArgs.Attributes;
using System.Collections.Generic;

namespace Octonauts.Core
{
    public class ProjectsParams : OctopusParams
    {
        [Option("project", "list of projects to perform action on",
            required: false)]
        public List<string> Projects { get; set; } = new List<string>();
        [Option("project-group", "perform action on all projects in this project group",
            required: false)]
        public string ProjectGroup { get; set; }
    }
}