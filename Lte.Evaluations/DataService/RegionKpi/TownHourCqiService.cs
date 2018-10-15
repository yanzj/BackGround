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

namespace Lte.Evaluations.DataService.RegionKpi
{
    public class TownHourCqiService
    {
        private readonly IENodebRepository _eNodebRepository;
        private readonly IHourCqiRepository _usersRepository;
        private readonly ICellRepository _cellRepository;

        public TownHourCqiService(IENodebRepository eNodebRepository, IHourCqiRepository usersRepository, ICellRepository cellRepository)
        {
            _eNodebRepository = eNodebRepository;
            _usersRepository = usersRepository;
            _cellRepository = cellRepository;
        }

        public List<TownHourCqi> GetTownCqiStats(DateTime statDate, FrequencyBandType bandType)
        {
            var end = statDate.AddDays(1);
            var userses = _usersRepository.GetAllList(x => x.StatTime >= statDate && x.StatTime < end);
            var townStatList = userses.GetTownFrequencyItems<HourCqi, TownHourCqi>(bandType, _cellRepository, _eNodebRepository);
            return townStatList.ToList();
        }

    }
}
