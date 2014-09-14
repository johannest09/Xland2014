using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Xland.Models
{
    public class PhotoGallery
    {
        [Key]
        public int ID { get; set; }
        public IEnumerable<Photo> Photos { get; set; }

        public virtual Project Project { get; set; }
        
    }
}