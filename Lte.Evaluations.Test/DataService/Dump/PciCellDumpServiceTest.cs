using Lte.Evaluations.MockItems;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Infrastructure;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Abstract.Infrastructure;

namespace Lte.Evaluations.DataService.Dump
{
    [TestFixture]
    public class PciCellDumpServiceTest : CellDumpServiceTestBase
    {
        [TestCase(1, 1, 110, 0, 88)]
        [TestCase(2, 1, 109, 1, 232)]
        [TestCase(2, 2, 108, 2, 256)]
        [TestCase(3, 1, 23, 3, 87)]
        [TestCase(3, 2, 567, 4, 8787)]
        [TestCase(3, 3, 288, 5, 237)]
        public void Test_DumpNewCellExcels_SingleItem(int eNodebId, byte sectorId, short originPci, int index, short modifiedPci)
        {
            CellRepository.MockSixCells(pci: originPci);
            CellRepository.MockGetId<ICellRepository, Cell>();
            CellRepository.MockOperations();
            var cellExcels = new List<CellExcel>
            {
                new CellExcel
                {
                    ENodebId = eNodebId,
                    SectorId = sectorId,
                    Pci = modifiedPci
                }
            };
            Service.DumpNewCellExcels(cellExcels);
            CellRepository.Object.Count().ShouldBe(6);
            var results = CellRepository.Object.GetAllList();
            for (var i = 0; i < 6; i++)
            {
                results[i].Pci.ShouldBe(i == index ? modifiedPci : originPci);
            }
        }

        [TestCase(1, 1, 110, 0, 88)]
        [TestCase(2, 1, 109, 1, 232)]
        [TestCase(2, 2, 108, 2, 256)]
        [TestCase(3, 1, 23, 3, 87)]
        [TestCase(3, 2, 567, 4, 8787)]
        [TestCase(3, 3, 288, 5, 237)]
        public void Test_DumpNewCellExcels_SingleItem_ConsideredInUse(int eNodebId, byte sectorId, short originPci,
            int index, short modifiedPci)
        {
            CellRepository.MockSixCells(pci: originPci, isInUse: false);
            CellRepository.MockGetId<ICellRepository, Cell>();
            CellRepository.MockOperations();
            var cellExcels = new List<CellExcel>
            {
                new CellExcel
                {
                    ENodebId = eNodebId,
                    SectorId = sectorId,
                    Pci = modifiedPci
                }
            };
            Service.DumpNewCellExcels(cellExcels);
            CellRepository.Object.Count().ShouldBe(6);
            var results = CellRepository.Object.GetAllList();
            for (int i = 0; i < 6; i++)
            {
                if (i == index)
                {
                    results[i].Pci.ShouldBe(modifiedPci);
                    results[i].IsInUse.ShouldBeTrue();
                }
                else
                {
                    results[i].Pci.ShouldBe(originPci);
                    results[i].IsInUse.ShouldBeFalse();
                }
            }
        }

        [TestCase(1, 1, 110, 0, 88)]
        [TestCase(2, 1, 109, 1, 232)]
        [TestCase(2, 2, 108, 2, 256)]
        [TestCase(3, 1, 23, 3, 87)]
        [TestCase(3, 2, 567, 4, 8787)]
        [TestCase(3, 3, 288, 5, 237)]
        public void Test_DumpSingleCell(int eNodebId, byte sectorId, short originPci, int index, short modifiedPci)
        {
            CellRepository.MockSixCells(pci: originPci);
            CellRepository.MockGetId<ICellRepository, Cell>();
            CellRepository.MockOperations();
            var cellExcel = new CellExcel
            {
                ENodebId = eNodebId,
                SectorId = sectorId,
                Pci = modifiedPci
            };
            Service.DumpSingleCellExcel(cellExcel);
            CellRepository.Object.Count().ShouldBe(6);
            var results = CellRepository.Object.GetAllList();
            for (int i = 0; i < 6; i++)
            {
                results[i].Pci.ShouldBe(i == index ? modifiedPci : originPci);
            }
        }
    }
}
