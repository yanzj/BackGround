using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Repositories;

namespace Lte.Parameters.Abstract.Kpi
{
    public interface IPreciseCoverage4GRepository : IRepository<PreciseCoverage4G>, ISaveChanges
    {
        List<PreciseCoverage4G> GetAllList(int cellId, byte sectorId, DateTime begin, DateTime end);

        List<PreciseCoverage4G> GetAllList(DateTime begin, DateTime end);
    }
}
