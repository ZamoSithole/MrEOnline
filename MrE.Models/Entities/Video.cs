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

        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public string RentalPrice { get; set; }
        DateTime ICreatable.DateCreated { get; set; }
        bool IDeletable.IsDeleted { get ; set; }
        DateTime? IDeletable.DateDeleted { get; set; }
        DateTime? IUpdatable.DateUpdated { get; set ; }
    }
}
