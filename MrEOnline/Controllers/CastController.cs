using MrE.Models.Entities;
using MrE.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MrEOnline.Controllers
{
    public class CastController : BaseController<Cast, int>
    {
        public IService<Title, int> TitleService { get; set; }
        public CastController(IService<Cast, int> castService, IService<Title, int> titleService)
            : base(castService) {
            TitleService = titleService;
        }

        public override async Task<ActionResult> CreateFor(int videoId) {
            await SetupSelectList();
            return View("CreateFor", new Cast { VideoId = videoId });
        }
        public ActionResult IndexPartial(int videoId) {

            var dataQuery = PrimaryService.Get().Where(e => e.VideoId == videoId);
            TransformQuery(ref dataQuery);
            if (dataQuery.Count() < 1)
                return new HttpNotFoundResult();
            return PartialView("_IndexPartial", dataQuery);
        }
        public ActionResult CastCheckoutIndexPartial(int videoId) {
            var dataQuery = PrimaryService.Get().Where(e => e.VideoId == videoId);
            TransformQuery(ref dataQuery);
            if (dataQuery.Count() < 1)
                return new HttpNotFoundResult();
            return PartialView("_CastCheckoutIndexPartial", dataQuery);
        }
        protected override void TransformQuery(ref IQueryable<Cast> dataQuery) {
            dataQuery = dataQuery.Include(m => m.Title);
            //dataQuery = dataQuery.Include(m => m.Video);
            
        }

        protected override async Task SetupSelectList() {
            ViewBag.TitleSelectList = new SelectList(await TitleService.Get().ToListAsync(), "Id", "Name");
        }
    }
}
