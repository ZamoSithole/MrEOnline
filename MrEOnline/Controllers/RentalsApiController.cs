using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MrE.Models.Entities;
using MrE.Services.Abstractions;
using Microsoft.AspNet.Identity.Owin;
using System.Web;

namespace MrEOnline.Controllers {
    [Authorize]
    public class RentalsApiController : ApiController
    {
        protected IService<Rental, int> PrimaryService { get; set; }
        private UserManager _userManager { get; set; }
        public UserManager UserManager {
            get {
                return _userManager ?? HttpContext.Current.Request.GetOwinContext().GetUserManager<UserManager>();
            }
            private set {
                _userManager = value;
            }
        }

        public RentalsApiController() { }
        public RentalsApiController(IService<Rental, int> rentalService) {
            PrimaryService = rentalService;
        }
        [Authorize]
        // POST: api/RentalsApi
        [ResponseType(typeof(Rental))]
        public async Task<IHttpActionResult> PostRental(Rental rental) {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            rental.UserId = user.Id;
            PrimaryService.Insert(rental);
            return Ok();
        }
    }
}