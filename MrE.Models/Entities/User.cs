using MrE.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace MrE.Models.Entities
{
    [Table("Users")]
    public class User : IPerson, ICreatable, IDeletable, IUpdatable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int UserID { get; set; }
        [Display(Name = "FirstName"), Required(ErrorMessage = "Fill out Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }

        [Display(Name = "Password")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Password should be 8 characters")]
        [DataType(DataType.Password, ErrorMessage = "Less than 8 characters "), Required(ErrorMessage = "Password Required")]
        public string Password { get; set; }
        public int ContactID { get; set; }
        public Contact Contact { get; set; }
        public int StatusID { get; set; }
        public Status Status { get; set; }

        public int UserRoleID { get; set; }
        public UserRoles UseRoles { get; set; }
        //public virtual ICollection<UseRoles> UseRoles { get; set; }
        public virtual ICollection<Contact> ContactsCreated { get; set; }
        public virtual ICollection<Status> StatusesCreated { get; set; }
        public virtual ICollection<Address> AddressCreated { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("UserCreated")]
        public int UserCreatedID { get; set; }
        public User UserCreated { get; set; }
        public DateTime DateCreated { get; set; }

        [ForeignKey("UserUpdated")]
        [Display(Name = "Date Updated")]
        public DateTime? DateUpdated { get; set; }
        public int? UserUpdateID { get; set; }
        public User UserUpdated { get; set; }

        [ForeignKey("UserDeleted")]
        public int? UserDeletedID { get; set; }
        public User UserDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
