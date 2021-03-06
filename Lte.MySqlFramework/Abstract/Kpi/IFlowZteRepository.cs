﻿using System;
using System.Collections.Generic;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract.Kpi
{
    public interface IFlowZteRepository : IRepository<FlowZte>, ISaveChanges, IMatchRepository<FlowZte>,
        IFilterTopRepository<FlowZte>
    {
        List<FlowZte> GetHighDownSwitchList(DateTime begin, DateTime end, int threshold);
    }
}