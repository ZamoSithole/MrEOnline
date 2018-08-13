using MrE.Models.Abstractions;
using MrEOnline.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrE.Models.Entities {
    [Table("Reviews")]
    public class Rating : IBaseEntity<int>, ICreatable, IDeletable,IUpdatable {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
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
