using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Xland.DAL;
using Xland.Models;

namespace Xland.Services
{
    public class StudioService : IStudioService
    {

        private XlandContext context = new XlandContext();

        public IEnumerable<Studio> GetAllStudios()
        {
            return context.Studios.ToList();
        }
        


        public void CreateStudio(Studio studio)
        {
            context.Studios.Add(studio);
            context.SaveChanges();
        }

        public void AttachStudio(Studio studio)
        {
            context.Studios.Attach(studio);
        }

        public void EditStudio(Studio studio)
        {
            context.Entry(studio).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteStudio(int id)
        {
            Studio studio = this.GetStudioByID(id);
            context.Studios.Remove(studio);
            context.SaveChanges();
        }

        public Studio GetStudioByID(int? id)
        {
            return context.Studios.Find(id);
        }
        public List<KeyValuePair<int, string>> GetStudioNamesAndID()
        {
            var studios = context.Studios.ToList();

            var keyValuPairs = new List<KeyValuePair<int, string>>();

            foreach (var studio in studios)
            {
                keyValuPairs.Add(new KeyValuePair<int, string>(studio.ID, studio.Name));
            }

            return keyValuPairs;
        }

        public IEnumerable<Studio> GetProjectStudios(int? id)
        {
            var query = (from s in context.Studios
                        where s.Projects.Any(p => p.ID == id)
                        select s).ToList();

            return query;
        }

        public void Dispose()
        {
            context.Dispose();
        }

    }
}