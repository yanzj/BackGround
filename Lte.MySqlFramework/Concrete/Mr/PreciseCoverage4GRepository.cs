using System;
using System.Collections.Generic;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common.Wireless.Cell;
using Lte.MySqlFramework.Abstract.Mr;

namespace Lte.MySqlFramework.Concrete.Mr
{
    public class PreciseCoverage4GRepository : EfRepositorySave<MySqlContext, PreciseCoverage4G>,
        IPreciseCoverage4GRepository
    {
        public List<PreciseCoverage4G> GetAllList(DateTime begin, DateTime end, FrequencyBandType frequency)
        {
            switch (frequency)
            {
                case FrequencyBandType.All:
                    return GetAllList(x => x.StatTime >= begin && x.StatTime < end);
                case FrequencyBandType.Band800VoLte:
                    return GetAllList(x =>
                        x.StatTime >= begin && x.StatTime < end && x.SectorId >= 16 && x.SectorId < 32);
                case FrequencyBandType.Band1800:
                    return GetAllList(x =>
                        x.StatTime >= begin && x.StatTime < end && x.SectorId >= 48 && x.SectorId < 64);
                case FrequencyBandType.Band2100:
                    return GetAllList(x => x.StatTime >= begin && x.StatTime < end && x.SectorId < 16);
                default:
                    return new List<PreciseCoverage4G>();
            }
        }
        
        public List<PreciseCoverage4G> GetAllList(int cellId, byte sectorId, DateTime begin, DateTime end)
        {
            return GetAllList(x =>
                x.StatTime >= begin && x.StatTime < end && x.CellId == cellId && x.SectorId == sectorId);
        }

        public PreciseCoverage4GRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
