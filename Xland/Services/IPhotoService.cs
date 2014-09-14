﻿using System;
using System.Collections.Generic;
using System.Drawing;
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
        IEnumerable<Photo> GetPhotos();
        void SetAsMainPhoto(int id);
        void SavePhotoText(int id, string title, string text);

        void CreatePhotoBulk(string galleryPath, string filename, string filepath, PhotoGallery gallery);
        bool UploadPhoto(HttpPostedFileBase file, string photoGalleryUploadPath, int maxWidth, int maxHeight);

        void Dispose();

    }
}