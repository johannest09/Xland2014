using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Xland.Models;
using Xland.DAL;
using Xland.Services;
using System.IO;
using System.Drawing;
using Xland.ViewModels;

namespace Xland.Controllers
{
    public class PhotoGalleryController : Controller
    {

        private IPhotoGalleryService photoGalleryService;
        private IPhotoService photoService;
        private IProjectService projectService;

        const int MAX_WIDTH = 1080;
        const int MAX_HEIGHT = 800;

        public const string GalleryUploadFolderPath = "~/Content/PhotoGalleries/";

        public PhotoGalleryController(IPhotoGalleryService photoGalleryService, IPhotoService photoService, IProjectService projectService)
        {
            this.photoGalleryService = photoGalleryService;
            this.photoService = photoService;
            this.projectService = projectService;
        }

        // GET: /PhotoGallery/
        public ActionResult Index()
        {
            var galleries = photoGalleryService.GetAllPhotoGalleries();

            ViewBag.ProjectTitles = photoGalleryService.GetPhotoGalleryProjectTitles();
            ViewBag.ImageGalleryImageCount = photoGalleryService.GetPhotoGalleryPhotoCount();

            return View(galleries);
        }

        // GET: /PhotoGallery/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var gallery = photoGalleryService.GetGalleryById(id);

           
            // Get All Images associated to ImageGallery

            var galleryPhotos = (from p in photoService.GetPhotos()
                                    where p.PhotoGallery.ID == gallery.ID
                                    select p).ToList();

            var projectTitle = projectService.GetProjectById(gallery.Project.ID).Title;

            var model = new PhotoGalleryDetailViewModel { 
                ID = gallery.ID, 
                ProjectTitle = projectTitle, 
                photoGallery = gallery, 
                photos = galleryPhotos 
            };

            return View(model);
        }

        // GET: /PhotoGallery/Create
        public ActionResult Create()
        {
          
            PhotoGalleryCreateViewModel vm = new PhotoGalleryCreateViewModel();

            //ViewBag.photogalleries = projectService.GetProjects().Select(x => new SelectListItem { Text = x.Title, Value = x.ID.ToString() });

            ViewBag.photogalleries = projectService.GetProjectsWithoutGalleries().Select(x => new SelectListItem { Text = x.Title, Value = x.ID.ToString() });
            return View(vm);
        }

        // POST: /PhotoGallery/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(PhotoGalleryCreateViewModel photogalleryVm, IEnumerable<HttpPostedFileBase> files)
        {

            if (ModelState.IsValid)
            {

                if (files != null)
                {

                    var photogallery = new PhotoGallery();

                    photogallery.Project = projectService.GetProjectById(photogalleryVm.ProjectId);
                    photoGalleryService.CreateGallery(photogallery);

                    string galleryUploadPath= Server.MapPath(GalleryUploadFolderPath);
                    string folderUniqueName = "Gallery" + photogallery.ID + "/";

                    Directory.CreateDirectory(galleryUploadPath + "\\" + folderUniqueName);
                    string galleryPath = GalleryUploadFolderPath + folderUniqueName;

                    foreach (var file in files)
                    {
                        if (file.ContentLength == 0)
                            continue;

                        if (file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            photoService.UploadPhoto(file, galleryPath, MAX_WIDTH, MAX_HEIGHT);
                            photoService.CreatePhotoEntity(GalleryUploadFolderPath, file.FileName, folderUniqueName, photogallery);
                        }
                    }
                }
                return RedirectToAction("Index");
                
            }

            return View(photogalleryVm);
        }

        // GET: /PhotoGallery/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var gallery = photoGalleryService.GetGalleryById(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // POST: /PhotoGallery/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var gallery = photoGalleryService.GetGalleryById(id);

            // Get the gallery directory
            string galleryPath = Server.MapPath(GalleryUploadFolderPath + "/Gallery" + gallery.ID);
            photoGalleryService.DeleteGallery(id, galleryPath);
           
            return RedirectToAction("Index");
        }

        public string DeletePhoto(int id)
        {
            var photo = photoService.GetPhotoById(id);

            if (photo == null)
            {
                return "0";
            }
            try
            {
                photoService.DeletePhoto(id);

                // Remove photo from directory
                string path = Server.MapPath(photo.Path);
                System.IO.File.Delete(path);

                return "1";
            }
            catch (Exception ex)
            {
            }

            return "0";
        }

        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                photoGalleryService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
