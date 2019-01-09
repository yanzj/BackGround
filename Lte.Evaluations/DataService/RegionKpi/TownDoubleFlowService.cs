﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.Domain.Common.Wireless.Cell;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Kpi;
using Lte.MySqlFramework.Support;

namespace Lte.Evaluations.DataService.RegionKpi
{
    public class TownDoubleFlowService
    {
        private readonly IENodebRepository _eNodebRepository;
        private readonly IDoubleFlowHuaweiRepository _doubleFlowHuaweiRepository;
        private readonly IDoubleFlowZteRepository _doubleFlowZteRepository;

        public TownDoubleFlowService(IENodebRepository eNodebRepository,
            IDoubleFlowHuaweiRepository doubleFlowHuaweiRepository, IDoubleFlowZteRepository doubleFlowZteRepository)
        {
            _eNodebRepository = eNodebRepository;
            _doubleFlowHuaweiRepository = doubleFlowHuaweiRepository;
            _doubleFlowZteRepository = doubleFlowZteRepository;
        }

        public List<TownDoubleFlow> GetTownDoubleFlowStats(DateTime statDate,
            FrequencyBandType frequency = FrequencyBandType.All)
        {
            var end = statDate.AddDays(1);
            var townStatList = new List<TownDoubleFlow>();
            var huaweiStats = _doubleFlowHuaweiRepository.QueryZteFlows<DoubleFlowHuawei, IDoubleFlowHuaweiRepository>(frequency, statDate, end);
            townStatList.AddRange(huaweiStats.GetTownStats<DoubleFlowHuawei, TownDoubleFlow>(_eNodebRepository));
            var zteStats = _doubleFlowZteRepository.QueryZteFlows<DoubleFlowZte, IDoubleFlowZteRepository>(frequency, statDate, end);
            townStatList.AddRange(zteStats.GetTownStats<DoubleFlowZte, TownDoubleFlow>(_eNodebRepository));
            return townStatList;
        }

    }
}
