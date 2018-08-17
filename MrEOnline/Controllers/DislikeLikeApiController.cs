using Microsoft.AspNet.Identity.Owin;
using MrE.Models.Entities;
using MrE.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace MrEOnline.Controllers
{
    [Authorize]
    public class DislikeLikeApiController : ApiController {
        protected IService<DislikeLike, int> PrimaryService { get; set; }
        private UserManager _userManager { get; set; }
        public UserManager UserManager {
            get {
                return _userManager ?? HttpContext.Current.Request.GetOwinContext().GetUserManager<UserManager>();
            } 
            private set {
                _userManager = value;
            }
        }

        public DislikeLikeApiController() { }
        public DislikeLikeApiController(IService<DislikeLike, int> dislikeLikeService) {
            PrimaryService = dislikeLikeService;
        }
        [ResponseType(typeof(DislikeLike))]
        public async Task<IHttpActionResult> PostDislikeLike(DislikeLike dislikeLike) {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            dislikeLike.UserId = user.Id;
            PrimaryService.Insert(dislikeLike);
            return Ok();
        }
    }
}
