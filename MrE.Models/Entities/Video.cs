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
    public class Video : IBaseEntity<int>, ICreatable, IDeletable,IUpdatable, IUploadable
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
        [DataType(DataType.Currency, ErrorMessage = "Less than zero ")]
        public decimal RentalPrice { get; set; }
        [Display(Name = "Age Restriction"), Required(ErrorMessage = "Age Restriction is required.")]
        public string AgeRestriction { get; set; }

        public virtual ICollection<Cast> Casts { get; set; }

        public int Quantity { get; set; }

        [Display(Name = "Movie")]
        [Column("MoviePhoto")]
        public byte[] Data { get; set; }

        public bool IsDeleted { get; set; }
 
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

        [Display(Name = "Date Deleted")]
        public DateTime? DateDeleted { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; }
        [NotMapped]
        public int QuantityRemaining {
            get { 
                var dataQuery = Rentals.Where(e => e.StatusId == 2).Count(r => r.VideoId == Id);
                return Quantity - dataQuery;

            }
        }
    }
}
