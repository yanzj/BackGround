using System;
using Abp.EntityFramework.AutoMapper;
using Abp.Reflection;
using AutoMapper;
using Lte.Evaluations.Policy;
using Lte.Parameters.Entities.Kpi;
using NUnit.Framework;
using Shouldly;

namespace Lte.Evaluations.MapperService
{
    [TestFixture]
    public class PreciseMongoMappingTest
    {
        private readonly ITypeFinder _typeFinder = new TypeFinder(new MyAssemblyFinder());

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            var module = new AbpAutoMapperModule(_typeFinder);
            module.PostInitialize();
        }

        [Test]
        public void Test()
        {
            var mongo = new PreciseMongo
            {
                CellId = "123-45",
                StatDate = new DateTime(2016,7,11)
            };
            var stat = Mapper.Map<PreciseMongo, PreciseCoverage4G>(mongo);
            Assert.IsNotNull(stat);
            stat.CellId.ShouldBe(123);
            stat.SectorId.ShouldBe((byte)45);
            stat.StatTime.ShouldBe(new DateTime(2016, 7, 11));
        }
    }
}
