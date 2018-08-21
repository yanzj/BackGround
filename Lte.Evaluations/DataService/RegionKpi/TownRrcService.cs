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
    public class TownRrcService
    {
        private readonly IENodebRepository _eNodebRepository;
        private readonly IRrcHuaweiRepository _rrcHuaweiRepository;
        private readonly IRrcZteRepository _rrcZteRepository;

        public TownRrcService(IENodebRepository eNodebRepository, IRrcHuaweiRepository rrcHuaweiRepository,
            IRrcZteRepository rrcZteRepository)
        {
            _eNodebRepository = eNodebRepository;
            _rrcHuaweiRepository = rrcHuaweiRepository;
            _rrcZteRepository = rrcZteRepository;
        }

        public List<TownRrcStat> GetTownRrcStats(DateTime statDate)
        {
            var end = statDate.AddDays(1);
            var townStatList = new List<TownRrcStat>();
            var huaweiRrcs = _rrcHuaweiRepository.GetAllList(x => x.StatTime >= statDate && x.StatTime < end);
            townStatList.AddRange(huaweiRrcs.GetTownStats<RrcHuawei, TownRrcStat>(_eNodebRepository));
            var zteRrcs = _rrcZteRepository.GetAllList(x => x.StatTime >= statDate && x.StatTime < end);
            townStatList.AddRange(zteRrcs.GetTownStats<RrcZte, TownRrcStat>(_eNodebRepository));
            return townStatList;
        }

    }
}
