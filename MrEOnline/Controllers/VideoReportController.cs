using DevExpress.Web.Mvc;
using MrE.Models.Entities;
using MrE.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MrEOnline.Controllers {
    [Authorize]
    public class VideoReportController : Controller {
        protected VideosAdded VideoAddedReport { get; set; }
        protected IService<Video, int> VideoService { get; set; }

        public VideoReportController(IService<Video, int> videoService) {
            VideoService = videoService;
            VideoAddedReport = new VideosAdded();
        }
        // GET: VideoReport
        public ActionResult Index() {
            return View();
        }

        

        public ActionResult VideosAddedViewerPartial() {
            VideoAddedReport.DataSource = VideoService.Get()
                .Include(c => c.Genre)
                .ToList();

            return PartialView("_VideosAddedViewerPartial", VideoAddedReport);
        }

        public ActionResult VideosAddedViewerPartialExport() {
            VideoAddedReport.DataSource = VideoService.Get()
               .Include(c => c.Genre)
               .ToList();
            return DocumentViewerExtension.ExportTo(VideoAddedReport, Request);
        }
    }
}