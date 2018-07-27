using MrE.Models.Entities;
using MrE.Repository.Abstractions;
using MrE.Services.Abstractions;
using MrEOnline.Services.Abstractions;

namespace MrE.Services {

    public class VideoService : Service<Video, int>
    {
        public VideoService(IRepository<Video, int> repository, IValidationService<Video> validationService) 
            : base(repository, validationService)
        {
        }
    }
}
