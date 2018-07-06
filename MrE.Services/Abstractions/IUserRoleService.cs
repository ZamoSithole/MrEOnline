using MrE.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrE.Services.Abstractions
{
    public interface IUserRoleService
    {
        IQueryable<UserRoles> GetByUserName(string userName);

        IList<string> GetUserRoles(string userName);
    }
}
