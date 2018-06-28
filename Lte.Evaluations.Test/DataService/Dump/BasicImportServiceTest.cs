using System;
using System.IO;
using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.MockItems;
using Moq;
using NUnit.Framework;
using Shouldly;
using System.Linq;
using Abp.EntityFramework.Entities;
using Lte.Domain.Excel;
using Lte.Domain.LinqToExcel;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;

namespace Lte.Evaluations.DataService.Dump
{
    [TestFixture]
    public class BasicImportServiceTest
    {
        private readonly Mock<IENodebRepository> _eNodebRepository = new Mock<IENodebRepository>();
        private readonly Mock<ICellRepository> _cellRepository = new Mock<ICellRepository>();
        private readonly Mock<IBtsRepository> _btsRepository = new Mock<IBtsRepository>();
        private readonly Mock<ICdmaCellRepository> _cdmaCellRepository = new Mock<ICdmaCellRepository>();

        private BasicImportService _service;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _service = new BasicImportService(_eNodebRepository.Object, _cellRepository.Object, _btsRepository.Object,
                _cdmaCellRepository.Object, null, null, null, null, null, null, null);
            _eNodebRepository.MockThreeENodebs();
            _btsRepository.MockThreeBtss();
            _cellRepository.MockSixCells();
            _cdmaCellRepository.MockSixCells();

            _eNodebRepository.MockOperations();
            _eNodebRepository.MockGetId<IENodebRepository, ENodeb>();
            _btsRepository.MockOperation();
            _btsRepository.MockGetId<IBtsRepository, CdmaBts>();
            _cellRepository.MockOperations();
            _cellRepository.MockGetId<ICellRepository, Cell>();
            _cdmaCellRepository.MockGetId<ICdmaCellRepository, CdmaCell>();
            _cdmaCellRepository.MockOperations();
        }

