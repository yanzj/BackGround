using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Regular;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Abstract.RegionKpi;
using Lte.MySqlFramework.Entities.Mr;
using Lte.MySqlFramework.Support.View;

namespace Lte.Evaluations.DataService.RegionKpi
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
                    _statRepository.GetAllList(x =>
                        x.StatDate >= beginDate & x.StatDate < endDate && x.FrequencyBandType == FrequencyBandType.All);
                return query.FilterTownList(_townRepository.GetAllList().Where(x => x.CityName == city).ToList());
            });
            var townViews = stats.ConstructViews<TownCoverageStat, TownCoverageView>(_townRepository);
            return townViews.QueryRegionDateDateView<CoverageRegionDateView, DistrictCoverageView, TownCoverageView>(initialDate,
                DistrictCoverageView.ConstructView);
        }

        public IEnumerable<CoverageRegionDateView> QueryDateViews(DateTime begin, DateTime end, string city)
        {
            var query = _statRepository.GetAllList(x =>
                x.StatDate >= begin & x.StatDate < end && x.FrequencyBandType == FrequencyBandType.All);
            var townViews = query.QueryTownStat<TownCoverageStat, TownCoverageView>(_townRepository, city);
            return
                townViews.QueryDateDateViews<CoverageRegionDateView, DistrictCoverageView, TownCoverageView>(
                    DistrictCoverageView.ConstructView);
        }
        
        public IEnumerable<CoverageRegionFrequencyView> QueryCityFrequencyViews(DateTime begin, DateTime end,
            string city)
        {
            var query = _statRepository.GetAllList(x =>
                x.StatDate >= begin && x.StatDate < end && x.FrequencyBandType != FrequencyBandType.All);
            return query.GroupBy(x => x.StatDate.Date).Select(g => new CoverageRegionFrequencyView
            {
                Region = city,
                StatDate = g.Key,
                FrequencyViews = new List<FrequencyCoverageView>
                {
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band2100)
                        .MapTo<IEnumerable<FrequencyCoverageView>>().ArraySum(),
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band1800)
                        .MapTo<IEnumerable<FrequencyCoverageView>>().ArraySum(),
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band800VoLte)
                        .MapTo<IEnumerable<FrequencyCoverageView>>().ArraySum()
                }
            });
        }
        
        public IEnumerable<CoverageRegionFrequencyView> QueryDistrictFrequencyViews(DateTime begin, DateTime end,
            string city, string district)
        {
            var towns = _townRepository.GetAllList(x => x.CityName == city && x.DistrictName == district);
            if (!towns.Any()) return new List<CoverageRegionFrequencyView>();
            var query = _statRepository.GetAllList(x =>
                x.StatDate >= begin && x.StatDate < end && x.FrequencyBandType != FrequencyBandType.All);
            var stats = from q in query join t in towns on q.TownId equals t.Id select q;
            return stats.GroupBy(x => x.StatDate.Date).Select(g => new CoverageRegionFrequencyView
            {
                Region = district,
                StatDate = g.Key,
                FrequencyViews = new List<FrequencyCoverageView>
                {
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band2100).ArraySum()
                        .MapTo<FrequencyCoverageView>(),
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band1800).ArraySum()
                        .MapTo<FrequencyCoverageView>(),
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band800VoLte).ArraySum()
                        .MapTo<FrequencyCoverageView>()
                }
            });
        }
        
        public IEnumerable<CoverageRegionFrequencyView> QueryTownFrequencyViews(DateTime begin, DateTime end,
            string city, string district, string town)
        {
            var townItem = _townRepository.FirstOrDefault(x =>
                x.CityName == city && x.DistrictName == district && x.TownName == town);
            if (townItem == null) return new List<CoverageRegionFrequencyView>();
            var query = _statRepository.GetAllList(x =>
                x.StatDate >= begin && x.StatDate < end && x.FrequencyBandType != FrequencyBandType.All &&
                x.TownId == townItem.Id);
            return query.GroupBy(x => x.StatDate.Date).Select(g => new CoverageRegionFrequencyView
            {
                Region = town,
                StatDate = g.Key,
                FrequencyViews = new List<FrequencyCoverageView>
                {
                    g.FirstOrDefault(x => x.FrequencyBandType == FrequencyBandType.Band2100)
                        .MapTo<FrequencyCoverageView>(),
                    g.FirstOrDefault(x => x.FrequencyBandType == FrequencyBandType.Band1800)
                        .MapTo<FrequencyCoverageView>(),
                    g.FirstOrDefault(x => x.FrequencyBandType == FrequencyBandType.Band800VoLte)
                        .MapTo<FrequencyCoverageView>()
                }
            });
        }
    }
}
