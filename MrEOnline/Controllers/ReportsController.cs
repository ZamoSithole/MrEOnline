using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Web.Mvc;
using System.Web.Mvc;

namespace MrEOnline.Controllers {
    [Authorize]
    public class ReportsController : Controller {
        protected Rentals Rentals { get; set; }
        protected IService<>
        // GET: Reports
        public ActionResult Index() {
            return View();
        }
    }
}