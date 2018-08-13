using MrE.Models.Entities;
using MrE.Repository.Abstractions;
using MrE.Services.Abstractions;
using MrEOnline.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrE.Services {
    public class RatingService : Service<Rating, int> {
        public RatingService(IRepository<Rating, int> repository, IValidationService<Rating> validationService) 
            : base(repository, validationService) {
        }
    }
}
