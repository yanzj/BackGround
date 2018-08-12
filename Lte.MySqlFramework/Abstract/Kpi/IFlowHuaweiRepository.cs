using System;
using System.Collections.Generic;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract.Kpi
{
    public interface IFlowHuaweiRepository : IRepository<FlowHuawei>, ISaveChanges, IMatchRepository<FlowHuawei>
    {
        List<FlowHuawei> GetBusyList(DateTime begin, DateTime end);

        List<FlowHuawei> GetHighDownSwitchList(DateTime begin, DateTime end, int threshold);
    }
}
