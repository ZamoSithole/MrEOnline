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
    public class RentalController : BaseController<Video> {
        public IService<Video> VideoService { get; set; }
        public IService<Genre> GenreService { get; set; }
        public IService<Title> TitleService { get; set; }
        public RentalController(IService<Video> videoService, IService<Genre> genreService)
            : base(videoService) {
            GenreService = genreService;
          
        }
        //// GET: Rental
        //public ActionResult Details() {
        //    return View();
        //}

        protected override async Task SetupSelectList() {
            ViewBag.GenreSelectList = new SelectList(await GenreService.Get().ToListAsync(), "Id", "Name");
        }

        protected override void TransformQuery(ref IQueryable<Video> dataQuery) {
            dataQuery = dataQuery.Include(m => m.Genre);
        }
    }
}