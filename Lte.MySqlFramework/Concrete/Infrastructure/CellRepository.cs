﻿using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Infrastructure;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Abstract.Infrastructure;

namespace Lte.MySqlFramework.Concrete.Infrastructure
{
    public class CellRepository : EfRepositorySave<MySqlContext, Cell>, ICellRepository
    {
        public void AddCells(IEnumerable<Cell> cells)
        {
            foreach (var cell in cells)
            {
                Insert(cell);
            }
        }

        public Cell GetBySectorId(int eNodebId, byte sectorId)
        {
            return FirstOrDefault(x => x.ENodebId == eNodebId && x.SectorId == sectorId);
        }

        public Cell GetByFrequency(int eNodebId, int frequency)
        {
            return FirstOrDefault(x => x.ENodebId == eNodebId && x.Frequency == frequency);
        }

        public List<Cell> GetAllList(int eNodebId)
        {
            return GetAll().Where(x => x.ENodebId == eNodebId).ToList();
        }

        public List<Cell> GetAllList(double west, double east, double south, double north)
        {
            return GetAllList(x =>
                x.Longtitute >= west
                && x.Longtitute <= east
                && x.Lattitute >= south
                && x.Lattitute <= north);
        }

        public List<Cell> GetAllList(FrequencyBandType bandType)
        {
            switch (bandType)
            {
                case FrequencyBandType.Band800VoLte:
                    return GetAllList(x => x.BandClass == 5 && x.Frequency < 2500);
                case FrequencyBandType.Band1800:
                    return GetAllList(x => x.BandClass == 3);
                case FrequencyBandType.Band2100:
                    return GetAllList(x => x.BandClass == 1);
                default:
                    return GetAllList();
            }
        }

        public List<Cell> GetAllInUseList()
        {
            return GetAll().Where(x => x.IsInUse).ToList();
        }

        public CellRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
        
        public Cell Match(CellExcel stat)
        {
            return FirstOrDefault(x => x.ENodebId == stat.ENodebId && x.SectorId == stat.SectorId);
        }

        public Cell Match(ConstructionExcel stat)
        {
            return FirstOrDefault(x => x.ENodebId == stat.ENodebId && x.SectorId == stat.SectorId);
        }
    }
}