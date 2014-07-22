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

        public IEnumerable<Project> GetAllProjects()
        {
            return this.context.Project.ToList();
        }

        public void AddProject(Project project)
        {
            this.context.Project.Add(project);
            this.context.SaveChanges();
        }

        public void AttachStudioToProject(Studio studio)
        {
            context.Studios.Attach(studio);
        }

        public void EditProject(Project project)
        {
            this.context.Entry(project).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        public Project GetProjectById(int? id)
        {
            return this.context.Project.Find(id);

        }

        public void DeleteProject(int id)
        {
            var project = context.Project.Find(id);

            context.Project.Remove(project);
            context.SaveChanges();
        }

        public Project GetProjectIncludeStudios(int id)
        {
            var project = context.Project.Include(p => p.Studios)
                .SingleOrDefault(p => p.ID == id);

            return project;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}