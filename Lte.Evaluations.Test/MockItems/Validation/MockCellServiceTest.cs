using System.Linq;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Cdma;
using Abp.EntityFramework.Entities.Infrastructure;
using Lte.Domain.Common.Wireless;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Abstract.Cdma;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Entities;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace Lte.Evaluations.MockItems.Validation
{
    [TestFixture]
    public class MockCellServiceTest
    {
        private readonly Mock<ICellRepository> _cellRepository = new Mock<ICellRepository>();

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _cellRepository.MockSixCells();
        }

        [SetUp]
        public void Setup()
        {
            _cellRepository.MockOperations();
            _cellRepository.MockGetId<ICellRepository, Cell>();
        }

        [Test]
        public void Test_GetAllList()
        {
            var allList = _cellRepository.Object.GetAllList();
            Assert.AreEqual(allList.Count, 6);
            allList[0].IsInUse.ShouldBeTrue();
        }

        [Test]
        public void Test_GetAllInUseList()
        {
            var inuseList = _cellRepository.Object.GetAllInUseList();
            Assert.AreEqual(inuseList.Count, 6);
        }
    }

    [TestFixture]
    public class MockInfrastructureServiceTest
    {
        private readonly Mock<IInfrastructureRepository> _repository = new Mock<IInfrastructureRepository>();

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _repository.MockThreeCollegeENodebs();
            _repository.MockOperations();
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void Test_GetENodebIds(int id)
        {
            var ids = _repository.Object.GetCollegeInfrastructureIds("College-" + id, InfrastructureType.ENodeb);
            Assert.AreEqual(ids.Count(), 1);
            Assert.AreEqual(ids.ElementAt(0), id);
        }
    }

    [TestFixture]
    public class MockBtsServiceTest
    {
        private readonly Mock<IBtsRepository> _btsRepository = new Mock<IBtsRepository>();

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _btsRepository.MockOperation();
            _btsRepository.MockGetId<IBtsRepository, CdmaBts>();
        }

        [SetUp]
        public void Setup()
        {
            _btsRepository.MockThreeBtss();
        }

        [TestCase("kdjowi")]
        [TestCase("kjow3iu4et09wi")]
        [TestCase("kokwu43982ui")]
        public void TestUpdateFirstItem(string name)
        {
            _btsRepository.Object.GetByBtsId(1).Name = name;
            Assert.AreEqual(_btsRepository.Object.GetByBtsId(1).Name, name);
        }
    }
}
