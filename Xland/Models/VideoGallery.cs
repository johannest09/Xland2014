using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Xland.Models
{
    public class VideoGallery
    {
        [Key]
        public int ID { get; set; }
        public IEnumerable<Video> Videos { get; set; }

        public virtual Project Project { get; set; }
    }
}