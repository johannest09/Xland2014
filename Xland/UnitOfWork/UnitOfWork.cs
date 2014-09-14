using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using Xland.DAL;

namespace Xland.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        protected string ConnectionString;
        private XlandContext context;

        public UnitOfWork() { }

        public UnitOfWork(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public XlandContext DbContext
        {
            get
            {
                if (context == null)
                {
                    context = new XlandContext();
                }
                return context;
            }
        }

        public int Save()
        {
            try
            {
                return context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    /*
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                     * */
                }
                throw;
            }

            //return context.SaveChanges();
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}