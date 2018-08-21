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
    public class TownQciService
    {
        private readonly IENodebRepository _eNodebRepository;
        private readonly IQciZteRepository _qciZteRepository;
        private readonly IQciHuaweiRepository _qciHuaweiRepository;

        public TownQciService(IENodebRepository eNodebRepository,
            IQciZteRepository qciZteRepository, IQciHuaweiRepository qciHuaweiRepository)
        {
            _eNodebRepository = eNodebRepository;
            _qciZteRepository = qciZteRepository;
            _qciHuaweiRepository = qciHuaweiRepository;
        }

        public List<TownQciStat> GetTownQciStats(DateTime statDate)
        {
            var end = statDate.AddDays(1);
            var townStatList = new List<TownQciStat>();
            var huaweiQcis = _qciHuaweiRepository.GetAllList(x => x.StatTime >= statDate && x.StatTime < end);
            townStatList.AddRange(huaweiQcis.GetTownStats<QciHuawei, TownQciStat>(_eNodebRepository));
            var zteQcis = _qciZteRepository.GetAllList(x => x.StatTime >= statDate && x.StatTime < end);
            townStatList.AddRange(zteQcis.GetTownStats<QciZte, TownQciStat>(_eNodebRepository));
            return townStatList;
        }

    }
}
