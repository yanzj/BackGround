using Abp.Domain.Repositories;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Entities.Kpi;

namespace Lte.Parameters.Abstract.Kpi
{
    public interface ITownPreciseCoverage4GStatRepository : IRepository<TownPreciseCoverage4GStat>, ISaveChanges,
        IMatchRepository<TownPreciseCoverage4GStat>
    {
    }
}