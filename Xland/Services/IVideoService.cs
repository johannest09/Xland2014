using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xland.Models;

namespace Xland.Services
{
    public interface IVideoService
    {
        void CreateVideo(Video video);
        void AttachVideo(Video video);
        void EditVideo(Video video);
        Video GetVideoById(int id);
        void DeleteVideo(int id);
        IEnumerable<Video> GetVideos();
        void SaveVideoDescription(int id, string descriptionIs, string descriptionEn);

        void CreateVideoEntity(string embed, VideoGallery gallery);
        void CreateVideoEntity(string galleryPath, string filename, string filepath, VideoGallery gallery);

        bool UploadVideo(HttpPostedFileBase file, string videoGalleryUploadPath);

        void Dispose();
    }
}
