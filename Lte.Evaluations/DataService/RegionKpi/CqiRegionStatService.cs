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

        public IEnumerable<CqiRegionFrequencyView> QueryCityFrequencyViews(DateTime begin, DateTime end,
            string city)
        {
            var query =_cqiRepository.GetAllList(x =>
                x.StatTime >= begin && x.StatTime < end && x.FrequencyBandType != FrequencyBandType.All);
            if (!query.Any()) return new List<CqiRegionFrequencyView>();
            return query.GroupBy(x => x.StatTime.Date).Select(g => new CqiRegionFrequencyView
            {
                Region = city,
                StatDate = g.Key,
                FrequencyViews = new List<FrequencyCqiView>
                {
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band2100)
                        .MapTo<IEnumerable<FrequencyCqiView>>().ArraySum(),
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band1800)
                        .MapTo<IEnumerable<FrequencyCqiView>>().ArraySum(),
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band800VoLte)
                        .MapTo<IEnumerable<FrequencyCqiView>>().ArraySum()
                }
            });
        }

        public IEnumerable<CqiRegionFrequencyView> QueryDistrictFrequencyViews(DateTime begin, DateTime end,
            string city, string district)
        {
            var towns = _townRepository.GetAllList(x => x.CityName == city && x.DistrictName == district);
            if (!towns.Any()) return new List<CqiRegionFrequencyView>();
            var query = _cqiRepository.GetAllList(x =>
                x.StatTime >= begin && x.StatTime < end && x.FrequencyBandType != FrequencyBandType.All);
            if (!query.Any()) return new List<CqiRegionFrequencyView>();
            var stats = from q in query join t in towns on q.TownId equals t.Id select q;
            return stats.GroupBy(x => x.StatTime.Date).Select(g => new CqiRegionFrequencyView
            {
                Region = district,
                StatDate = g.Key,
                FrequencyViews = new List<FrequencyCqiView>
                {
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band2100).ArraySum()
                        .MapTo<FrequencyCqiView>(),
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band1800).ArraySum()
                        .MapTo<FrequencyCqiView>(),
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band800VoLte).ArraySum()
                        .MapTo<FrequencyCqiView>()
                }
            });
        }

        public IEnumerable<CqiRegionFrequencyView> QueryTownFrequencyViews(DateTime begin, DateTime end,
            string city, string district, string town)
        {
            var townItem = _townRepository.FirstOrDefault(x =>
                x.CityName == city && x.DistrictName == district && x.TownName == town);
            if (townItem == null) return new List<CqiRegionFrequencyView>();
            var query = _cqiRepository.GetAllList(x =>
                x.StatTime >= begin && x.StatTime < end && x.FrequencyBandType != FrequencyBandType.All &&
                x.TownId == townItem.Id);
            if (!query.Any()) return new List<CqiRegionFrequencyView>();
            return query.GroupBy(x => x.StatTime.Date).Select(g => new CqiRegionFrequencyView
            {
                Region = town,
                StatDate = g.Key,
                FrequencyViews = new List<FrequencyCqiView>
                {
                    g.FirstOrDefault(x => x.FrequencyBandType == FrequencyBandType.Band2100)
                        .MapTo<FrequencyCqiView>(),
                    g.FirstOrDefault(x => x.FrequencyBandType == FrequencyBandType.Band1800)
                        .MapTo<FrequencyCqiView>(),
                    g.FirstOrDefault(x => x.FrequencyBandType == FrequencyBandType.Band800VoLte)
                        .MapTo<FrequencyCqiView>()
                }
            });
        }
    }
}