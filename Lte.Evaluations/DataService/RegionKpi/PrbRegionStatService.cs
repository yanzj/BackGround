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
                    _statRepository.GetAllList(x =>
                        x.StatTime >= beginDate & x.StatTime < endDate && x.FrequencyBandType == FrequencyBandType.All);
                return query.FilterTownList(_townRepository.GetAllList().Where(x => x.CityName == city).ToList());
            });
            var townViews = stats.ConstructViews<TownPrbStat, TownPrbView>(_townRepository);
            return townViews.QueryRegionDateDateView<PrbRegionDateView, DistrictPrbView, TownPrbView>(initialDate,
                DistrictPrbView.ConstructView);
        }

        public IEnumerable<PrbRegionDateView> QueryDateViews(DateTime begin, DateTime end, string city)
        {
            var query = _statRepository.GetAllList(x =>
                x.StatTime >= begin & x.StatTime < end && x.FrequencyBandType == FrequencyBandType.All);
            var townViews = query.QueryTownStat<TownPrbStat, TownPrbView>(_townRepository, city);
            return
                townViews.QueryDateDateViews<PrbRegionDateView, DistrictPrbView, TownPrbView>(
                    DistrictPrbView.ConstructView);
        }
        
        public IEnumerable<PrbRegionFrequencyView> QueryCityFrequencyViews(DateTime begin, DateTime end,
            string city)
        {
            var query = _statRepository.GetAllList(x =>
                 x.StatTime >= begin && x.StatTime < end && x.FrequencyBandType != FrequencyBandType.All);
            if (!query.Any()) return new List<PrbRegionFrequencyView>();
            return query.GroupBy(x => x.StatTime.Date).Select(g => new PrbRegionFrequencyView
            {
                Region = city,
                StatDate = g.Key,
                FrequencyViews = new List<FrequencyPrbView>
                {
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band2100)
                        .MapTo<IEnumerable<FrequencyPrbView>>().ArraySum(),
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band1800)
                        .MapTo<IEnumerable<FrequencyPrbView>>().ArraySum(),
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band800VoLte)
                        .MapTo<IEnumerable<FrequencyPrbView>>().ArraySum()
                }
            });
        }

        public IEnumerable<PrbRegionFrequencyView> QueryDistrictFrequencyViews(DateTime begin, DateTime end,
            string city, string district)
        {
            var towns = _townRepository.GetAllList(x => x.CityName == city && x.DistrictName == district);
            if (!towns.Any()) return new List<PrbRegionFrequencyView>();
            var query = _statRepository.GetAllList(x =>
                x.StatTime >= begin && x.StatTime < end && x.FrequencyBandType != FrequencyBandType.All);
            if (!query.Any()) return new List<PrbRegionFrequencyView>();
            var stats = from q in query join t in towns on q.TownId equals t.Id select q;
            return stats.GroupBy(x => x.StatTime.Date).Select(g => new PrbRegionFrequencyView
            {
                Region = district,
                StatDate = g.Key,
                FrequencyViews = new List<FrequencyPrbView>
                {
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band2100).ArraySum()
                        .MapTo<FrequencyPrbView>(),
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band1800).ArraySum()
                        .MapTo<FrequencyPrbView>(),
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band800VoLte).ArraySum()
                        .MapTo<FrequencyPrbView>()
                }
            });
        }

        public IEnumerable<PrbRegionFrequencyView> QueryTownFrequencyViews(DateTime begin, DateTime end,
            string city, string district, string town)
        {
            var townItem = _townRepository.FirstOrDefault(x =>
                x.CityName == city && x.DistrictName == district && x.TownName == town);
            if (townItem == null) return new List<PrbRegionFrequencyView>();
            var query = _statRepository.GetAllList(x =>
                x.StatTime >= begin && x.StatTime < end && x.FrequencyBandType != FrequencyBandType.All &&
                x.TownId == townItem.Id);
            if (!query.Any()) return new List<PrbRegionFrequencyView>();
            return query.GroupBy(x => x.StatTime.Date).Select(g => new PrbRegionFrequencyView
            {
                Region = town,
                StatDate = g.Key,
                FrequencyViews = new List<FrequencyPrbView>
                {
                    g.FirstOrDefault(x => x.FrequencyBandType == FrequencyBandType.Band2100)
                        .MapTo<FrequencyPrbView>(),
                    g.FirstOrDefault(x => x.FrequencyBandType == FrequencyBandType.Band1800)
                        .MapTo<FrequencyPrbView>(),
                    g.FirstOrDefault(x => x.FrequencyBandType == FrequencyBandType.Band800VoLte)
                        .MapTo<FrequencyPrbView>()
                }
            });
        }
    }
}
