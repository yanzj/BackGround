using Abp.EntityFramework.AutoMapper;
using Abp.Reflection;
using Lte.Evaluations.MockItems;
using Lte.Evaluations.Policy;
using Lte.Parameters.MockOperations;
using Moq;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Cdma;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Abstract;

namespace Lte.Evaluations.DataService.Dump
{
    [TestFixture]
    public class CdmaCellDumpServiceTest
    {
        private readonly Mock<ICdmaCellRepository> _cellRepository = new Mock<ICdmaCellRepository>();
        private CdmaCellDumpService _service;
        private readonly ITypeFinder _typeFinder = new TypeFinder(new MyAssemblyFinder());

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            var module = new AbpAutoMapperModule(_typeFinder);
            module.PostInitialize();
            _service =new CdmaCellDumpService(_cellRepository.Object, null);
            _cellRepository.MockGetId<ICdmaCellRepository, CdmaCell>();
            _cellRepository.MockOperations();
            _cellRepository.MockRepositorySaveItems<CdmaCell, ICdmaCellRepository>();
        }

        [SetUp]
        public void Setup()
        {
            _cellRepository.MockSixCells();
        }

        [TestCase(1, 2)]
        [TestCase(3, 4)]
        [TestCase(5, 6)]
        public void Test_DumpNewCellExcels_SingleItem(int btsId, byte sectorId)
        {
            var cellExcels = new List<CdmaCellExcel>
            {
                new CdmaCellExcel
                {
                    BtsId = btsId,
                    SectorId = sectorId
                }
            };
            _service.DumpNewCellExcels(cellExcels);
            _cellRepository.Object.Count().ShouldBe(7);
            var lastObject = _cellRepository.Object.GetAllList()[6];
            lastObject.BtsId.ShouldBe(btsId);
            lastObject.SectorId.ShouldBe(sectorId);
        }
    }
}
