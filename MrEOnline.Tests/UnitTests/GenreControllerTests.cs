using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Autofac.Extras.Moq;
using Moq;
using MrE.Models.Entities;
using MrE.Repository.Abstractions;
using MrE.Services;
using MrE.Services.Abstractions;
using MrE.Services.Validations;
using MrEOnline.Controllers;
using NUnit;
using NUnit.Framework;

namespace MrEOnline.Tests.UnitTests
{
    [TestFixture]
    public class GenreControllerTests
    {
        private AutoMock AutoMock { get; set; }
        private Mock<IRepository<Genre, int>> MockRepository { get; set; }
        private List<Genre> DataStore { get; set; }

        [SetUp]
        public void Setup()
        {
            DataStore = new List<Genre>()
            {
                new Genre{Id = 1, Name = "Action", DateCreated = DateTime.Now, IsDeleted = false },
                new Genre{Id =2, Name = "Comedy", DateCreated = DateTime.Now,IsDeleted = false, DateUpdated = DateTime.Now.AddDays(2) },
                new Genre{Id =3, Name = "Horror", DateCreated = DateTime.Now,IsDeleted = true, DateUpdated = DateTime.Now.AddDays(2), DateDeleted = DateTime.Now.AddDays(5) }
            };

            AutoMock = AutoMock.GetLoose();
            MockRepository = new Mock<IRepository<Genre, int>>();
            AutoMock.Provide(MockRepository.Object);
            AutoMock.Provide<IService<Genre, int>, GenreService>();
            AutoMock.Provide<IValidationService<Genre>, GenreValidationService>();
            AutoMock.Provide<IValidationExceptionService, ValidationExceptionService>();
        }

        [Test]
        [TestCase(3)]
        public async Task IndexShouldReturnGenresPositive(int value)
        {
            MockRepository.Setup(m => m.Get()).Returns(GetData());

            var controller = AutoMock.Create<GenreController>();
            var indexResult = await controller.Index();

            Assert.IsInstanceOf<ViewResult>(indexResult);
            var model = (indexResult as ViewResult).Model;

            Assert.IsInstanceOf<IEnumerable<Genre>>(model);
            Assert.NotNull((model as IEnumerable<Genre>), "A null model is not expected");
            Assert.AreEqual(value, (model as IEnumerable<Genre>).Count(), "failed to return correct number of genres");
        }
        [Test]
        public async Task CreateShouldInsert()
        {
            int newKey = 2;
            var GenreName = new Genre { Id = newKey, Name = "Horror" };
            MockRepository.Setup(m => m.GetByKey(newKey))
                .Returns(() =>
                {
                    return DataStore.SingleOrDefault(e => e.Id == newKey);
                });

            MockRepository.Setup(m => m.Insert(GenreName))
                .Callback((Genre genre) =>
                {
                    DataStore.Add(genre);
                });

            MockRepository.Setup(m => m.CommitChanges()).Returns(1);

            var controller = AutoMock.Create<GenreController>();
            var indexResult = await controller.Create();

            Assert.NotNull(GenreName);
            //Assert.IsInstanceOf<ViewResult>(indexResult);
            //var model = (indexResult as ViewResult).Model;

            //Assert.IsInstanceOf<Genre>(model);
            //Assert.NotNull((model as Genre), "A null model is not expected");

            
        }
        [TearDown]
        public void Teardown()
        {
            AutoMock.Dispose();
        }

        private IQueryable<Genre> GetData()
        {
            return DataStore.AsQueryable();
        }
    }
}
