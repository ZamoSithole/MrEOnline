using System;
using MrEOnline.Models;

namespace MrE.Models.Abstractions
{
    public interface IDeletable
    {
        bool IsDeleted { get; set; }
        DateTime? DateDeleted { get; set; }
        //string DeletedById { get; set; }
        //User DeletedBy { get; set; }
    }
}
