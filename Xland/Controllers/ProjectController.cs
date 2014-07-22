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

namespace Xland.Controllers
{
    public class ProjectController : Controller
    {

        private IProjectService projectService;
        private IStudioService studioService;

        public ProjectController(IProjectService projectService, IStudioService studioService)
        {
            this.projectService = projectService;
            this.studioService = studioService;
        }

        // GET: /Project/
        
        public ActionResult Index()
        {
            //return View(db.Project.ToList());

            /*
            var projectTitles = service.GetProjectTitles();
            var list = new List<ProjectViewModel>();

            foreach (var title in projectTitles)
            {
                var vm = new ProjectViewModel();
                vm.Title = title;
                list.Add(vm);
            }

             * */

            var projects = projectService.GetAllProjects();

            IEnumerable<ProjectIndexViewModel> viewModel = Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectIndexViewModel>>(projects.Cast<Project>().AsEnumerable());

            return View(viewModel);
        }

        // GET: /Project/Details/5
        public ActionResult Details(int? id)
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

        // GET: /Project/Create
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
        [HttpPost]
        public ActionResult Create(ProjectCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var project = new Project
                {
                    Title = viewModel.Project.Title,
                    Author = viewModel.Project.Author,
                    Studios = new List<Studio>()
                };

                if (viewModel.Studios != null)
                {
                    foreach (var selectedStudio in viewModel.Studios.Where(s => s.IsSelected))
                    {
                        var studio = new Studio { ID = selectedStudio.StudioID };
                        projectService.AttachStudioToProject(studio);
                        project.Studios.Add(studio);
                    }

                    projectService.SaveChanges();
                }

                projectService.AddProject(project);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: /Project/Edit/5
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
        public ActionResult Edit(ProjectEditViewModel model)
        {

            //IEnumerable<ProjectIndexViewModel> viewModel = Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectIndexViewModel>>(projects.Cast<Project>().AsEnumerable());
            
            //var project = model.Project;

            var project = projectService.GetProjectIncludeStudios(model.ID);

            if (model.Studios != null)
            {
                foreach (var studio in model.Studios)
                {
                    if (studio.IsSelected)
                    {
                        if (!project.Studios.Any( s => s.ID == studio.StudioID))
                        {
                            // if studio is selected but not yet
                            // related in DB, add relationship
                            var addedStudio = new Studio { ID = studio.StudioID };
                            projectService.AttachStudioToProject(addedStudio);
                            project.Studios.Add(addedStudio);
                        }
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

                projectService.SaveChanges();
            }


            return View(model);
        }

        // GET: /Project/Delete/5
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
        public ActionResult DeleteConfirmed(int id)
        {
            
            projectService.DeleteProject(id);

            return RedirectToAction("Index");
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
