using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Xland.Models;
using Xland.Services;
using Xland.ViewModels;
using AutoMapper;
using Xland.Utilities;
using System.Globalization;
using Xland.Filters;
using Xland.Helpers;
using Newtonsoft.Json;
using Mustache;
using System.Text;


namespace Xland.Controllers
{
    public class ProjectController : BaseController
    {
        private IProjectService projectService;
        private IStudioService studioService;
        private IPhotoGalleryService photogalleryService;
        private IVideoGalleryService videoGalleryService;
        private IPhotoService photoservice;
        private IVideoService videoService;

        public ProjectController(IProjectService projectService, IStudioService studioService, IPhotoGalleryService photogalleryService, IPhotoService photoservice, IVideoGalleryService videoGalleryService, IVideoService videoService)
        {
            this.projectService = projectService;
            this.studioService = studioService;
            this.photogalleryService = photogalleryService;
            this.videoGalleryService = videoGalleryService;
            this.photoservice = photoservice;
            this.videoService = videoService;
        }

        // GET: /Project/
        
        [Authorize]
        public ActionResult Index()
        {
            var projects = projectService.GetProjects();

            IEnumerable<ProjectIndexViewModel> viewModel = Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectIndexViewModel>>(projects.Cast<Project>().AsEnumerable());

            return View(viewModel);
        }

        public ActionResult Info(int? id)
        {
            /*
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var project = projectService.GetProjectIncludeStudios(id);

            if (project == null)
            {
                return HttpNotFound();
            }

            var model = new ProjectInfoViewModel
            {
                Project = project
            };

            var gallery = (from g in photogalleryService.GetAllPhotoGalleries()
                          where g.Project.ID == project.ID
                          select g).SingleOrDefault();

            if (gallery != null)
            {
                var photos = (from p in photoservice.GetPhotos()
                              where p.PhotoGallery.ID == gallery.ID
                              select p).ToList();
                model.Photos = photos;

            }

            return View(model);
            */



            return RedirectToAction("Index", "Home", new { id = id });
        }

        [OutputCache(Duration = 3600, VaryByParam = "none")]
        public string Info2(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return null;
            }

            var project = projectService.GetProjectIncludeStudios(id);

            var model = new ProjectInfoViewModel
            {
                Project = project
            };

            var photoGallery = (from g in photogalleryService.GetAllPhotoGalleries()
                           where g.Project.ID == project.ID
                           select g).SingleOrDefault();

            if (photoGallery != null)
            {
                model.Photos = (from p in photoservice.GetPhotos()
                                where p.PhotoGallery.ID == photoGallery.ID
                                select p).ToList();
            }

            var videoGallery = (from vg in videoGalleryService.GetAllVideoGalleries()
                                where vg.Project.ID == project.ID
                                select vg).SingleOrDefault();

            if (videoGallery != null)
            {
                model.Videos = (from v in videoService.GetVideos()
                                where v.VideoGallery.ID == videoGallery.ID
                                select v).ToList();
            }

            if (project.ProjectBeginDate != null && project.ProjectEndDate != null)
            {
                if (project.ProjectBeginDate.Date.Year == project.ProjectEndDate.Date.Year)
                {
                    model.ProjectExecutionPeriod = project.ProjectBeginDate.Date.Year.ToString();
                }
                else
                {
                    model.ProjectExecutionPeriod = project.ProjectBeginDate.Date.Year.ToString() + " &#8211; " + project.ProjectEndDate.Date.Year.ToString(); 
                }
            }


            model.Photos.ToList().ForEach(p => p.Path = p.Path.Substring(2));

            string pType = model.Project.ProjectType.ToString();
            model.ProjectType = Resources.Resources.ResourceManager.GetString(pType);

            var culture = CultureHelper.GetCurrentCulture();

            if (culture.ToLower() == "en-us")
            {
                if (model.Project.DescriptionEnglish == null)
                {
                    model.ProjectDescription = model.ProjectDescription = Resources.Resources.NoDescriptionMessage;
                }
                else 
                {
                    model.ProjectDescription = model.Project.DescriptionEnglish;
                }
            }
            else
            {
                if (model.Project.Description == null)
                {
                    model.ProjectDescription = Resources.Resources.NoDescriptionMessage;
                }
                else
                {
                    model.ProjectDescription = model.Project.Description;
                }
            }

