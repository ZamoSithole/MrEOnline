using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public class RentalServiceTests {
        
        protected MockRepositorySetup<Rental> MockRepositorySetup { get; set; }
        public AutoMock AutoMock { get; set; }
        //System under test.
        protected IService<Rental, int> RentalService { get; set; } 

        [SetUp]
        public void Setup() {
            MockRepositorySetup = new MockRepositorySetup<Rental>(
                new Mock<IRepository<Rental, int>>(),
                (new List<Rental>() {
                    new Rental{ Id=1,
                    VideoId=1,
                    StatusId=1,
                    UserId="a017433b-ff94-44a7-a519-c5df1c18e7ad",
                    DateCreated = DateTime.Now,
                    IsCheckedOut = true}

                    //new Rental{ Id=1,
                    //VideoId=1,
                    //StatusId=1,
                    //UserId="a017433b-ff94-44a7-a519-c5df1c18e7ad",
                    //DateCreated = DateTime.Now,
                    //IsCheckedOut = true,
                    // DateUpdated = DateTime.Now.AddDays(2)}
                })).Setup();
            AutoMock = AutoMock.GetLoose();
            AutoMock.Provide(MockRepositorySetup.MockRepository.Object);
            AutoMock.Provide<IValidationService<Rental>, RentalValidationService>();
            AutoMock.Provide<IValidationExceptionService, ValidationExceptionService>();
            RentalService = AutoMock.Create<RentalService>();
        }

        [Test]
        [TestCase("a017433b-ff94-44a7-a519-c5df1c18e7ad")]
        public void GetReturnsCorrectNumberOfItemsBasedOnUserId(string expected) {
            var actual = RentalService.Get().SingleOrDefault(e => e.UserId == expected);
            Assert.IsNotNull(actual, "Get Failed to return correct data.");
        }

        [Test]
        public void InsertShouldPass() {
            var rental = new Rental {
                Id = 2,
                VideoId = 1,
                StatusId = 1,
                UserId = "a017433b-ff94-44a7-a519-c5df1c18e7ad",
                DateCreated = DateTime.Now,
                IsCheckedOut = true
            };
            var actual = RentalService.Insert(rental);
            Assert.AreEqual(rental, actual, "Insert failed to insert correctly.");
        }

        [Test]
        [TestCase("a017433b-ff94-44a7-a519-c5df1c18e7ad")]
        public void UpdateShouldConfirm(string expected) {
            var rental = RentalService.Get().SingleOrDefault(e => e.UserId == expected);

            var oldStatus = rental.StatusId;
            rental.StatusId = 2;
            rental.DateUpdated = DateTime.Now;
            RentalService.Update(rental);

            Assert.AreNotEqual(oldStatus, rental.StatusId);
        }
    }
}
