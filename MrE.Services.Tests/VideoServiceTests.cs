using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Extras.Moq;
using Moq;
using MrE.Models.Entities;
using MrE.Repository.Abstractions;
using MrE.Services.Abstractions;
using MrE.Services.Validations;
using NUnit.Framework;
using TestUtils;


namespace MrE.Services.Tests {
    [TestFixture]
    public class VideoServiceTests {
        protected MockRepositorySetup<Video> MockRepositorySetup { get; set; }
        public AutoMock AutoMock { get; set; }
        protected IService<Video, int> VideoService { get; set; }

        [SetUp]
        public void Setup() {
            MockRepositorySetup = new MockRepositorySetup<Video>(
                new Mock<IRepository<Video, int>>(),
                (new List<Video>()
                {
                    new Video{
                        Id = 1,
                        Title = "Hello kitty",
                        AgeRestriction = "4",
                        GenreID = 1,
                        RentalPrice =10,
                        DateCreated = DateTime.Now,
                        IsDeleted = false},
                    new Video {
                        Id = 2,
                        Title = "Hello kitty",
                        AgeRestriction = "4",
                        GenreID = 1,
                        RentalPrice = 10,
                        DateCreated = DateTime.Now,
                        IsDeleted = false,
                        DateUpdated = DateTime.Now.AddDays(2),
                    },
                    new Video {
                        Id = 3,
                        Title = "Hello kitty",
                        AgeRestriction = "4",
                        GenreID = 1,
                        RentalPrice = 10,
                        DateCreated = DateTime.Now,
                        IsDeleted = true,
                        DateUpdated = DateTime.Now.AddDays(2),
                        DateDeleted = DateTime.Now.AddDays(5)
                    }
                    })).Setup();
            AutoMock = AutoMock.GetLoose();
            AutoMock.Provide(MockRepositorySetup.MockRepository.Object);
            AutoMock.Provide<IValidationService<Video>, VideoValidationService>();
            AutoMock.Provide<IValidationExceptionService, ValidationExceptionService>();
            VideoService = AutoMock.Create<VideoService>();
        }

        [Test]
        [TestCase(3)]
        public void GetReturnsCorrectNumberOfItems(int expected) {
            var actual = VideoService.Get().Count();
            Assert.AreEqual(expected, actual, "Get Fail to return the correct number of rows");
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void GetByKeyShouldSucceed(int value) {
            var actual = VideoService.GetByKey(value);
            Assert.IsNotNull(actual, "GetByKey failed to return correct info");
        }
        //Negative Test
        [Test]
        [TestCase(1000)]
        public void GetByKeyShouldFail(int value) {
            var actual = VideoService.GetByKey(value);
            Assert.IsNull(actual, "GetBykey failed to return incorrect info");
        }

        [Test]
        public void InsertShouldSucceed() {
            var video = new Video {
                Id = 4,
                Title = "Kitty Dies",
                GenreID = 1,
                RentalPrice = 10,
                DateCreated = DateTime.Now,
                IsDeleted = false
            };
            var actual = VideoService.Insert(video);
            Assert.AreEqual(video, actual, "Insert test failed to insert correctly.");
        }
    }
}
