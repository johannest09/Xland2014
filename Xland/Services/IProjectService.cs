using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xland.Models;
using Xland.ViewModels;

namespace Xland.Services
{
    public interface IProjectService
    {
        IList<string> GetProjectTitles();
        IList<ProjectIndexViewModel> GetProjectTitlesAndID();
        Project GetProject(int? id);
        void AddProject(Project project);
        void EditProject(Project project);
        void DeleteProject(int id);

        void Dispose();

        IEnumerable<Project> GetAllProjects();

    }
}
