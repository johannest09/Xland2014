using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xland.Models
{
    public class Project
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        public virtual ICollection<Studio> Studios { get; set; }
    }
}