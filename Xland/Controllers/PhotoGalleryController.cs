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

namespace Xland.Controllers
{
    public class PhotoGalleryController : Controller
    {

        private IPhotoGalleryService photoGalleryService;
        private IPhotoService photoService;
        private IProjectService projectService;

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

            //IEnumerable<ProjectIndexViewModel> viewModel = Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectIndexViewModel>>(projects.Cast<Project>().AsEnumerable());

            return View(galleries);
        }

        // GET: /PhotoGallery/Details/5
        public ActionResult Details(int? id)
        {
            
            return View();
        }

        // GET: /PhotoGallery/Create
        public ActionResult Create()
        {
            ViewBag.ProjectID = new SelectList(projectService.GetAllProjects(), "ID", "Title");
            return View();
        }

        // POST: /PhotoGallery/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID")] PhotoGallery photogallery, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                // Get the project id and attach to imagegallery
                var projectId = Convert.ToInt32(Request.Params["ProjectID"]);
                photogallery.Project = projectService.GetProjectById(projectId);
                photoGalleryService.CreateGallery(photogallery);
                
                if (files != null)
                {
                    // Create image folder for the gallery using the gallery id as unique name
                    string ImageGalleryDirectory = Server.MapPath("~/Content/PhotoGalleries");
                    string foldername = "Gallery" + photogallery.ID;
                    Directory.CreateDirectory(ImageGalleryDirectory + "\\" + foldername);

                    foreach (var file in files)
                    {
                        string filename = System.IO.Path.GetFileName(file.FileName);
                        string path = System.IO.Path.Combine(Server.MapPath("~/Content/PhotoGalleries/" + foldername), filename);
                        file.SaveAs(path);
                        photoService.CreatePhotoBulk(filename, foldername, path, photogallery);
                    }
                }
                return RedirectToAction("Index");
            }

            return View(photogallery);
        }

        // GET: /PhotoGallery/Edit/5
        public ActionResult Edit(int? id)
        {
           
            return View();
        }

        // POST: /PhotoGallery/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID")] PhotoGallery photogallery)
        {
            if (ModelState.IsValid)
            {
                
                return RedirectToAction("Index");
            }
            return View(photogallery);
        }

        // GET: /PhotoGallery/Delete/5
        public ActionResult Delete(int? id)
        {
            
            return View();
        }

        // POST: /PhotoGallery/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           
            return RedirectToAction("Index");
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
