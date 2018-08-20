using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity.Owin;
using MrE.Models.Entities;
using MrE.Repository;
using MrE.Services.Abstractions;

namespace MrEOnline.Controllers
{
    public class DislikeLikeApiController : ApiController
    {
        protected IService<DislikeLike, int> PrimaryService { get; set; }
        private UserManager _userManager { get; set; }
        public UserManager UserManager {
            get {
                return _userManager ?? HttpContext.Current.Request.GetOwinContext().GetUserManager<UserManager>();
            }
            private set {
                _userManager = value;
            }
        }
        public DislikeLikeApiController() { }
        public DislikeLikeApiController(IService<DislikeLike, int> dislikeLikeService) {
            PrimaryService = dislikeLikeService;
        }
        //// GET: api/DislikeLikeApi
        //public IQueryable<DislikeLike> GetDislikeLikes()
        //{
        //    return db.DislikeLikes;
        //}

        //// GET: api/DislikeLikeApi/5
        //[ResponseType(typeof(DislikeLike))]
        //public IHttpActionResult GetDislikeLike(int id)
        //{
        //    DislikeLike dislikeLike = db.DislikeLikes.Find(id);
        //    if (dislikeLike == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(dislikeLike);
        //}

        // PUT: api/DislikeLikeApi/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutDislikeLike(int id, DislikeLike dislikeLike)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != dislikeLike.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(dislikeLike).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DislikeLikeExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // POST: api/DislikeLikeApi
        [ResponseType(typeof(DislikeLike))]
        public async Task<IHttpActionResult> PostDislikeLike(DislikeLike dislikeLike)
        {
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            dislikeLike.UserId = user.Id;
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try {
                PrimaryService.Insert(dislikeLike);
            } catch (Exception exception) {

                throw;
            }

            return Ok();
        }

        //// DELETE: api/DislikeLikeApi/5
        //[ResponseType(typeof(DislikeLike))]
        //public IHttpActionResult DeleteDislikeLike(int id)
        //{
        //    DislikeLike dislikeLike = db.DislikeLikes.Find(id);
        //    if (dislikeLike == null)
        //    {
        //        return NotFound();
        //    }

        //    db.DislikeLikes.Remove(dislikeLike);
        //    db.SaveChanges();

        //    return Ok(dislikeLike);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool DislikeLikeExists(int id)
        //{
        //    return db.DislikeLikes.Count(e => e.Id == id) > 0;
        //}
    }
}