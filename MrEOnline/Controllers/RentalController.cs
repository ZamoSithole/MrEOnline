using Microsoft.AspNet.Identity.Owin;
using MrE.Models.Entities;
using MrE.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MrEOnline.Controllers {
    //[Route("api/rentals")]
    //public class RentalApiController : Controller {
    //    public IService<Rental> PrimaryService { get; set; }
    //    public RentalApiController(IService<Rental> rentalService) {
    //        PrimaryService = rentalService;
    //    }

    //    [HttpPost]
    //    public ActionResult Post()
    //}

    public class RentalController : BaseController<Rental, int> {
        public IService<Video, int> VideoService { get; set; }
        public IService<Genre, int> GenreService { get; set; }
        public IService<Title, int> TitleService { get; set; }
        public IService<Rental, int> RentalService { get; set; }
        private UserManager _userManager { get; set; }
        public UserManager UserManager {
            get {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<UserManager>();
            }
            private set {
                _userManager = value;
            }
        }
        public RentalController(IService<Video, int> videoService, IService<Genre, int> genreService, IService<Rental, int> rentalService)
            : base(rentalService) {
            GenreService = genreService;
            RentalService = rentalService;
        }
        //// GET: Rental
        //public ActionResult Create() {
        //    return View();
        //}
        [HttpPost]
        public ActionResult Post(IEnumerable<Rental> rentals) {
            try {
                foreach(var item in rentals)
                 RentalService.Insert(item);
            } catch (Exception) {

                throw;
            }
            return Json(rentals);
        }
        public virtual async Task<ActionResult> Confirm() {
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            var userId = user.Id;
            var dataQuery = PrimaryService.Get().Where(e => e.UserId == userId);
            TransformQuery(ref dataQuery);
            if (dataQuery.Count() < 1)
                return new HttpNotFoundResult();
            return View("Confirm",dataQuery);
        }
        public async Task<ActionResult> DoConfirm() {
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            var userId = user.Id;
            var dataQuery = await PrimaryService.Get().Where(e => e.UserId == userId).ToListAsync();
            try {
                foreach (var item in dataQuery) {
                    item.StatusId = 2;
                    PrimaryService.Update(item);
                }
            } catch (Exception exception) {

                throw;
            }
            return View("ThankYou");
        }

        protected override async Task SetupSelectList() {
            ViewBag.GenreSelectList = new SelectList(await GenreService.Get().ToListAsync(), "Id", "Name");
        }

        protected override void TransformQuery(ref IQueryable<Rental> dataQuery) {
            dataQuery = dataQuery.Include(m => m.User)
                .Include(m => m.Status)
                .Include(m => m.Video);
        }
    }
}