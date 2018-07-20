
using Microsoft.AspNet.Identity.EntityFramework;
using MrE.Models.Entities;
using MrEOnline.Models;
using System.Data.Entity;

namespace MrE.Repository
{
    public class DataStoreContext : IdentityDbContext<User>
    {       
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }

        public virtual DbSet<Video> Videos { get; set; }
        public virtual DbSet<Title> Titles { get; set; }
        public virtual DbSet<Cast> Casts { get; set; }

        public virtual DbSet<Rental> CheckOuts { get; set; }
       
        public DataStoreContext() : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual object Update(object item) {
            if (Entry(item).State == EntityState.Detached) {
                Set(item.GetType()).Attach(item);
                Entry(item).State = EntityState.Modified;
            }
            return item;
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Rental>()
                .HasRequired(p => p.Status)
                .WithMany(p => p.Rentals)
                .HasForeignKey(p => p.StatusId)
                .WillCascadeOnDelete(false);
        }
    }
}
