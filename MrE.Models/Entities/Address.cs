using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MrE.Models.Abstractions;
using MrEOnline.Models;

namespace MrE.Models.Entities
{
    [Table("Addresses")]
    public class Address : IBaseEntity<int>, ICreatable, IDeletable, IUpdatable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        [Display(Name = "Street Number")]
        public int? StreetNumber { get; set; }

        [Display(Name = "Street Name")]
        public string StreetName { get; set; }

        [Display(Name = "Suburb/Town")]
        public string SuburbTown { get; set; }

        [Display(Name = "Area Code")]
        public string AreaCode { get; set; }

        public DateTime DateCreated { get; set;}

        public bool IsDeleted { get; set;}
        public DateTime? DateDeleted { get; set;}

        public DateTime? DateUpdated { get; set; }
    }
}
