using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xland.DAL;
using Xland.Services;
using Xland.Models;
using Xland.UnitOfWork;
using Xland.Repository;
using System.IO;

namespace Xland.Services
{
    public class PhotoGalleryService : IPhotoGalleryService
    {

        private IUnitOfWork unitOfWork;
        private IGenericRepository<PhotoGallery> photoGalleryRepository;
        private IGenericRepository<Project> projectRepository;
        private IGenericRepository<Photo> photoRepository;

        public PhotoGalleryService(IUnitOfWork unitOfWork, IGenericRepository<PhotoGallery> photoGalleryRepository, IGenericRepository<Project> projectRepository, IGenericRepository<Photo> photoRepository)
        {
            this.unitOfWork = unitOfWork;
            this.photoGalleryRepository = photoGalleryRepository;
            this.projectRepository = projectRepository;
            this.photoRepository = photoRepository;
        }

        public void CreateGallery(PhotoGallery gallery)
        {
            photoGalleryRepository.Add(gallery);
            unitOfWork.Save();
        }

        public void EditGallery(PhotoGallery gallery)
        {
            throw new NotImplementedException();
        }

        public PhotoGallery GetGalleryById(int? id)
        {
            return photoGalleryRepository.Find(id);
        }

        public void DeleteGallery(int id, string gallerypath)
        {
            PhotoGallery gallery = photoGalleryRepository.Find(id);

            var photos = (from p in photoRepository.GetAll()
                         where p.PhotoGallery.ID == gallery.ID
                         select p).ToList();

            try
            {
                if (photos.Count > 0)
                {
                    photoRepository.DeleteMany(photos);
                    unitOfWork.Save();
                    var dir = new DirectoryInfo(gallerypath);
                    // Recursive delete
                    dir.Delete(true);
                }
            }
            catch (Exception ex)
            {

            }

            photoGalleryRepository.Delete(gallery);
            unitOfWork.Save();
        }

        public void Dispose()
        {
            photoGalleryRepository.Dispose();
        }
         
        public IEnumerable<PhotoGallery> GetAllPhotoGalleries()
        {
            var galleries = photoGalleryRepository.GetAll().ToList();
            if (galleries != null)
            {
                return galleries;
            }
            return null;
        }

        public Dictionary<int, string> GetPhotoGalleryProjectTitles()
        {
            var projectTitles = new Dictionary<int, string>();
            var galleries = photoGalleryRepository.GetAll();

            foreach (var gallery in galleries.ToList())
            {
                var project = projectRepository.Find(gallery.Project.ID);
                if (!projectTitles.ContainsKey(project.ID))
                {
                    projectTitles.Add(project.ID, project.Title);
                }
            }
            return projectTitles;
        }

        public Dictionary<int, int> GetPhotoGalleryPhotoCount()
        {
            var imageGalleryCount = new Dictionary<int, int>();

            foreach (var gallery in photoGalleryRepository.GetAll().ToList())
            {
                var imageCount = photoRepository.Count(photo => photo.PhotoGallery.ID == gallery.ID);
                imageGalleryCount.Add(gallery.ID, Convert.ToInt32(imageCount));
            }

            return imageGalleryCount;
        }

    }
}