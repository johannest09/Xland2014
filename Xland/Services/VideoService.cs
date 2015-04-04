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
    public class VideoService : IVideoService
    {
        private IUnitOfWork unitOfWork;
        private IGenericRepository<Video> videoRepository;

        public VideoService(IUnitOfWork unitOfWork, IGenericRepository<Video> videoRepository)
        {
            this.unitOfWork = unitOfWork;
            this.videoRepository = videoRepository;
        }

        public void CreateVideo(Models.Video video)
        {
            videoRepository.Add(video);
            unitOfWork.Save();
        }

        public void AttachVideo(Models.Video video)
        {
            throw new NotImplementedException();
        }

        public void EditVideo(Models.Video video)
        {
            throw new NotImplementedException();
        }

        public Models.Video GetVideoById(int id)
        {
            return videoRepository.Find(id);
        }

        public void DeleteVideo(int id)
        {
            Video video = videoRepository.Find(id);
            videoRepository.Delete(video);
            unitOfWork.Save();
        }

        public IEnumerable<Models.Video> GetVideos()
        {
            return videoRepository.GetAll();
        }

        public void CreateVideoEntity(string galleryPath, string filename, string filepath, Models.VideoGallery gallery)
        {
            var video = new Video
            {
                Name = filename,
                //Path = galleryPath + foldername + "/" + filename,
                Path = galleryPath + filepath + filename,
                VideoGallery = gallery
            };

            this.CreateVideo(video);
        }

        public bool UploadVideo(HttpPostedFileBase file, string videoGalleryUploadPath)
        {
            var path = Path.Combine(HttpContext.Current.Request.MapPath(videoGalleryUploadPath), file.FileName);
            string extension = Path.GetExtension(file.FileName);

            //make sure the file is valid
            if (!validateExtension(extension))
                return false;

            try
            {
                file.SaveAs(path);
                return true;
            }
            catch (Exception ex)
            {
                //return false;
                throw ex;
            }
        }

        private static bool validateExtension(string extension)
        {
            extension = extension.ToLower();
            switch (extension)
            {
                case ".mp4":
                    return true;
                case ".avi":
                    return true;
                case ".mpeg":
                    return true;
                case ".mov":
                    return true;
                default:
                    return false;
            }
        }

        public void Dispose()
        {
            videoRepository.Dispose();
        }
    }
}