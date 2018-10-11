using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.RegionKpi;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Common.Wireless.Region;
using Lte.Domain.Regular;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Abstract.RegionKpi;
using Lte.MySqlFramework.Entities.RegionKpi;
using Lte.MySqlFramework.Support.View;

namespace Lte.Evaluations.DataService.RegionKpi
{
    public class TownCqiService
    {
        private readonly ITownCqiRepository _repository;
        private readonly ITownRepository _townRepository;

        public TownCqiService(ITownCqiRepository repository, ITownRepository townRepository)
        {
            _repository = repository;
            _townRepository = townRepository;
        }

        public IEnumerable<TownCqiView> QueryLastDateView(DateTime initialDate,
            FrequencyBandType frequency = FrequencyBandType.All)
        {
            var stats = _repository.QueryLastDate(initialDate,
                (repository, beginDate, endDate) =>
                    repository.GetAllList(x => x.StatTime >= beginDate && x.StatTime < endDate 
                    && x.FrequencyBandType == frequency)
                            .OrderBy(x => x.StatTime)
                        .ToList());
            return stats.Select(x => x.ConstructView<TownCqiStat, TownCqiView>(_townRepository));
        }

        public IEnumerable<TownCqiView> QueryLastDateView(DateTime initialDate, string city, string district,
            FrequencyBandType frequency = FrequencyBandType.All)
        {
            var towns = _townRepository.GetAllList(x => x.CityName == city && x.DistrictName == district);
            var stats = _repository.QueryLastDate(initialDate,
                (repository, beginDate, endDate) =>
                    repository.GetAllList(x => x.StatTime >= beginDate && x.StatTime < endDate
                    && x.FrequencyBandType == frequency)
                            .OrderBy(x => x.StatTime)
                        .ToList());
            var filterStats = from s in stats join t in towns on s.TownId equals t.Id select s;
            return filterStats.Select(x => x.ConstructView<TownCqiStat, TownCqiView>(_townRepository));
        }

        public IEnumerable<TownCqiStat> QueryCurrentDateStats(DateTime currentDate, FrequencyBandType frequency)
        {
            var beginDate = currentDate.Date;
            var endDate = beginDate.AddDays(1);
            return
                _repository.GetAllList(
                    x => x.StatTime >= beginDate && x.StatTime < endDate && x.FrequencyBandType == frequency);
        }

        private IEnumerable<TownCqiStat> QueryDateSpanStats(DateTime begin, DateTime end, string city, string district,
            string townName)
        {
            var town =
                _townRepository.FirstOrDefault(
                    x => x.CityName == city && x.DistrictName == district && x.TownName == townName);
            return town == null
                ? new List<TownCqiStat>()
                : _repository.GetAllList(x => x.TownId == town.Id && x.StatTime >= begin && x.StatTime < end);
        }

        public IEnumerable<TownCqiView> QueryDateSpanViews(DateTime begin, DateTime end, string city, string district,
            string townName)
        {
            var views = QueryDateSpanStats(begin, end, city, district, townName).MapTo<List<TownCqiView>>();
            views.ForEach(view =>
            {
                view.City = city;
                view.District = district;
                view.Town = townName;
            });
            return views;
        }

        public IEnumerable<TownCqiView> QueryDateSpanGroupByFrequency(DateTime begin, DateTime end, string city,
            string district, string town)
        {
            var views = 
                QueryDateSpanStats(begin, end, city, district, town)
                    .GroupBy(x => x.FrequencyBandType)
                    .Select(g => g.ArraySum()).MapTo<List<TownCqiView>>();
            views.ForEach(view =>
            {
                view.City = city;
                view.District = district;
                view.Town = town;
            });
            return views;
        }

        public IEnumerable<CqiRegionDateView> QueryDateSpanStats(DateTime begin, DateTime end, string city,
            FrequencyBandType frequency = FrequencyBandType.All)
        {
            var townViews = QueryTownCqiViews(begin, end, city, frequency);
            return from view in townViews
                group view by view.StatTime into g
                select new CqiRegionDateView
                {
                    StatDate = g.Key,
                    TownViews = g.Select(x => x),
                    DistrictViews = g.Select(x => x).Merge(v =>v.MapTo<DistrictCqiView>())
                };
        }

        private List<TownCqiView> QueryTownCqiViews(DateTime begin, DateTime end, string city, FrequencyBandType frequency)
        {
            var query =
                _repository.GetAllList(x => x.StatTime >= begin && x.StatTime < end && x.FrequencyBandType == frequency)
                    .OrderBy(x => x.StatTime)
                    .ToList();
            var townViews = query.QueryTownStat<TownCqiStat, TownCqiView>(_townRepository, city);
            return townViews;
        }

        public List<TownCqiStat> QueryTownCqiViews(DateTime begin, DateTime end, int townId, FrequencyBandType frequency)
        {
            var query =
                _repository.GetAllList(
                        x =>
                            x.StatTime >= begin && x.StatTime < end && x.FrequencyBandType == frequency &&
                            x.TownId == townId)
                    .OrderBy(x => x.StatTime)
                    .ToList();
            return query;
        }

        public TownCqiStat QueryOneDateBandStat(DateTime statDate, FrequencyBandType frequency)
        {
            var end = statDate.AddDays(1);
            var result = _repository
                .GetAllList(x => x.StatTime >= statDate && x.StatTime < end && x.FrequencyBandType == frequency);
            return result.Any() ? result.ArraySum() : null;
        }

        public TownCqiStat Update(TownCqiStat stat)
        {
            return _repository.ImportOne(stat);
        }
    }
}
