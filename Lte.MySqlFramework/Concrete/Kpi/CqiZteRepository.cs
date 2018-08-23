using System;
using System.Collections.Generic;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.Kpi;

namespace Lte.MySqlFramework.Concrete.Kpi
{
    public class CqiZteRepository : EfRepositorySave<MySqlContext, CqiZte>, ICqiZteRepository
    {
        public CqiZteRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public CqiZte Match(CqiZte stat)
        {
            return FirstOrDefault(x =>
                x.StatTime == stat.StatTime && x.ENodebId == stat.ENodebId &&
                x.SectorId == stat.SectorId);
        }

        public List<CqiZte> FilterTopList(DateTime begin, DateTime end)
        {
            return GetAllList(
                x => x.StatTime >= begin && x.StatTime < end
                     && x.Cqi0Reports + x.Cqi1Reports + x.Cqi2Reports + x.Cqi3Reports + x.Cqi4Reports > 300000);
        }
    }
}