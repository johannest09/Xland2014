using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Xland.DAL;
using Xland.Models;
using Xland.UnitOfWork;
using Xland.Repository;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace Xland.Services
{
    public class PhotoService : IPhotoService
    {

        private IUnitOfWork unitOfWork;
        private IGenericRepository<Photo> photoRepository;

        public PhotoService(IUnitOfWork unitOfWork, IGenericRepository<Photo> photoRepository)
        {
            this.unitOfWork = unitOfWork;
            this.photoRepository = photoRepository;
        }

        public void CreatePhoto(Photo photo)
        {
            photoRepository.Add(photo);
            unitOfWork.Save();
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
            return photoRepository.Find(id);
        }

        public void DeletePhoto(int id)
        {
            Photo photo = photoRepository.Find(id);
            photoRepository.Delete(photo);
            unitOfWork.Save();
        }

        public IEnumerable<Photo> GetPhotos()
        {
            return photoRepository.GetAll();
        }

        public void Dispose()
        {
            photoRepository.Dispose();
        }

        public void CreatePhotoEntity(string galleryPath, string filename, string filepath, PhotoGallery gallery)
        {
            var photo = new Photo
            {
                Name = filename,
                //Path = galleryPath + foldername + "/" + filename,
                Path = galleryPath + filepath + filename,
                IsMainPhoto = false,
                PhotoGallery = gallery
            };

            this.CreatePhoto(photo);
        }

        public bool UploadPhoto(HttpPostedFileBase file, string galleryUploadPath, int maxWidth, int maxHeight)
        {
            var path = Path.Combine(HttpContext.Current.Request.MapPath(galleryUploadPath), file.FileName);
            string extension = Path.GetExtension(file.FileName);

            //make sure the file is valid
            if (!validateExtension(extension))
                return false;

            try
            {
                var imgOriginal = Image.FromStream(file.InputStream);

                if (imgOriginal.Width > maxWidth || imgOriginal.Height > maxHeight)
                {
                    var imgActual = ResizeImage(imgOriginal, maxWidth, maxHeight);
                    imgOriginal.Dispose();
                    imgActual.Save(path);
                    imgActual.Dispose();
                }
                else
                {
                    imgOriginal.Save(path);
                    imgOriginal.Dispose();
                }

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
                case ".jpg":
                    return true;
                case ".png":
                    return true;
                case ".gif":
                    return true;
                case ".jpeg":
                    return true;
                default:
                    return false;
            }
        }

        public Image ResizeImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);

            return (Image)(new Bitmap(newImage));
        }

        public void SetAsMainPhoto(int id)
        {
            var photos = photoRepository.GetAll().ToList();

            foreach (var p in photos)
            {
                if (p.ID == id)
                {
                    p.IsMainPhoto = true;
                }
                else
                {
                    p.IsMainPhoto = false;
                }
                photoRepository.Edit(p);
            }
            unitOfWork.Save();
        }

        public void SavePhotoDescription(int id, string descriptionIs, string descriptionEn)
        {
            var photo = photoRepository.Find(id);

            if (photo != null)
            {
                photo.DescriptionIS = descriptionIs;
                photo.DescriptionEN = descriptionEn;
                photoRepository.Edit(photo);
                unitOfWork.Save();
            }
        }

        
    }
}