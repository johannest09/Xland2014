﻿using System;
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
        Project GetProjectById(int? id);
        void AddProject(Project project);
        void AttachStudioToProject(Studio studio);
        void EditProject(Project project);
        void DeleteProject(int id);

        Project GetProjectIncludeStudios(int? id);
        string GetProjectGalleryMainPhotoPath(int id);

        IEnumerable<Project> GetProjects();
        IEnumerable<Project> GetProjectsWithoutPhotoGalleries();
        IEnumerable<Project> GetProjectsWithoutVideoGalleries();

        void SaveChanges();

        void Dispose();

    }
}
