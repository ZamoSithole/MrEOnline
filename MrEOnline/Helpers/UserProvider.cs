using MrE.Repository.Abstractions;
using System.Web;

namespace MrEOnline.Web.Helpers {
    public class UserProvider : IUserProvider {
        protected HttpContextBase Context { get; set; }

        public UserProvider(HttpContextBase httpContext) {
            Context = httpContext;
        }

        public string GetUserName() {
            return Context?.User.Identity.Name ?? string.Empty;
        }
    }
}
