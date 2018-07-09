using MrE.Models.Entities;
using MrE.Repository.Abstractions;
using MrE.Services.Abstractions;
using MrEOnline.Services.Abstractions;

namespace MrE.Services
{
    public class GenreService : Service<Genre>
    {
        public GenreService(IRepository<Genre> repository, IValidationService<Genre> validationService)
           : base(repository, validationService)
        {
        }
    }
}
