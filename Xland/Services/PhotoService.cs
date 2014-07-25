using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Xland.DAL;
using Xland.Models;

namespace Xland.Services
{
    public class PhotoService : IPhotoService
    {

        private XlandContext context = new XlandContext();

        public void CreatePhoto(Photo photo)
        {
            context.Photos.Add(photo);
            context.SaveChanges();
        }

     

        public void AttachPhoto(Photo photo)
        {
            throw new NotImplementedException();
        }

        public void EditPhoto(Models.Photo photo)
        {
            throw new NotImplementedException();
        }

        public Photo GetPhotoById(int id)
        {
            return context.Photos.Find(id);
        }

        public void DeletePhoto(int id)
        {
            Photo photo = this.GetPhotoById(id);
            context.Photos.Remove(photo);
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }


        public void CreatePhotoBulk(string filename, string foldername, string filepath, PhotoGallery gallery)
        {
            var photo = new Photo
            {
                Title = filename,
                Path = "~/Content/GalleryImages/" + foldername + "/" + filename,
                IsMainPhoto = false,
                PhotoGallery = gallery
            };

            this.CreatePhoto(photo);
        }
    }
}