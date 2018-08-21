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
    public class TownHourPrbService
    {
        private readonly IENodebRepository _eNodebRepository;
        private readonly IHourPrbRepository _prbRepository;

        public TownHourPrbService(IENodebRepository eNodebRepository, IHourPrbRepository prbRepository)
        {
            _eNodebRepository = eNodebRepository;
            _prbRepository = prbRepository;
        }

        public List<TownHourPrb> GetTownPrbStats(DateTime statDate)
        {
            var end = statDate.AddDays(1);
            var prbs = _prbRepository.GetAllList(x => x.StatTime >= statDate && x.StatTime < end);
            var townStatList = prbs.GetTownDateStats<HourPrb, TownHourPrb>(_eNodebRepository);
            return townStatList.ToList();
        }

    }
}
