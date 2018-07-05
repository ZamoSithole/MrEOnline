using MrE.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace MrE.Models.Entities
{
    [Table("Users")]
    public class User : IPerson
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    }
}
