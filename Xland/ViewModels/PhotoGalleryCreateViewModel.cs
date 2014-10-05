using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xland.Models;
using System.Collections;
using System.Web.Mvc;

namespace Xland.ViewModels
{
    public class PhotoGalleryCreateViewModel
    
    {
        public int ProjectId { get; set; }
        public PhotoGallery Photogallery { get; set; }
        public IEnumerable<Project> Projects { get; set; }

    }
}