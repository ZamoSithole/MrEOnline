using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Autofac.Extras.Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MrE.Models.Entities;
using MrE.Repository;
using MrE.Repository.Abstractions;
using MrE.Services.Abstractions;
using MrE.Services.Validations;
using NUnit.Framework;
using TestUtils;

namespace MrE.Services.Tests {

    [TestFixture]
    public class CastServiceTests {
        private TestUtil TestUtil { get; set; }
        [SetUp]
        public void Setup() {
            TestUtil = new TestUtil();
            TestUtil.Setup();

            TestUtil.AutoMock.Provide<IRepository<Cast>, Repository<Cast>>();
            TestUtil.AutoMock.Provide<IRepository<Title>, Repository<Title>>();
            TestUtil.AutoMock.Provide<IService<Cast>, CastService>();
            TestUtil.AutoMock.Provide<IService<Title>, TitleService>();
            TestUtil.AutoMock.Provide<IValidationService<Cast>, CastValidationService>();
            TestUtil.AutoMock.Provide<IValidationExceptionService, ValidationExceptionService>();
        }
        
        [Test]
        public void TestMethod1() {
        }        
    }
}
