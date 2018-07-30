namespace MrE.Repository.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using MrE.Models.Entities;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DataStoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataStoreContext context)
        {
            context.Roles.AddOrUpdate(
                p => p.Name,
                new IdentityRole { Name = "Administrator"},
                new IdentityRole { Name="Customer"}
                );

            context.Statuses.AddOrUpdate(
                p => p.Name,
                new Status { Name = "Pending",Description = "Pending action", DateCreated = DateTime.Now, IsDeleted = false },
                new Status { Name = "CheckedOut", Description = "Video checked out", DateCreated = DateTime.Now, IsDeleted = false },
                new Status { Name = "CheckedIn", Description = "Video checked in", DateCreated = DateTime.Now, IsDeleted = false },
                new Status { Name = "Active", Description = "Active status", DateCreated = DateTime.Now, IsDeleted = false }
                );
            context.Genres.AddOrUpdate(
                p => p.Name,
                new Genre { Name = "Action", DateCreated = DateTime.Now, IsDeleted = false },
                new Genre { Name = "Adventure", DateCreated = DateTime.Now, IsDeleted = false },
                new Genre { Name = "Romance", DateCreated = DateTime.Now, IsDeleted = false },
                new Genre { Name = "Comedy", DateCreated = DateTime.Now, IsDeleted = false }
                );
            context.Titles.AddOrUpdate(
                p => p.Name,
                 new Title { Name = "Producer", DateCreated = DateTime.Now },
                 new Title { Name = "Director", DateCreated = DateTime.Now },
                 new Title { Name = "Actor", DateCreated = DateTime.Now },
                 new Title { Name = "Editor", DateCreated = DateTime.Now },
                 new Title { Name = "Screenwriter", DateCreated = DateTime.Now }
                 );
            context.SaveChanges();
        }

    }
}
