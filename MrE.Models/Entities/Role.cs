using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MrE.Models.Entities
{
    [Table("Roles")]
    public class Role
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int RoleID { get; set; }

        [Display(Name = "Role")]
        public string Name { get; set; }

        public virtual ICollection<UserRoles> UseRoles { get; set; }
    }
}
