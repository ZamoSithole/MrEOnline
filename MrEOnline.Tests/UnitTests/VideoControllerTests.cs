using Autofac.Extras.Moq;
using Moq;
using MrE.Models.Entities;
using MrE.Repository;
using MrE.Repository.Abstractions;
using MrE.Services;
using MrE.Services.Abstractions;
using MrE.Services.Validations;
using MrEOnline.Controllers;
using MrEOnline.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MrEOnline.Tests.UnitTests
{
    [TestFixture]
    public class VideoControllerTests {
        public AutoMock AutoMock { get; set; }
        private Mock<IRepository<Video, int>> MockRepository { get; set; }
        private List<Video> DataStore { get; set; }
        public IService<Genre, int> GenreService { get; set; }
        public IService<Title, int> TitleService { get; set; }

        [SetUp]
        public void Setup() {
            DataStore = new List<Video>()
            {
                 new Video {Id = 1, Title = "The Hitman's Bodyguard",Description="",RentalPrice=40,GenreID=1,AgeRestriction="16", DateCreated = DateTime.Now, IsDeleted = false },
                 new Video {Id = 2, Title = "Kunfu Master",Description="",RentalPrice=35,GenreID=1,AgeRestriction="16", DateCreated = DateTime.Now, IsDeleted = true, DateUpdated = DateTime.Now.AddDays(2), DateDeleted = DateTime.Now.AddDays(5) },
                 new Video {Id = 3, Title = "Kunfu Master 3",Description="",RentalPrice=10,GenreID=1,AgeRestriction="16", DateCreated = DateTime.Now, IsDeleted = false, DateUpdated = DateTime.Now.AddDays(2) }
            };

            AutoMock = AutoMock.GetLoose();
            MockRepository = new Mock<IRepository<Video, int>>();
            MockRepository.Setup(m => m.Get()).Returns(GetData());

            AutoMock.Provide(MockRepository.Object);
            AutoMock.Provide<IService<Video, int>, VideoService>();
            AutoMock.Provide<IValidationService<Video>, VideoValidationService>();
            AutoMock.Provide<IValidationExceptionService, ValidationExceptionService>();
        }
        [Test]
        [TestCase(3)]
        public async Task IndexShouldReturnVideos(int value) {
            var controller = AutoMock.Create<VideoController>();
            var indexResult = await controller.Index();

            Assert.IsInstanceOf<ViewResult>(indexResult);
            var model = (indexResult as ViewResult).Model;

            Assert.IsInstanceOf<IEnumerable<Video>>(model);
            Assert.NotNull((model as IEnumerable<Video>), "A null model is not expected");
            Assert.AreEqual(value, (model as IEnumerable<Video>).Count(), "failed to return correct number of statuses");
        }

        [Test]
        public async Task ModelStateShouldBeValidForCreate() {
            int newKey = 4;
            var VideoInfo = new Video {
                Id = newKey,
                Title = "The Hitman's Bodyguard",
                Description = "The Hitman's Bodyguard",
                RentalPrice = 40,
                GenreID = 1,
                AgeRestriction = "16",
                DateCreated = DateTime.Now,
                IsDeleted = false
            };
            var controller = AutoMock.Create<VideoController>();
            //controller.ModelState.Clear();

            ActionResult result = await controller.Create(VideoInfo);
            
            Assert.IsTrue(controller.ModelState.IsValid);

            Assert.IsInstanceOf<RedirectToRouteResult>(result);

            var redirectResult = (result as RedirectToRouteResult);
            Assert.AreEqual(redirectResult.RouteValues["action"], "Edit");
            Assert.AreEqual(redirectResult.RouteValues["id"], newKey);
            Assert.AreEqual(redirectResult.RouteValues["message"], "Successfully saved your changes.");
            Assert.AreEqual(redirectResult.RouteValues["messageType"], ViewMessageType.Success);
        }
        [Test]
        public async Task ModelStateShouldNotBeValidForCreate() {
            var VideoInfo = new Video {
            };
            var controller = AutoMock.Create<VideoController>();
            controller.ModelState.AddModelError("error", "error");
            ActionResult result = await controller.Create(VideoInfo); ;


            Assert.IsFalse(controller.ModelState.IsValid);
           // Assert.IsNull(controller);
        }
        [Test]
        public async Task ExceptionTestForCreate() {
            var VideoInfo = new Video ();
            VideoInfo = null;
            var controller = AutoMock.Create<VideoController>();
            ActionResult result = await controller.Create(VideoInfo);

            var validationResults = new List<ValidationResult>();
            
            Assert.AreEqual(false, (validationResults.Count > 0), "Test Equalateral");
            
        }

        [Test]
        public async Task ModelStateShouldBeValidForEdit() {

            var VideoInfo = new Video {
                Id = 3,
                Title = "Kunfu Master 3", Description = "", RentalPrice = 10,
                GenreID = 1, AgeRestriction = "16", DateCreated = DateTime.Now,
                IsDeleted = false, DateUpdated = DateTime.Now.AddDays(2)
            };

            var controller = AutoMock.Create<VideoController>();

            ActionResult result = await controller.Edit(VideoInfo);

            Assert.IsTrue(controller.ModelState.IsValid);

            Assert.IsInstanceOf<RedirectToRouteResult>(result);

            var redirectResult = (result as RedirectToRouteResult);
            Assert.AreEqual(redirectResult.RouteValues["action"], "Edit");
            Assert.AreEqual(redirectResult.RouteValues["id"], VideoInfo.Id);
            Assert.AreEqual(redirectResult.RouteValues["message"], "Successfully saved your changes.");
            Assert.AreEqual(redirectResult.RouteValues["messageType"], ViewMessageType.Success);
        }
        [Test]
        public async Task ModelStateShouldNotBeValidForEdit() {
            var VideoInfo = new Video {
            };
            var controller = AutoMock.Create<VideoController>();
            controller.ModelState.AddModelError("error", "error");
            ActionResult result = await controller.Edit(VideoInfo); ;


            Assert.IsFalse(controller.ModelState.IsValid);
            // Assert.IsNull(controller);
        }
        [Test]
        public async Task ExceptionTestForEdit() {
            var VideoInfo = new Video();
            VideoInfo = null;
            var controller = AutoMock.Create<VideoController>();
            ActionResult result = await controller.Edit(VideoInfo);

            var validationResults = new List<ValidationResult>();

            Assert.AreEqual(false, (validationResults.Count > 0), "Test Equalateral");

        }

        [Test]
        [TestCase(1)]
        public async Task CheckingExistenceForDelete(int expected) {
            var VideoInfo = new Video();
            VideoInfo = null;
            var controller = AutoMock.Create<VideoController>();
           
            ActionResult result = await controller.Delete(VideoInfo,expected);
            Assert.That(result, Is.InstanceOf(typeof(HttpNotFoundResult)));
        }

        [Test]
        [TestCase(1)]
        public async Task CheckingExistenceForRecover(int expected) {
            var VideoInfo = new Video();
            VideoInfo = null;
            var controller = AutoMock.Create<VideoController>();

            ActionResult result = await controller.Recover(VideoInfo, expected);
            Assert.That(result, Is.InstanceOf(typeof(HttpNotFoundResult)));
        }


        [Test]
        [TestCase(1)]
        public async Task DeleteShouldDelete(int expected) {
            var videoInfo = new Video {
                Id = expected,
                Title = "Kunfu Master",
                Description = "",
                RentalPrice = 35,
                GenreID = 1,
                AgeRestriction = "16",
                DateCreated = DateTime.Now,
                IsDeleted = true,
                DateUpdated = DateTime.Now.AddDays(2),
                DateDeleted = DateTime.Now.AddDays(5)
            };

             MockRepository.Setup(m => m.GetByKey(expected))
                .Returns(() => {
                    return DataStore.SingleOrDefault(e => e.Id == expected);
                });
            MockRepository.Setup(m => m.Delete(videoInfo));
            MockRepository.Setup(m => m.CommitChanges()).Returns(1);

            var controller = AutoMock.Create<VideoController>();
            var indexResult = await controller.Delete(videoInfo,expected);

            Assert.AreNotEqual(DataStore, videoInfo);
        }

        [TearDown]
        public void Teardown()
        {
            AutoMock.Dispose();
        }
        private IQueryable<Video> GetData()
        {
            return DataStore.AsQueryable();
        }
    }
}
