using System;
using System.Collections.Generic;
using System.Text;
using MrE.Models.Entities;
using MrEOnline.Models;

namespace MrE.Models.Abstractions
{

    public interface ICreatable
    {
        //User CreatedBy { get; set; }
        //string CreatedById { get; set; }
        DateTime DateCreated { get; set; }
    }
}
