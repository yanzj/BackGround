using Abp.EntityFramework.AutoMapper;
using Abp.Reflection;
using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.MockItems;
using Lte.Evaluations.Policy;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Entities.Basic;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Entities;
using Lte.Evaluations.ViewModels.RegionKpi;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;

namespace Lte.Evaluations.DataService.Queries
{
    [TestFixture]
    public class ENodebQueryServiceTest
    {
        private readonly ITypeFinder _typeFinder = new TypeFinder(new MyAssemblyFinder());
        private readonly Mock<ITownRepository> _townRepository = new Mock<ITownRepository>();
        private readonly Mock<IENodebRepository> _eNodebRepository = new Mock<IENodebRepository>();
        private ENodebQueryService _service;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            var module = new AbpAutoMapperModule(_typeFinder);
            module.PostInitialize();
            _service = new ENodebQueryService(_townRepository.Object, _eNodebRepository.Object, null, null, null);
            _townRepository.MockSixTowns();
            _townRepository.MockGetId<ITownRepository, Town>();
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 1)]
        [TestCase(4, 0)]
        [TestCase(6, 0)]
        [TestCase(13, 0)]
        [TestCase(24, 0)]
        public void Test_GetByTownNames(int townId, int count)
        {
            _eNodebRepository.MockThreeENodebs();
            _eNodebRepository.MockOperations();
            _eNodebRepository.MockGetId<IENodebRepository, ENodeb>();
            var eNodebList = _service.GetByTownNames("city-" + townId, "district-" + townId, "town-" + townId) ?? new List<ENodebView>();
            Assert.AreEqual(eNodebList.Count(), count);
        }

        [TestCase("ENodeb", 3)]
        [TestCase("1", 1)]
        [TestCase("2", 1)]
        [TestCase("3", 1)]
        [TestCase("Address", 3)]
        [TestCase("FSL", 3)]
        public void Test_GetByGeneralName(string queryString, int count)
        {
            _eNodebRepository.MockThreeENodebs();
            _eNodebRepository.MockOperations();
            _eNodebRepository.MockGetId<IENodebRepository, ENodeb>();
            var eNodebList = _service.GetByGeneralName(queryString);
            Assert.AreEqual(eNodebList.Count(), count);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void Test_GetByBtsId(int id)
        {
            _eNodebRepository.MockThreeENodebs();
            _eNodebRepository.MockOperations();
            _eNodebRepository.MockGetId<IENodebRepository, ENodeb>();
            var eNodeb = _service.GetByENodebId(id);
            Assert.AreEqual(eNodeb.Name, "ENodeb-" + id);
        }
    }
}
