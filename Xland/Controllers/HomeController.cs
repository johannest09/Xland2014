using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xland.Helpers;
using Xland.Services;

namespace Xland.Controllers
{
    public class HomeController : BaseController
    {
        private IProjectService projectService;

        public HomeController(IProjectService projectService)
        {
            this.projectService = projectService;
        }
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SetCulture(string culture)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
            //RouteData.Values["culture"] = culture;  // set culture

            // Save culture in a cookie

            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);

            var currentUri = Request.Url.AbsoluteUri;

            return Redirect("Index");

        }

        [OutputCache(Duration = 3600, VaryByParam = "none")]
        public JsonResult GetMarkers()
        {
            try
            {
                var markers =
                (from p in projectService.GetProjects()
                 select new { p.ID, p.Title, p.ProjectType, p.Lat, p.Long }).ToList();

                return Json(markers, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                //throw;
                return null;
            }

        }

        [OutputCache(Duration = 3600, VaryByParam = "none")]
        public JsonResult GetMarkerInfo(int id)
        {
            var markerInfo =
               (from p in projectService.GetProjects()
                where p.ID == id
                select new { p.ID, p.Title }).SingleOrDefault();

            var mainPhotoPath = projectService.GetProjectGalleryMainPhotoPath(id);

            if (mainPhotoPath != "")
            {
                return Json(new { Id = markerInfo.ID, Title = markerInfo.Title, MainPhoto = mainPhotoPath }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Id = markerInfo.ID, Title = markerInfo.Title }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
