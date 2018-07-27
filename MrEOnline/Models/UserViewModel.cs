using Microsoft.AspNet.Identity.EntityFramework;
using MrE.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MrEOnline.Models {   
    public class UserRoleViewModel  {
        public RoleViewModel Role { get; set; }
        public string UserId { get; set; }    
        
        public UserRoleViewModel() { }
        public UserRoleViewModel(RoleViewModel role, string userId) {
            Role = role;
            UserId = userId;
        }
    }

    public class RoleViewModel {
        public string RoleId { get; set; }
        public string Name { get; set; }

        public RoleViewModel() { }

        public RoleViewModel(IdentityRole role) {
            RoleId = role.Id;
            Name = role.Name;
        }
    }
    public class UserViewModel : IBaseEntity<string> {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<IdentityUserRole> Roles { get; set; }

        public UserViewModel() { }

        public UserViewModel(User user) {
            Id = user.Id;
            FirstName = user.FirstName;
            Surname = user.Surname;
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;
            Roles = user.Roles;
        }
    }
}