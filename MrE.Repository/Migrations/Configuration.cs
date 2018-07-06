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

            context.SaveChanges();
        }
    }
}
