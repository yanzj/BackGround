using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.RegionKpi;
using Abp.EntityFramework.Repositories;

namespace Lte.Parameters.Abstract.Kpi
{
    public interface ITownPreciseCoverage4GStatRepository : IRepository<TownPreciseCoverage4GStat>, ISaveChanges,
        IMatchRepository<TownPreciseCoverage4GStat>
    {
    }
}