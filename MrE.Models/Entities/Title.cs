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
    [Table("Titles")]
    public class Title : IBaseEntity<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Cast> Casts { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
