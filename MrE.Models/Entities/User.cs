using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MrE.Models.Entities;

namespace MrEOnline.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual ICollection<Contact> ContactsCreated { get; set; }
        public virtual ICollection<Status> StatusesCreated { get; set; }
        public virtual ICollection<Address> AddressCreated { get; set; }
        public virtual ICollection<Member> MembersCreated { get; set; }

        public virtual ICollection<Contact> ContactsUpdated { get; set; }
        public virtual ICollection<Status> StatusesUpdated { get; set; }
        public virtual ICollection<Address> AddressUpdated { get; set; }
        public virtual ICollection<Member> MembersUpdated { get; set; }

        public virtual ICollection<Contact> ContactsDeleted { get; set; }
        public virtual ICollection<Status> StatusesDeleted { get; set; }
        public virtual ICollection<Address> AddressDeleted { get; set; }
        public virtual ICollection<Member> MembersDeleted { get; set; }

    }
}