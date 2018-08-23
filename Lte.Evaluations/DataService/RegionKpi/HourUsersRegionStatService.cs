using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Abstract.RegionKpi;
using Lte.MySqlFramework.Entities.RegionKpi;
using Lte.MySqlFramework.Support.View;

namespace Lte.Evaluations.DataService.RegionKpi
{
    public class HourUsersRegionStatService
    {
        private readonly ITownHourUsersRepository _statRepository;
        private readonly ITownRepository _townRepository;

        public HourUsersRegionStatService(ITownHourUsersRepository statRepository, ITownRepository townRepository)
        {
            _statRepository = statRepository;
            _townRepository = townRepository;
        }

        public HourUsersRegionDateView QueryLastDateStat(DateTime initialDate, string city)
        {
            var stats = _statRepository.QueryDate(initialDate, (repository, beginDate, endDate) =>
            {
                var query =
                    _statRepository.GetAllList(x => x.StatDate >= beginDate & x.StatDate < endDate);
                return query.FilterTownList(_townRepository.GetAllList().Where(x => x.CityName == city).ToList());
            });
            var townViews = stats.ConstructViews<TownHourUsers, TownHourUsersView>(_townRepository);
            return townViews.QueryRegionDateDateView<HourUsersRegionDateView, DistrictHourUsersView, TownHourUsersView>(initialDate,
                DistrictHourUsersView.ConstructView);
        }

        public IEnumerable<HourUsersRegionDateView> QueryDateViews(DateTime begin, DateTime end, string city)
        {
            var query = _statRepository.GetAllList(x => x.StatDate >= begin & x.StatDate < end);
            var townViews = query.QueryTownStat<TownHourUsers, TownHourUsersView>(_townRepository, city);
            return
                townViews.QueryDateDateViews<HourUsersRegionDateView, DistrictHourUsersView, TownHourUsersView>(
                    DistrictHourUsersView.ConstructView);
        }
    }
}
