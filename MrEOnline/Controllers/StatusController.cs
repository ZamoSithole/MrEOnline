using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using System.Web.Mvc;
using MrE.Models;
using MrE.Models.Entities;
using MrE.Services.Abstractions;

namespace MrEOnline.Controllers
{
    public class StatusController : BaseController<Status>
    {
        public IService<Status> StatusService { get; set; }

        public StatusController(IService<Status> statusService)
            :base(statusService)
        {
        }
        protected override void TransformQuery(ref IQueryable<Status> dataQuery)
        {            
        }

        protected override async Task SetupSelectList()
        {
            await Task.Delay(0);
        }
    }
}