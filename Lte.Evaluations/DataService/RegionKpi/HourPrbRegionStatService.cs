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
    public class HourPrbRegionStatService
    {
        private readonly ITownHourPrbRepository _statRepository;
        private readonly ITownRepository _townRepository;

        public HourPrbRegionStatService(ITownHourPrbRepository statRepository, ITownRepository townRepository)
        {
            _statRepository = statRepository;
            _townRepository = townRepository;
        }

        public HourPrbRegionDateView QueryLastDateStat(DateTime initialDate, string city)
        {
            var stats = _statRepository.QueryDate(initialDate, (repository, beginDate, endDate) =>
            {
                var query =
                    _statRepository.GetAllList(x => x.StatDate >= beginDate & x.StatDate < endDate);
                return query.FilterTownList(_townRepository.GetAllList().Where(x => x.CityName == city).ToList());
            });
            var townViews = stats.ConstructViews<TownHourPrb, TownHourPrbView>(_townRepository);
            return townViews.QueryRegionDateDateView<HourPrbRegionDateView, DistrictHourPrbView, TownHourPrbView>(initialDate,
                DistrictHourPrbView.ConstructView);
        }

        public IEnumerable<HourPrbRegionDateView> QueryDateViews(DateTime begin, DateTime end, string city)
        {
            var query = _statRepository.GetAllList(x => x.StatDate >= begin & x.StatDate < end);
            var townViews = query.QueryTownStat<TownHourPrb, TownHourPrbView>(_townRepository, city);
            return
                townViews.QueryDateDateViews<HourPrbRegionDateView, DistrictHourPrbView, TownHourPrbView>(
                    DistrictHourPrbView.ConstructView);
        }
    }
}
