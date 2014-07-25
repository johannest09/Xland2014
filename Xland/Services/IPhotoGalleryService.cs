using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xland.Models;

namespace Xland.Services
{
    public interface IPhotoGalleryService
    {
        IEnumerable<PhotoGallery> GetAllPhotoGalleries();
        void CreateGallery(PhotoGallery gallery);
        void EditGallery(PhotoGallery gallery);
        PhotoGallery GetGalleryById(int id);
        void DeleteGallery(int id);

        void Dispose();
    }
}
