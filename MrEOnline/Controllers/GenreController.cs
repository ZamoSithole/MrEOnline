using MrE.Models.Entities;
using MrE.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MrEOnline.Controllers
{
    public class GenreController : BaseController<Genre, int>
    {
        protected IService<Genre, int> GenreService { get; set; }

        public GenreController(IService<Genre, int> genreService)
            :base (genreService)
        {
            GenreService = genreService;
        }

        // GET: Genre
        [HttpGet]
        public JsonResult GetGenres()
        {
            return Json(GenreService.Get().ToList(), JsonRequestBehavior.AllowGet);
        }

        protected override void TransformQuery(ref IQueryable<Genre> dataQuery)
        {
        }

        protected override async Task SetupSelectList()
        {
            await Task.Delay(0);
        }
    }
}