using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Kpi;

namespace Lte.Evaluations.DataService.RegionKpi
{
    public class TownCqiService
    {
        private readonly IENodebRepository _eNodebRepository;
        private readonly ICqiZteRepository _cqiZteRepository;
        private readonly ICqiHuaweiRepository _cqiHuaweiRepository;

        public TownCqiService(IENodebRepository eNodebRepository,
            ICqiZteRepository cqiZteRepository, ICqiHuaweiRepository cqiHuaweiRepository)
        {
            _eNodebRepository = eNodebRepository;
            _cqiZteRepository = cqiZteRepository;
            _cqiHuaweiRepository = cqiHuaweiRepository;
        }

        public List<TownCqiStat> GetTownCqiStats(DateTime statDate)
        {
            var end = statDate.AddDays(1);
            var townStatList = new List<TownCqiStat>();
            var huaweiCqis = _cqiHuaweiRepository.GetAllList(x => x.StatTime >= statDate && x.StatTime < end);
            townStatList.AddRange(huaweiCqis.GetTownStats<CqiHuawei, TownCqiStat>(_eNodebRepository));
            var zteCqis = _cqiZteRepository.GetAllList(x => x.StatTime >= statDate && x.StatTime < end);
            townStatList.AddRange(zteCqis.GetTownStats<CqiZte, TownCqiStat>(_eNodebRepository));
            return townStatList;
        }

    }
}
