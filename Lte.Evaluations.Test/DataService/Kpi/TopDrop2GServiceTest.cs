using Abp.EntityFramework.AutoMapper;
using Abp.Reflection;
using Lte.Evaluations.MockItems;
using Lte.Evaluations.Policy;
using Lte.Evaluations.TestService;
using Lte.MySqlFramework.Abstract;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Linq;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Cdma;
using Abp.EntityFramework.Entities.Infrastructure;
using Lte.MySqlFramework.Entities;

namespace Lte.Evaluations.DataService.Kpi
{
    [TestFixture]
    public class TopDrop2GServiceTest
    {
        private AbpAutoMapperModule _module;
        private TypeFinder _typeFinder;
        private readonly Mock<ITopDrop2GCellRepository> _repository = new Mock<ITopDrop2GCellRepository>();
        private readonly Mock<IBtsRepository> _btsRepository = new Mock<IBtsRepository>();
        private readonly Mock<IENodebRepository> _eNodebRepository = new Mock<IENodebRepository>();
        private TopDrop2GService _service;
        private TopDrop2GTestService _testService;

        [TestFixtureSetUp]
        public void TestFixtureService()
        {
            _typeFinder = new TypeFinder(new MyAssemblyFinder());
            _module = new AbpAutoMapperModule(_typeFinder);
            _module.PostInitialize();
            _service = new TopDrop2GService(_repository.Object, _btsRepository.Object, _eNodebRepository.Object);
            _testService = new TopDrop2GTestService(_repository, _btsRepository, _eNodebRepository);
            _repository.MockOperation();
            _btsRepository.MockOperation();
            _btsRepository.MockGetId<IBtsRepository, CdmaBts>();
            _eNodebRepository.MockOperations();
            _eNodebRepository.MockGetId<IENodebRepository, ENodeb>();
            _eNodebRepository.MockThreeENodebs();
            _btsRepository.MockSixBtssWithENodebId();
        }

        [TestCase(1, 2, 3, 111, "ENodeb-1", "Bts-1")]
        [TestCase(2, 2, 3, 211, "ENodeb-2", "Bts-2")]
        [TestCase(3, 4, 7, 131, "ENodeb-3", "Bts-3")]
        [TestCase(4, 21, 32, 1611, "无匹配LTE基站", "Bts-4")]
        [TestCase(5, 21, 322, 1611, "无匹配LTE基站", "Bts-5")]
        [TestCase(6, 7, 32, 1611, "无匹配LTE基站", "Bts-6")]
        [TestCase(7, 21, 32, 1611, "无匹配LTE基站", "无匹配CDMA基站")]
        public void Test_GetViews_SingleStat(int btsId, byte sectorId, int drops, int assignmentSuccess,
            string lteName, string cdmaName)
        {
            _testService.ImportOneStat(btsId, sectorId, drops, assignmentSuccess);
            var dateView = _service.GetDateView(DateTime.Parse("2015-1-1"), "Foshan");
            var views = dateView.StatViews;
            Assert.AreEqual(views.Count(), 1);
            views.ElementAt(0).AssertEqual(sectorId, drops, assignmentSuccess, lteName, cdmaName);
        }

        [TestCase(1, 2, 3, 111, "ENodeb-1", "Bts-1-2")]
        [TestCase(2, 2, 3, 211, "ENodeb-2", "Bts-2-2")]
        [TestCase(3, 4, 7, 131, "ENodeb-3", "Bts-3-4")]
        [TestCase(4, 21, 32, 1611, "无匹配LTE基站", "Bts-4-21")]
        [TestCase(5, 21, 322, 1611, "无匹配LTE基站", "Bts-5-21")]
        [TestCase(6, 7, 32, 1611, "无匹配LTE基站", "Bts-6-7")]
        [TestCase(7, 21, 32, 1611, "无匹配LTE基站", "无匹配CDMA基站-21")]
        public void Test_GetTrendViews_SingleStat(int btsId, byte sectorId, int drops, int assignmentSuccess,
            string eNodebName, string cellName)
        {
            _testService.ImportOneStat(btsId, sectorId, drops, assignmentSuccess);
            var views = _service.GetTrendViews(DateTime.Parse("2014-12-31"), DateTime.Parse("2015-1-2"), "Foshan");
            views.Count().ShouldBe(1);
            views.ElementAt(0).AssertEqual(drops, assignmentSuccess, eNodebName, cellName, 1);
        }
    }
}
