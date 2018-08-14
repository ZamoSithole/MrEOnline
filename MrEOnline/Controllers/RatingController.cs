using Microsoft.AspNet.Identity.Owin;
using MrE.Models.Entities;
using MrE.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace MrEOnline.Controllers {
    [Authorize]
    public class RatingController : BaseController<Rating, int> {
        private UserManager _userManager { get; set; }
        public UserManager UserManager {
            get {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<UserManager>();
            }
            private set {
                _userManager = value;
            }
        }
        public RatingController(IService<Rating, int> ratingService)
            : base(ratingService) {
        }

        public async Task<ActionResult> CreatePartial(int? videoId) {
            return View("_CreatePartial");
        }

        [HttpPost]
        public async Task<ActionResult> CreatePartial(Rating item) {
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            var userId = user.Id;
            item.UserId = userId;
            if (!ModelState.IsValid) return View(item);
            try {

                PrimaryService.Insert(item);

            } catch (Exception exception) {

                throw;
            }
            return this.RedirectToAction("Catalog", "Video");
        }
        // GET: Rating
        public ActionResult Index() {
            return View();
        }

        protected override void TransformQuery(ref IQueryable<Rating> dataQuery) {


        }

        protected override async Task SetupSelectList() {
            await Task.Delay(0);
        }
    }
}