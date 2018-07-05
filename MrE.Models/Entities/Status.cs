using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MrE.Models.Abstractions;

namespace MrE.Models.Entities
{
    [Table("Statuses")]
    public class Status : ICreatable, IDeletable, IUpdatable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StatusID { get; set; }
        [Display(Name = " Status")]
        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsDeleted { get; set; }
        [ForeignKey("UserCreated")]
        public int UserCreatedID { get; set; }
        public User UserCreated { get; set; }
        public DateTime DateCreated { get; set; }

        [ForeignKey("UserUpdated")]
        public DateTime? DateUpdated { get; set; }
        public int? UserUpdateID { get; set; }
        public User UserUpdated { get; set; }

        [ForeignKey("UserDeleted")]
        public int? UserDeletedID { get; set; }
        public User UserDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
