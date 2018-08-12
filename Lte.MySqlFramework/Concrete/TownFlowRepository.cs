﻿using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.RegionKpi;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
{
    public class TownFlowRepository : EfRepositorySave<MySqlContext, TownFlowStat>, ITownFlowRepository
    {
        public TownFlowRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public TownFlowStat Match(TownFlowStat stat)
        {
            return
                FirstOrDefault(
                    x =>
                        x.TownId == stat.TownId && x.StatTime == stat.StatTime &&
                        x.FrequencyBandType == stat.FrequencyBandType);
        }
    }
}