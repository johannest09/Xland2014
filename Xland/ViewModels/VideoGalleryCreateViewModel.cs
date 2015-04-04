using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xland.Models;

namespace Xland.ViewModels
{
    public class VideoGalleryCreateViewModel
    {
        public int ProjectId { get; set; }
        public VideoGallery VideoGallery { get; set; }
        public IEnumerable<Project> Projects { get; set; }
    }
}