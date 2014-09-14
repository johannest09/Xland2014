using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xland.Models;
using System.Web.Mvc;

namespace Xland.ViewModels
{
    public class ProjectEditViewModel
    {
        public int ID { get; set; }
        public Project Project { get; set; }

        //[Display(Name = "Tegund verkefnis")]
        //public int SelectedProjectType { get; set; }
        //public IEnumerable<SelectListItem> ProjectTypes { get; set; }

        public ICollection<StudioEditViewModel> Studios { get; set; }
    }
}