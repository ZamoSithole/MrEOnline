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
    [Table("Video")]
    public class Video : IBaseEntity<int>, ICreatable, IDeletable,IUpdatable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        [Display(Name = " Title"), Required(ErrorMessage = "Status name is required.")]
        public string Title { get; set; }
        [Display(Name = " Description"), Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        [Display(Name = " Genre"), Required(ErrorMessage = "Genre is required.")]
        public int GenreID { get; set; }
        public Genre Genre { get; set; }

        [Display(Name = " Rental Price"), Required(ErrorMessage = "RentalnPrice is required.")]
        public decimal RentalPrice { get; set; }

        public bool IsDeleted { get; set; }
 
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

        [Display(Name = "Date Deleted")]
        public DateTime? DateDeleted { get; set; }
    }
}
