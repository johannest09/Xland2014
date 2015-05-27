using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xland.DAL;
using Xland.Services;

namespace Xland.Controllers
{
    public class VideoController : Controller
    {
        private IVideoService videoService;

        public VideoController(IVideoService videoservice)
        {
            this.videoService = videoservice;
        }

        //
        // GET: /Video/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public void SaveVideoDescription(int id, string descriptionIS = "", string descriptionEN = "")
        {
            videoService.SaveVideoDescription(id, descriptionIS, descriptionEN);
        }

	}
}