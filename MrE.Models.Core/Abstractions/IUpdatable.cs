using System;
using System.Collections.Generic;
using System.Text;
using MrE.Models.Entities;

namespace MrE.Models.Abstractions
{
    public interface IUpdatable
    {
        DateTime? DateUpdated { get; set; }
        int? UserUpdateID { get; set; }
        User UserUpdated { get; set; }
    }
}
