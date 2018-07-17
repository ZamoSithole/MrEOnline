using MrE.Models.Entities;
using MrE.Services;
using MrE.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MrEOnline.Controllers
{

    public class VideoController : BaseController<Video>
    {
        public IService<Genre> GenreService { get; set; }
        public IService<Title> TitleService { get; set; }
        public IService<Video> VideoService { get; set; }
        public VideoController(IService<Video> videoService, IService<Genre> genreService)
            :base (videoService)
        {
            GenreService = genreService;
            VideoService = videoService;
        }
        public ActionResult UploadVideo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadVideo(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Uploaded"), _FileName);
                    file.SaveAs(_path);
                }
                return View();
            }
            catch (Exception)
            {

                ViewBag.Message = "Failed";
                return View();
            }                       
        }
        
        protected override void TransformQuery(ref IQueryable<Video> dataQuery)
        {
            dataQuery = dataQuery.Include(m => m.Genre);
        }

        protected override async Task SetupSelectList()
        {
            ViewBag.GenreSelectList = new SelectList(await GenreService.Get().ToListAsync(), "Id", "Name");
        }
    }
}