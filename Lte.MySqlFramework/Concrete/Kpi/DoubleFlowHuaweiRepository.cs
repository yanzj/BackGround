using System;
using System.Collections.Generic;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.Kpi;

namespace Lte.MySqlFramework.Concrete.Kpi
{
    public class DoubleFlowHuaweiRepository : EfRepositorySave<MySqlContext, DoubleFlowHuawei>,
        IDoubleFlowHuaweiRepository
    {
        public DoubleFlowHuaweiRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public DoubleFlowHuawei Match(DoubleFlowHuawei stat)
        {
            return FirstOrDefault(x =>
                x.StatTime == stat.StatTime && x.ENodebId == stat.ENodebId &&
                x.SectorId == stat.SectorId);
        }

        public List<DoubleFlowHuawei> FilterTopList(DateTime begin, DateTime end)
        {
            return GetAllList(x => x.StatTime >= begin
                                   && x.StatTime < end
                                   && (x.CloseLoopRank2Prbs + x.OpenLoopRank2Prbs) * 5 <
                                   (x.CloseLoopRank1Prbs + x.OpenLoopRank1Prbs)
                                   && (x.CloseLoopRank1Prbs + x.OpenLoopRank1Prbs) > 500000000);
        }
    }
}