using MrE.Models.Entities;
using MrE.Repository.Abstractions;
using MrE.Services.Abstractions;
using MrEOnline.Services.Abstractions;

namespace MrE.Services {
    public class CastService : Service<Cast, int>
    {

        public CastService(IRepository<Cast, int> repository, IValidationService<Cast> castValidationService)
            : base(repository, castValidationService) { }

    }
}
