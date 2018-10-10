using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.Domain.Common.Wireless.Cell;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Abstract.RegionKpi;
using Lte.MySqlFramework.Entities.RegionKpi;
using Lte.MySqlFramework.Support.View;

namespace Lte.Evaluations.DataService.RegionKpi
{
    public class CqiRegionStatService
    {
        private readonly ITownQciRepository _statRepository;
        private readonly ITownRepository _townRepository;
        private readonly ITownCqiRepository _cqiRepository;

        public CqiRegionStatService(ITownQciRepository statRepository, ITownRepository townRepository,
            ITownCqiRepository cqiRepository)
        {
            _statRepository = statRepository;
            _townRepository = townRepository;
            _cqiRepository = cqiRepository;
        }

        public QciRegionDateView QueryLastDateStat(DateTime initialDate, string city)
        {
            var stats = _statRepository.QueryLastDate(initialDate, (repository, beginDate, endDate) =>
            {
                var query =
                    _statRepository.GetAllList(x => x.StatTime >= beginDate & x.StatTime < endDate);
                return query.FilterTownList(_townRepository.GetAllList().Where(x => x.CityName == city).ToList());
            });
            var townViews = stats.ConstructViews<TownQciStat, TownQciView>(_townRepository);
            return townViews.QueryRegionDateView<QciRegionDateView, DistrictQciView, TownQciView>(initialDate,
                DistrictQciView.ConstructView);
        }

        public CqiRegionDateView QueryLastDateCqi(DateTime initialDate, string city)
        {
            var stats = _cqiRepository.QueryLastDate(initialDate, (repository, beginDate, endDate) =>
            {
                var query =
                    _cqiRepository.GetAllList(x => x.StatTime >= beginDate & x.StatTime < endDate && x.FrequencyBandType == FrequencyBandType.All);
                return query.FilterTownList(_townRepository.GetAllList().Where(x => x.CityName == city).ToList());
            });
            var townViews = stats.ConstructViews<TownCqiStat, TownCqiView>(_townRepository);
            return townViews.QueryRegionDateView<CqiRegionDateView, DistrictCqiView, TownCqiView>(initialDate,
                DistrictCqiView.ConstructView);
        }

        public IEnumerable<QciRegionDateView> QueryDateViews(DateTime begin, DateTime end, string city)
        {
            var query = _statRepository.GetAllList(x => x.StatTime >= begin & x.StatTime < end);
            var townViews = query.QueryTownStat<TownQciStat, TownQciView>(_townRepository, city);
            return
                townViews.QueryDateSpanViews<QciRegionDateView, DistrictQciView, TownQciView>(
                    DistrictQciView.ConstructView);
        }

        public IEnumerable<CqiRegionDateView> QueryDateCqis(DateTime begin, DateTime end, string city)
        {
            var query = _cqiRepository.GetAllList(x => x.StatTime >= begin & x.StatTime < end && x.FrequencyBandType == FrequencyBandType.All);
            var townViews = query.QueryTownStat<TownCqiStat, TownCqiView>(_townRepository, city);
            return
                townViews.QueryDateSpanViews<CqiRegionDateView, DistrictCqiView, TownCqiView>(
                    DistrictCqiView.ConstructView);
        }
    }
}