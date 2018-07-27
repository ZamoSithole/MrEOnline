using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MrE.Models.Entities;
using MrE.Repository;
using MrE.Services.Abstractions;

namespace MrEOnline.Controllers
{
    public class RentalsApiController : ApiController
    {
        protected IService<Rental, int> PrimaryService { get; set; }

        public RentalsApiController() { }
        public RentalsApiController(IService<Rental, int> rentalService) {
            PrimaryService = rentalService;
        }       
                
        // POST: api/RentalsApi
        [ResponseType(typeof(Rental))]
        public IHttpActionResult PostRental(Rental rental)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PrimaryService.Insert(rental);
            return Ok();
        }        
    }
}