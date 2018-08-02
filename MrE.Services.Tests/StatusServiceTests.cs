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
    public class StatusServiceTests {

        protected MockRepositorySetup<Status> MockRepositorySetup { get; set; }
        public AutoMock AutoMock { get; set; }
        protected IService<Status, int> StatusService { get; set; }

        [SetUp]
        public void Setup() {
            MockRepositorySetup = new MockRepositorySetup<Status>(
                new Mock<IRepository<Status, int>>(),
                (new List<Status>() {
                    new Status{ Id=1,
                    Name="Pending",
                    Description="Pending",
                    DateCreated = DateTime.Now,
                    IsDeleted = false},

                    new Status{ Id=2,
                    Name="Active",
                    Description="Active",
                    DateCreated = DateTime.Now,
                    IsDeleted = false,
                    DateUpdated = DateTime.Now.AddDays(2)},

                    new Status{ Id=3,
                    Name="Inactive",
                    Description="Inactive",
                    DateCreated = DateTime.Now,
                    IsDeleted = true,
                    DateUpdated = DateTime.Now.AddDays(2),
                    DateDeleted = DateTime.Now.AddDays(5)}
                })).Setup();
            AutoMock = AutoMock.GetLoose();
            AutoMock.Provide(MockRepositorySetup.MockRepository.Object);
            AutoMock.Provide<IValidationService<Status>, StatusValidationService>();
            AutoMock.Provide<IValidationExceptionService, ValidationExceptionService>();
            StatusService = AutoMock.Create<StatusService>();
        }

        [Test]
        [TestCase(3)]
        public void GetReturnsCorrectNumberOfItems(int expected) {
            var actual = StatusService.Get().Count();
            Assert.AreEqual(expected, actual, "Get fail to return correct number of rows");
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void GetByKeyShouldSucceed(int value) {
            var actual = StatusService.GetByKey(value);
            Assert.IsNotNull(actual, "GetByKey failed to return correct information");
        }
        //Negative Test
        [Test]
        [TestCase(1000)]
        public void GetByKeyShouldFail(int value) {
            var actual = StatusService.GetByKey(value);
            Assert.IsNull(actual, "GetByKey failed to return incorrect data");
        }

        [Test]
        public void InsertShouldBeSuccessful() {
            var status = new Status {
                Id = 1,
                Name = "Status",
                Description = "Status",
                DateCreated = DateTime.Now,
                IsDeleted = false
            };
            var actual = StatusService.Insert(status);
            Assert.AreEqual(status, actual, "Insert test failed to insert correctly");
        }

        [Test]
        [TestCase(1)]
        public void UpdateShouldBeSuccessful(int value) {
            var status = StatusService.GetByKey(value);
            var oldeDateUpdated = status.DateUpdated;
            var oldName = status.Name;

            status.Name = DateTime.Now.GetHashCode().ToString();

            StatusService.Update(status);

            Assert.AreNotEqual(oldName, status.Name);
            Assert.AreNotEqual(oldeDateUpdated, status.DateUpdated);
        }

        [Test]
        public void DeleteShouldBeSuccessful() {
            var status = StatusService.Get().FirstOrDefault(e => !e.IsDeleted);
            var oldIsDeleted = status.IsDeleted;
            var oldDate = status.DateDeleted;

            StatusService.Delete(status);
            Assert.AreNotEqual(oldDate, status.DateDeleted);
            Assert.AreEqual(true, status.IsDeleted);
        }

        [Test]
        public void RecoverShouldBeSuccessful() {
            var status = StatusService.Get().FirstOrDefault(e => e.IsDeleted);
            var oldIsDeleted = status.IsDeleted;
            var oldDateUpdate = status.DateUpdated;

            StatusService.Recover(status);
            Assert.AreNotEqual(oldDateUpdate, status.DateUpdated);
            Assert.AreEqual(false, status.IsDeleted);
            Assert.IsNull(status.DateDeleted);
        }

        [TearDown]
        public void TearDown() {
            AutoMock?.Dispose();
        }
    }
}
