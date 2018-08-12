using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract.Kpi;
using System;
using System.Collections.Generic;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Kpi;

namespace Lte.Parameters.Concrete.Kpi
{
    public class PreciseCoverage4GRepository : EfRepositorySave<EFParametersContext, PreciseCoverage4G>,
        IPreciseCoverage4GRepository
    {
        public List<PreciseCoverage4G> GetAllList(DateTime begin, DateTime end)
        {
            return GetAllList(x => x.StatTime >= begin && x.StatTime < end);
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
