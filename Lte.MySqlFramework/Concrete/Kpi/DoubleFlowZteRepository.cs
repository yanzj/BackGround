using System;
using System.Collections.Generic;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.Kpi;

namespace Lte.MySqlFramework.Concrete.Kpi
{
    public class DoubleFlowZteRepository : EfRepositorySave<MySqlContext, DoubleFlowZte>, IDoubleFlowZteRepository
    {
        public DoubleFlowZteRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public DoubleFlowZte Match(DoubleFlowZte stat)
        {
            return FirstOrDefault(x =>
                x.StatTime == stat.StatTime && x.ENodebId == stat.ENodebId &&
                x.SectorId == stat.SectorId);
        }

        public List<DoubleFlowZte> FilterTopList(DateTime begin, DateTime end)
        {
            return GetAllList(x => (x.SchedulingTm3Rank2Old + x.SchedulingTm4Rank2) * 6 <
                                   (x.SchedulingTm2 + x.SchedulingTm3Old + x.SchedulingTm4)
                                   && (x.SchedulingTm2 + x.SchedulingTm3Old + x.SchedulingTm4) > 200000000);
        }
    }
}