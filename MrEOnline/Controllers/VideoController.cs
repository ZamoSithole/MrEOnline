using MrE.Models.Entities;
using MrE.Services.Abstractions;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MrEOnline.Controllers {
    [Authorize(Roles ="Administrator")]
    public class VideoController : BaseController<Video, int>
    {
        public IService<Genre, int> GenreService { get; set; }
        public IService<Title, int> TitleService { get; set; }
        public IService<Video, int> VideoService { get; set; }
        public VideoController(IService<Video, int> videoService, IService<Genre, int> genreService)
            :base (videoService)
        {
            GenreService = genreService;
            VideoService = videoService;
        }

        [AllowAnonymous]
        public async Task<ActionResult> Catalog() {
            var videos =  PrimaryService.Get();
            TransformQuery(ref videos);
            return View(videos);
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