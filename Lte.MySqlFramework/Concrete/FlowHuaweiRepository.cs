using System;
using System.Collections.Generic;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
{
    public class FlowHuaweiRepository : EfRepositorySave<MySqlContext, FlowHuawei>, IFlowHuaweiRepository
    {
        public FlowHuaweiRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
        
        public FlowHuawei Match(FlowHuawei stat)
        {
            return FirstOrDefault(x =>
                x.StatTime == stat.StatTime && x.ENodebId == stat.ENodebId &&
                x.LocalCellId == stat.LocalCellId);
        }

        public List<FlowHuawei> GetBusyList(DateTime begin, DateTime end)
        {
            return GetAllList(
                x => x.StatTime >= begin && x.PdcpDownlinkFlow + x.PdcpUplinkFlow > 200000 && x.MaxUsers > 10);
        }

        public List<FlowHuawei> GetHighDownSwitchList(DateTime begin, DateTime end, int threshold)
        {
            return GetAllList(
                x => x.StatTime >= begin && x.StatTime < end && x.RedirectCdma2000 > threshold);
        }
    }
}