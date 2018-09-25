using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.Mr;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Regular;
using Lte.MySqlFramework.Abstract.Mr;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Entities.Mr;
using Lte.MySqlFramework.Support.View;

namespace Lte.Evaluations.DataService.RegionKpi
{
    public class MrsRegionStatService
    {
        private readonly ITownMrsRsrpRepository _statRepository;
        private readonly ITownRepository _townRepository;

        public MrsRegionStatService(ITownMrsRsrpRepository statRepository, ITownRepository townRepository)
        {
            _statRepository = statRepository;
            _townRepository = townRepository;
        }

        public MrsRegionDateView QueryLastDateStat(DateTime initialDate, string city)
        {
            var stats = _statRepository.QueryDate(initialDate, (repository, beginDate, endDate) =>
            {
                var query =
                    _statRepository.GetAllList(x =>
                        x.StatDate >= beginDate & x.StatDate < endDate && x.FrequencyBandType == FrequencyBandType.All);
                return query.FilterTownList(_townRepository.GetAllList().Where(x => x.CityName == city).ToList());
            });
            var townViews = stats.ConstructViews<TownMrsRsrp, TownMrsRsrpView>(_townRepository);
            return townViews.QueryRegionDateDateView<MrsRegionDateView, DistrictMrsRsrpView, TownMrsRsrpView>(initialDate,
                DistrictMrsRsrpView.ConstructView);
        }

        public IEnumerable<MrsRegionDateView> QueryDateViews(DateTime begin, DateTime end, string city)
        {
            var query = _statRepository.GetAllList(x =>
                x.StatDate >= begin & x.StatDate < end && x.FrequencyBandType == FrequencyBandType.All);
            var townViews = query.QueryTownStat<TownMrsRsrp, TownMrsRsrpView>(_townRepository, city);
            return
                townViews.QueryDateDateViews<MrsRegionDateView, DistrictMrsRsrpView, TownMrsRsrpView>(
                    DistrictMrsRsrpView.ConstructView);
        }
        
        public IEnumerable<MrsRsrpRegionFrequencyView> QueryCityFrequencyViews(DateTime begin, DateTime end,
            string city)
        {
            var query = _statRepository.GetAllList(x =>
                x.StatDate >= begin && x.StatDate < end && x.FrequencyBandType != FrequencyBandType.All);
            return query.GroupBy(x => x.StatDate.Date).Select(g => new MrsRsrpRegionFrequencyView
            {
                Region = city,
                StatDate = g.Key,
                FrequencyViews = new List<FrequencyMrsRsrpView>
                {
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band2100)
                        .MapTo<IEnumerable<FrequencyMrsRsrpView>>().ArraySum(),
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band1800)
                        .MapTo<IEnumerable<FrequencyMrsRsrpView>>().ArraySum(),
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band800VoLte)
                        .MapTo<IEnumerable<FrequencyMrsRsrpView>>().ArraySum()
                }
            });
        }
        
        public IEnumerable<MrsRsrpRegionFrequencyView> QueryDistrictFrequencyViews(DateTime begin, DateTime end,
            string city, string district)
        {
            var towns = _townRepository.GetAllList(x => x.CityName == city && x.DistrictName == district);
            if (!towns.Any()) return new List<MrsRsrpRegionFrequencyView>();
            var query = _statRepository.GetAllList(x =>
                x.StatDate >= begin && x.StatDate < end && x.FrequencyBandType != FrequencyBandType.All);
            var stats = from q in query join t in towns on q.TownId equals t.Id select q;
            return stats.GroupBy(x => x.StatDate.Date).Select(g => new MrsRsrpRegionFrequencyView
            {
                Region = district,
                StatDate = g.Key,
                FrequencyViews = new List<FrequencyMrsRsrpView>
                {
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band2100).ArraySum()
                        .MapTo<FrequencyMrsRsrpView>(),
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band1800).ArraySum()
                        .MapTo<FrequencyMrsRsrpView>(),
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band800VoLte).ArraySum()
                        .MapTo<FrequencyMrsRsrpView>()
                }
            });
        }
        
        public IEnumerable<MrsRsrpRegionFrequencyView> QueryTownFrequencyViews(DateTime begin, DateTime end,
            string city, string district, string town)
        {
            var townItem = _townRepository.FirstOrDefault(x =>
                x.CityName == city && x.DistrictName == district && x.TownName == town);
            if (townItem == null) return new List<MrsRsrpRegionFrequencyView>();
            var query = _statRepository.GetAllList(x =>
                x.StatDate >= begin && x.StatDate < end && x.FrequencyBandType != FrequencyBandType.All &&
                x.TownId == townItem.Id);
            return query.GroupBy(x => x.StatDate.Date).Select(g => new MrsRsrpRegionFrequencyView
            {
                Region = town,
                StatDate = g.Key,
                FrequencyViews = new List<FrequencyMrsRsrpView>
                {
                    g.FirstOrDefault(x => x.FrequencyBandType == FrequencyBandType.Band2100)
                        .MapTo<FrequencyMrsRsrpView>(),
                    g.FirstOrDefault(x => x.FrequencyBandType == FrequencyBandType.Band1800)
                        .MapTo<FrequencyMrsRsrpView>(),
                    g.FirstOrDefault(x => x.FrequencyBandType == FrequencyBandType.Band800VoLte)
                        .MapTo<FrequencyMrsRsrpView>()
                }
            });
        }
    }
}