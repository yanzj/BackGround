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
        private readonly ITownHourUsersRepository _townHourUsersRepository;
        private readonly TownHourUsersService _usersService;
        private readonly ITownHourCqiRepository _townHourCqiRepository;
        private readonly TownHourCqiService _cqiService;

        public TownHourKpiService(ITownHourPrbRepository townHourPrbRepository, TownHourPrbService prbService,
            ITownHourUsersRepository townHourUsersRepository, TownHourUsersService usersService,
            ITownHourCqiRepository townHourCqiRepository, TownHourCqiService cqiService)
        {
            _townHourPrbRepository = townHourPrbRepository;
            _prbService = prbService;
            _townHourUsersRepository = townHourUsersRepository;
            _usersService = usersService;
            _townHourCqiRepository = townHourCqiRepository;
            _cqiService = cqiService;
        }

        public async Task<int[]> GenerateTownStats(DateTime statDate)
        {
            var end = statDate.AddDays(1);
            var item1 = _townHourPrbRepository.Count(x => x.StatDate >= statDate && x.StatDate < end);
            if (item1 == 0)
            {
                var townList = _prbService.GetTownPrbStats(statDate);
                foreach (var stat in townList.GetDateMergeStats(statDate))
                {
                    await _townHourPrbRepository.InsertAsync(stat);
                }
                item1 = _townHourPrbRepository.SaveChanges();
            }
            var item2 = _townHourUsersRepository.Count(x => x.StatDate >= statDate && x.StatDate < end);
            if (item2 == 0)
            {
                var townList = _usersService.GetTownUsersStats(statDate);
                foreach (var stat in townList.GetDateMergeStats(statDate))
                {
                    await _townHourUsersRepository.InsertAsync(stat);
                }
                item2 = _townHourUsersRepository.SaveChanges();
            }
            var item3 = _townHourCqiRepository.Count(x => x.StatDate >= statDate && x.StatDate < end);
            if (item3 == 0)
            {
                var townList = _cqiService.GetTownCqiStats(statDate);
                foreach (var stat in townList.GetDateMergeStats(statDate))
                {
                    await _townHourCqiRepository.InsertAsync(stat);
                }
                item3 = _townHourCqiRepository.SaveChanges();
            }
            return new [] { item1, item2, item3 };
        }

    }
}
