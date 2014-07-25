using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xland.Models
{
    public class PhotoGallery
    {
        public int ID { get; set; }
        public IEnumerable<Photo> Photos { get; set; }

        public virtual Project Project { get; set; }
        
    }
}