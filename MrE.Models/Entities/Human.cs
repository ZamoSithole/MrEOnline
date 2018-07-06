using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MrE.Models.Entities
{
    [Table("Humans")]
   public class Human
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int HumanID { get; set; }
        public virtual ICollection<UserRoles> UserRoles { get; set; }
        public virtual ICollection<Contact> ContactsCreated { get; set; }
        public virtual ICollection<Status> StatusesCreated { get; set; }
        public virtual ICollection<UserRoles> UserRolesCreated { get; set; }
        public virtual ICollection<Address> AddressCreated { get; set; }
    }
}
