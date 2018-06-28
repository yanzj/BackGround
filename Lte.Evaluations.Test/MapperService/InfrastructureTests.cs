using Abp.EntityFramework.AutoMapper;
using Abp.Reflection;
using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.MockItems;
using Lte.Evaluations.Policy;
using Lte.Evaluations.ViewModels.Precise;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;
using Moq;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Abp.EntityFramework.Entities;
using Lte.Domain.Common.Geo;
using Lte.Evaluations.ViewModels.RegionKpi;

namespace Lte.Evaluations.MapperService
{
    [TestFixture]
    public class QuerySectorsTest
    {
        private readonly Mock<ICellRepository> _repository = new Mock<ICellRepository>();
        private readonly Mock<IENodebRepository> _eNodebRepository = new Mock<IENodebRepository>();
        private CellService _service;
        private readonly ITypeFinder _typeFinder = new TypeFinder(new MyAssemblyFinder());

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            var module = new AbpAutoMapperModule(_typeFinder);
            module.PostInitialize();
            _eNodebRepository.MockThreeENodebs();
            _repository.MockRangeCells();
            _service = new CellService(_repository.Object, _eNodebRepository.Object);
        }

        [Test]
        public void TestQuerySectors()
        {
            var sectors = _service.QuerySectors(113, 114, 22, 23);
            Assert.AreEqual(sectors.Count(), 6);
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 1)]
        [TestCase(3, 2)]
        [TestCase(3, 3)]
        public void TestQuerySectors_ExcludedOneCell(int eNodebId, byte sectorId)
        {
            var container = new SectorRangeContainer
            {
                West = 113,
                East = 114,
                South = 22,
                North = 23,
                ExcludedCells = new List<CellIdPair>
                {
                    new CellIdPair {CellId = eNodebId, SectorId = sectorId}
                }
            };
            var sectors = _service.QuerySectors(container);
            Assert.AreEqual(sectors.Count(), 5);
        }

        [Test]
        public void Test_GetExceptCells()
        {
            var container = new SectorRangeContainer
            {
                West = 113,
                East = 114,
                South = 22,
                North = 23,
                ExcludedCells = new List<CellIdPair>
                {
                    new CellIdPair {CellId = 1, SectorId = 2}
                }
            };
            var cells = new List<Cell>
            {
                new Cell {ENodebId = 1, SectorId = 2},
                new Cell {ENodebId = 3, SectorId = 4}
            };
            var excludeCells = from cell in cells
                join sector in container.ExcludedCells on new
                {
                    CellId = cell.ENodebId,
                    cell.SectorId
                } equals new
                {
                    sector.CellId,
                    sector.SectorId
                }
                select cell;
            Assert.AreEqual(excludeCells.Count(), 1);
        }
    }

    [TestFixture]
    public class MyAssmeblyFinderTest
    {
        [Test]
        public void FinderConstructorTest()
        {
            var finder = new MyAssemblyFinder();
            var assmeblies = finder.GetAllAssemblies();
            Assert.AreEqual(assmeblies.Count, 3);
        }

        [Test]
        public void NewFinderTest()
        {
            var finder = new TypeFinder(null);
        }
    }

    [TestFixture]
    public class AbpAutoMapperModuleTest
    {
        private AbpAutoMapperModule module;
        private TypeFinder typeFinder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            typeFinder = new TypeFinder(new MyAssemblyFinder());
            module = new AbpAutoMapperModule(typeFinder);
            module.PostInitialize();
        }

        [Test]
        public void Test_QueryFinders()
        {
            var types = typeFinder.Find(type =>
                type.IsDefined(typeof(AutoMapAttribute)) ||
                type.IsDefined(typeof(AutoMapFromAttribute)) ||
                type.IsDefined(typeof(AutoMapToAttribute))
                );
            types.FirstOrDefault(t => t == typeof(ENodebView)).ShouldNotBeNull();
        }

        [Test]
        public void Test_Module_Map_ENodebView()
        {
            var eNodeb = new ENodeb
            {
                ENodebId = 1,
                Factory = "Huawei"
            };
            var view = eNodeb.MapTo<ENodebView>();
            view.ENodebId.ShouldBe(1);
            view.Factory.ShouldBe("Huawei");
        }

        [Test]
        public void Test_Module_Map_CdmaBtsView()
        {
            var bts = new CdmaBts
            {
                BtsId = 1,
                Address = "Pussy Cat's house"
            };
            var view = bts.MapTo<CdmaBtsView>();
            view.BtsId.ShouldBe(1);
            view.Address.ShouldBe("Pussy Cat's house");
        }

        [Test]
        public void Test_Module_Map_PreciseInterferenceNeighborDto()
        {
            var dto = new PreciseInterferenceNeighborDto
            {
                ENodebId = 2,
                SectorId = 3,
                Db10Share = 10.5,
                Mod3Share = 2.3
            };
            var cell = dto.MapTo<PreciseWorkItemCell>();
            cell.ENodebId.ShouldBe(2);
            cell.SectorId.ShouldBe((byte)3);
            cell.Db10Share.ShouldBe(10.5);
            cell.Mod3Share.ShouldBe(2.3);
        }
    }

    [TestFixture]
    public class BtsViewMapperTest
    {
        private readonly ITypeFinder _typeFinder = new TypeFinder(new MyAssemblyFinder());

        [TestFixtureSetUp]
        public void Setup()
        {
            var module = new AbpAutoMapperModule(_typeFinder);
            module.PostInitialize();
        }

        [Test]
        public void Map_Null_Tests()
        {
            CdmaBts bts = null;
            var btsView = bts.MapTo<CdmaBtsView>();
            btsView.ShouldBeNull();
        }

        [Test]
        public void Map_BtsId_Test()
        {
            var bts = new CdmaBts { BtsId = 23 };
            var btsView = bts.MapTo<CdmaBtsView>();
            btsView.BtsId.ShouldBe(23);
        }

        [Test]
        public void Map_Address_Test()
        {
            var bts = new CdmaBts { Address = "oqwufjoiwofui" };
            var btsView = bts.MapTo<CdmaBtsView>();
            btsView.Address.ShouldBe("oqwufjoiwofui");
        }

        [Test]
        public void Map_TwoItems_Test()
        {
            var btsList = new List<CdmaBts>
            {
                new CdmaBts {Address = "123"},
                new CdmaBts {Address = "456"}
            };
            var btsViewList = btsList.MapTo<IEnumerable<CdmaBts>>();
            btsViewList.Count().ShouldBe(2);
            btsViewList.ElementAt(0).Address.ShouldBe("123");
            btsViewList.ElementAt(1).Address.ShouldBe("456");
        }
    }

    [TestFixture]
    public class ENodebViewMapperTest
    {
        private readonly ITypeFinder _typeFinder = new TypeFinder(new MyAssemblyFinder());

        [TestFixtureSetUp]
        public void Setup()
        {
            var module = new AbpAutoMapperModule(_typeFinder);
            module.PostInitialize();
        }

        [Test]
        public void Map_Null_Tests()
        {
            ENodeb eNodeb = null;
            var eNodebView = eNodeb.MapTo<ENodebView>();
            eNodebView.ShouldBeNull();
        }

        [Test]
        public void Map_ENodebId_Test()
        {
            var eNodeb = new ENodeb { ENodebId = 22 };
            var eNodebView = eNodeb.MapTo<ENodebView>();
            eNodebView.ShouldNotBeNull();
            eNodebView.ENodebId.ShouldBe(22);
        }

        [Test]
        public void Map_Name_Test()
        {
            var eNodeb = new ENodeb { Name = "abcde" };
            var eNodebView = eNodeb.MapTo<ENodebView>();
            eNodebView.ShouldNotBeNull();
            eNodebView.Name.ShouldBe("abcde");
        }
    }
}
