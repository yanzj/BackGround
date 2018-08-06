using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;
using Lte.MySqlFramework.Support;

namespace Lte.Evaluations.DataService.Kpi
{
    public class CoverageRegionStatService
    {
        private readonly ITownCoverageRepository _statRepository;
        private readonly ITownRepository _townRepository;

        public CoverageRegionStatService(ITownCoverageRepository statRepository, ITownRepository townRepository)
        {
            _statRepository = statRepository;
            _townRepository = townRepository;
        }

        public CoverageRegionDateView QueryLastDateStat(DateTime initialDate, string city)
        {
            var stats = _statRepository.QueryDate(initialDate, (repository, beginDate, endDate) =>
            {
                var query =
                    _statRepository.GetAllList(x => x.StatDate >= beginDate & x.StatDate < endDate);
                return query.FilterTownList(_townRepository.GetAllList().Where(x => x.CityName == city).ToList());
            });
            var townViews = stats.ConstructViews<TownCoverageStat, TownCoverageView>(_townRepository);
            return townViews.QueryRegionDateDateView<CoverageRegionDateView, DistrictCoverageView, TownCoverageView>(initialDate,
                DistrictCoverageView.ConstructView);
        }

        public IEnumerable<CoverageRegionDateView> QueryDateViews(DateTime begin, DateTime end, string city)
        {
            var query = _statRepository.GetAllList(x => x.StatDate >= begin & x.StatDate < end);
            var townViews = query.QueryTownStat<TownCoverageStat, TownCoverageView>(_townRepository, city);
            return
                townViews.QueryDateDateViews<CoverageRegionDateView, DistrictCoverageView, TownCoverageView>(
                    DistrictCoverageView.ConstructView);
        }
    }
}
