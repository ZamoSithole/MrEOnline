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
using System.ComponentModel.DataAnnotations;
using MrEOnline.Models;

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
            var viewMessage = new ViewMessage();
            item.UserId = userId;

            if (!ModelState.IsValid) return View(item);
            try {

                PrimaryService.Insert(item);
                viewMessage.Message = "Successfully saved your changes.";
                viewMessage.Type = ViewMessageType.Success;

            } catch (Exception exception) {

                if (exception is ValidationException)
                    ModelState.AddModelError("", exception.ToString());
                else
                    ModelState.AddModelError("", "There was an error processing your request, please try again later!!");
            }
            return this.RedirectToAction("Catalog", "Video");
        }
        public ActionResult ReviewsPartial(int? videoId) {
            var dataQuery = PrimaryService.Get().Where(e => e.VideoId == videoId);
            TransformQuery(ref dataQuery);
            if (dataQuery.Count() < 1)
                return new HttpNotFoundResult();
            return View("_ReviewsPartial", dataQuery);
        }
        // GET: Rating
        public ActionResult Index() {
            return View();
        }

        protected override void TransformQuery(ref IQueryable<Rating> dataQuery) {
            dataQuery = dataQuery.Include(m => m.User);
        }

        protected override async Task SetupSelectList() {
            await Task.Delay(0);
        }
    }
}