using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;
using Lte.MySqlFramework.Support;

namespace Lte.Evaluations.DataService.Kpi
{
    public class PrbRegionStatService
    {
        private readonly ITownPrbRepository _statRepository;
        private readonly ITownRepository _townRepository;

        public PrbRegionStatService(ITownPrbRepository statRepository, ITownRepository townRepository)
        {
            _statRepository = statRepository;
            _townRepository = townRepository;
        }

        public PrbRegionDateView QueryLastDateStat(DateTime initialDate, string city)
        {
            var stats = _statRepository.QueryLastDate(initialDate, (repository, beginDate, endDate) =>
            {
                var query =
                    _statRepository.GetAllList(x => x.StatTime >= beginDate & x.StatTime < endDate);
                return query.FilterTownList(_townRepository.GetAllList().Where(x => x.CityName == city).ToList());
            });
            var townViews = stats.ConstructViews<TownPrbStat, TownPrbView>(_townRepository);
            return townViews.QueryRegionDateDateView<PrbRegionDateView, DistrictPrbView, TownPrbView>(initialDate,
                DistrictPrbView.ConstructView);
        }

        public IEnumerable<PrbRegionDateView> QueryDateViews(DateTime begin, DateTime end, string city)
        {
            var query = _statRepository.GetAllList(x => x.StatTime >= begin & x.StatTime < end);
            var townViews = query.QueryTownStat<TownPrbStat, TownPrbView>(_townRepository, city);
            return
                townViews.QueryDateDateViews<PrbRegionDateView, DistrictPrbView, TownPrbView>(
                    DistrictPrbView.ConstructView);
        }
    }
}
