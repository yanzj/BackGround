using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Evaluations.DataService.Basic;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Kpi;
using Lte.MySqlFramework.Support;

namespace Lte.Evaluations.DataService.RegionKpi
{
    public class RegionTownFlowService
    {
        private readonly BandCellService _cellService;
        private readonly IENodebRepository _eNodebRepository;
        private readonly IFlowHuaweiRepository _huaweiRepository;
        private readonly IFlowZteRepository _zteRepository;

        public RegionTownFlowService(BandCellService cellService, IENodebRepository eNodebRepository,
            IFlowHuaweiRepository huaweiRepository, IFlowZteRepository zteRepository)
        {
            _cellService = cellService;
            _eNodebRepository = eNodebRepository;
            _huaweiRepository = huaweiRepository;
            _zteRepository = zteRepository;
        }

        public List<TownFlowStat> GetTownFlowStats(DateTime statDate,
            FrequencyBandType frequency = FrequencyBandType.All)
        {
            var end = statDate.AddDays(1);
            var townStatList = new List<TownFlowStat>();
            var huaweiFlows = _huaweiRepository.GetAllList(x => x.StatTime >= statDate && x.StatTime < end);
            var zteFlows = _zteRepository.QueryZteFlows<FlowZte, IFlowZteRepository>(frequency, statDate, end);
            if (frequency != FrequencyBandType.All)
            {
                var cells = _cellService.GetHuaweiCellsByBandType(frequency);
                huaweiFlows = (from f in huaweiFlows
                    join c in cells on new { f.ENodebId, f.LocalCellId } equals
                        new { c.ENodebId, LocalCellId = c.LocalSectorId }
                    select f).ToList();
            }
            townStatList.AddRange(huaweiFlows.GetTownStats<FlowHuawei, TownFlowStat>(_eNodebRepository));
            townStatList.AddRange(zteFlows.GetTownStats<FlowZte, TownFlowStat>(_eNodebRepository));
            return townStatList;
        }

    }
}
