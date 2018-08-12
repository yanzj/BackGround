using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.RegionKpi;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract
{
    public interface ITownFlowRepository : IRepository<TownFlowStat>, ISaveChanges, IMatchRepository<TownFlowStat>
    {
    }
}