using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.Domain.Common.Wireless.Cell;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Abstract.RegionKpi;
using Lte.MySqlFramework.Support;

namespace Lte.Evaluations.DataService.RegionKpi
{
    public class PreciseRegionStatService
    {
        private readonly ITownPreciseCoverageRepository _statRepository;
        private readonly ITownRepository _townRepository;

        public PreciseRegionStatService(ITownPreciseCoverageRepository statRepository,
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
                    _statRepository.GetAllList(x =>
                        x.StatTime >= beginDate & x.StatTime < endDate && x.FrequencyBandType == FrequencyBandType.All);
                return query.FilterTownList(_townRepository.GetAllList().Where(x => x.CityName == city).ToList());
            });
            var townViews = stats.ConstructViews<TownPreciseStat, TownPreciseView>(_townRepository);
            return townViews.QueryRegionDateView<PreciseRegionDateView, DistrictPreciseView, TownPreciseView>(initialDate,
                DistrictPreciseView.ConstructView);
        }

        public IEnumerable<PreciseRegionDateView> QueryDateViews(DateTime begin, DateTime end, string city)
        {
            var query = _statRepository.GetAllList(x =>
                x.StatTime >= begin & x.StatTime < end && x.FrequencyBandType == FrequencyBandType.All);
            var townViews = query.QueryTownStat<TownPreciseStat, TownPreciseView>(_townRepository, city);
            return
                townViews.QueryDateSpanViews<PreciseRegionDateView, DistrictPreciseView, TownPreciseView>(
                    DistrictPreciseView.ConstructView);
        }
    }
}
