using System;
using System.Collections.Generic;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common.Wireless.Cell;

namespace Lte.MySqlFramework.Abstract.Mr
{
    public interface IPreciseCoverage4GRepository : IRepository<PreciseCoverage4G>, ISaveChanges
    {
        List<PreciseCoverage4G> GetAllList(int cellId, byte sectorId, DateTime begin, DateTime end);

        List<PreciseCoverage4G> GetAllList(DateTime begin, DateTime end, FrequencyBandType frequency);
    }
}
