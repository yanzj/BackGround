using System;
using System.Collections.Generic;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.Kpi;

namespace Lte.MySqlFramework.Concrete.Kpi
{
    public class RrcHuaweiRepository : EfRepositorySave<MySqlContext, RrcHuawei>, IRrcHuaweiRepository
    {
        public RrcHuaweiRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public RrcHuawei Match(RrcHuawei stat)
        {
            return FirstOrDefault(x =>
                x.StatTime == stat.StatTime && x.ENodebId == stat.ENodebId &&
                x.LocalCellId == stat.LocalCellId);
        }

        public List<RrcHuawei> FilterTopList(DateTime begin, DateTime end)
        {
            return GetAllList(
                x =>
                    x.StatTime >= begin && x.StatTime < end &&
                    x.MoDataRrcRequest + x.MoSignallingRrcRequest + x.MtAccessRrcRequest > 20000);
        }
    }
}