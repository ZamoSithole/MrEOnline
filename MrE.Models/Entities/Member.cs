using MrE.Models.Abstractions;
using MrEOnline.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MrE.Models.Entities
{
    [Table("Member")]
    public class Member : IBaseEntity<int>, IPerson, ICreatable, IDeletable, IUpdatable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        [Display(Name = "FirstName"), Required(ErrorMessage = "Fill out Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public int ContactID { get; set; }
        public Contact Contact { get; set; }

        public int StatusID { get; set; }
        public Status Status { get; set; }
       
        public bool IsDeleted { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; }
        public DateTime? DateUpdated { get; set; }


    }
}
