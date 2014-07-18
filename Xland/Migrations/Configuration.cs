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
                
                new Project { Title = "Sk�lagata 42", Author = "J�hannes" },
                new Project { Title = "Skaftahl�� 4", Author = "Sj�fn" },
                new Project { Title = "Hverabakki 2", Author = "�orleifur" }
            };

            projects.ForEach(s => context.Project.AddOrUpdate(p => p.Title, s));
            context.SaveChanges();


        }
    }
}
