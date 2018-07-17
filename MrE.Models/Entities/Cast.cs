﻿using MrE.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrE.Models.Entities
{
    [Table("Cast")]
    public class Cast : IBaseEntity<int>, ICreatable, IDeletable, IUpdatable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        [Display(Name = "Cast")]
        public string CastMembers
        {
            get
            {
                return $"{Title?.Name ?? string.Empty}{FirstName}{LastName}";
            }

        }
        [Display(Name = "FirstName"), Required(ErrorMessage = "Fill out Name")]
        public string FirstName {get;set;}
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        public int TitleId { get; set; }
        public Title Title { get; set; }
        public int VideoId { get; set; }
        public Video Video { get; set; }
        public bool IsDeleted { get; set; }

        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Date Updated")]
        public DateTime? DateUpdated { get; set; }

        [Display(Name = "Date Deleted")]
        public DateTime? DateDeleted { get; set; }
    }
}