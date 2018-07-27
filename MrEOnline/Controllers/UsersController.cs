using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MrE.Models.Entities;
using MrE.Services.Abstractions;
using MrEOnline.Models;

namespace MrEOnline.Controllers {

    [Authorize(Roles = "Administrator")]
    public class UsersController : BaseViewController<User, UserViewModel, string> {
        public UsersController(IService<User, string> primaryService) : base(primaryService) {
        }

        public override async Task<ActionResult> Index() {
            return View((await PrimaryService.Get().ToListAsync()).Select(e => new UserViewModel(e)));
        }

        protected override void TransformQuery(ref IQueryable<User> dataQuery) {
            dataQuery = dataQuery.Include(e => e.Roles);
        }

        protected async override Task SetupSelectList() {
            //ViewBag.RoleSelectList = new SelectList(await RoleService.Get().ToListAsync(), "Id", "Name");
        }

        protected override UserViewModel CreateViewObject(User item) {
            return new UserViewModel(item);
        }

        protected override void CreateObjectFromViewModel(UserViewModel item, ref User entity) {
            entity.FirstName = item.FirstName;
            entity.Surname = item.Surname;
            entity.PhoneNumber = item.PhoneNumber;
        }
    }
}