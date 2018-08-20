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
    public class DislikeLikeService : Service<DislikeLike, int> {
        public DislikeLikeService(IRepository<DislikeLike, int> repository, IValidationService<DislikeLike> validationService) 
            : base(repository, validationService) {
        }
        //public override DislikeLike Insert(DislikeLike item) {
        //    item.IsDislike = true;
        //    return base.Insert(item);
        //}
    }
}
