using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xland.Models;

namespace Xland.ViewModels
{
    public class ProjectEditViewModel
    {
        public int ID { get; set; }
        public Project Project { get; set; }
        public ICollection<StudioEditViewModel> Studios { get; set; }
    }
}