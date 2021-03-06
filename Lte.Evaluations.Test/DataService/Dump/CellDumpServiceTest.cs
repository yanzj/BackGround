﻿using Lte.Evaluations.MockItems;
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
    public class CellDumpServiceTest : CellDumpServiceTestBase
    {
        [SetUp]
        public void Setup()
        {
            CellRepository.MockSixCells();
            CellRepository.MockGetId<ICellRepository, Cell>();
            CellRepository.MockOperations();
        }

        [TestCase(1, 2)]
        [TestCase(3, 4)]
        [TestCase(5, 6)]
        public void Test_DumpNewCellExcels_SingleItem(int eNodebId, byte sectorId)
        {
            var cellExcels = new List<CellExcel>
            {
                new CellExcel
                {
                    ENodebId = eNodebId,
                    SectorId = sectorId
                }
            };
            Service.DumpNewCellExcels(cellExcels);
            CellRepository.Object.Count().ShouldBe(7);
            var lastObject = CellRepository.Object.GetAllList()[6];
            lastObject.ENodebId.ShouldBe(eNodebId);
            lastObject.SectorId.ShouldBe(sectorId);
        }

        [TestCase(1, 2)]
        [TestCase(3, 4)]
        [TestCase(5, 6)]
        public void Test_DumpSingleExcel(int eNodebId, byte sectorId)
        {
            var cellExcel = new CellExcel
            {
                ENodebId = eNodebId,
                SectorId = sectorId,
                ShareCdmaInfo = "1"
            };
            Assert.IsTrue(Service.DumpSingleCellExcel(cellExcel));
            CellRepository.Object.Count().ShouldBe(7);
            var lastObject = CellRepository.Object.GetAllList()[6];
            lastObject.ENodebId.ShouldBe(eNodebId);
            lastObject.SectorId.ShouldBe(sectorId);
        }

        [TestCase(1, 2, "1_2_2", 2)]
        [TestCase(2, 2, "1_2_2", 2)]
        [TestCase(4, 2, "1_2_2", 2)]
        [TestCase(1, 2, "1_4_2", -1)]
        public void Test_UpdateENodebBtsIds_SingleItem(int eNodebId, byte sectorId, string shareInfo, int btsId)
        {
            var cellExcels = new List<CellExcel>
            {
                new CellExcel
                {
                    ENodebId = eNodebId,
                    SectorId = sectorId,
                    ShareCdmaInfo = shareInfo
                }
            };
            Service.UpdateENodebBtsIds(cellExcels);
            if (btsId > 0)
            {
                BtsRepository.Object.GetByBtsId(btsId).ENodebId.ShouldBe(eNodebId);
            }
        }
    }
}
