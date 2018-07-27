using MrE.Models.Entities;
using MrE.Repository.Abstractions;
using MrE.Services.Abstractions;
using MrEOnline.Services.Abstractions;

namespace MrE.Services
{
    public class StatusService : Service<Status, int>
    {
        public StatusService(IRepository<Status, int> repository, IValidationService<Status> validationService) 
            : base(repository, validationService)
        {
        }
    }
}
