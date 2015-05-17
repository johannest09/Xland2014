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
        public string Embed { get; set; }
        public string DescriptionIS { get; set; }
        public string DescriptionEN { get; set; }
        public VideoType VideoType { get; set; }

        public virtual VideoGallery VideoGallery { get; set; }

    }

    public enum VideoType
    {
        Upload,
        Youtube,
        Vimeo,
        Other
    }
}