using System;
using System.Collections.Generic;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract.Kpi
{
    public interface ICqiHuaweiRepository : IRepository<CqiHuawei>, ISaveChanges, IMatchRepository<CqiHuawei>,
        IFilterTopRepository<CqiHuawei>
    {
    }
}