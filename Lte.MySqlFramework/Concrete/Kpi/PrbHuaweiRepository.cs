using System;
using System.Collections.Generic;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.Kpi;

namespace Lte.MySqlFramework.Concrete.Kpi
{
    public class PrbHuaweiRepository : EfRepositorySave<MySqlContext, PrbHuawei>, IPrbHuaweiRepository
    {
        public PrbHuaweiRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public PrbHuawei Match(PrbHuawei stat)
        {
            return FirstOrDefault(x =>
                x.StatTime == stat.StatTime && x.ENodebId == stat.ENodebId &&
                x.LocalCellId == stat.LocalCellId);
        }

        public List<PrbHuawei> FilterTopList(DateTime begin, DateTime end)
        {
            return GetAllList(x => x.StatTime >= begin && x.StatTime < end
                                                       && x.DownlinkPrbSubframe > 1000
                                                       && (x.DownlinkPrbSubframe < 7 * x.PdschPrbs ||
                                                           x.UplinkPrbSubframe < 7 * x.PuschPrbs));
        }
    }
}