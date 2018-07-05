using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MrE.Models.Entities
{
    [Table("")]
    public class Contact : ICreatable, IDeletable, IUpdatable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    }
}
