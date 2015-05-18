using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Xland.Models;
using Xland.Repository;
using Xland.UnitOfWork;

namespace Xland.Services
{
    public class VideoGalleryService : IVideoGalleryService
    {
        private IUnitOfWork unitOfWork;
        private IGenericRepository<VideoGallery> videoGalleryRepository;
        private IGenericRepository<Project> projectRepository;
        private IGenericRepository<Video> videoRepository;

        public VideoGalleryService(IUnitOfWork unitOfWork, IGenericRepository<VideoGallery> videoGalleryRepository, IGenericRepository<Project> projectRepository, IGenericRepository<Video> videoRepository)
        {
            this.unitOfWork = unitOfWork;
            this.videoGalleryRepository = videoGalleryRepository;
            this.projectRepository = projectRepository;
            this.videoRepository = videoRepository;
        }

        public void CreateGallery(VideoGallery gallery)
        {
            videoGalleryRepository.Add(gallery);
            unitOfWork.Save();
        }

        public void EditGallery(VideoGallery gallery)
        {
            throw new NotImplementedException();
        }

        public VideoGallery GetGalleryById(int? id)
        {
            return videoGalleryRepository.Find(id);
        }

        public void DeleteGallery(int id, string gallerypath)
        {
            VideoGallery gallery = videoGalleryRepository.Find(id);

            var videos = (from v in videoRepository.GetAll()
                          where v.VideoGallery.ID == gallery.ID
                          select v).ToList();

            var dir = new DirectoryInfo(gallerypath);

            try
            {
                bool dirDeleted = false;

                if (videos.Count > 0)
                {
                    videoRepository.DeleteMany(videos);
                    unitOfWork.Save();

                    if (dir.Exists)
                    {
                        // Recursive delete
                        dir.Delete(true);
                        dirDeleted = true;
                    }
                }

                videoGalleryRepository.Delete(gallery);
                unitOfWork.Save();

                if (dir.Exists && !dirDeleted)
                {
                    dir.Delete();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Dispose()
        {
            videoGalleryRepository.Dispose();
        }

        public IEnumerable<VideoGallery> GetAllVideoGalleries()
        {
            var galleries = videoGalleryRepository.GetAll().ToList();
            if (galleries != null)
            {
                return galleries;
            }
            return null;
        }

        public Dictionary<int, string> GetVideoGalleryProjectTitles()
        {
            var projectTitles = new Dictionary<int, string>();
            var galleries = videoGalleryRepository.GetAll();

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

        public Dictionary<int, int> GetVideoGalleryVideoCount()
        {
            var videoGalleryCount = new Dictionary<int, int>();

            foreach (var gallery in videoGalleryRepository.GetAll().ToList())
            {
                var videoCount = videoRepository.Count(video => video.VideoGallery.ID == gallery.ID);
                videoGalleryCount.Add(gallery.ID, Convert.ToInt32(videoCount));
            }

            return videoGalleryCount;
        }


    }

}