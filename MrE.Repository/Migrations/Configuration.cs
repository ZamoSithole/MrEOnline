namespace MrE.Repository.Migrations
{
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
            context.Statuses.AddOrUpdate(
                p => p.Name,
                new Status { Name = "Pending", DateCreated = DateTime.Now, IsDeleted = false }
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
