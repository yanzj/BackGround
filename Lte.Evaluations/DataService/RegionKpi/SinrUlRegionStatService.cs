using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.Mr;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.MySqlFramework.Abstract.Mr;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Entities.Mr;
using Lte.MySqlFramework.Entities.RegionKpi;
using Lte.MySqlFramework.Support.View;

namespace Lte.Evaluations.DataService.RegionKpi
{
    public class SinrUlRegionStatService
    {
        private readonly ITownMrsSinrUlRepository _statRepository;
        private readonly ITownRepository _townRepository;

        public SinrUlRegionStatService(ITownMrsSinrUlRepository statRepository, ITownRepository townRepository)
        {
            _statRepository = statRepository;
            _townRepository = townRepository;
        }

        public SinrUlRegionDateView QueryLastDateStat(DateTime initialDate, string city)
        {
            var stats = _statRepository.QueryDate(initialDate, (repository, beginDate, endDate) =>
            {
                var query =
                    _statRepository.GetAllList(x => x.StatDate >= beginDate & x.StatDate < endDate);
                return query.FilterTownList(_townRepository.GetAllList().Where(x => x.CityName == city).ToList());
            });
            var townViews = stats.ConstructViews<TownMrsSinrUl, TownMrsSinrUlView>(_townRepository);
            return townViews.QueryRegionDateDateView<SinrUlRegionDateView, DistrictMrsSinrUlView, TownMrsSinrUlView>(initialDate,
                DistrictMrsSinrUlView.ConstructView);
        }

        public IEnumerable<SinrUlRegionDateView> QueryDateViews(DateTime begin, DateTime end, string city)
        {
            var query = _statRepository.GetAllList(x => x.StatDate >= begin & x.StatDate < end);
            var townViews = query.QueryTownStat<TownMrsSinrUl, TownMrsSinrUlView>(_townRepository, city);
            return
                townViews.QueryDateDateViews<SinrUlRegionDateView, DistrictMrsSinrUlView, TownMrsSinrUlView>(
                    DistrictMrsSinrUlView.ConstructView);
        }
    }
}
