using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xland.DAL;
using Xland.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace Xland.Services
{
    public class ProjectService : IProjectService
    {

        private XlandContext context = new XlandContext();

        public IList<string> GetProjectTitles()
        {
            var projects = from p in this.context.Project
                           select p.Title;

            return projects.ToList();
        }

        
        public IDictionary<int, string> GetProjectTitlesAndID() 
        {
            var projects = (from p in this.context.Project
                            select new
                            {
                                ID = p.ID,
                                Title = p.Title
                            }).ToList();


            return projects.ToDictionary(p => p.ID, s => s.Title);

        }

        public IEnumerable<Project> GetAllProjects()
        {
       
            return this.context.Project.ToList();

        }


        System.Collections.Generic.IList<ViewModels.ProjectIndexViewModel> IProjectService.GetProjectTitlesAndID()
        {
            throw new NotImplementedException();
        }


        public void AddProject(Project project)
        {
            this.context.Project.Add(project);
            this.context.SaveChanges();
            
        }


        public void EditProject(Project project)
        {
            this.context.Entry(project).State = EntityState.Modified;
            this.context.SaveChanges();
        }


        public Project GetProject(int? id)
        {
            return this.context.Project.Find(id);

        }

        public void DeleteProject(int id)
        {
            var project = context.Project.Find(id);

            context.Project.Remove(project);
            context.SaveChanges();

        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}