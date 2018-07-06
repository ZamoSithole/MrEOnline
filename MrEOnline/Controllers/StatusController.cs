using System;
using System.Web.ModelBinding;
using System.Web.Mvc;
using MrE.Models;
using MrE.Models.Entities;
using MrE.Services.Abstractions;

namespace MrEOnline.Controllers
{
    public class StatusController : Controller
    {
        public IService<Status> StatusService { get; set; }

        public StatusController(IService<Status> statusService)
        {
            StatusService = statusService;
        }

        public ActionResult Index()
        {
            var statuses = StatusService.Get();

            return View(statuses);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Status status)
        {
            if (!ModelState.IsValid) return View(status);
            try
            {
                StatusService.Insert(status);

                return RedirectToAction("index");
            }
            catch (Exception exception)
            {
                if (exception is ValidationException)
                    ModelState.AddModelError("", exception.ToString());
                else
                    ModelState.AddModelError("", "There was an error processing your request, please try again later!!");
            }

            return View(status);
        }

        public ActionResult Edit(int id)
        {
            var status = StatusService.GetByKey(id);
            if (status == null) return HttpNotFound("Could not find the status you are looking for.");
            return View(status);
        }

        [HttpPost]
        public ActionResult Edit(Status status)
        {
            if (!ModelState.IsValid) return View(status);            
            try
            {
                StatusService.Update(status);

                return RedirectToAction("index");
            }
            catch (Exception exception)
            {
                if (exception is ValidationException)
                    ModelState.AddModelError("", exception.ToString());
                else
                    ModelState.AddModelError("", "There was an error processing your request, please try again later!!");
            }

            return View(status);
        }

        public ActionResult Details(int id)
        {
            var status = StatusService.GetByKey(id);
            if (status == null) return HttpNotFound("Status cannot be found.");

            return View(status);
        }

        public ActionResult Delete(int id)
        {
            var status = StatusService.GetByKey(id);
            if (status == null) return HttpNotFound("Status cannot be found.");

            return View(status);
        }

        [HttpPost]
        public ActionResult Delete(Status status, int id)
        {
            var existing = StatusService.GetByKey(id);
            if (existing == null) return HttpNotFound("Cannot find the item to be deleted.");
            try
            {
                StatusService.Delete(existing);
                return RedirectToAction("index");
            }
            catch (Exception exception)
            {
                if (exception is ValidationException)
                    ModelState.AddModelError("", exception.ToString());
                else
                    ModelState.AddModelError("", "There was an error processing your request, please try again later!!");
            }

            return View(status);
        }
    }
}