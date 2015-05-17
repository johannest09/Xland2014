using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xland.Models;

namespace Xland.ViewModels
{
    public class ProjectInfoViewModel
    {
        public Project Project { get; set; }
        public string ProjectExecutionPeriod { get; set; }
        public string ProjectType { get; set; }
        public string ProjectDescription { get; set; }

        public IEnumerable<PhotoViewModel> Photos { get; set; }
        public IEnumerable<VideoViewModel> Videos { get; set; }

    }
}