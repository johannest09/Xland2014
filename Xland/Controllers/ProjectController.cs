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
        private IPhotoService photoservice;

        public ProjectController(IProjectService projectService, IStudioService studioService, IPhotoGalleryService photogalleryService, IPhotoService photoservice)
        {
            this.projectService = projectService;
            this.studioService = studioService;
            this.photogalleryService = photogalleryService;
            this.photoservice = photoservice;
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

        }

        public string Info2(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return null;
            }

            
            var project = projectService.GetProjectIncludeStudios(id);

            var photos = new List<Photo>();

            var model = new ProjectInfoViewModel
            {
                Project = project
            };

            var gallery = (from g in photogalleryService.GetAllPhotoGalleries()
                           where g.Project.ID == project.ID
                           select g).SingleOrDefault();

            if (gallery != null)
            {
                photos = (from p in photoservice.GetPhotos()
                              where p.PhotoGallery.ID == gallery.ID
                              select p).ToList();
                model.Photos = photos;

            }
             
            //return JsonConvert.SerializeObject(model);

            
            FormatCompiler compiler = new FormatCompiler();
            //Generator generator = compiler.Compile("Hello, {{this}}!!!");
            //string result = generator.Render("Bob");

            StringBuilder projectHtml = new StringBuilder();

            projectHtml.Append("<div class=\"row\">");
            projectHtml.Append("<div class=\"col-sm-4 col-md-3\">");
            projectHtml.Append("<div id=\"project-info\">");
            projectHtml.Append("<div class=\"project-type\"><span>" + Resources.Resources.Category + "</span>{{ProjectType}}</div>");

            projectHtml.Append("<h3>" + Resources.Resources.StudiosAndParticipants + "</h3>");

            projectHtml.Append("<dl>");
            if (model.Project.Studios.Count > 0)
            {
                projectHtml.Append("<dt>" + Resources.Resources.Studios + "</dt>");
                projectHtml.Append("<dd>");
                foreach (var studio in model.Project.Studios)
                { 
                    projectHtml.Append("<span>" + studio.Name + "</span>");
                }
                projectHtml.Append("</dd>");
            }

            if (project.Designers != null)
            {
                projectHtml.Append("<dt>" + Resources.Resources.Designers + "</dt>");
                projectHtml.Append("<dd>" + project.Designers + "</dd>");
            }

            if (project.ContactPerson != null)
            {
                projectHtml.Append("<dt>" + Resources.Resources.ContactPerson + "</dt>");
                projectHtml.Append("<dd>" + project.ContactPerson + "</dd>");
            }

            projectHtml.Append("</dl>");

            projectHtml.Append("<h3>" + Resources.Resources.GeneralInformation + "</h3>");

            projectHtml.Append("<dl>");

            if (project.Affiliates != null)
            {
                projectHtml.Append("<dt>" + Resources.Resources.Affiliations + "</dt>");
                projectHtml.Append("<dd>" + project.Affiliates + "</dd>");
            }
            if (project.ProjectOwner != null)
            {
                projectHtml.Append("<dt>" + Resources.Resources.ProjectOwner + "</dt>");
                projectHtml.Append("<dd>" + project.ProjectOwner + "</dd>");
            }
            if (project.Contractor != null)
            {
                projectHtml.Append("<dt>" + Resources.Resources.Contractor + "</dt>");
                projectHtml.Append("<dd>" + project.Contractor + "</dd>");
            }
            if (project.ProjectBeginDate != null)
            {
                projectHtml.Append("<dt>" + Resources.Resources.ProjectStarted + "</dt>");
                projectHtml.Append("<dd>" + project.ProjectBeginDate + "</dd>");
            }
            if (project.ProjectEndDate != null)
            {
                projectHtml.Append("<dt>" + Resources.Resources.ProjectFinished + "</dt>");
                projectHtml.Append("<dd>" + project.ProjectEndDate + "</dd>");
            }
            if (project.CapitalCost != null)
            {
                projectHtml.Append("<dt>" + Resources.Resources.CapitalCost + "</dt>");
                projectHtml.Append("<dd>" + project.CapitalCost + "</dd>");
            }

            if (project.AreaSize != null)
            {
                projectHtml.Append("<dt>" + Resources.Resources.AreaSize + "</dt>");
                projectHtml.Append("<dd>" + project.AreaSize + "</dd>");
            }

            if (project.ProjectLocation != null)
            {
                projectHtml.Append("<dt>" + Resources.Resources.ProjectLocation + "</dt>");
                projectHtml.Append("<dd>" + project.ProjectLocation + "</dd>");
            }

            if (project.Locality != null)
            {
                projectHtml.Append("<dt>" + Resources.Resources.Locality + "</dt>");
                projectHtml.Append("<dd>" + project.Locality + "</dd>");
            }

            projectHtml.Append("</dl>");

            projectHtml.Append("</div></div>");


            projectHtml.Append("<div class=\"col-xs-12 col-sm-8 col-md-9\">");
            projectHtml.Append("<div class=\"main-content\">");

            if (photos.Count > 0)
            {
                projectHtml.Append("<div id=\"grid-gallery\" class=\"grid-gallery\">");
                projectHtml.Append("<section class=\"grid-wrap\">");
                projectHtml.Append("<ul class=\"grid cs-style-3\"><li class=\"grid-sizer\"></li>");

                foreach (var p in photos)
                {
                    projectHtml.AppendFormat("<li><figure><img src=\"{0}?width=120\" alt=\"{1}\" data-imageid=\"{2}\" class=\"item\"></li>", Url.Content(p.Path), p.Title, p.ID);
                }

                projectHtml.Append("</ul>");
                projectHtml.Append("</section>");

                projectHtml.Append("<section class=\"slideshow\"><ul>");

                foreach (var p in photos)
                {
                    projectHtml.AppendFormat("<li><figure><img src=\"{0}?w=740&h=500&bgcolor=d9d9d9\" alt=\"{1}\" data-imageid=\"{2}\" class=\"item\"></img><figcaption><h3>{3}</h3><span>{4}</span></figcaption></figure></li>", Url.Content(p.Path), p.Title, p.ID, p.Title, p.Description);
                }

                projectHtml.Append("</ul>");
                projectHtml.Append("<nav><span class=\"icon nav-prev\"></span><span class=\"icon nav-next\"></span><span class=\"icon nav-close\"></span></nav>");
                projectHtml.Append("<div class=\"info-keys icon\">Navigate with arrow keys</div>");

                projectHtml.Append("</section>");

                projectHtml.Append("</div>");
            }

            projectHtml.Append("<div class=\"row\">");
            projectHtml.Append("<div id=\"project-description\" class=\"col-sm-12 col-md-12\">");
            projectHtml.AppendFormat("<h1 class=\"project-title\">{0}</h1>", project.Title);

            if(CultureHelper.GetCurrentCulture().ToLower() == "is-is") {
                    
                if (project.Description != null)
                {
                    projectHtml.Append("<div>" + project.Description + "</div>");
                } 
                else 
                {
                    projectHtml.Append("<div>" + project.DescriptionEnglish + "</div>");
                }
            }

            projectHtml.Append("</div></div>");

            projectHtml.Append("</div></div>");
            projectHtml.Append("</div>"); // Close row

            Generator generator = compiler.Compile(projectHtml.ToString());

            
            string result = generator.Render(model.Project);

            return result;
           
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
                project.ProjectType = (ProjectType)Enum.Parse(typeof(ProjectType), projectType);
                project.ContactPerson = model.Project.ContactPerson;
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
