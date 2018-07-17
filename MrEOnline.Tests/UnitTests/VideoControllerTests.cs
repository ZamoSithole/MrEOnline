﻿using Autofac.Extras.Moq;
using Moq;
using MrE.Models.Entities;
using MrE.Repository.Abstractions;
using MrE.Services;
using MrE.Services.Abstractions;
using MrE.Services.Validations;
using MrEOnline.Controllers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MrEOnline.Tests.UnitTests
{
    [TestFixture]
    public class VideoControllerTests
    {
        public AutoMock AutoMock { get; set; }
        private Mock<IRepository<Video>> MockRepository { get; set; }
        private List<Video> DataStore { get; set; }

        [SetUp]
        public void Setup()
        {
            DataStore = new List<Video>()
            {
                 new Video {Id = 1, Title = "The Hitman's Bodyguard",Description="",RentalPrice=40,GenreID=1,AgeRestriction="16", DateCreated = DateTime.Now, IsDeleted = false },
                 new Video {Id = 2, Title = "Kunfu Master",Description="",RentalPrice=35,GenreID=1,AgeRestriction="16", DateCreated = DateTime.Now, IsDeleted = true, DateUpdated = DateTime.Now.AddDays(2), DateDeleted = DateTime.Now.AddDays(5) },
                 new Video {Id = 3, Title = "Kunfu Master 3",Description="",RentalPrice=10,GenreID=1,AgeRestriction="16", DateCreated = DateTime.Now, IsDeleted = false, DateUpdated = DateTime.Now.AddDays(2) }
            };

            AutoMock = AutoMock.GetLoose();
            MockRepository = new Mock<IRepository<Video>>();
            MockRepository.Setup(m => m.Get()).Returns(GetData());

            AutoMock.Provide(MockRepository.Object);
            AutoMock.Provide<IService<Video>, VideoService>();
            AutoMock.Provide<IValidationService<Video>, VideoValidationService>();
            AutoMock.Provide<IValidationExceptionService, ValidationExceptionService>();
        }
        [Test]
        [TestCase(3)]
        public async Task IndexShouldReturnVideos(int value)
        {
            var controller = AutoMock.Create<VideoController>();
            var indexResult = await controller.Index();

            Assert.IsInstanceOf<ViewResult>(indexResult);
            var model = (indexResult as ViewResult).Model;

            Assert.IsInstanceOf<IEnumerable<Video>>(model);
            Assert.NotNull((model as IEnumerable<Video>), "A null model is not expected");
            Assert.AreEqual(value, (model as IEnumerable<Video>).Count(), "failed to return correct number of statuses");
        }

        //[Test]
        //public async Task CreateShouldInsert()
        //{
        //    var controller = AutoMock.Create<VideoController>();
        //    var indexResult = await controller.Create();
        //    //DataStore.Add(new Video { Id = 4, Title = "The Hitman's Bodyguarding", Description = "", RentalPrice = 40, GenreID = 1, AgeRestriction = "16", DateCreated = DateTime.Now, IsDeleted = false });

        //    Assert.IsInstanceOf<ViewResult>(indexResult);
        //    var model = (indexResult as ViewResult).Model;

        //    Assert.IsInstanceOf<IEnumerable<Video>>(model);
        //    Assert.NotNull((model as IEnumerable<Video>), "A null model is not expected");

        //    //DataStore.Remove()
        //}

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