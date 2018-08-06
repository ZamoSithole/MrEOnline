using MrE.Models.Abstractions;
using MrE.Services.Abstractions;
using MrEOnline.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MrEOnline.Controllers {
    public abstract class BaseViewController<T, V, K> : Controller
        where T: class
        {
        protected IService<T, K> PrimaryService { get; set; }

        public BaseViewController(IService<T, K> primaryService) {
            PrimaryService = primaryService;
        }

        public virtual async Task<ActionResult> Index() {
            var dataQuery = PrimaryService.Get();

            TransformQuery(ref dataQuery);

            return View(dataQuery);
        }

        public async Task<ActionResult> Edit(K id, string message = null, ViewMessageType? messageType = null) {
            var item = PrimaryService.GetByKey(id);

            await SetupSelectList();
            if (item == null) return HttpNotFound("Could not find the user you are looking for.");

            if (!string.IsNullOrEmpty(message) && messageType.HasValue)
                ViewData["ViewMessage"] = new ViewMessage(message, messageType.Value);

            return View(CreateViewObject(item));
        }

        [HttpPost]
       public virtual async Task<ActionResult> Edit(V item) {
            var viewMessage = new ViewMessage();
            await SetupSelectList();
            if (!ModelState.IsValid) return View(item);
            try {
                var entity = PrimaryService.GetByKey((item as IBaseEntity<K>).Id);
                CreateObjectFromViewModel(item, ref entity);

                PrimaryService.Update(entity);
                viewMessage.Message = "Successfully saved your changes.";
                viewMessage.Type = ViewMessageType.Success;

                return RedirectToAction("Edit", new {
                    id = (item as IBaseEntity<K>).Id,
                    message = viewMessage.Message,
                    messageType = viewMessage.Type
                });
            } catch (Exception exception) {
                if (exception is ValidationException)
                    ModelState.AddModelError("", exception.ToString());
                else
                    ModelState.AddModelError("", "There was an error processing your request, please try again later!!");
            }

            return View(item);
        }

        public async Task<ActionResult> Details(K id) {
            var item = PrimaryService.GetByKey(id);

            await SetupSelectList();
            if (item == null) return HttpNotFound("Could not find the user you are lokking for");
            return View(CreateViewObject(item));
            
        }
        protected abstract V CreateViewObject(T item);
        protected abstract void CreateObjectFromViewModel(V item, ref T entity);
        protected abstract void TransformQuery(ref IQueryable<T> dataQuery);
        protected abstract Task SetupSelectList();

    }
}