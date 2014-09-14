using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Xland.DAL;
using Xland.Models;
using Xland.UnitOfWork;
using Xland.Repository;

namespace Xland.Services
{
    public class StudioService : IStudioService
    {

        private IUnitOfWork unitOfWork;
        private IGenericRepository<Studio> studioRepository;

        public StudioService(Xland.UnitOfWork.UnitOfWork unitOfWork, IGenericRepository<Studio> studioRepository)
        {
            this.unitOfWork = unitOfWork;
            this.studioRepository = studioRepository;
        }

        public IEnumerable<Studio> GetAllStudios()
        {
            return studioRepository.GetAll();
        }
        
        public void CreateStudio(Studio studio)
        {
            studioRepository.Add(studio);
            unitOfWork.Save();
        }

        public void AttachStudio(Studio studio)
        {
            studioRepository.Attach(studio);
        }

        public void EditStudio(Studio studio)
        {
            studioRepository.Edit(studio);
            unitOfWork.Save();
        }

        public void DeleteStudio(int id)
        {
            Studio studio = studioRepository.Find(id);
            studioRepository.Delete(studio);
            unitOfWork.Save();
        }

        public Studio GetStudioByID(int? id)
        {
            return studioRepository.Find(id);
        }

        public IEnumerable<Studio> GetProjectStudios(int? id)
        {

            var projectStudios = (from s in studioRepository.GetAll()
                          where s.Projects.Any(p => p.ID == id)
                          select s).ToList();

            return projectStudios;
        }

    }
}