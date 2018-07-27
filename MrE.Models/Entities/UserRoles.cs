using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MrE.Models.Abstractions;
using MrEOnline.Models;

namespace MrE.Models.Entities
{
    [Table("UserRoles")]
    public class UserRoles :ICreatable, IDeletable,IUpdatable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int UserRoleID { get; set; }
        public int UserID { get; set; }
        public virtual Member User { get; set; }
        public int RoleID { get; set; }
        //public virtual Role Role { get; set; }
        public User CreatedBy { get; set;}
        public string CreatedById { get; set;}
        public DateTime DateCreated { get; set;}
        public bool IsDeleted { get; set;}
        public DateTime? DateDeleted { get; set;}
        public string DeletedById { get; set;}
        public User DeletedBy { get; set;}
        public DateTime? DateUpdated { get; set;}
        public string UpdatedById { get; set;}
        public User UpdatedBy { get; set;}
    }
}
