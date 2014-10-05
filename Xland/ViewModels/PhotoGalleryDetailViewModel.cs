using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xland.Models;

namespace Xland.ViewModels
{
    public class PhotoGalleryDetailViewModel
    {
        public int ID { get; set; }
        public string ProjectTitle { get; set; }
        public PhotoGallery photoGallery { get; set; }
        public IEnumerable<Photo> photos { get; set; } 
    }
}