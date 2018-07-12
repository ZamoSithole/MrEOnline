using MrE.Models;
using MrE.Models.Abstractions;
using MrE.Services.Abstractions;
using MrEOnline.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MrEOnline.Controllers
{
    public abstract class BaseController<T> : Controller
        where T : class
    {
        public IService<T> PrimaryService { get; set; }

        public BaseController(IService<T> primaryService)
        {
            PrimaryService = primaryService;
        }

        public virtual async Task<ActionResult> Index()
        {
            await SetupSelectList();
            var dataQuery = PrimaryService.Get();

            TransformQuery(ref dataQuery);

            return View(dataQuery);
        }
        public async Task<ActionResult> Details(int id)
        {
            await SetupSelectList();
            var dataQuery = PrimaryService.GetByKey(id);
            if (dataQuery == null) return HttpNotFound("Video information cannot be found.");

            return View(dataQuery);

        }
        public virtual async Task<ActionResult> Create()
        {
            await SetupSelectList();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(T item)
        {
            await SetupSelectList();
            var viewMessage = new ViewMessage();
            if (!ModelState.IsValid) return View(item);
            try
            {
                PrimaryService.Insert(item);
                viewMessage.Message = "Successfully saved your changes.";
                viewMessage.Type = ViewMessageType.Success;

                return RedirectToAction("Edit", new {
                    id = (item as IBaseEntity<int>).Id,
                    message =viewMessage.Message,
                    messageType = viewMessage.Type
                });
            }
            catch (Exception exception)
            {
               
                viewMessage.Type = ViewMessageType.Success;
                if (exception is ValidationException)
                    viewMessage.Message = exception.ToString();
                else
                    viewMessage.Message = "There was an error processing your request, please try again later!!";
            }
            return View(item);
        }

        public async Task<ActionResult> Edit(int id, string message = null, ViewMessageType? messageType = null)
        {
            var dataQuery = PrimaryService.GetByKey(id);
            await SetupSelectList();
            if (dataQuery == null) return HttpNotFound("Could not find the video you are looking for.");

            if (!string.IsNullOrEmpty(message) && messageType.HasValue)
                ViewBag.ViewMessage = new ViewMessage(message, messageType.Value);

            return View(dataQuery);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(T item)
        {
            await SetupSelectList();
            if (!ModelState.IsValid) return View(item);
            try
            {
                PrimaryService.Update(item);

                return RedirectToAction("index");
            }
            catch (Exception exception)
            {
                if (exception is ValidationException)
                    ModelState.AddModelError("", exception.ToString());
                else
                    ModelState.AddModelError("", "There was an error processing your request, please try again later!!");
            }

            return View(item);
        }
        public async Task<ActionResult> Delete(int id)
        {
            var dataQuery = PrimaryService.GetByKey(id);
            await SetupSelectList();
            if (dataQuery == null) return HttpNotFound("Status cannot be found.");

            return View(dataQuery);
        }
        [HttpPost]
        public async Task<ActionResult> Delete(T item, int id)
        {
            await SetupSelectList();
            var existing = PrimaryService.GetByKey(id);

            if (existing == null) return HttpNotFound("Cannot find the item to be deleted.");
            try
            {
                PrimaryService.Delete(existing);
                return RedirectToAction("index");
            }
            catch (Exception exception)
            {
                if (exception is ValidationException)
                    ModelState.AddModelError("", exception.ToString());
                else
                    ModelState.AddModelError("", "There was an error processing your request, please try again later!!");
            }
            return View(item);
        }
        public async Task<ActionResult> Recover(int id)
        {
            var dataQuery = PrimaryService.GetByKey(id);
            await SetupSelectList();
            if (dataQuery == null) return HttpNotFound("Video Information cannot be found.");

            return View(dataQuery);
        }
        [HttpPost]
        public async Task<ActionResult> Recover(T item, int id)
        {
            await SetupSelectList();
            var existing = PrimaryService.GetByKey(id);
            if (existing == null) return HttpNotFound("Cannot find the item to be recovered.");
            try
            {
                PrimaryService.Recover(existing);
                return RedirectToAction("index");
            }
            catch (Exception exception)
            {
                if (exception is ValidationException)
                    ModelState.AddModelError("", exception.ToString());
                else
                    ModelState.AddModelError("", "There was an error processing your request, please try again later!!");
            }
            return View(item);
        }
        protected abstract void TransformQuery(ref IQueryable<T> dataQuery);
        protected abstract Task SetupSelectList();
    }
}