using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Support;
using Lte.Parameters.Abstract.Kpi;

namespace Lte.Evaluations.DataService.RegionKpi
{
    public class PreciseRegionStatService
    {
        private readonly ITownPreciseCoverage4GStatRepository _statRepository;
        private readonly ITownRepository _townRepository;

        public PreciseRegionStatService(ITownPreciseCoverage4GStatRepository statRepository,
            ITownRepository townRepository)
        {
            _statRepository = statRepository;
            _townRepository = townRepository;
        }

        public PreciseRegionDateView QueryLastDateStat(DateTime initialDate, string city)
        {
            var stats = _statRepository.QueryLastDate(initialDate, (repository, beginDate, endDate) =>
            {
                var query =
                    _statRepository.GetAllList(x => x.StatTime >= beginDate & x.StatTime < endDate);
                return query.FilterTownList(_townRepository.GetAllList().Where(x => x.CityName == city).ToList());
            });
            var townViews = stats.ConstructViews<TownPreciseCoverage4GStat, TownPreciseView>(_townRepository);
            return townViews.QueryRegionDateView<PreciseRegionDateView, DistrictPreciseView, TownPreciseView>(initialDate,
                DistrictPreciseView.ConstructView);
        }

        public IEnumerable<PreciseRegionDateView> QueryDateViews(DateTime begin, DateTime end, string city)
        {
            var query = _statRepository.GetAllList(x => x.StatTime >= begin & x.StatTime < end);
            var townViews = query.QueryTownStat<TownPreciseCoverage4GStat, TownPreciseView>(_townRepository, city);
            return
                townViews.QueryDateSpanViews<PreciseRegionDateView, DistrictPreciseView, TownPreciseView>(
                    DistrictPreciseView.ConstructView);
        }
    }
}
