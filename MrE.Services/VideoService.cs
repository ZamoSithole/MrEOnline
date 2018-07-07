using MrE.Models.Entities;
using MrE.Repository.Abstractions;
using MrE.Services.Abstractions;
using MrEOnline.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrE.Services
{
    public class VideoService : Service<Video>
    {
        public VideoService(IRepository<Video> repository, IValidationService<Video> validationService) 
            : base(repository, validationService)
        {
        }
    }
}