        [TestCase(new[] { 1, 2, 3, 4 }, new[] { 4 })]
        [TestCase(new[] { 1, 2  }, new int[] { })]
        [TestCase(new[] { 1, 2, 4 }, new[] { 4 })]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, new[] { 4, 5 })]
        public void Test_GetNewENodebExcels(int[] inputENodebIds, int[] outputENodebIds)
        {
            BasicImportContainer.ENodebExcels = inputENodebIds.Select(x => new ENodebExcel
            {
                ENodebId = x,
                Name = "ENodeb-" + x
            }).ToList();
            var results = _service.GetNewENodebExcels();
            results.Select(x => x.ENodebId).ToArray().ShouldBe(outputENodebIds);
        }

        [TestCase(new[] { 1, 2, 3, 4 }, new[] { 4 })]
        [TestCase(new[] { 1, 2 }, new int[] { })]
        [TestCase(new[] { 1, 2, 4 }, new[] { 4 })]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, new[] { 4, 5 })]
        public void Test_GetNewBtsExcels(int[] inputBtsIds, int[] outputBtsIds)
        {
            BasicImportService.BtsExcels = inputBtsIds.Select(x => new BtsExcel
            {
                BtsId = x,
                Name = "Bts-" + x
            }).ToList();
            var results = _service.GetNewBtsExcels();
            results.Select(x => x.BtsId).ToArray().ShouldBe(outputBtsIds);
        }

        [TestCase(new[] { 1, 2, 3, 4 }, new byte[] { 1, 2, 3, 4 }, new[] { 4 }, new byte[] { 4 })]
        [TestCase(new[] { 1, 2, 3, 4 }, new byte[] { 1, 2, 3, 3 }, new[] { 4 }, new byte[] { 3 })]
        [TestCase(new[] { 1, 2, 3, 4 }, new byte[] { 1, 2, 3, 4 }, new[] { 4 }, new byte[] { 4 })]
        [TestCase(new[] { 1, 2, 3, 3 }, new byte[] { 1, 2, 3, 4 }, new[] { 3 }, new byte[] { 4 })]
        [TestCase(new[] { 1, 2, 3, 2 }, new byte[] { 1, 2, 3, 4 }, new[] { 2 }, new byte[] { 4 })]
        [TestCase(new[] { 1, 2, 3, 4 }, new byte[] { 1, 2, 3, 1 }, new[] { 4 }, new byte[] { 1 })]
        [TestCase(new[] { 1, 2 }, new byte[] { 1, 2 }, new int[] { }, new byte[] { })]
        [TestCase(new[] { 1, 2, 4 }, new byte[] { 1, 2, 4 }, new[] { 4 }, new byte[] { 4 })]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, new byte[] { 1, 2, 3, 4, 5 }, new[] { 4, 5 }, new byte[] { 4, 5 })]
        [TestCase(new[] { 1, 1, 3, 1, 5 }, new byte[] { 1, 1, 3, 4, 5 }, new[] { 1, 5 }, new byte[] { 4, 5 })]
        public void Test_GetNewCellExcels(int[] inputENodebIds, byte[] inputSectorIds, int[] outputENodebIds,
            byte[] outputSectorIds)
        {
            BasicImportContainer.CellExcels = inputENodebIds.Select((t, i) => new CellExcel
            {
                ENodebId = t,
                SectorId = inputSectorIds[i],
                Pci = 111
            }).ToList();
            var results = _service.GetNewCellExcels().ToArray();
            results.Select(x => x.ENodebId).ToArray().ShouldBe(outputENodebIds);
            results.Select(x => x.SectorId).ToArray().ShouldBe(outputSectorIds);
        }

        [TestCase(new[] { 1, 2, 3, 4 }, new byte[] { 1, 2, 3, 4 }, new short[] { 111, 111, 111, 111},
            new[] { 4 }, new byte[] { 4 })]
        [TestCase(new[] { 1, 2, 3, 4 }, new byte[] { 1, 2, 3, 4 }, new short[] { 110, 111, 111, 111 },
            new[] { 1, 4 }, new byte[] { 1, 4 })]
        [TestCase(new[] { 1, 2, 3, 4 }, new byte[] { 1, 2, 3, 3 }, new short[] { 111, 111, 111, 111 },
            new[] { 4 }, new byte[] { 3 })]
        [TestCase(new[] { 1, 2, 3, 4 }, new byte[] { 1, 2, 3, 3 }, new short[] { 111, 109, 111, 111 },
            new[] { 2, 4 }, new byte[] { 2, 3 })]
        [TestCase(new[] { 1, 2, 3, 4 }, new byte[] { 1, 2, 3, 4 }, new short[] { 111, 111, 111, 111 },
            new[] { 4 }, new byte[] { 4 })]
        [TestCase(new[] { 1, 2, 3, 3 }, new byte[] { 1, 2, 3, 4 }, new short[] { 111, 111, 111, 111 },
            new[] { 3 }, new byte[] { 4 })]
        [TestCase(new[] { 1, 2, 3, 2 }, new byte[] { 1, 2, 3, 4 }, new short[] { 111, 111, 111, 111 },
            new[] { 2 }, new byte[] { 4 })]
        [TestCase(new[] { 1, 2, 3, 4 }, new byte[] { 1, 2, 3, 1 }, new short[] { 111, 111, 111, 111 },
            new[] { 4 }, new byte[] { 1 })]
        [TestCase(new[] { 1, 2 }, new byte[] { 1, 2 }, new short[] { 111, 111 },
            new int[] { }, new byte[] { })]
        [TestCase(new[] { 1, 2, 4 }, new byte[] { 1, 2, 4 }, new short[] { 111, 111, 111 },
            new[] { 4 }, new byte[] { 4 })]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, new byte[] { 1, 2, 3, 4, 5 }, new short[] { 111, 111, 111, 111, 111 },
            new[] { 4, 5 }, new byte[] { 4, 5 })]
        [TestCase(new[] { 1, 1, 3, 1, 5 }, new byte[] { 1, 1, 3, 4, 5 }, new short[] { 111, 111, 111, 111, 111 },
            new[] { 1, 5 }, new byte[] { 4, 5 })]
        public void Test_GetNewCellExcels_PciConsidered(int[] inputENodebIds, byte[] inputSectorIds, short[] inputPcis,
            int[] outputENodebIds, byte[] outputSectorIds)
        {
            BasicImportContainer.CellExcels = inputENodebIds.Select((t, i) => new CellExcel
            {
                ENodebId = t,
                SectorId = inputSectorIds[i],
                Pci = inputPcis[i]
            }).ToList();
            var results = _service.GetNewCellExcels().ToArray();
            results.Select(x => x.ENodebId).ToArray().ShouldBe(outputENodebIds);
            results.Select(x => x.SectorId).ToArray().ShouldBe(outputSectorIds);
        }

        [TestCase(new[] { 1, 2, 3, 4 }, new byte[] { 1, 2, 3, 4 }, new[] { 4 }, new byte[] { 4 })]
        [TestCase(new[] { 1, 2, 3, 4 }, new byte[] { 1, 2, 3, 3 }, new[] { 4 }, new byte[] { 3 })]
        [TestCase(new[] { 1, 2, 3, 4 }, new byte[] { 1, 2, 3, 4 }, new[] { 4 }, new byte[] { 4 })]
        [TestCase(new[] { 1, 2, 3, 3 }, new byte[] { 1, 2, 3, 4 }, new[] { 3 }, new byte[] { 4 })]
        [TestCase(new[] { 1, 2, 3, 2 }, new byte[] { 1, 2, 3, 4 }, new[] { 2 }, new byte[] { 4 })]
        [TestCase(new[] { 1, 2, 3, 4 }, new byte[] { 1, 2, 3, 1 }, new[] { 4 }, new byte[] { 1 })]
        [TestCase(new[] { 1, 2 }, new byte[] { 1, 2 }, new int[] { }, new byte[] { })]
        [TestCase(new[] { 1, 2, 4 }, new byte[] { 1, 2, 4 }, new[] { 4 }, new byte[] { 4 })]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, new byte[] { 1, 2, 3, 4, 5 }, new[] { 4, 5 }, new byte[] { 4, 5 })]
        [TestCase(new[] { 1, 1, 3, 1, 5 }, new byte[] { 1, 1, 3, 4, 5 }, new[] { 1, 5 }, new byte[] { 4, 5 })]
        public void Test_GetNewCdmaCellExcels(int[] inputBtsIds, byte[] inputSectorIds, int[] outputBtsIds,
            byte[] outputSectorIds)
        {
            BasicImportContainer.CdmaCellExcels = inputBtsIds.Select((t, i) => new CdmaCellExcel
            {
                BtsId = t,
                SectorId = inputSectorIds[i]
            }).ToList();
            var results = _service.GetNewCdmaCellExcels().ToArray();
            results.Select(x => x.BtsId).ToArray().ShouldBe(outputBtsIds);
            results.Select(x => x.SectorId).ToArray().ShouldBe(outputSectorIds);
        }

        [Test]
        public void Test_ExcelReader()
        {
            var testDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var excelFilesDirectory = Path.Combine(testDirectory, "ExcelFiles");
            var path = Path.Combine(excelFilesDirectory, "LteCells.xlsx");
            var service = new BasicImportService(null, null, null, null, null, null, null, null, null, null, null);
            //service.ImportLteParameters(path);
            var repo = new ExcelQueryFactory { FileName = path };
            var cellExcels = (from c in repo.Worksheet<CellExcel>("小区级")
                              select c).ToList();
            cellExcels[0].PlanNum.ShouldBe("FSL13996");
            BasicImportContainer.CellExcels = cellExcels;
            BasicImportContainer.CellExcels.ShouldNotBeNull();
            BasicImportContainer.CellExcels[0].PlanNum.ShouldBe("FSL13996");
            BasicImportContainer.CellExcels[0].Azimuth.ShouldBe(140);
            BasicImportContainer.CellExcels[0].ENodebId.ShouldBe(870238);
            BasicImportContainer.CellExcels[0].AntennaInfo.ShouldBe("4端口单频F");
        }
        
        [Test]
        public void Test_ExcelReader3()
        {
            var testDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var excelFilesDirectory = Path.Combine(testDirectory, "ExcelFiles");
            var path = Path.Combine(excelFilesDirectory, "LteCells.xlsx");
            var service = new BasicImportService(null, null, null, null, null, null, null, null, null, null, null);
            BasicImportContainer.CellExcels = service.ImportCellExcels(path);

            BasicImportContainer.CellExcels.ShouldNotBeNull();
            BasicImportContainer.CellExcels[0].PlanNum.ShouldBe("FSL13996");
            BasicImportContainer.CellExcels[0].Azimuth.ShouldBe(140);
            BasicImportContainer.CellExcels[0].ENodebId.ShouldBe(870238);
            BasicImportContainer.CellExcels[0].AntennaInfo.ShouldBe("4端口单频F");
        }
    }
}
