using Autofac.Extras.Moq;
using Moq;
using MrE.Models.Entities;
using MrE.Repository;
using MrE.Repository.Abstractions;
using MrE.Services;
using MrE.Services.Abstractions;
using MrE.Services.Validations;
using MrEOnline.Controllers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MrEOnline.Tests.UnitTests {
    [TestFixture]
    public class CastControllerTests {
        private BaseController<Cast, int> Controller { get; set; }

        private AutoMock AutoMock { get; set; }
        private IRepository<Cast, int> Repository { get; set; }
        private List<Cast> DataStore { get; set; }
        private Mock<DbSet<Title>> MockTitleSet { get; set; }
        private Mock<DbSet<Cast>> MockCastSet { get; set; }

        private Mock<DataStoreContext> MockContext { get; set; }        

        [SetUp]
        public void Setup() {
            DataStore = new List<Cast>()
            {
                new Cast{Id = 1, FirstName = "Action",LastName="Hello", VideoId=1, DateCreated = DateTime.Now, IsDeleted = false , TitleId= 1},
                new Cast{Id =2, FirstName = "Comedy",LastName="bye",VideoId=1, DateCreated = DateTime.Now,IsDeleted = false, DateUpdated = DateTime.Now.AddDays(2), TitleId= 1 },
                new Cast{Id =3, FirstName = "Horror",LastName="Great",VideoId=1, DateCreated = DateTime.Now,IsDeleted = true, DateUpdated = DateTime.Now.AddDays(2), DateDeleted = DateTime.Now.AddDays(5) , TitleId= 1}
            };
            SetupTitlesMockDbSet();
            SetupCastMockDbSet();

            MockContext = new Mock<DataStoreContext>();
            MockContext.Setup(m => m.Set<Title>()).Returns(MockTitleSet.Object);
            MockContext.Setup(m => m.Set<Cast>()).Returns(MockCastSet.Object);
            MockContext.Setup(m => m.SaveChanges()).Returns(1);

            MockContext.Setup(c => c.Titles).Returns(MockTitleSet.Object);
            MockContext.Setup(c => c.Casts).Returns(MockCastSet.Object);

            AutoMock = AutoMock.GetLoose();            
            AutoMock.Provide(MockContext.Object);
            AutoMock.Provide<IRepository<Cast, int>, Repository<Cast, int>>();
            AutoMock.Provide<IRepository<Title, int>, Repository<Title, int>>();
            AutoMock.Provide<IService<Cast, int>, CastService>();
            AutoMock.Provide<IService<Title, int>, TitleService>();
            AutoMock.Provide<IValidationService<Cast>, CastValidationService>();
            AutoMock.Provide<IValidationExceptionService, ValidationExceptionService>();
            Controller = AutoMock.Create<CastController>();
        }
       
        [Test]
        [TestCase(3)]
        public async Task IndexShouldReturnCast(int value) {
            var indexResult = await Controller.Index();

            Assert.IsInstanceOf<ViewResult>(indexResult);
            var model = (indexResult as ViewResult).Model;

            Assert.IsInstanceOf<IEnumerable<Cast>>(model);
            Assert.NotNull((model as IEnumerable<Cast>), "A null model is not expected");
            Assert.AreEqual(value, (model as IEnumerable<Cast>).Count(), "failed to return correct number of casts");
        }

        [Test]
        public async Task CreateShouldReturnView() {

        }

        [Test]
        public async Task CreateShouldInsert() {
            int newKey = 4;
            var cast = new Cast {
                Id = newKey,
                FirstName = "Action",
                LastName = "Hello",
                VideoId = 1,
                DateCreated = DateTime.Now,
                IsDeleted = false,
                TitleId = 1};

            var indexResult = await Controller.Create(newKey);
            Assert.AreEqual(4, DataStore.Count(), "Failed to insert cast.");
        }

        [TearDown]
        public void Teardown() {
            AutoMock?.Dispose();
            Controller?.Dispose();
        }

        private void SetupTitlesMockDbSet() {
            var titles = GetTitles();

            MockTitleSet = new Mock<DbSet<Title>>();

            MockTitleSet.As<IDbAsyncEnumerable<Title>>()
               .Setup(m => m.GetAsyncEnumerator())
               .Returns(new TestDbAsyncEnumerator<Title>(titles.GetEnumerator()));

            MockTitleSet.As<IQueryable<Title>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Title>(titles.Provider));

            MockTitleSet.As<IQueryable<Title>>().Setup(m => m.Expression).Returns(titles.Expression);
            MockTitleSet.As<IQueryable<Title>>().Setup(m => m.ElementType).Returns(titles.ElementType);
            MockTitleSet.As<IQueryable<Title>>().Setup(m => m.GetEnumerator()).Returns(titles.GetEnumerator());            
        }

        private void SetupCastMockDbSet() {
            var casts = GetData();
            MockCastSet = new Mock<DbSet<Cast>>();

            MockCastSet.As<IDbAsyncEnumerable<Cast>>()
               .Setup(m => m.GetAsyncEnumerator())
               .Returns(new TestDbAsyncEnumerator<Cast>(casts.GetEnumerator()));

            MockCastSet.As<IQueryable<Cast>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Cast>(casts.Provider));

            MockCastSet.As<IQueryable<Cast>>().Setup(m => m.Expression).Returns(casts.Expression);
            MockCastSet.As<IQueryable<Cast>>().Setup(m => m.ElementType).Returns(casts.ElementType);
            MockCastSet.As<IQueryable<Cast>>().Setup(m => m.GetEnumerator()).Returns(casts.GetEnumerator());
        }

        private IQueryable<Cast> GetData() {
            return DataStore.AsQueryable();
        }

        private IQueryable<Title> GetTitles() {
            return (new List<Title>() {
                new Title{Name = "Actor", DateCreated = DateTime.Now},
                new Title{Name = "Director", DateCreated = DateTime.Now},
                new Title{Name = "Producer", DateCreated = DateTime.Now},
            }).AsQueryable();
        }

    }
}