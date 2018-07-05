using System;
using System.Collections.Generic;
using System.Text;
using MrE.Models.Entities;

namespace MrE.Models.Abstractions
{
    public interface ICreatable
    {
        User UserCreated { get; set; }
        int UserCreatedID { get; set; }
        DateTime DateCreated { get; set; }
    }
}
