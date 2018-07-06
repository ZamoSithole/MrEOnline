using MrE.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrE.Services.Abstractions
{
    public interface IStatus
    {
        IQueryable<Status> GetByStatusType(string StatusType);
    }
}
