using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MrE.Models.Abstractions;

namespace MrE.Models.Entities
{
    [Table("UserRoles")]
    public class UserRoles :ICreatable, IDeletable,IUpdatable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int UserRoleID { get; set; }
        public int UserID { get; set; }
        public virtual User User { get; set; }
        public int RoleID { get; set; }
        public virtual Role Role { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("UserCreated")]
        public int UserCreatedID { get; set; }
        public User UserCreated { get; set; }
        public DateTime DateCreated { get; set; }

        
        [ForeignKey("UserUpdated")]
        public int? UserUpdateID { get; set; }
        public User UserUpdated { get; set; }
        public DateTime? DateUpdated { get; set; }

        [ForeignKey("UserDeleted")]
        public int? UserDeletedID { get; set; }
        public User UserDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
