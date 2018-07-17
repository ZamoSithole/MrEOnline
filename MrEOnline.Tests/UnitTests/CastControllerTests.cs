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
    public class CastControllerTests
    {
        private AutoMock AutoMock { get; set; }
        private Mock<IRepository<Cast>> MockRepository { get; set; }
        private List<Cast> DataStore { get; set; }

        [SetUp]
        public void Setup()
        {
            DataStore = new List<Cast>()
            {
                new Cast{Id = 1, FirstName = "Action",LastName="Hello", VideoId=1, DateCreated = DateTime.Now, IsDeleted = false , TitleId= 1},
                new Cast{Id =2, FirstName = "Comedy",LastName="bye",VideoId=1, DateCreated = DateTime.Now,IsDeleted = false, DateUpdated = DateTime.Now.AddDays(2), TitleId= 1 },
                new Cast{Id =3, FirstName = "Horror",LastName="Great",VideoId=1, DateCreated = DateTime.Now,IsDeleted = true, DateUpdated = DateTime.Now.AddDays(2), DateDeleted = DateTime.Now.AddDays(5) , TitleId= 1}
            };
            AutoMock = AutoMock.GetLoose();
            MockRepository = new Mock<IRepository<Cast>>();
            AutoMock.Provide(MockRepository.Object);
            AutoMock.Provide<IService<Cast>, CastService>();
            AutoMock.Provide<IValidationService<Cast>, CastValidationService>();
            AutoMock.Provide<IValidationExceptionService, ValidationExceptionService>();
        }
        [Test]
        [TestCase(3)]
        public async Task IndexShouldReturnCast(int value)
        {

            MockRepository.Setup(m => m.Get()).Returns(GetData());

            var controller = AutoMock.Create<CastController>();
            var indexResult = await controller.Index();

            Assert.IsInstanceOf<ViewResult>(indexResult);
            var model = (indexResult as ViewResult).Model;

            Assert.IsInstanceOf<IEnumerable<Cast>>(model);
            Assert.NotNull((model as IEnumerable<Cast>), "A null model is not expected");
            Assert.AreEqual(value, (model as IEnumerable<Cast>).Count(), "failed to return correct number of casts");
        }
        [Test]
        public async Task CreateShouldInsert()
        {
            int newKey = 2;
            var CastMember = new Cast { Id = newKey, FirstName = "Action", LastName = "Hello", VideoId = 1, DateCreated = DateTime.Now, IsDeleted = false , TitleId= 1};
            MockRepository.Setup(m => m.GetByKey(newKey))
                .Returns(() =>
                {
                    return DataStore.SingleOrDefault(e => e.Id == newKey);
                });

            MockRepository.Setup(m => m.Insert(CastMember))
                .Callback((Cast cast) =>
                {
                    DataStore.Add(cast);
                });

            MockRepository.Setup(m => m.CommitChanges()).Returns(1);

            var controller = AutoMock.Create<CastController>();
            var indexResult = await controller.Create();

            Assert.NotNull(CastMember);

        }
        [TearDown]
        public void Teardown()
        {
            AutoMock.Dispose();
        }
        private IQueryable<Cast> GetData()
        {
            return DataStore.AsQueryable();
        }
    }
}
