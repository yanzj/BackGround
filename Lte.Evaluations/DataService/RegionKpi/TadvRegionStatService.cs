using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class TadvRegionStatService
    {
        private readonly ITownMrsTadvRepository _statRepository;
        private readonly ITownRepository _townRepository;

        public TadvRegionStatService(ITownMrsTadvRepository statRepository, ITownRepository townRepository)
        {
            _statRepository = statRepository;
            _townRepository = townRepository;
        }

        public TadvRegionDateView QueryLastDateStat(DateTime initialDate, string city)
        {
            var stats = _statRepository.QueryDate(initialDate, (repository, beginDate, endDate) =>
            {
                var query =
                    _statRepository.GetAllList(x =>
                        x.StatDate >= beginDate & x.StatDate < endDate && x.FrequencyBandType == FrequencyBandType.All);
                return query.FilterTownList(_townRepository.GetAllList().Where(x => x.CityName == city).ToList());
            });
            var townViews = stats.ConstructViews<TownMrsTadv, TownMrsTadvView>(_townRepository);
            return townViews.QueryRegionDateDateView<TadvRegionDateView, DistrictMrsTadvView, TownMrsTadvView>(initialDate,
                DistrictMrsTadvView.ConstructView);
        }

        public IEnumerable<TadvRegionDateView> QueryDateViews(DateTime begin, DateTime end, string city)
        {
            var query = _statRepository.GetAllList(x =>
                x.StatDate >= begin & x.StatDate < end && x.FrequencyBandType == FrequencyBandType.All);
            var townViews = query.QueryTownStat<TownMrsTadv, TownMrsTadvView>(_townRepository, city);
            return
                townViews.QueryDateDateViews<TadvRegionDateView, DistrictMrsTadvView, TownMrsTadvView>(
                    DistrictMrsTadvView.ConstructView);
        }
        
        public IEnumerable<MrsTadvRegionFrequencyView> QueryCityFrequencyViews(DateTime begin, DateTime end,
            string city)
        {
            var query = _statRepository.GetAllList(x =>
                x.StatDate >= begin && x.StatDate < end && x.FrequencyBandType != FrequencyBandType.All);
            return query.GroupBy(x => x.StatDate.Date).Select(g => new MrsTadvRegionFrequencyView
            {
                Region = city,
                StatDate = g.Key,
                FrequencyViews = new List<FrequencyMrsTadvView>
                {
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band2100)
                        .MapTo<IEnumerable<FrequencyMrsTadvView>>().ArraySum(),
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band1800)
                        .MapTo<IEnumerable<FrequencyMrsTadvView>>().ArraySum(),
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band800VoLte)
                        .MapTo<IEnumerable<FrequencyMrsTadvView>>().ArraySum()
                }
            });
        }
        
        public IEnumerable<MrsTadvRegionFrequencyView> QueryDistrictFrequencyViews(DateTime begin, DateTime end,
            string city, string district)
        {
            var towns = _townRepository.GetAllList(x => x.CityName == city && x.DistrictName == district);
            if (!towns.Any()) return new List<MrsTadvRegionFrequencyView>();
            var query = _statRepository.GetAllList(x =>
                x.StatDate >= begin && x.StatDate < end && x.FrequencyBandType != FrequencyBandType.All);
            var stats = from q in query join t in towns on q.TownId equals t.Id select q;
            return stats.GroupBy(x => x.StatDate.Date).Select(g => new MrsTadvRegionFrequencyView
            {
                Region = district,
                StatDate = g.Key,
                FrequencyViews = new List<FrequencyMrsTadvView>
                {
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band2100).ArraySum()
                        .MapTo<FrequencyMrsTadvView>(),
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band1800).ArraySum()
                        .MapTo<FrequencyMrsTadvView>(),
                    g.Where(x => x.FrequencyBandType == FrequencyBandType.Band800VoLte).ArraySum()
                        .MapTo<FrequencyMrsTadvView>()
                }
            });
        }
        
        public IEnumerable<MrsTadvRegionFrequencyView> QueryTownFrequencyViews(DateTime begin, DateTime end,
            string city, string district, string town)
        {
            var townItem = _townRepository.FirstOrDefault(x =>
                x.CityName == city && x.DistrictName == district && x.TownName == town);
            if (townItem == null) return new List<MrsTadvRegionFrequencyView>();
            var query = _statRepository.GetAllList(x =>
                x.StatDate >= begin && x.StatDate < end && x.FrequencyBandType != FrequencyBandType.All &&
                x.TownId == townItem.Id);
            return query.GroupBy(x => x.StatDate.Date).Select(g => new MrsTadvRegionFrequencyView
            {
                Region = town,
                StatDate = g.Key,
                FrequencyViews = new List<FrequencyMrsTadvView>
                {
                    g.FirstOrDefault(x => x.FrequencyBandType == FrequencyBandType.Band2100)
                        .MapTo<FrequencyMrsTadvView>(),
                    g.FirstOrDefault(x => x.FrequencyBandType == FrequencyBandType.Band1800)
                        .MapTo<FrequencyMrsTadvView>(),
                    g.FirstOrDefault(x => x.FrequencyBandType == FrequencyBandType.Band800VoLte)
                        .MapTo<FrequencyMrsTadvView>()
                }
            });
        }
    }
}
