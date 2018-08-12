using System;
using System.Collections.Generic;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Abp.EntityFramework.Entities.Kpi;
using Lte.MySqlFramework.Abstract.Kpi;

namespace Lte.MySqlFramework.Concrete.Kpi
{
    public class FlowZteRepository : EfRepositorySave<MySqlContext, FlowZte>, IFlowZteRepository
    {
        public FlowZteRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
        
        public FlowZte Match(FlowZte stat)
        {
            return FirstOrDefault(x =>
                x.StatTime == stat.StatTime && x.ENodebId == stat.ENodebId &&
                x.SectorId == stat.SectorId);
        }

        public List<FlowZte> FilterTopList(DateTime begin, DateTime end)
        {
            return GetAllList(x =>
                x.StatTime >= begin && x.StatTime < end && x.DownlinkPdcpFlow + x.PdcpUplinkDuration > 200000 &&
                x.MaxRrcUsers > 10);
        }

        public List<FlowZte> GetHighDownSwitchList(DateTime begin, DateTime end, int threshold)
        {
            return GetAllList(
                x => x.StatTime >= begin && x.StatTime < end && x.RedirectA2 + x.RedirectB2 > threshold);
        }
    }
}
