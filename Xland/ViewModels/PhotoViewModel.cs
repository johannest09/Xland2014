using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xland.Models;

namespace Xland.ViewModels
{
    public class PhotoViewModel
    {
        
        public Photo Photo { get; set; }
        public string PhotoDescription { get; set; }

        public PhotoViewModel(Photo photo, string photoDecription)
        {
            this.Photo = photo;
            this.PhotoDescription = photoDecription;
        }
    }
}