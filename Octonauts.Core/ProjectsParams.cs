namespace Octonauts.Core
{
    using System.Collections.Generic;
    using coreArgs.Attributes;

    public class ProjectsParams : OctopusParams
    {
        [Option("projects", "list of projects to perform action on",
            required: false)]
        public List<string> Projects { get; set; } = new List<string>();
        [Option("project-group", "perform action on all projects in this project group",
            required: false)]
        public string ProjectGroup { get; set; }

        [Option("exclude-projects", "list of projects to exclude",
            required: false)]
        public List<string> ExcludeProjects { get; set; } = new List<string>();
    }
}