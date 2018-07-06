using MrE.Models.Abstractions;
using MrEOnline.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MrE.Models.Entities
{
    [Table("Contacts")]
    public class Contact : IBaseEntity<int>, ICreatable, IDeletable, IUpdatable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]              
        public int Id { get; set; }
        public int? AddressID { get; set; }
        public virtual Address Address { get; set; }

        [DataType(DataType.EmailAddress), Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }

        [Display(Name = "Cell #")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Your Phone number should be 10 digits")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Contact Number is incorrect"), Required(ErrorMessage = "Phone Number required")]

        public string ContactNumber { get; set; }

        public DateTime DateCreated { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }
        
        public DateTime? DateUpdated { get; set; }
    }
}
