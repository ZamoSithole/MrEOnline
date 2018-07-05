using MrE.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MrE.Models.Entities
{
    [Table("Contacts")]
    public class Contact : ICreatable, IDeletable, IUpdatable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       

        public int ContactID { get; set; }
        public int? AddressID { get; set; }
        public virtual Address Address { get; set; }

        [DataType(DataType.EmailAddress), Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }

        [Display(Name = "Cell #")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Your Phone number should be 10 digits")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Contact Number is incorrect"), Required(ErrorMessage = "Phone Number required")]

        public string ContactNumber { get; set; }

        [ForeignKey("UserCreated")]
        public int UserCreatedID { get; set; }
        public User UserCreated { get; set; }
        public DateTime DateCreated { get; set; }

        public bool IsDeleted { get; set; }
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
