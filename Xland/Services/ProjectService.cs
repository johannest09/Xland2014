using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xland.DAL;
using Xland.Models;
using System.Collections.Generic;

namespace Xland.Services
{
    public class ProjectService : IProjectService
    {

        private XlandContext projectContext = new XlandContext();

        public IList<string> GetProjectTitles()
        {
            var projects = from p in this.projectContext.Project
                           select p.Title;

            return projects.ToList();
        }

        
        public IDictionary<int, string> GetProjectTitlesAndID() 
        {
            var projects = (from p in this.projectContext.Project
                            select new
                            {
                                ID = p.ID,
                                Title = p.Title
                            }).ToList();


            return projects.ToDictionary(p => p.ID, s => s.Title);

        }

        public IEnumerable<Project> GetAllProjects()
        {
       
            return this.projectContext.Project.ToList();

        }





        System.Collections.Generic.IList<ViewModels.ProjectIndexViewModel> IProjectService.GetProjectTitlesAndID()
        {
            throw new NotImplementedException();
        }
    }
}