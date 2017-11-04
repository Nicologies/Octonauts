using System.Collections.Generic;

namespace Nicologies.Octonauts.Core
{
    public class ProjectsParams : OctopusParams
    {
        public List<string> Projects { get; set; } = new List<string>();
        public string ProjectGroup { get; set; }
    }
}