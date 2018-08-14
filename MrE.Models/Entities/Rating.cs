using MrE.Models.Abstractions;
using MrEOnline.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MrE.Models.Entities {
    [Table("Reviews")]
    public class Rating : IBaseEntity<int>, ICreatable, IDeletable,IUpdatable {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        [Display(Name = " Title"), Required(ErrorMessage = "Title name is required.")]
        public string Title { get; set; }
        [Display(Name = " Comment"), Required(ErrorMessage = "Comment is required.")]
        public string Comment { get; set; }
        [Display(Name = " Rating"), Required(ErrorMessage = "Rating is required.")]
        public int Ratings { get; set; }
        public int VideoId { get; set; }
        public Video Video { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public bool IsDeleted { get; set; }
        [Display(Name = "Date Deleted")]
        public DateTime? DateDeleted { get; set; }
    }
}
