using System;
using System.Collections.Generic;
using System.Text;
using MrE.Models.Entities;

namespace MrE.Models.Abstractions
{
    public interface IPerson
    {
        string FirstName { get; set; }
        string Surname { get; set; }
        Contact Contact { get; set; }
        Status Status { get; set; }
    }
}
