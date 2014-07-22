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
    public class StudioController : Controller
    {
        private IStudioService studioService;

        public StudioController(IStudioService service)
        {
            this.studioService = service;
        }

        // GET: /Studio/
        public ActionResult Index()
        {
            return View(studioService.GetAllStudios());
        }

        // GET: /Studio/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Studio studio = studioService.GetStudioByID(id);


            if (studio == null)
            {
                return HttpNotFound();
            }
            return View(studio);
        }

        // GET: /Studio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Studio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Name,Email,Website,Address")] Studio studio)
        {
            if (ModelState.IsValid)
            {
                studioService.CreateStudio(studio);
                return RedirectToAction("Index");
            }

            return View(studio);
        }

        // GET: /Studio/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Studio studio = studioService.GetStudioByID(id);
            if (studio == null)
            {
                return HttpNotFound();
            }
            return View(studio);
        }

        // POST: /Studio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Name,Email,Website,Address")] Studio studio)
        {
            if (ModelState.IsValid)
            {
                studioService.EditStudio(studio);
                return RedirectToAction("Index");
            }
            return View(studio);
        }

        // GET: /Studio/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Studio studio = studioService.GetStudioByID(id);
            if (studio == null)
            {
                return HttpNotFound();
            }
            return View(studio);
        }

        // POST: /Studio/Delete/5
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
                studioService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
