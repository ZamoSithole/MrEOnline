using MrE.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrE.Models.Entities {
    [Table("Rented")]
    public class CheckOut : IBaseEntity<int>, ICreatable, IUpdatable {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public int VideoId { get; set;}
        public Video Video { get; set; }
        public int MemberID { get; set; }
        public Member Member { get; set; }
        public bool IsCheckedOut { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
