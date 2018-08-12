using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Cdma;
using Abp.EntityFramework.Entities.Infrastructure;
using Abp.Reflection;
using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.MockItems;
using Lte.Evaluations.Policy;
using Lte.Evaluations.ViewModels.Precise;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Abstract.Cdma;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Entities;
using Moq;
using NUnit.Framework;

namespace Lte.Evaluations.ViewModel
{
    [TestFixture]
    public class CellViewTest
    {
        private readonly Mock<IENodebRepository> _repository = new Mock<IENodebRepository>();
        private readonly ITypeFinder _typeFinder = new TypeFinder(new MyAssemblyFinder());

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            var module = new AbpAutoMapperModule(_typeFinder);
            module.PostInitialize();
            _repository.MockOperations();
            _repository.MockGetId<IENodebRepository, ENodeb>();
            _repository.MockThreeENodebs();
        }

        [TestCase(1, 2, true, "室外", 12.3, 1.1, 1.3, 1122)]
        [TestCase(1, 4, true, "室外", 12.6, 1.7, 1.3, 11192)]
        [TestCase(2, 4, false, "室内", 14.6, 3.7, 2.3, 1354)]
        [TestCase(9, 4, false, "室内", 14.6, 3.7, 2.3, 2067)]
        public void Test_Construct(int eNodebId, byte sectorId, bool outdoor, string indoor,
            double height, double eTilt, double mTilt, int tac)
        {
            var cell = new Cell
            {
                ENodebId = eNodebId,
                SectorId = sectorId,
                IsOutdoor = outdoor,
                Height = height,
                ETilt = eTilt,
                MTilt = mTilt,
                Tac = tac
            };
            var view = CellView.ConstructView(cell, _repository.Object);
            if (eNodebId > 0 && eNodebId <= 3)
                Assert.AreEqual(view.ENodebName, "ENodeb-" + eNodebId);
            else
            {
                Assert.IsNull(view.ENodebName);
            }
            Assert.AreEqual(view.ENodebId, eNodebId);
            Assert.AreEqual(view.SectorId, sectorId);
            Assert.AreEqual(view.Indoor, indoor);
            Assert.AreEqual(view.Height, height);
            Assert.AreEqual(view.DownTilt, eTilt + mTilt);
            Assert.AreEqual(view.Tac, tac);
        }
    }

    [TestFixture]
    public class CdmaCellViewTest
    {
        private readonly Mock<IBtsRepository> _repository = new Mock<IBtsRepository>();
        private readonly ITypeFinder _typeFinder = new TypeFinder(new MyAssemblyFinder());

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            var module = new AbpAutoMapperModule(_typeFinder);
            module.PostInitialize();
            _repository.MockOperation();
            _repository.MockGetId<IBtsRepository, CdmaBts>();
            _repository.MockThreeBtss();
        }

        [TestCase(1, 2, 3, true, "室外", 12.3, 1.1, 1.3, "0x1122")]
        [TestCase(1, 4, 3, true, "室外", 12.6, 1.7, 1.3, "0x1192")]
        [TestCase(2, 4, 7, false, "室内", 14.6, 3.7, 2.3, "0x1192")]
        [TestCase(9, 4, 7, false, "室内", 14.6, 3.7, 2.3, "0x1192")]
        public void Test_Construct(int btsId, byte sectorId, int cellId, bool outdoor, string indoor,
            double height, double eTilt, double mTilt, string lac)
        {
            var cell = new CdmaCell
            {
                BtsId = btsId,
                SectorId = sectorId,
                CellId = cellId,
                IsOutdoor = outdoor,
                Height = height,
                ETilt = eTilt,
                MTilt = mTilt,
                Lac = lac
            };
            var view = cell.ConstructView(_repository.Object);
            if (btsId > 0 && btsId <= 3)
                Assert.AreEqual(view.BtsName, "Bts-" + btsId);
            else
            {
                Assert.IsNull(view.BtsName);
            }
            Assert.AreEqual(view.BtsId, btsId);
            Assert.AreEqual(view.SectorId, sectorId);
            Assert.AreEqual(view.CellId, cellId);
            Assert.AreEqual(view.Indoor, indoor);
            Assert.AreEqual(view.Height, height);
            Assert.AreEqual(view.DownTilt, eTilt + mTilt);
            Assert.AreEqual(view.Lac, lac);
        }
    }

    [TestFixture]
    public class CellPreciseKpiViewTest
    {
        private readonly ITypeFinder _typeFinder = new TypeFinder(new MyAssemblyFinder());
        private readonly Mock<IENodebRepository> _eNodebRepository = new Mock<IENodebRepository>();

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            var module = new AbpAutoMapperModule(_typeFinder);
            module.PostInitialize();
            _eNodebRepository.MockOperations();
            _eNodebRepository.MockGetId<IENodebRepository, ENodeb>();
            _eNodebRepository.MockThreeENodebs();
        }

        [TestCase(1, 2, 3, 1.1, 2.2, 12.3, true, 11.2, 22.34, 17.2)]
        [TestCase(2, 2, 4, 1.1, 2.2, 12.3, true, 11.2, 22.34, 19.2)]
        [TestCase(1, 2, 3, 1.1, 2.2, 12.3, false, 12.2, 22.34, 17.2)]
        [TestCase(3, 2, 3, 1.1, 2.2, 12.3, false, 11.2, 22.34, 17.2)]
        [TestCase(4, 2, 4, 1.1, 2.2, 12.3, true, 11.7, 22.34, 17.2)]
        [TestCase(5, 2, 3, 1.1, 2.2, 12.3, false, 11.2, 22.34, 17.2)]
        public void Test_Construction(int eNodebId, byte sectorId, int frequency, double rsPower, double height,
            double azimuth, bool isOutdoor, double eTilt, double mTilt, double antennaGain)
        {
            var cell = new Cell
            {
                ENodebId = eNodebId,
                SectorId = sectorId,
                Frequency = frequency,
                RsPower = rsPower,
                Height = height,
                Azimuth = azimuth,
                IsOutdoor = isOutdoor,
                ETilt = eTilt,
                MTilt = mTilt,
                AntennaGain = antennaGain
            };
            var view = CellPreciseKpiView.ConstructView(cell, _eNodebRepository.Object);
            Assert.AreEqual(view.ENodebId, eNodebId);
            Assert.AreEqual(view.SectorId, sectorId);
            if (eNodebId > 0 && eNodebId <= 3)
                Assert.AreEqual(view.ENodebName, "ENodeb-" + eNodebId);
            else
            {
                Assert.IsNull(view.ENodebName);
            }
            Assert.AreEqual(view.Frequency, frequency);
            Assert.AreEqual(view.RsPower, rsPower);
            Assert.AreEqual(view.Height, height);
            Assert.AreEqual(view.Azimuth, azimuth);
            Assert.AreEqual(view.Indoor, isOutdoor ? "室外" : "室内");
            Assert.AreEqual(view.DownTilt, eTilt + mTilt);
            Assert.AreEqual(view.AntennaGain, antennaGain);
        }
    }
}
