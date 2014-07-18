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
        //IDictionary<int, string> GetProjectTitlesAndID();
        IList<ProjectIndexViewModel> GetProjectTitlesAndID();

        IEnumerable<Project> GetAllProjects();

    }
}
