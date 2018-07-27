using Moq;
using MrE.Models.Abstractions;
using MrE.Repository;
using MrE.Repository.Abstractions;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace TestUtils {
    public class MockRepositorySetup<T> where T : class {
        public Mock<IRepository<T, int>> MockRepository { get; set; }
        private List<T> DataStore { get; set; }

        public MockRepositorySetup(
            Mock<IRepository<T, int>> mockRepository,
            List<T> dataStore
            ) {
            MockRepository = mockRepository;
            DataStore = dataStore;
        }

        public MockRepositorySetup<T> Setup() {
            MockRepository.Setup(m => m.Get())
                .Returns(DataStore.AsQueryable());

            MockRepository.Setup(m => m.GetByKey(It.IsAny<int>()))
                .Returns<int>(e => DataStore.SingleOrDefault(item => (item as IBaseEntity<int>).Id == e));

            MockRepository.Setup(m => m.Insert(It.IsAny<T>()))
                .Callback((T item) => {
                    DataStore.Add(item);
                });

            MockRepository.Setup(m => m.Update(It.IsAny<T>()))
                .Callback((T item) => {
                    DataStore[
                        DataStore.FindIndex(
                        e => (e as IBaseEntity<int>).Id == (item as IBaseEntity<int>).Id)
                        ] = item;
                });

            MockRepository.Setup(m => m.Delete(It.IsAny<T>()))
                .Callback((T item) => {
                    (item as IDeletable).IsDeleted = true;

                    DataStore[
                        DataStore.FindIndex(
                        e => (e as IBaseEntity<int>).Id == (item as IBaseEntity<int>).Id)
                        ] = item;
                });

            MockRepository.Setup(m => m.CommitChanges()).Returns(1);
            return this;
        }
    }
    

    public sealed class SetupMockContext<T>  where T : class {
        private Mock<DbSet<T>> MockSet { get; set; }
        public Mock<DataStoreContext> MockContext { get; set; }
        private IQueryable<T> DataStore { get; set; }

        public SetupMockContext(
            Mock<DataStoreContext> mockContext,
            IQueryable<T> dataStore
            ) {
            MockSet = new Mock<DbSet<T>>();
            MockContext = mockContext;
            DataStore = dataStore;
        }

        private void SetupMockSet() {
            MockSet.As<IDbAsyncEnumerable<T>>()
              .Setup(m => m.GetAsyncEnumerator())
              .Returns(new TestDbAsyncEnumerator<T>(DataStore.GetEnumerator()));

            MockSet.As<IQueryable<T>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<T>(DataStore.Provider));

            MockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(DataStore.Expression);
            MockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(DataStore.ElementType);
            MockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(DataStore.GetEnumerator());
            MockSet.Setup(m => m.Add(It.IsAny<T>()))
                .Callback((T item) => {
                    DataStore.ToList().Add(item);
                });
            MockSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(key => DataStore.SingleOrDefault(e => (e as IBaseEntity<int>).Id == (int)key[0]));            
        }

        public SetupMockContext<T> Setup() {
            SetupMockSet();

            MockContext = new Mock<DataStoreContext>();
            MockContext.Setup(m => m.Set<T>()).Returns(MockSet.Object);
            MockContext.Setup(m => m.Update(It.IsAny<T>()))
                .Returns<T>(e => e);
            MockContext.Setup(m => m.SaveChanges()).Returns(1);

            return this;
        }
    }
}