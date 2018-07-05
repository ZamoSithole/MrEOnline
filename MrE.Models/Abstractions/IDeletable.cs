using System;
using System.Collections.Generic;
using System.Text;
using MrE.Models.Entities;

namespace MrE.Models.Abstractions
{
    public interface IDeletable
    {
        bool IsDeleted { get; set; }
        DateTime? DateDeleted { get; set; }
        int? UserDeletedID { get; set; }
        User UserDeleted { get; set; }
    }
}
