using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.RegionKpi;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract.RegionKpi
{
    public interface ITownFlowRepository : IRepository<TownFlowStat>, ISaveChanges, IMatchRepository<TownFlowStat>
    {
    }
}