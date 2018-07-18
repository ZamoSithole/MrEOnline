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
    public class CastController : BaseController<Cast>
    {
        public IService<Title> TitleService { get; set; }
        public CastController(IService<Cast> castService, IService<Title> titleService)
            : base(castService) {
            TitleService = titleService;
        }

        public override async Task<ActionResult> Create(int? videoId = null) {
            await SetupSelectList();
            if (videoId.HasValue)
                return View(new Cast { VideoId = videoId.Value });
            return View();
        }
        public ActionResult IndexPartial(int videoId) {

            var dataQuery = PrimaryService.Get().Where(e => e.VideoId == videoId);

            if (dataQuery.Count() < 1)
                return new HttpNotFoundResult();
            return PartialView("_IndexPartial", dataQuery);
        }
        public ActionResult CastCheckoutIndexPartial(int videoId) {
            var dataQuery = PrimaryService.Get().Where(e => e.VideoId == videoId);
            if (dataQuery.Count() < 1)
                return new HttpNotFoundResult();
            return PartialView("_CastCheckoutIndexPartial", dataQuery);
        }
        protected override void TransformQuery(ref IQueryable<Cast> dataQuery) {
            dataQuery = dataQuery.Include(m => m.Video);
            dataQuery = dataQuery.Include(m => m.Title);
        }

        protected override async Task SetupSelectList() {
            ViewBag.TitleSelectList = new SelectList(await TitleService.Get().ToListAsync(), "Id", "Name");
        }
    }
}
