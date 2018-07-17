using Autofac.Extras.Moq;
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
    public class StatusControllerTests
    {
        private AutoMock AutoMock { get; set; }
        private Mock<IRepository<Status>> MockRepository { get; set; }

        [SetUp]
        public void Setup()
        {
            AutoMock = AutoMock.GetLoose();
            MockRepository = new Mock<IRepository<Status>>();
            MockRepository.Setup(m => m.Get()).Returns(GetData());

            AutoMock.Provide(MockRepository.Object);
            AutoMock.Provide<IService<Status>, StatusService>();
            AutoMock.Provide<IValidationService<Status>, StatusValidationService>();
            AutoMock.Provide<IValidationExceptionService, ValidationExceptionService>();
        }
        [Test]
        [TestCase(3)]
        public async Task IndexShouldReturnStatues(int value)
        {
            var controller = AutoMock.Create<StatusController>();
            var indexResult = await controller.Index();

            Assert.IsInstanceOf<ViewResult>(indexResult);
            var model = (indexResult as ViewResult).Model;

            Assert.IsInstanceOf<IEnumerable<Status>>(model);
            Assert.NotNull((model as IEnumerable<Status>), "A null model is not expected");
            Assert.AreEqual(value, (model as IEnumerable<Status>).Count(), "failed to return correct number of statuses");
        }

        [TearDown]
        public void Teardown()
        {
            AutoMock.Dispose();
        }
        private IQueryable<Status> GetData()
        {
            return (new List<Status>()
            {
                 new Status {Id = 1, Name = "Pending", DateCreated = DateTime.Now, IsDeleted = false },
                 new Status {Id = 2, Name = "In-Progress", DateCreated = DateTime.Now, IsDeleted = true, DateUpdated = DateTime.Now.AddDays(2), DateDeleted = DateTime.Now.AddDays(5) },
                 new Status {Id = 3, Name = "Active", DateCreated = DateTime.Now, IsDeleted = false, DateUpdated = DateTime.Now.AddDays(2) }
            }).AsQueryable();
        }
    }
}
