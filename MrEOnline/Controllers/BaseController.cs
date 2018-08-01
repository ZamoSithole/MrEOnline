using MrE.Models;
using MrE.Models.Abstractions;
using MrE.Services.Abstractions;
using MrEOnline.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MrEOnline.Controllers {

    public abstract class BaseController<T, K> : Controller
        where T : class
    {
        protected IService<T, K> PrimaryService { get; set; }

        public BaseController(IService<T, K> primaryService)
        {
            PrimaryService = primaryService;
        }

        public virtual async Task<ActionResult> Index()
        {
            var dataQuery = PrimaryService.Get();

            TransformQuery(ref dataQuery);

            return View(dataQuery);
        }

        public virtual async Task<ActionResult> Details(K id) {
            await SetupSelectList();
            var dataQuery = PrimaryService.GetByKey(id);
            if (dataQuery == null) return HttpNotFound("Video information cannot be found.");

            return View(dataQuery);

        }
        //public virtual async Task<ActionResult> Create()
        //{
        //    await SetupSelectList();
        //    return View();
        //}
        public virtual async Task<ActionResult> Create(int? foreignKey = null) {
            await SetupSelectList();
            return View();
        }
        //public virtual async Task<ActionResult> CreateFor(K foreignKey) {
        //    await SetupSelectList();
        //    return View();
        //}

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

                return RedirectToAction("Edit", new
                {
                    id = (item as IBaseEntity<int>).Id,
                    message = viewMessage.Message,
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

        private static void HandleUploads(T item, HttpPostedFileBase upload)
        {
            if (upload != null && upload.ContentLength > 0)
            {
                using (var reader = new System.IO.BinaryReader(upload.InputStream))
                {
                    if (item is IUploadable) (item as IUploadable).Data = reader.ReadBytes(upload.ContentLength);
                }
            }
        }
        public async Task<ActionResult> UploadData(K id)
        {
            var dataQuery = PrimaryService.GetByKey(id);
            await SetupSelectList();
            if (dataQuery == null) return HttpNotFound("Could not find the video you are looking for.");

            return View(dataQuery);
        }
        [HttpPost]
        public async Task<ActionResult> UploadData(T item, HttpPostedFileBase upload)
        {
            if (upload != null && upload.ContentLength > 0)
            {
                using (var reader = new System.IO.BinaryReader(upload.InputStream))
                {
                    if (item is IUploadable) (item as IUploadable).Data = reader.ReadBytes(upload.ContentLength);
                }
            }
            var viewMessage = new ViewMessage();
            await SetupSelectList();
            if (!ModelState.IsValid) return View(item);
            try
            {
                PrimaryService.Update(item);
                viewMessage.Message = "Successfully saved your changes.";
                viewMessage.Type = ViewMessageType.Success;

                return RedirectToAction("Edit", new
                {
                    id = (item as IBaseEntity<int>).Id,
                    message = viewMessage.Message,
                    messageType = viewMessage.Type
                });
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

        public virtual async Task<ActionResult> Edit(K id, string message = null, ViewMessageType? messageType = null)
        {
            var dataQuery = PrimaryService.GetByKey(id);
            
            await SetupSelectList();
            if (dataQuery == null) return HttpNotFound("Could not find the video you are looking for.");

            if (!string.IsNullOrEmpty(message) && messageType.HasValue)
                ViewData["ViewMessage"] = new ViewMessage(message, messageType.Value);

            return View(dataQuery);
        }

        [HttpPost]
        public virtual async Task<ActionResult> Edit(T item)
        {
            var viewMessage = new ViewMessage();
            await SetupSelectList();
            if (!ModelState.IsValid) return View(item);
            try
            {
                PrimaryService.Update(item);
                viewMessage.Message = "Successfully saved your changes.";
                viewMessage.Type = ViewMessageType.Success;

                return RedirectToAction("Edit", new
                {
                    id = (item as IBaseEntity<K>).Id,
                    message = viewMessage.Message,
                    messageType = viewMessage.Type
                });
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
        public async Task<ActionResult> Delete(K id)
        {
            var dataQuery = PrimaryService.GetByKey(id);
            await SetupSelectList();
            if (dataQuery == null) return HttpNotFound("Status cannot be found.");

            return View(dataQuery);
        }
        [HttpPost]
        public async Task<ActionResult> Delete(T item, K id)
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
        public async Task<ActionResult> Recover(K id)
        {
            var dataQuery = PrimaryService.GetByKey(id);
            await SetupSelectList();
            if (dataQuery == null) return HttpNotFound("Video Information cannot be found.");

            return View(dataQuery);
        }
        [HttpPost]
        public async Task<ActionResult> Recover(T item, K id)
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