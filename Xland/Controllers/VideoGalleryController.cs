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
using Xland.ViewModels;
using System.IO;

namespace Xland.Controllers
{
    [Authorize]
    public class VideoGalleryController : Controller
    {
        private IProjectService projectService;
        private IVideoGalleryService videoGalleryService;
        private IVideoService videoService;

        public const string GalleryUploadFolderPath = "~/Content/VideoGalleries/";

        public VideoGalleryController(IVideoGalleryService videoGalleryService, IVideoService videoService, IProjectService projectService)
        {
            this.videoGalleryService = videoGalleryService;
            this.videoService = videoService;
            this.projectService = projectService;
        }

        // GET: /VideoGallery/
        public ActionResult Index()
        {
            var galleries = videoGalleryService.GetAllVideoGalleries();

            if (galleries != null)
            {
                ViewBag.ProjectTitles = videoGalleryService.GetVideoGalleryProjectTitles();
                ViewBag.VideoGalleryVideoCount = videoGalleryService.GetVideoGalleryVideoCount();
            }
            
            return View(galleries);
        }

        // GET: /VideoGallery/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoGallery videogallery = videoGalleryService.GetGalleryById(id);

            // Get all videos associated to VideoGallery

            var galleryVideos = (from v in videoService.GetVideos()
                                 where v.VideoGallery.ID == videogallery.ID
                                 select v).ToList();

            var projectTitle = projectService.GetProjectById(videogallery.Project.ID).Title;

            var model = new VideoGalleryDetailViewModel
            {
                ID = videogallery.ID,
                ProjectTitle = projectTitle,
                videoGallery = videogallery,
                videos = galleryVideos
            };
     
            return View(model);
        }

        // GET: /VideoGallery/Create
        public ActionResult Create()
        {
            VideoGalleryCreateViewModel vm = new VideoGalleryCreateViewModel();

            ViewBag.videogalleries = projectService.GetProjectsWithoutVideoGalleries().Select(x => new SelectListItem { Text = x.Title, Value = x.ID.ToString() });

            return View(vm);
        }

        // POST: /VideoGallery/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(VideoGalleryCreateViewModel videoGalleryVm, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                var videogallery = new VideoGallery();
                videogallery.Project = projectService.GetProjectById(videoGalleryVm.ProjectId);

                bool galleryCreated = false;
                bool galleryDeleted = false;

                // Uploaded videos
                if (files.First() != null)
                {
                    videoGalleryService.CreateGallery(videogallery);
                    galleryCreated = true;

                    string galleryUploadPath = Server.MapPath(GalleryUploadFolderPath);
                    string folderUniqueName = "Gallery" + videogallery.ID + "/";

                    Directory.CreateDirectory(galleryUploadPath + "\\" + folderUniqueName);
                    string galleryPath = GalleryUploadFolderPath + folderUniqueName;

                    int videoCounter = 0;

                    foreach (var file in files)
                    {
                        if (file.ContentLength == 0)
                            continue;

                        if (file.ContentLength > 0)
                        {
                            if (videoService.UploadVideo(file, galleryPath))
                            {
                                videoService.CreateVideoEntity(GalleryUploadFolderPath, file.FileName, folderUniqueName, videogallery);
                                videoCounter++;
                            }
                        }
                    }

                    if (videoCounter < 1)
                    {
                        // Delete the gallery folder already created if no videos have been created
                        string galleryAbsolutePath = Server.MapPath(GalleryUploadFolderPath + "/Gallery" + videogallery.ID);
                        videoGalleryService.DeleteGallery(videogallery.ID, galleryAbsolutePath);
                        galleryDeleted = true;
                    }
                }

                // embedded videos
                if (Request.Files.Count > 0)
                {
                    if (galleryDeleted)
                    {
                        videogallery.Project = projectService.GetProjectById(videoGalleryVm.ProjectId);
                    }
                    
                    string[] embeds = Request["videoembed"].Split(',');
             
                    foreach (string embed in embeds)
                    {
                        if (!galleryCreated)
                        {
                            videoGalleryService.CreateGallery(videogallery);
                            galleryCreated = true;
                        }
                        if (embed != "")
                        {
                            videoService.CreateVideoEntity(embed, videogallery);
                        }
                    }
                }
               
                return RedirectToAction("Index");
            }

            return View(videoGalleryVm);
        }

        // GET: /VideoGallery/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoGallery videogallery = videoGalleryService.GetGalleryById(id);
            if (videogallery == null)
            {
                return HttpNotFound();
            }
            return View(videogallery);
        }

        // POST: /VideoGallery/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VideoGallery videogallery = videoGalleryService.GetGalleryById(id);
            
            // Get the gallery directory
            string galleryPath = Server.MapPath(GalleryUploadFolderPath + "/Gallery" + videogallery.ID);
            videoGalleryService.DeleteGallery(id, galleryPath);

            return RedirectToAction("Index");
        }

        public string DeleteVideo(int id)
        {
            var video = videoService.GetVideoById(id);

            if (video == null)
            {
                return "0";
            }
            try
            {
                videoService.DeleteVideo(id);

                // Remove photo from directory
                string path = Server.MapPath(video.Path);
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
                videoGalleryService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
