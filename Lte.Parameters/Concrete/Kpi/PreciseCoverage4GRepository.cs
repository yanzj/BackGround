using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract.Kpi;
using System;
using System.Collections.Generic;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Kpi;
using Lte.Domain.Common.Wireless.Cell;

namespace Lte.Parameters.Concrete.Kpi
{
    public class PreciseCoverage4GRepository : EfRepositorySave<EFParametersContext, PreciseCoverage4G>,
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

        public PreciseCoverage4GRepository(IDbContextProvider<EFParametersContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
