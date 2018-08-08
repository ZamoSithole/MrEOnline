using DevExpress.Web.Mvc;
using MrE.Models.Entities;
using MrE.Services.Abstractions;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MrEOnline.Controllers {
    [Authorize]
    public class OverDueReportController : Controller {
        protected OverDueVideos OverDueVideosReport { get; set; }
        protected IService<Rental, int> RentalService { get; set; }
        public OverDueReportController(IService<Rental, int> rentalService) {
            RentalService = rentalService;
            OverDueVideosReport = new OverDueVideos();
        }
        // GET: OverDueReport
        public ActionResult Index() {
            return View();
        }

        public async Task<ActionResult> OverDueVideosViewerPartial() {
            OverDueVideosReport.DataSource = (await RentalService.Get()             
                .Include(c => c.Status)
                .Include(c => c.User)
                .Include(c => c.Video).ToListAsync())
            .Where(e => e.OverDueDays > 0);

            return PartialView("_OverDueVideosViewerPartial", OverDueVideosReport);
        }

        public async Task<ActionResult> OverDueVideosViewerPartialExport() {
            OverDueVideosReport.DataSource = (await RentalService.Get()
                .Include(c => c.Status)
                .Include(c => c.User)
                .Include(c => c.Video).ToListAsync())
            .Where(e => e.OverDueDays > 0);

            return DocumentViewerExtension.ExportTo(OverDueVideosReport, Request);
        }
    }
}