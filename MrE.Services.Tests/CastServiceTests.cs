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
    public class CastServiceTests {
        protected MockRepositorySetup<Cast> MockRepositorySetup { get; set; }
        public AutoMock AutoMock { get; set; }
        protected IService<Cast, int> CastService { get; set; }

        [SetUp]
        public void Setup() {
            MockRepositorySetup = new MockRepositorySetup<Cast>(
                new Mock<IRepository<Cast, int>>(),
                (new List<Cast>()
           {
                new Cast{Id = 1, FirstName = "Action",LastName="Hello", VideoId=1, DateCreated = DateTime.Now, IsDeleted = false , TitleId= 1},
                new Cast{Id =2, FirstName = "Comedy",LastName="bye",VideoId=1, DateCreated = DateTime.Now,IsDeleted = false, DateUpdated = DateTime.Now.AddDays(2), TitleId= 1 },
                new Cast{Id =3, FirstName = "Horror",LastName="Great",VideoId=1, DateCreated = DateTime.Now,IsDeleted = true, DateUpdated = DateTime.Now.AddDays(2), DateDeleted = DateTime.Now.AddDays(5) , TitleId= 1}
            }))
           .Setup();

            AutoMock = AutoMock.GetLoose();
            AutoMock.Provide(MockRepositorySetup.MockRepository.Object);
            AutoMock.Provide<IValidationService<Cast>, CastValidationService>();
            AutoMock.Provide<IValidationExceptionService, ValidationExceptionService>();
            CastService = AutoMock.Create<CastService>();
        }

        [Test]
        [TestCase(3)]
        public void GetShouldReturnCorrectNumberOfItems(int expected) {
            var actual = CastService.Get().Count();
            Assert.AreEqual(expected, actual, "Get failed to return the correct number of rows.");
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void GetByKeyShouldSucceed(int value) {
            var actual = CastService.GetByKey(value);
            Assert.IsNotNull(actual, "GetByKey failed to return correct data.");
        }

        [Test]
        [TestCase(1000000)]
        public void GetByKeyShouldFail(int value) {
            var actual = CastService.GetByKey(value);
            Assert.IsNull(actual, "GetByKey failed to return incorrect data.");
        }

        [Test]
        public void InsertShouldPass() {
            var cast = new Cast {
                Id = 4, FirstName = "Action", LastName = "Hello", VideoId = 1, DateCreated = DateTime.Now, IsDeleted = false, TitleId = 1
            };

            var actual = CastService.Insert(cast);
            Assert.AreEqual(cast, actual, "Insert test failed to correctly insert new cast.");
        }

        [Test]
        [TestCase(1)]
        public void UpdateShouldPass(int value) {
            var cast = CastService.GetByKey(value);
            var oldDateUpdated = cast.DateUpdated;
            var oldName = cast.FullName;

            cast.FirstName = DateTime.Now.GetHashCode().ToString();
            
            CastService.Update(cast);

            Assert.AreNotEqual(oldName, cast.FullName);
            Assert.AreNotEqual(oldDateUpdated, cast.DateUpdated);
        }

        [Test]
        public void DeleteShouldPass() {
            var cast = CastService.Get().FirstOrDefault(e => !e.IsDeleted);
            var oldIsDeleted = cast.IsDeleted;
            var oldDate = cast.DateDeleted;

            CastService.Delete(cast);
            Assert.AreNotEqual(oldDate, cast.DateDeleted);
            Assert.AreEqual(true, cast.IsDeleted);
        }

        [Test]
        public void RecoverShouldPass() {
            var cast = CastService.Get().FirstOrDefault(e => e.IsDeleted);
            var oldIsDeleted = cast.IsDeleted;
            var oldDateUpdate = cast.DateUpdated;

            CastService.Recover(cast);
            Assert.AreNotEqual(oldDateUpdate, cast.DateUpdated);
            Assert.AreEqual(false, cast.IsDeleted);
            Assert.IsNull(cast.DateDeleted);
        }

        [TearDown]
        public void Teardown() {
            AutoMock?.Dispose();
        }
    }
}