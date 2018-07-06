using System;
using MrEOnline.Models;

namespace MrE.Models.Abstractions
{
    public interface IUpdatable
    {
        DateTime? DateUpdated { get; set; }
        //string UpdatedById { get; set; }
        //User UpdatedBy { get; set; }
    }
}
