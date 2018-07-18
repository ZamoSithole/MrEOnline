using Autofac.Extras.Moq;
using Moq;
using MrE.Models.Entities;
using MrE.Repository;
using MrE.Repository.Abstractions;
using MrE.Services;
using MrE.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUtils
{
    public class TestUtil
    {
        public  AutoMock AutoMock { get; set; }
        private  IRepository<Cast> Repository { get; set; }
        private  List<Cast> CastDataStore { get; set; }
        private  Mock<DbSet<Title>> MockTitleSet { get; set; }
        private  Mock<DbSet<Cast>> MockCastSet { get; set; }
        private  Mock<DataStoreContext> MockContext { get; set; }
        public  void SetupTitlesMockDbSet() {
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

        public  void SetupCastMockDbSet() {
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

        public  IQueryable<Cast> GetData() {
            CastDataStore = new List<Cast>()
            {
                new Cast{Id = 1, FirstName = "Action",LastName="Hello", VideoId=1, DateCreated = DateTime.Now, IsDeleted = false , TitleId= 1},
                new Cast{Id =2, FirstName = "Comedy",LastName="bye",VideoId=1, DateCreated = DateTime.Now,IsDeleted = false, DateUpdated = DateTime.Now.AddDays(2), TitleId= 1 },
                new Cast{Id =3, FirstName = "Horror",LastName="Great",VideoId=1, DateCreated = DateTime.Now,IsDeleted = true, DateUpdated = DateTime.Now.AddDays(2), DateDeleted = DateTime.Now.AddDays(5) , TitleId= 1}
            };
            return CastDataStore.AsQueryable();
        }

        public void Setup() {
            SetupTitlesMockDbSet();
            SetupCastMockDbSet();
            SetupMockContext();

            AutoMock = AutoMock.GetLoose();
            AutoMock.Provide(MockContext.Object);
        }

        public  IQueryable<Title> GetTitles() {
            return (new List<Title>() {
                new Title{Name = "Actor", DateCreated = DateTime.Now},
                new Title{Name = "Director", DateCreated = DateTime.Now},
                new Title{Name = "Producer", DateCreated = DateTime.Now},
            }).AsQueryable();
        }

        public  void SetupMockContext() {
            MockContext = new Mock<DataStoreContext>();
            MockContext.Setup(m => m.Set<Title>()).Returns(MockTitleSet.Object);
            MockContext.Setup(m => m.Set<Cast>()).Returns(MockCastSet.Object);
            MockContext.Setup(m => m.SaveChanges()).Returns(1);

            MockContext.Setup(c => c.Titles).Returns(MockTitleSet.Object);
            MockContext.Setup(c => c.Casts).Returns(MockCastSet.Object);
        }
    }
}
