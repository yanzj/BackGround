using Abp.Domain.Repositories;
using Abp.EntityFramework.Repositories;
using Abp.EntityFramework.Entities;

namespace Lte.MySqlFramework.Abstract
{
    public interface IFlowHuaweiRepository : IRepository<FlowHuawei>, ISaveChanges, IMatchRepository<FlowHuawei>
    {
    }
}
