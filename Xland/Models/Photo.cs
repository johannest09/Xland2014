﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xland.Models
{
    public class Photo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Title { get; set; }
        public string DescriptionIS { get; set; }
        public string DescriptionEN { get; set; }
        public bool IsMainPhoto { get; set; }

        public virtual PhotoGallery PhotoGallery { get; set; }
    }
}