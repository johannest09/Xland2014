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

namespace Xland.Controllers
{
    public class PhotoController : Controller
    {
        private IPhotoService photoService;

        public PhotoController(IPhotoService photoService)
        {
            this.photoService = photoService;
        }

        // GET: /Photo/
        public ActionResult Index()
        {
            
            return View();



        }

        // GET: /Photo/Details/5
        public ActionResult Details(int? id)
        {
            return View();
        }

        // GET: /Photo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Photo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Path,Description,IsMainPhoto")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                photoService.CreatePhoto(photo);
                return RedirectToAction("Index");
            }

            return View(photo);
        }

        // GET: /Photo/Edit/5
        public ActionResult Edit(int? id)
        {
            return View();
        }

        // POST: /Photo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Path,Description,IsMainPhoto")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                
                return RedirectToAction("Index");
            }
            return View(photo);
        }

        // GET: /Photo/Delete/5
        public ActionResult Delete(int? id)
        {
            return View();
        }

        // POST: /Photo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            return RedirectToAction("Index");
        }

        [Authorize]
        public void DeletePhoto(int id)
        {
            photoService.DeletePhoto(id);
        }

        [HttpPost]
        public void SetAsMainPhoto(int id)
        {
            photoService.SetAsMainPhoto(id);
        }

        [HttpPost]
        [Authorize]
        public void SavePhotoText(int id, string title = "", string text = "")
        {
            photoService.SavePhotoText(id, title, text);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                photoService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
