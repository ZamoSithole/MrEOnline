using MrE.Models.Entities;
using MrE.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MrEOnline.Controllers
{
    public class GenreController : Controller
    {
        protected IService<Genre> GenreService { get; set; }

        public GenreController(IService<Genre> genreService)
        {
            GenreService = genreService;
        }
        // GET: Genre
        public ActionResult Index()
        {
            var genres = GenreService.Get();
            return View(genres);
        }
        [HttpGet]
        public JsonResult GetGenres()
        {
            return Json(GenreService.Get().ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}