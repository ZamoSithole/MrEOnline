using MrE.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrE.Models.Entities
{
    [Table("Genres")]
   public class Genre : IBaseEntity<int>, ICreatable,IDeletable,IUpdatable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        [Display(Name = " Genre"), Required(ErrorMessage = "Genre name is required.")]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        [Display(Name = "Date Deleted")]
        public DateTime? DateDeleted { get; set; }
    }
}
