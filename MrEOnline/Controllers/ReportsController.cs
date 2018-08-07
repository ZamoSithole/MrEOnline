using DevExpress.Web.Mvc;
using MrE.Models.Entities;
using System.Web.Mvc;
using MrE.Services.Abstractions;
using System.Data.Entity;
using System.Linq;

namespace MrEOnline.Controllers {
    [Authorize]
    public class ReportsController : Controller {
        protected Rentals RentalsReport { get; set; }
        protected IService<Rental, int> RentalService { get; set; }

        public ReportsController(IService<Rental, int> rentalService) {
            RentalService = rentalService;
            RentalsReport = new Rentals();
        }
        // GET: Reports
        public ActionResult Index() {
            return View();
        }



        public ActionResult RentalReportViewerPartial() {
            RentalsReport.DataSource = RentalService.Get()
                .Include(c => c.Status)
                .Include(c => c.User)
                .Include(c => c.Video)
                .ToList();
            return PartialView("_RentalReportViewerPartial", RentalsReport);
        }

        public ActionResult RentalReportViewerPartialExport() {
            RentalsReport.DataSource = RentalService.Get()
                .Include(c => c.Status)
                .Include(c => c.User)
                .Include(c => c.Video)
                .ToList();
            return DocumentViewerExtension.ExportTo(RentalsReport, Request);
        }
    }
}