﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xland.DAL;
using Xland.Models;
using System.Collections.Generic;
using System.Data.Entity;
using Xland.UnitOfWork;
using Xland.Repository;

namespace Xland.Services
{
    public class ProjectService : IProjectService
    {

        //private XlandContext context = new XlandContext();

        private IUnitOfWork unitOfWork;
        private IGenericRepository<Project> projectRepository;
        private IGenericRepository<Studio> studioRepository;
        private IGenericRepository<PhotoGallery> galleryRepository;
        private IGenericRepository<Photo> photoRepository;

        public ProjectService(IUnitOfWork unitOfWork, IGenericRepository<Project> projectRepository, IGenericRepository<Studio> studioRepository, IGenericRepository<PhotoGallery> galleryRepository, IGenericRepository<Photo> photoRepository)
        {
            this.unitOfWork = unitOfWork;
            this.projectRepository = projectRepository;
            this.studioRepository = studioRepository;
            this.galleryRepository = galleryRepository;
            this.photoRepository = photoRepository;
        }

        public IList<string> GetProjectTitles()
        {
            var projects = from p in projectRepository.GetAll()
                       select p.Title;

            return projects.ToList();
        }

        public IEnumerable<Project> GetProjects()
        {
            return projectRepository.GetAll().ToList();
        }

        public string GetProjectGalleryMainPhotoPath(int id)
        {

            var gallery = (from g in galleryRepository.GetAll()
                       where g.Project.ID == id
                       select g).SingleOrDefault();

            if (gallery != null)
            {
                var photo = (from p in photoRepository.GetAll()
                             where p.PhotoGallery.ID == gallery.ID && p.IsMainPhoto == true
                             select p).SingleOrDefault();
                if (photo != null)
                {
                    return photo.Path;
                }
                else
                {
                    return "";
                }
                
            }
            return "";
        } 

        public void AddProject(Project project)
        {
            projectRepository.Add(project);
            unitOfWork.Save();
        }

        public void AttachStudioToProject(Studio studio)
        {
            studioRepository.Attach(studio);
        }

        public void EditProject(Project project)
        {
            projectRepository.Edit(project);
            unitOfWork.Save();
        }

        public Project GetProjectById(int? id)
        {
            return projectRepository.Find(id);
        }

        public void DeleteProject(int id)
        {
            var project = projectRepository.Find(id);
            projectRepository.Delete(project);
            unitOfWork.Save();
        }

        public Project GetProjectIncludeStudios(int? id)
        {
            var project = projectRepository.GetAll().Include(p => p.Studios)
                .SingleOrDefault(p => p.ID == id);

            return project;
        }

        public IEnumerable<Project> GetProjectsWithoutGalleries()
        {
            var projects = projectRepository.GetAll();
            var galleries = galleryRepository.GetAll();

            var projectsWithGalleries =
            from p in projects
            join g in galleries on p.ID equals g.Project.ID
            select p;

            var noGalleries = projects.Where(x => !projectsWithGalleries.Contains(x)).ToList();

            return noGalleries;

        }

        public void SaveChanges()
        {
            unitOfWork.Save();
        }

        public void Dispose()
        {
            //context.Dispose();
        }

    }
}