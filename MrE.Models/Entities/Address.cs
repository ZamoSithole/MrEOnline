using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MrE.Models.Abstractions;

namespace MrE.Models.Entities
{
    [Table("Addresses")]
    public class Address : ICreatable, IDeletable, IUpdatable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int AddressID { get; set; }

        [Display(Name = "Street Number")]
        public int? StreetNumber { get; set; }

        [Display(Name = "Street Name")]
        public string StreetName { get; set; }

        [Display(Name = "Suburb/Town")]
        public string SuburbTown { get; set; }

        [Display(Name = "Area Code")]
        public string AreaCode { get; set; }

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
