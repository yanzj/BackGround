using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Infrastructure;
using Abp.Reflection;
using Lte.Evaluations.Policy;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Abstract.Switch;
using Lte.Parameters.Concrete.Basic;
using Lte.Parameters.Concrete.Switch;
using Lte.Parameters.Entities.Switch;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace Lte.Evaluations.DataService.Switch
{
    [TestFixture]
    public class IntraFreqHoServiceTests
    {
        private readonly Mock<IUeEUtranMeasurementRepository> _zteMeasurementRepository =
            new Mock<IUeEUtranMeasurementRepository>();
        private readonly ICellMeasGroupZteRepository _zteGroupRepository = new CellMeasGroupZteRepository();

        private readonly IEUtranCellMeasurementZteRepository _zteCellGroupRepository =
            new EUtranCellMeasurementZteRepository();

        private readonly IIntraFreqHoGroupRepository _huaweiCellHoRepository = new IntraFreqHoGroupRepository();

        private readonly Mock<IIntraRatHoCommRepository> _huaweiENodebHoRepository = new Mock<IIntraRatHoCommRepository>();

        private readonly ICellHuaweiMongoRepository _huaweiCellRepository = new CellHuaweiMongoRepository();

        private readonly Mock<IENodebRepository> _eNodebRepository = new Mock<IENodebRepository>();

        private AbpAutoMapperModule _module;
        private TypeFinder _typeFinder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _eNodebRepository.Setup(x => x.FirstOrDefault(e => e.ENodebId == It.IsAny<int>())).Returns(new ENodeb
            {
                Factory = "中兴"
            });
            _eNodebRepository.Setup(x => x.FirstOrDefault(e => e.ENodebId == It.Is<int>(id => id == 500814))).Returns(new ENodeb
            {
                Factory = "华为"
            });
            _huaweiENodebHoRepository.Setup(x => x.GetRecent(It.IsAny<int>()))
                .Returns<int>(eNodebId => new IntraRatHoComm
                {
                    eNodeB_Id = eNodebId,
                    IntraFreqHoA3RprtQuan = 3344
                });
            _zteMeasurementRepository.Setup(x => x.GetRecent(It.IsAny<int>(), It.IsAny<int>()))
                .Returns<int, int>((eNodebId, configId) => new UeEUtranMeasurementZte
                {
                    eNodeB_Id = eNodebId,
                    reportAmount = 1234,
                    reportQuantity = 5678
                });

            _typeFinder = new TypeFinder(new MyAssemblyFinder());
            _module = new AbpAutoMapperModule(_typeFinder);
            _module.PostInitialize();
        }

        [TestCase(500814)]
        [TestCase(500923)]
        public void Test_HuaweiIntraFreqENodebQuery(int eNodebId)
        {
            var query = new HuaweiIntraFreqENodebMongoQuery(_huaweiENodebHoRepository.Object, eNodebId);
            Assert.IsNotNull(query);
            var result = query.Query();
            Assert.IsNotNull(result);
            result.ENodebId.ShouldBe(eNodebId);
            result.ReportQuantity.ShouldBe(3344);
        }

        [TestCase(500814)]
        [TestCase(500923)]
        public void Test_ZteIntraFreqENodebQuery(int eNodebId)
        {
            var query = new ZteIntraFreqENodebQuery(_zteGroupRepository, _zteMeasurementRepository.Object, eNodebId);
            Assert.IsNotNull(query);
            var result = query.Query();
            Assert.IsNotNull(result);
            result.ENodebId.ShouldBe(eNodebId);
            result.ReportAmount.ShouldBe(1234);
            result.ReportQuantity.ShouldBe(5678);
        }

        [TestCase(500814)]
        [TestCase(501766)]
        public void Test_ENodebQuery(int eNodebId)
        {
            var service = new IntraFreqHoService(_zteMeasurementRepository.Object, _zteGroupRepository, _zteCellGroupRepository,
                _huaweiCellHoRepository, _huaweiENodebHoRepository.Object, _huaweiCellRepository, _eNodebRepository.Object);
            var result = service.QueryENodebHo(eNodebId);
            Assert.IsNotNull(result);
        }
    }
}
