﻿using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.RegionKpi;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract.RegionKpi
{
    public interface ITownCqiRepository : IRepository<TownCqiStat>, ISaveChanges, IMatchRepository<TownCqiStat>
    {

    }
}