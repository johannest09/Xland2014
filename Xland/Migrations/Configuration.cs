namespace Xland.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Xland.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Xland.DAL.XlandContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Xland.DAL.XlandContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var projects = new List<Project> {
                
                new Project { Title = "Skúlagata 42", Author = "Jóhannes" },
                new Project { Title = "Skaftahlíð 4", Author = "Sjöfn" },
                new Project { Title = "Hverabakki 2", Author = "Þorleifur" }
            };

            projects.ForEach(s => context.Project.AddOrUpdate(p => p.Title, s));
            context.SaveChanges();


        }
    }
}
