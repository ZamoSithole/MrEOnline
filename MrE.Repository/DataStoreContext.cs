
using Microsoft.AspNet.Identity.EntityFramework;
using MrE.Models.Entities;
using MrEOnline.Models;
using System.Data.Entity;

namespace MrE.Repository
{
    public class DataStoreContext : IdentityDbContext<User>
    {       
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Contact> Contacts { get; set; }             
        public DbSet<Status> Statuses { get; set; }               
        public DbSet<Member> Members { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public DbSet<Video> Videos { get; set; }
       
        public DataStoreContext() : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<Contact>()
            //    .HasRequired(p => p.CreatedBy)
            //    .WithMany(p => p.ContactsCreated)
            //    .HasForeignKey(p => p.CreatedById)
            //    .WillCascadeOnDelete(false);

            //builder.Entity<Address>()
            //    .HasRequired(p => p.CreatedBy)
            //    .WithMany(p => p.AddressCreated)
            //    .HasForeignKey(p => p.CreatedById)
            //    .WillCascadeOnDelete(false);

            //builder.Entity<Member>()
            //    .HasOptional(p => p.CreatedBy)
            //    .WithMany(p => p.MembersCreated)
            //    .HasForeignKey(f => f.CreatedById)
            //    .WillCascadeOnDelete(false);

            //builder.Entity<Status>()
            //    .HasRequired(p => p.CreatedBy)
            //    .WithMany(p => p.StatusesCreated)
            //    .HasForeignKey(f => f.CreatedById)
            //    .WillCascadeOnDelete(false);


            //builder.Entity<Contact>()
            //   .HasRequired(p => p.UpdatedBy)
            //   .WithMany(p => p.ContactsUpdated)
            //   .HasForeignKey(p => p.UpdatedById)
            //   .WillCascadeOnDelete(false);

            //builder.Entity<Address>()
            //    .HasRequired(p => p.UpdatedBy)
            //    .WithMany(p => p.AddressUpdated)
            //    .HasForeignKey(p => p.UpdatedById)
            //    .WillCascadeOnDelete(false);

            //builder.Entity<Member>()
            //    .HasOptional(p => p.UpdatedBy)
            //    .WithMany(p => p.MembersUpdated)
            //    .HasForeignKey(f => f.UpdatedById)
            //    .WillCascadeOnDelete(false);

            //builder.Entity<Status>()
            //    .HasRequired(p => p.UpdatedBy)
            //    .WithMany(p => p.StatusesUpdated)
            //    .HasForeignKey(f => f.UpdatedById)
            //    .WillCascadeOnDelete(false);

            //builder.Entity<Contact>()
            //   .HasRequired(p => p.DeletedBy)
            //   .WithMany(p => p.ContactsDeleted)
            //   .HasForeignKey(p => p.DeletedById)
            //   .WillCascadeOnDelete(false);

            //builder.Entity<Address>()
            //    .HasRequired(p => p.DeletedBy)
            //    .WithMany(p => p.AddressDeleted)
            //    .HasForeignKey(p => p.DeletedById)
            //    .WillCascadeOnDelete(false);

            //builder.Entity<Member>()
            //    .HasOptional(p => p.DeletedBy)
            //    .WithMany(p => p.MembersDeleted)
            //    .HasForeignKey(f => f.DeletedById)
            //    .WillCascadeOnDelete(false);

            //builder.Entity<Status>()
            //    .HasRequired(p => p.DeletedBy)
            //    .WithMany(p => p.StatusesDeleted)
            //    .HasForeignKey(f => f.DeletedById)
            //    .WillCascadeOnDelete(false);
        }
    }
}
