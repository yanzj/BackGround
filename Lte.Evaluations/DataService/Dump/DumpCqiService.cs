using System;
using System.Collections.Generic;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Evaluations.DataService.RegionKpi;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Kpi;
using Lte.MySqlFramework.Support;

namespace Lte.Evaluations.DataService.Dump
{
    public class DumpCqiService
    {
        private readonly IENodebRepository _eNodebRepository;
        private readonly ICqiZteRepository _cqiZteRepository;
        private readonly ICqiHuaweiRepository _cqiHuaweiRepository;

        public DumpCqiService(IENodebRepository eNodebRepository,
            ICqiZteRepository cqiZteRepository, ICqiHuaweiRepository cqiHuaweiRepository)
        {
            _eNodebRepository = eNodebRepository;
            _cqiZteRepository = cqiZteRepository;
            _cqiHuaweiRepository = cqiHuaweiRepository;
        }

        public List<TownCqiStat> GetTownCqiStats(DateTime statDate,
            FrequencyBandType frequency = FrequencyBandType.All)
        {
            var end = statDate.AddDays(1);
            var townStatList = new List<TownCqiStat>();
            var huaweiCqis = _cqiHuaweiRepository.QueryZteFlows<CqiHuawei, ICqiHuaweiRepository>(frequency, statDate, end);
            townStatList.AddRange(huaweiCqis.GetTownStats<CqiHuawei, TownCqiStat>(_eNodebRepository));
            var zteCqis = _cqiZteRepository.QueryZteFlows<CqiZte, ICqiZteRepository>(frequency, statDate, end);
            townStatList.AddRange(zteCqis.GetTownStats<CqiZte, TownCqiStat>(_eNodebRepository));
            return townStatList;
        }

    }
}
