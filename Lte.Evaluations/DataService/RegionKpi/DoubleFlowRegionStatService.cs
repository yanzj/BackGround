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
using Lte.MySqlFramework.Entities.RegionKpi;
using Lte.MySqlFramework.Support.View;

namespace Lte.Evaluations.DataService.RegionKpi
{
    public class DoubleFlowRegionStatService
    {
        private readonly ITownDoubleFlowRepository _statRepository;
        private readonly ITownRepository _townRepository;

        public DoubleFlowRegionStatService(ITownDoubleFlowRepository statRepository, ITownRepository townRepository)
        {
            _statRepository = statRepository;
            _townRepository = townRepository;
        }

        public DoubleFlowRegionDateView QueryLastDateStat(DateTime initialDate, string city)
        {
            var stats = _statRepository.QueryLastDate(initialDate, (repository, beginDate, endDate) =>
            {
                var query =
                    _statRepository.GetAllList(x =>
                        x.StatTime >= beginDate & x.StatTime < endDate && x.FrequencyBandType == FrequencyBandType.All);
                return query.FilterTownList(_townRepository.GetAllList().Where(x => x.CityName == city).ToList());
            });
            var townViews = stats.ConstructViews<TownDoubleFlow, TownDoubleFlowView>(_townRepository);
            return townViews.QueryRegionDateView<DoubleFlowRegionDateView, DistrictDoubleFlowView, TownDoubleFlowView>(
                initialDate,
                DistrictDoubleFlowView.ConstructView);
        }

        public IEnumerable<DoubleFlowRegionDateView> QueryDateViews(DateTime begin, DateTime end, string city)
        {
            var query = _statRepository.GetAllList(x =>
                x.StatTime >= begin & x.StatTime < end && x.FrequencyBandType == FrequencyBandType.All);
            var townViews = query.QueryTownStat<TownDoubleFlow, TownDoubleFlowView>(_townRepository, city);
            return
                townViews.QueryDateSpanViews<DoubleFlowRegionDateView, DistrictDoubleFlowView, TownDoubleFlowView>(
                    DistrictDoubleFlowView.ConstructView);
        }
        
        public IEnumerable<DoubleFlowRegionFrequencyView> QueryCityFrequencyViews(DateTime begin, DateTime end,
            string city)
        {
            var query = _statRepository.GetAllList(x =>
                 x.StatTime >= begin && x.StatTime < end && x.FrequencyBandType != FrequencyBandType.All);
            if (!query.Any()) return new List<DoubleFlowRegionFrequencyView>();
            return query.GroupBy(x => x.StatTime.Date).Select(g => new DoubleFlowRegionFrequencyView
            {
                Region = city,
                StatDate = g.Key,
                FrequencyViews = new List<FrequencyDoubleFlowView>
                {
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band2100)
                        .MapTo<IEnumerable<FrequencyDoubleFlowView>>().ArraySum(),
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band1800)
                        .MapTo<IEnumerable<FrequencyDoubleFlowView>>().ArraySum(),
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band800VoLte)
                        .MapTo<IEnumerable<FrequencyDoubleFlowView>>().ArraySum()
                }
            });
        }

        public IEnumerable<DoubleFlowRegionFrequencyView> QueryDistrictFrequencyViews(DateTime begin, DateTime end,
            string city, string district)
        {
            var towns = _townRepository.GetAllList(x => x.CityName == city && x.DistrictName == district);
            if (!towns.Any()) return new List<DoubleFlowRegionFrequencyView>();
            var query = _statRepository.GetAllList(x =>
                x.StatTime >= begin && x.StatTime < end && x.FrequencyBandType != FrequencyBandType.All);
            if (!query.Any()) return new List<DoubleFlowRegionFrequencyView>();
            var stats = from q in query join t in towns on q.TownId equals t.Id select q;
            return stats.GroupBy(x => x.StatTime.Date).Select(g => new DoubleFlowRegionFrequencyView
            {
                Region = district,
                StatDate = g.Key,
                FrequencyViews = new List<FrequencyDoubleFlowView>
                {
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band2100).ArraySum()
                        .MapTo<FrequencyDoubleFlowView>(),
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band1800).ArraySum()
                        .MapTo<FrequencyDoubleFlowView>(),
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band800VoLte).ArraySum()
                        .MapTo<FrequencyDoubleFlowView>()
                }
            });
        }

        public IEnumerable<DoubleFlowRegionFrequencyView> QueryTownFrequencyViews(DateTime begin, DateTime end,
            string city, string district, string town)
        {
            var townItem = _townRepository.FirstOrDefault(x =>
                x.CityName == city && x.DistrictName == district && x.TownName == town);
            if (townItem == null) return new List<DoubleFlowRegionFrequencyView>();
            var query = _statRepository.GetAllList(x =>
                x.StatTime >= begin && x.StatTime < end && x.FrequencyBandType != FrequencyBandType.All &&
                x.TownId == townItem.Id);
            if (!query.Any()) return new List<DoubleFlowRegionFrequencyView>();
            return query.GroupBy(x => x.StatTime.Date).Select(g => new DoubleFlowRegionFrequencyView
            {
                Region = town,
                StatDate = g.Key,
                FrequencyViews = new List<FrequencyDoubleFlowView>
                {
                    g.FirstOrDefault(x => x.FrequencyBandType == FrequencyBandType.Band2100)
                        .MapTo<FrequencyDoubleFlowView>(),
                    g.FirstOrDefault(x => x.FrequencyBandType == FrequencyBandType.Band1800)
                        .MapTo<FrequencyDoubleFlowView>(),
                    g.FirstOrDefault(x => x.FrequencyBandType == FrequencyBandType.Band800VoLte)
                        .MapTo<FrequencyDoubleFlowView>()
                }
            });
        }
    }
}
