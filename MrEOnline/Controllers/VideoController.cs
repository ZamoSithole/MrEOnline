using MrE.Models.Entities;
using MrE.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MrEOnline.Controllers
{
    public class VideoController : Controller
    {
        public IService<Video> VideoService { get; set; }

        public VideoController(IService<Video> videoService)
        {
            VideoService = videoService;
        }
        // GET: Video
        public ActionResult Index()
        {
            var videos = VideoService.Get();
            return View(videos);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Video video)
        {
            if (!ModelState.IsValid) return View(video);
            try
            {
                VideoService.Insert(video);

                return RedirectToAction("index");
            }
            catch (Exception exception)
            {

                if (exception is ValidationException)
                    ModelState.AddModelError("", exception.ToString());
                else
                    ModelState.AddModelError("", "There was an error processing your request, please try again later!!");
            }
            return View(video);
        }
        [HttpPost]
        public ActionResult UploadVideo()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fName;

                        fName = file.FileName;

                        List<Video> videos = new List<Video>();

                        string csvData = System.IO.File.ReadAllText(fName);

                        int RowCount = 0;

                        foreach (string row in csvData.Split('\n').Skip(1))
                        {
                            Video video = new Video();
                            if (!string.IsNullOrEmpty(row))
                            {
                                int a = 0;
                                string VideoList = "";
                                string Title = "";
                                string Description = "";
                                string Genre = "";
                                string RentalPrice = "";

                                foreach (string cellValue in row.Split(';'))
                                {
                                    string cell = cellValue.Replace("\r\n","").Replace("\r", "").Replace("\n", "");
                                    if (a == 0)
                                    {
                                        Title = cell;
                                    }
                                    else if (a == 1)
                                    {
                                        Description = cell;
                                    }
                                    else if (a == 2)
                                    {
                                        Genre = cell;
                                    }
                                    else if (a == 3)
                                    {
                                        RentalPrice = cell;
                                    }
                                    a++;
                                }
                                RowCount++;
                                if (Title == "" && Description == "" && Genre == "" && RentalPrice == "")
                                {

                                }
                                else
                                {
                                    video.Title = Title;
                                    video.Description = Description;
                                    video.Genre = Genre;
                                    video.RentalPrice = RentalPrice;
                                }
                                
                            }
                            //video.add(video);
                            VideoService.Insert(video);
                        }
                        //VideoService.Insert(video);
                        return RedirectToAction("index");
                    }
                    return RedirectToAction("index");
                }
                catch (Exception exception)
                {

                    if (exception is ValidationException)
                        ModelState.AddModelError("", exception.ToString());
                    else
                        ModelState.AddModelError("", "There was an error processing your request, please try again later!!");
                }
            }
            return View(); 
        }
    }
}