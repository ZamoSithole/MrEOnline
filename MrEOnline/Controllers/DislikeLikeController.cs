using Microsoft.AspNet.Identity.Owin;
using MrE.Models.Entities;
using MrE.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MrEOnline.Controllers {
   
    public class DislikeLikeController : BaseController<DislikeLike, int> {
        public IService<DislikeLike, int> DislikeLikeService { get; set; }
        private UserManager _userManager { get; set; }
        public UserManager UserManager {
            get {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<UserManager>();
            }
            private set {
                _userManager = value;
            }
        }
        public DislikeLikeController(IService<DislikeLike, int> dislikeLikeService)
             : base(dislikeLikeService) {

        }
        // GET: DislikeLike
        public ActionResult Index() {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CriticsAdd(DislikeLike item) {
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            item.UserId = user.Id;
            PrimaryService.Insert(item);
            return View("ThankYou");
        }

        protected override void TransformQuery(ref IQueryable<DislikeLike> dataQuery) {
            
        }

        protected async override Task SetupSelectList() {
            
        }
    }
}