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
    [Table("Critics")]
    public class DislikeLike : IBaseEntity<int>, ICreatable {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public int videoId { get; set; }
        public Video Video { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public bool IsLike { get; set; }
        public bool IsDislike { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
