using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract.Kpi
{
    public interface IDoubleFlowHuaweiRepository : IRepository<DoubleFlowHuawei>, ISaveChanges,
        IMatchRepository<DoubleFlowHuawei>
    {
        
    }
}