            return JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });

        }

        // GET: /Project/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var project = projectService.GetProjectById(id);

            if (project == null)
            {
                return HttpNotFound();
            }



            return View(project);
        }

        // GET: /Project/Create
        [Authorize]
        public ActionResult Create()
        {

            var viewModel = new ProjectCreateViewModel 
            { 
                Studios = studioService.GetAllStudios().Select(s => new StudioSelectViewModel
                {
                    StudioID = s.ID,
                    Name = s.Name,
                    IsSelected = false
                }).ToList()
            };

            return View(viewModel);
            
        }

        // POST: /Project/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        public ActionResult Create(ProjectCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
          
                var project = viewModel.Project;

                project.DateCreated = DateTime.Now;
                project.DateUpdated = DateTime.Now;
                
                project.Studios = new List<Studio>();

                if (viewModel.Studios != null)
                {
                    foreach (var selectedStudio in viewModel.Studios.Where(s => s.IsSelected))
                    {
                        var studio = new Studio { ID = selectedStudio.StudioID };
                        projectService.AttachStudioToProject(studio);
                        project.Studios.Add(studio);
                    }
                }

                projectService.AddProject(project);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: /Project/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var project = projectService.GetProjectById(id);

            if (project == null)
            {
                return HttpNotFound();
            }

            var selectedStudios = studioService.GetProjectStudios(id);

            var viewModel = new ProjectEditViewModel
            {
                ID = project.ID,
                Project = project
            };

            viewModel.Studios = studioService.GetAllStudios()
                .Select(c => new StudioEditViewModel
                {
                    StudioID = c.ID,
                    Name = c.Name
                }).ToList();

            foreach (var s in viewModel.Studios)
            {
                s.IsSelected = selectedStudios.Any(x => x.ID == s.StudioID);
            }

            var projectTypes = from ProjectType s in Enum.GetValues(typeof(ProjectType))
               select new { ID = s, Name = s.ToString() };

            ViewData["projectTypes"] = new SelectList(projectTypes, "ID", "Name", project.ProjectType);

            return View(viewModel);
        }

        // POST: /Project/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Title,Author")] Project project)
        {
            if (ModelState.IsValid)
            {
                projectService.EditProject(project);
                return RedirectToAction("Index");
            }
            return View(project);
        }
         * */

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(ProjectEditViewModel model)
        {

            if (ModelState.IsValid)
            {
                var project = projectService.GetProjectIncludeStudios(model.ID);
                string projectType = Request["ProjectTypes"];

                // Mapper does not work!
                //project = Mapper.Map<ProjectEditViewModel, Project>(model, project);
                //ProjectMapper.Map(model.Project, project);


                project.ID = model.ID;
                project.Title = model.Project.Title;
                project.IsVisible = model.Project.IsVisible;
                project.ProjectType = (ProjectType)Enum.Parse(typeof(ProjectType), projectType);
                project.ProjectBeginDate = model.Project.ProjectBeginDate;
                project.ProjectEndDate = model.Project.ProjectEndDate;
                project.InProgress = model.Project.InProgress;
                project.CapitalCost = model.Project.CapitalCost;
                project.Designers = model.Project.Designers;
                project.Affiliates = model.Project.Affiliates;
                project.ProjectOwner = model.Project.ProjectOwner;
                project.Contractor = model.Project.Contractor;
                project.AreaSize = model.Project.AreaSize;
                project.Description = model.Project.Description;
                project.DescriptionEnglish = model.Project.DescriptionEnglish;
                project.Lat = model.Project.Lat;
                project.Long = model.Project.Long;
                project.ProjectLocation = model.Project.ProjectLocation;
                project.Locality = model.Project.Locality;
                project.DateUpdated = DateTime.Now;

                if (model.Studios != null)
                {
                    foreach (var studio in model.Studios)
                    {
                        if (studio.IsSelected)
                        {
                            if (!project.Studios.Any(s => s.ID == studio.StudioID))
                            {
                                // if studio is selected but not yet
                                // related in DB, add relationship
                                var addedStudio = new Studio { ID = studio.StudioID };
                                projectService.AttachStudioToProject(addedStudio);
                                project.Studios.Add(addedStudio);
                            }

                            projectService.SaveChanges();
                        }
                        else
                        {
                            var removedStudio = project.Studios
                                .SingleOrDefault(s => s.ID == studio.StudioID);
                            if (removedStudio != null)
                            {
                                // if studio is not selected but currently
                                // related in DB, remove relationship
                                project.Studios.Remove(removedStudio);
                            }
                        }
                    }
                    projectService.EditProject(project);
                    projectService.SaveChanges();
                }

                var projectTypes = from ProjectType s in Enum.GetValues(typeof(ProjectType))
                                   select new { ID = s, Name = s.ToString() };

                ViewData["projectTypes"] = new SelectList(projectTypes, "ID", "Name", project.ProjectType);
            }

            return View(model);
        }

        // GET: /Project/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = projectService.GetProjectById(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: /Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            
            projectService.DeleteProject(id);

            return RedirectToAction("Index");
        }
        
        /*
        public enum ProjectType
        {
            Almenningsrými, Saga, Samkeppnir, Skipulag
        }
         * */

        private IEnumerable<SelectListItem> GetProjectTypes()
        {
            
            return null;
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

            var returUrl = Request["returnUrl"];

            return Redirect(returUrl);

        }    


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
               projectService.Dispose();
            }
            base.Dispose(disposing);
        }

        
    }
}
