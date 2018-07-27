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
    public class TitleService : Service<Title, int>
    {
        public TitleService(IRepository<Title, int> repository, IValidationService<Title> titleValidationService)
            : base(repository, titleValidationService) { }

      
    }
}
