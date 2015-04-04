using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xland.Models
{
    public class Video
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual VideoGallery VideoGallery { get; set; }
    }
}