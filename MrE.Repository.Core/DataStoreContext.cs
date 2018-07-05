using MrE.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace MrE.Repository
{
    public class DataStoreContext : DbContext
    {
       
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Contact> Contacts { get; set; }
       
       
        public DbSet<Status> Statuses { get; set; }
        
        
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }
       



        public DataStoreContext() : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);

            

            builder.Entity<Contact>()
                .HasRequired(p => p.UserCreated)
                .WithMany(p => p.ContactsCreated)
                .HasForeignKey(p => p.UserCreatedID)
                .WillCascadeOnDelete(false);

           

            builder.Entity<Address>()
                .HasRequired(p => p.UserCreated)
                .WithMany(p => p.AddressCreated)
                .HasForeignKey(p => p.UserCreatedID)
                .WillCascadeOnDelete(false);

           

            //builder.Entity<User>()
            //    .HasOptional(p => p.UserCreated)
            //    .WithMany(p => p.Users)
            //    .HasForeignKey(f => f.ContactID)
            //    .WillCascadeOnDelete(false);

           

            builder.Entity<Status>()
                .HasRequired(p => p.UserCreated)
                .WithMany(p => p.StatusesCreated)
                .HasForeignKey(f => f.UserCreatedID)
                .WillCascadeOnDelete(false);

           
        }
    }
}
