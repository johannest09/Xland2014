using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xland.Models;

namespace Xland.ViewModels
{
    public class VideoGalleryDetailViewModel
    {
        public int ID { get; set; }
        public string ProjectTitle { get; set; }
        public VideoGallery videoGallery { get; set; }
        public IEnumerable<Video> videos { get; set; }
    }
}