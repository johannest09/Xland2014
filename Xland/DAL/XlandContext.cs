using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Xland.Models;

namespace Xland.DAL
{
    public class XlandContext : DbContext
    {
        public XlandContext() : base("XlandContext") { }

        public DbSet<Project> Project { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }
    }
}