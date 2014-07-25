using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xland.Models;

namespace Xland.Services
{
    public interface IPhotoService
    {
        void CreatePhoto(Photo photo);
        void AttachPhoto(Photo photo);
        void EditPhoto(Photo photo);
        Photo GetPhotoById(int id);
        void DeletePhoto(int id);

        void CreatePhotoBulk(string filename, string foldername, string filepath, PhotoGallery gallery);

        void Dispose();

    }
}
