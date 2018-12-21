using System;
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
    public class TownPrbService
    {
        private readonly IENodebRepository _eNodebRepository;
        private readonly IPrbHuaweiRepository _prbHuaweiRepository;
        private readonly IPrbZteRepository _prbZteRepository;

        public TownPrbService(IENodebRepository eNodebRepository,
            IPrbZteRepository prbZteRepository, IPrbHuaweiRepository prbHuaweiRepository)
        {
            _eNodebRepository = eNodebRepository;
            _prbHuaweiRepository = prbHuaweiRepository;
            _prbZteRepository = prbZteRepository;
        }

        public List<TownPrbStat> GetTownPrbStats(DateTime statDate,
            FrequencyBandType frequency = FrequencyBandType.All)
        {
            var end = statDate.AddDays(1);
            var townStatList = new List<TownPrbStat>();
            var huaweiPrbs = _prbHuaweiRepository.QueryZteFlows<PrbHuawei, IPrbHuaweiRepository>(frequency, statDate, end);
            townStatList.AddRange(huaweiPrbs.GetTownStats<PrbHuawei, TownPrbStat>(_eNodebRepository));
            var ztePrbs = _prbZteRepository.QueryZteFlows<PrbZte, IPrbZteRepository>(frequency, statDate, end);
            townStatList.AddRange(ztePrbs.GetTownStats<PrbZte, TownPrbStat>(_eNodebRepository));
            return townStatList;
        }

    }
}
