using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract.Kpi
{
    public interface IRrcHuaweiRepository : IRepository<RrcHuawei>, ISaveChanges, IMatchRepository<RrcHuawei>,
        IFilterTopRepository<RrcHuawei>
    {
        
    }
}