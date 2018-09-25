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
using Lte.MySqlFramework.Support.View;

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

        public IEnumerable<PreciseRegionFrequencyView> QueryCityFrequencyViews(DateTime begin, DateTime end,
            string city)
        {
            var query = _statRepository.GetAllList(x =>
                x.StatTime >= begin && x.StatTime < end && x.FrequencyBandType != FrequencyBandType.All);
            return query.GroupBy(x => x.StatTime.Date).Select(g => new PreciseRegionFrequencyView
            {
                Region = city,
                StatDate = g.Key,
                FrequencyViews = new List<FrequencyPreciseView>
                {
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band2100)
                        .MapTo<IEnumerable<FrequencyPreciseView>>().ArraySum(),
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band1800)
                        .MapTo<IEnumerable<FrequencyPreciseView>>().ArraySum(),
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band800VoLte)
                        .MapTo<IEnumerable<FrequencyPreciseView>>().ArraySum()
                }
            });
        }
        
        public IEnumerable<PreciseRegionFrequencyView> QueryDistrictFrequencyViews(DateTime begin, DateTime end,
            string city, string district)
        {
            var towns = _townRepository.GetAllList(x => x.CityName == city && x.DistrictName == district);
            if (!towns.Any()) return new List<PreciseRegionFrequencyView>();
            var query = _statRepository.GetAllList(x =>
                x.StatTime >= begin && x.StatTime < end && x.FrequencyBandType != FrequencyBandType.All);
            var stats = from q in query join t in towns on q.TownId equals t.Id select q;
            return stats.GroupBy(x => x.StatTime.Date).Select(g => new PreciseRegionFrequencyView
            {
                Region = district,
                StatDate = g.Key,
                FrequencyViews = new List<FrequencyPreciseView>
                {
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band2100).ArraySum()
                        .MapTo<FrequencyPreciseView>(),
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band1800).ArraySum()
                        .MapTo<FrequencyPreciseView>(),
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band800VoLte).ArraySum()
                        .MapTo<FrequencyPreciseView>()
                }
            });
        }
        
        public IEnumerable<PreciseRegionFrequencyView> QueryTownFrequencyViews(DateTime begin, DateTime end,
            string city, string district, string town)
        {
            var townItem = _townRepository.FirstOrDefault(x =>
                x.CityName == city && x.DistrictName == district && x.TownName == town);
            if (townItem == null) return new List<PreciseRegionFrequencyView>();
            var query = _statRepository.GetAllList(x =>
                x.StatTime >= begin && x.StatTime < end && x.FrequencyBandType != FrequencyBandType.All &&
                x.TownId == townItem.Id);
            return query.GroupBy(x => x.StatTime.Date).Select(g => new PreciseRegionFrequencyView
            {
                Region = town,
                StatDate = g.Key,
                FrequencyViews = new List<FrequencyPreciseView>
                {
                    g.FirstOrDefault(x => x.FrequencyBandType == FrequencyBandType.Band2100)
                        .MapTo<FrequencyPreciseView>(),
                    g.FirstOrDefault(x => x.FrequencyBandType == FrequencyBandType.Band1800)
                        .MapTo<FrequencyPreciseView>(),
                    g.FirstOrDefault(x => x.FrequencyBandType == FrequencyBandType.Band800VoLte)
                        .MapTo<FrequencyPreciseView>()
                }
            });
        }
    }
}
