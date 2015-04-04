using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xland.Models;

namespace Xland.Services
{
    public interface IVideoGalleryService
    {
        IEnumerable<VideoGallery> GetAllVideoGalleries();
        void CreateGallery(VideoGallery gallery);
        void EditGallery(VideoGallery gallery);
        VideoGallery GetGalleryById(int? id);
        void DeleteGallery(int id, string galleryPath);

        Dictionary<int, string> GetVideoGalleryProjectTitles();
        Dictionary<int, int> GetVideoGalleryVideoCount();

        void Dispose();
    }
}
