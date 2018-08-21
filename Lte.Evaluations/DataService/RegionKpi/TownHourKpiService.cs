using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.MySqlFramework.Abstract.RegionKpi;

namespace Lte.Evaluations.DataService.RegionKpi
{
    public class TownHourKpiService
    {
        private readonly ITownHourPrbRepository _townHourPrbRepository;
        private readonly TownHourPrbService _prbService;

        public TownHourKpiService(ITownHourPrbRepository townHourPrbRepository, TownHourPrbService prbService)
        {
            _townHourPrbRepository = townHourPrbRepository;
            _prbService = prbService;
        }

        public async Task<int[]> GenerateTownStats(DateTime statDate)
        {
            var end = statDate.AddDays(1);
            var item1 = _townHourPrbRepository.Count(x => x.StatDate >= statDate && x.StatDate < end);
            if (item1 == 0)
            {
                var townCqiList = _prbService.GetTownPrbStats(statDate);
                foreach (var stat in townCqiList.GetDateMergeStats(statDate))
                {
                    await _townHourPrbRepository.InsertAsync(stat);
                }
                item1 = _townHourPrbRepository.SaveChanges();
            }
            return new [] { item1 };
        }

    }
}
