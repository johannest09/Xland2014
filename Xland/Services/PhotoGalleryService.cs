using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xland.DAL;
using Xland.Services;
using Xland.Models;

namespace Xland.Services
{
    public class PhotoGalleryService : IPhotoGalleryService
    {
        private XlandContext context = new XlandContext();

        public void CreateGallery(PhotoGallery gallery)
        {
            context.PhotoGalleries.Add(gallery);
            context.SaveChanges();
        }

        public void EditGallery(Models.PhotoGallery gallery)
        {
            throw new NotImplementedException();
        }

        public PhotoGallery GetGalleryById(int id)
        {
            return context.PhotoGalleries.Find(id);
        }

        public void DeleteGallery(int id)
        {
            PhotoGallery gallery = this.GetGalleryById(id);
            context.PhotoGalleries.Remove(gallery);
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IEnumerable<PhotoGallery> GetAllPhotoGalleries()
        {
            return this.context.PhotoGalleries.ToList();
        }
    }
}