using System;
using System.Collections.Generic;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract
{
    public interface IQciHuaweiRepository : IRepository<QciHuawei>, ISaveChanges, IMatchRepository<QciHuawei>
    {
        List<QciHuawei> FilterTopList(DateTime begin, DateTime end);
    }
}