using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MrE.Models.Abstractions;
using MrEOnline.Models;

namespace MrE.Models.Entities
{
    [Table("Statuses")]
    public class Status : IBaseEntity<int>, ICreatable, IDeletable, IUpdatable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = " Status"), Required(ErrorMessage ="Status name is required.")]
        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }

        [Display(Name = "Date Deleted")]
        public DateTime? DateDeleted { get; set; }

        public ICollection<Rental> Rentals { get; set; }
    }
}
