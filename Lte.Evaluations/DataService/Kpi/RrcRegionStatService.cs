using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities;
using Lte.Evaluations.ViewModels.RegionKpi;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;

namespace Lte.Evaluations.DataService.Kpi
{
    public class RrcRegionStatService
    {
        private readonly ITownRrcRepository _statRepository;
        private readonly ITownRepository _townRepository;

        public RrcRegionStatService(ITownRrcRepository statRepository, ITownRepository townRepository)
        {
            _statRepository = statRepository;
            _townRepository = townRepository;
        }

        public RrcRegionDateView QueryLastDateStat(DateTime initialDate, string city)
        {
            var stats = _statRepository.QueryLastDate(initialDate, (repository, beginDate, endDate) =>
            {
                var query =
                    _statRepository.GetAllList(x => x.StatTime >= beginDate & x.StatTime < endDate);
                return query.FilterTownList(_townRepository.GetAllList().Where(x => x.CityName == city).ToList());
            });
            var townViews = stats.ConstructViews<TownRrcStat, TownRrcView>(_townRepository);
            return townViews.QueryRegionDateView<RrcRegionDateView, DistrictRrcView, TownRrcView>(initialDate,
                DistrictRrcView.ConstructView);
        }

        public IEnumerable<RrcRegionDateView> QueryDateViews(DateTime begin, DateTime end, string city)
        {
            var query = _statRepository.GetAllList(x => x.StatTime >= begin & x.StatTime < end);
            var townViews = query.QueryTownStat<TownRrcStat, TownRrcView>(_townRepository, city);
            return
                townViews.QueryDateSpanViews<RrcRegionDateView, DistrictRrcView, TownRrcView>(
                    DistrictRrcView.ConstructView);
        }
    }
}