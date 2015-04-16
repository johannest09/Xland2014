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
        public IEnumerable<Photo> Photos { get; set; }
        public IEnumerable<Video> Videos { get; set; }

    }
}