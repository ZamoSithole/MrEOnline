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
    public class GenreServiceTests {

        protected MockRepositorySetup<Genre> MockRepositorySetup { get; set; }
        public AutoMock AutoMock { get; set; }
        protected IService<Genre, int> GenreService { get; set; }

        [SetUp]
        public void Setup() {
            MockRepositorySetup = new MockRepositorySetup<Genre>(
                new Mock<IRepository<Genre, int>>(),
                (new List<Genre>() {
                    new Genre{ Id=1,
                    Name="Horror",
                    DateCreated = DateTime.Now,
                    IsDeleted = false},

                    new Genre{ Id=2,
                    Name="Action",
                    DateCreated = DateTime.Now,
                    IsDeleted = false,
                    DateUpdated = DateTime.Now.AddDays(2)},

                    new Genre{ Id=3,
                    Name="Comedy",
                    DateCreated = DateTime.Now,
                    IsDeleted = true,
                    DateUpdated = DateTime.Now.AddDays(2),
                    DateDeleted = DateTime.Now.AddDays(5)}
                })).Setup();
            AutoMock = AutoMock.GetLoose();
            AutoMock.Provide(MockRepositorySetup.MockRepository.Object);
            AutoMock.Provide<IValidationService<Genre>, GenreValidationService>();
            AutoMock.Provide<IValidationExceptionService, ValidationExceptionService>();
            GenreService = AutoMock.Create<GenreService>();
        }

        [Test]
        [TestCase(3)]
        public void GetReturnsCorrectNumberOfItems(int expected) {
            var actual = GenreService.Get().Count();
            Assert.AreEqual(expected, actual, "Get failed to return correct number of rows");
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void GetByKeyShouldPass(int value) {
            var actual = GenreService.GetByKey(value);
            Assert.IsNotNull(actual, "GetByKey Failed to return correct data.");
        }
        //Negative Test
        [Test]
        [TestCase(10000)]
        public void GetByKeyShouldFail(int value) {
            var actual = GenreService.GetByKey(value);
            Assert.IsNull(actual, "GetByKey failed to inccorect data");
        }

        [Test]
        public void InsertShouldPass() {
            var genre = new Genre {
                Id = 4,
                Name = "Romance",
                DateCreated = DateTime.Now,
                IsDeleted = false
            };
            var actual = GenreService.Insert(genre);
            Assert.AreEqual(genre, actual, "Insert failed to insert correctly");
        }

        [Test]
        [TestCase(1)]
        public void UpdateShouldPass(int value) {
            var genre = GenreService.GetByKey(value);
            var OldDateUpdated = genre.DateUpdated;
            var oldName = genre.Name;

            genre.Name = DateTime.Now.GetHashCode().ToString();

            GenreService.Update(genre);

            Assert.AreNotEqual(oldName, genre.Name);
            Assert.AreNotEqual(OldDateUpdated, genre.DateUpdated);
        }

        [Test]
        public void DeleteShouldPass() {
            var genre = GenreService.Get().FirstOrDefault(e => !e.IsDeleted);
            var oldIsDeleted = genre.IsDeleted;
            var oldDate = genre.DateDeleted;

            GenreService.Delete(genre);
            Assert.AreNotEqual(oldDate, genre.DateDeleted);
            Assert.AreEqual(true, genre.IsDeleted);
        }

        [Test]
        public void RecoverShouldPass() {
            var genre = GenreService.Get().FirstOrDefault(e => e.IsDeleted);
            var OldIsDeleted = genre.IsDeleted;
            var oldDateUpdate = genre.DateUpdated;

            GenreService.Recover(genre);
            Assert.AreNotEqual(oldDateUpdate, genre.DateUpdated);
            Assert.AreEqual(false, genre.IsDeleted);
            Assert.IsNull(genre.DateDeleted);
        }

        [TearDown]
        public void TearDown() {
            AutoMock?.Dispose();
        }
    }
}
