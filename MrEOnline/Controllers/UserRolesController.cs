using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MrE.Models.Entities;
using MrE.Services.Abstractions;
using MrEOnline.Models;

namespace MrEOnline.Controllers {
    public class UserRolesController : Controller {
        private RoleManager _roleManager { get; set; }
        private UserManager _userManager { get; set; }
        public UserManager UserManager {
            get {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<UserManager>();
            }
            private set {
                _userManager = value;
            }
        }

        public RoleManager RoleService {
            get {
                return _roleManager ?? HttpContext.GetOwinContext().Get<RoleManager>();
            }
            private set {
                _roleManager = value;
            }
        }
        public UserRolesController() { }
        public UserRolesController(RoleManager roleService, UserManager userManager) {
            RoleService = roleService;
            UserManager = userManager;
        }

        public async Task<ActionResult> Index(string userId) {
            var user = UserManager.FindById(userId);
            var data = from u in user.Roles
                       join r in RoleService.Roles on u.RoleId equals r.Id
                       select new RoleViewModel(r);

            return View(data);
        }

        public async Task<ActionResult> Create(string userId) {
            var uRole = new UserRoleViewModel() {
                UserId = userId
            };

            ViewBag.RoleSelectList = new SelectList(await RoleService.Roles.ToListAsync(), "Id", "Name");

            return View(uRole);
        }
    }
}