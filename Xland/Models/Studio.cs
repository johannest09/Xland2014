using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xland.Models
{
    public class Studio
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}