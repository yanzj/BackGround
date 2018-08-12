using Abp.EntityFramework.AutoMapper;
using Abp.Reflection;
using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.MockItems;
using Lte.Evaluations.Policy;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Cdma;
using Abp.EntityFramework.Entities.Region;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;

namespace Lte.Evaluations.DataService.Queries
{
    [TestFixture]
    public class BtsQueryServiceTest
    {
        private readonly ITypeFinder _typeFinder = new TypeFinder(new MyAssemblyFinder());
        private readonly Mock<ITownRepository> _townRepository = new Mock<ITownRepository>();
        private readonly Mock<IBtsRepository> _btsRepository = new Mock<IBtsRepository>();
        private BtsQueryService _service;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            var module = new AbpAutoMapperModule(_typeFinder);
            module.PostInitialize();
            _service = new BtsQueryService(_townRepository.Object, _btsRepository.Object, null);
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
            _btsRepository.MockThreeBtss();
            _btsRepository.MockOperation();
            _btsRepository.MockGetId<IBtsRepository, CdmaBts>();
            var btsList = _service.GetByTownNames("city-" + townId, "district-" + townId, "town-" + townId) ?? new List<CdmaBtsView>();
            Assert.AreEqual(btsList.Count(), count);
        }

        [TestCase(1, 1, new[] {1, 2, 3})]
        [TestCase(1, 2, new[] { 1, 1, 3 })]
        [TestCase(2, 1, new[] { 1, 2, 3 })]
        [TestCase(2, 3, new[] { 2, 2, 2 })]
        [TestCase(3, 1, new[] { 1, 2, 3 })]
        [TestCase(4, 0, new[] { 1, 2, 3 })]
        [TestCase(6, 0, new[] { 1, 2, 3 })]
        [TestCase(13, 0, new[] { 1, 2, 3 })]
        [TestCase(24, 0, new[] { 1, 2, 3 })]
        public void Test_GetByTownNames_TownIdAssigned(int townId, int count, int[] assighedTownIds)
        {
            _btsRepository.MockThreeBtss(assighedTownIds);
            _btsRepository.MockOperation();
            _btsRepository.MockGetId<IBtsRepository, CdmaBts>();
            var btsList = _service.GetByTownNames("city-" + townId, "district-" + townId, "town-" + townId) ?? new List<CdmaBtsView>();
            Assert.AreEqual(btsList.Count(), count);
        }

        [TestCase("Bts", 3)]
        [TestCase("1", 1)]
        [TestCase("2", 1)]
        [TestCase("3", 1)]
        [TestCase("Address", 3)]
        public void Test_GetByGeneralName(string queryString, int count)
        {
            _btsRepository.MockThreeBtss();
            _btsRepository.MockOperation();
            _btsRepository.MockGetId<IBtsRepository, CdmaBts>();
            var btsList = _service.GetByGeneralName(queryString);
            Assert.AreEqual(btsList.Count(), count);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void Test_GetByBtsId(int id)
        {
            _btsRepository.MockThreeBtss();
            _btsRepository.MockOperation();
            _btsRepository.MockGetId<IBtsRepository, CdmaBts>();
            var bts = _service.GetByBtsId(id);
            Assert.AreEqual(bts.Name, "Bts-" + id);
        }
    }
}
