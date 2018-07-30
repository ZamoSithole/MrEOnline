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
    public class RentalService : Service<Rental, int> {
        public RentalService(IRepository<Rental, int> repository, IValidationService<Rental> RentalValidationService)
            : base(repository, RentalValidationService) { }

        public override Rental Insert(Rental item) {
            item.IsCheckedOut = true;
            item.StatusId = 1;            
            return base.Insert(item);
        }
        public override Rental Update(Rental item) {
            item.IsCheckedOut = true;
            item.StatusId = 2;
            return base.Insert(item);
        }
    }
}
