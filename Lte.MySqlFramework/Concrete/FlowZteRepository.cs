using System;
using System.Collections.Generic;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;
using Abp.EntityFramework.Entities;

namespace Lte.MySqlFramework.Concrete
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

        public List<FlowZte> GetBusyList(DateTime begin, DateTime end)
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
