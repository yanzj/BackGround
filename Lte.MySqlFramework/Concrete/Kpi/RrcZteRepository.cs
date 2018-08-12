using System;
using System.Collections.Generic;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.Kpi;

namespace Lte.MySqlFramework.Concrete.Kpi
{
    public class RrcZteRepository : EfRepositorySave<MySqlContext, RrcZte>, IRrcZteRepository
    {
        public RrcZteRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public RrcZte Match(RrcZte stat)
        {
            return FirstOrDefault(x =>
                x.StatTime == stat.StatTime && x.ENodebId == stat.ENodebId &&
                x.SectorId == stat.SectorId);
        }

        public List<RrcZte> FilterTopList(DateTime begin, DateTime end)
        {
            return GetAllList(
                x =>
                    x.StatTime >= begin && x.StatTime < end &&
                    x.MoDataRrcRequest + x.MoSignallingRrcRequest + x.MtAccessRrcRequest > 20000);
        }
    }
}