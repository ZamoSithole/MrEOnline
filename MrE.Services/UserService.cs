using MrE.Models.Entities;
using MrE.Repository.Abstractions;
using MrE.Services.Abstractions;
using MrEOnline.Models;
using MrEOnline.Services.Abstractions;

namespace MrE.Services {

    public class UserService : Service<User, string> {
        public UserService(IRepository<User, string> repository, IValidationService<User> validationService) : base(repository, validationService) {
        }
    }
}
