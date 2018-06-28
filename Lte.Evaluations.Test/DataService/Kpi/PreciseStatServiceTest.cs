using Abp.EntityFramework.AutoMapper;
using Abp.Reflection;
using Lte.Domain.Common.Wireless;
using Lte.Evaluations.MockItems;
using Lte.Evaluations.Policy;
using Lte.Parameters.Abstract.Kpi;
using Lte.Parameters.Entities.Kpi;
using Lte.Parameters.MockOperations;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Entities;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;

namespace Lte.Evaluations.DataService.Kpi
{
    [TestFixture]
    public class PreciseStatServiceTest
    {
        private readonly ITypeFinder _typeFinder = new TypeFinder(new MyAssemblyFinder());
        private readonly Mock<IPreciseCoverage4GRepository> _repository = new Mock<IPreciseCoverage4GRepository>();
        private readonly Mock<IENodebRepository> _eNodebRepository = new Mock<IENodebRepository>();
        private readonly Mock<ICellRepository> _cellRepository = new Mock<ICellRepository>();
        private readonly Mock<IInfrastructureRepository> _infrastructure = new Mock<IInfrastructureRepository>(); 
        private PreciseStatService _service;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            var module = new AbpAutoMapperModule(_typeFinder);
            module.PostInitialize();
            _service = new PreciseStatService(_repository.Object, _eNodebRepository.Object);
            _repository.MockOperations();
            _eNodebRepository.MockOperations();
            _eNodebRepository.MockGetId<IENodebRepository, ENodeb>();
            _eNodebRepository.MockThreeENodebs();
            _cellRepository.MockGetId<ICellRepository, Cell>();
            _cellRepository.MockOperations();
            _cellRepository.MockSixCells();
            _infrastructure.MockOperations();
            _infrastructure.MockSixCollegeCdmaCells();
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-2)]
        [TestCase(-3)]
        public void Test_GetTopCountViews_IllegalTopCounts(int topCount)
        {
            _repository.MockQueryItems(new List<PreciseCoverage4G>
            {
                new PreciseCoverage4G
                {
                    CellId = 1,
                    SectorId = 1,
                    StatTime = DateTime.Parse("2015-1-1")
                }
            }.AsQueryable());
            var views = _service.GetTopCountViews(DateTime.Parse("2014-12-30"), DateTime.Parse("2015-1-4"), topCount, 
                OrderPreciseStatPolicy.OrderBySecondRate);
            Assert.AreEqual(views.Count(), 0);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void Test_GetTopCountViews_LegalTopCounts(int topCount)
        {
            _repository.MockQueryItems(new List<PreciseCoverage4G>
            {
                new PreciseCoverage4G
                {
                    CellId = 1,
                    SectorId = 1,
                    StatTime = DateTime.Parse("2015-1-1"),
                    TotalMrs = 5000
                }
            }.AsQueryable());
            var views = _service.GetTopCountViews(DateTime.Parse("2014-12-30"), DateTime.Parse("2015-1-4"), topCount,
                OrderPreciseStatPolicy.OrderBySecondRate);
            Assert.AreEqual(views.Count(), 1);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void Test_GetTopCountStats(int topCount)
        {
            _repository.MockQueryItems(new List<PreciseCoverage4G>
            {
                new PreciseCoverage4G
                {
                    CellId = 1,
                    SectorId = 1,
                    StatTime = DateTime.Parse("2015-1-1"),
                    TotalMrs = 5000
                }
            }.AsQueryable());
            var stats = _service.GetTopCountViews(DateTime.Parse("2014-12-30"), DateTime.Parse("2015-1-4"), topCount,
                OrderPreciseStatPolicy.OrderBySecondRate);
            Assert.AreEqual(stats.Count(), 1);
        }
        
        [TestCase(new[] {"2015-2-1", "2015-2-2" }, new[] { "2015-2-1", "2015-2-2" }, "2015-1-31", "2015-2-3")]
        [TestCase(new[] { "2015-2-2", "2015-2-1" }, new[] { "2015-2-1", "2015-2-2" }, "2015-1-31", "2015-2-3")]
        public void Test_GetTimeSpanStats(string[] soureDates, string[] resultDates, string beginDate, string endDate)
        {
            _repository.MockQueryItems(soureDates.Select(x=>new PreciseCoverage4G
            {
                CellId = 1,
                SectorId = 1,
                StatTime = DateTime.Parse(x)
            }).AsQueryable());
            var stats = _service.GetTimeSpanStats(1, 1, DateTime.Parse(beginDate), DateTime.Parse(endDate));
            stats.Select(x => x.StatTime).ShouldBe(resultDates.Select(DateTime.Parse));
        }
    }
}
