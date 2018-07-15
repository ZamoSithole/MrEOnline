using MrE.Models.Entities;
using MrE.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MrEOnline.Controllers
{
    public class CastController : BaseController<Cast>
    {
        
        public IService<Cast> CastService { get; set; }
        public IService<Title> TitleService { get; set; }
        public CastController(IService<Cast> castService, IService<Title> titleService)
            : base(castService)
        {
            TitleService = titleService;
        }
        // GET: Cast
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: Cast/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Cast/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Cast/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Cast/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Cast/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Cast/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Cast/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        protected override void TransformQuery(ref IQueryable<Cast> dataQuery)
        {
            
        }

        protected override async Task SetupSelectList()
        {
            ViewBag.TitleSelectList = new SelectList(await TitleService.Get().ToListAsync(), "Id", "Name");
        }
    }
}
