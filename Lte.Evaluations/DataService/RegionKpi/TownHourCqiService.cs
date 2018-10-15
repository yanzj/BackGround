using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Entities.RegionKpi;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Common.Wireless.Region;
using Lte.Domain.Regular;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Kpi;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Abstract.RegionKpi;
using Lte.MySqlFramework.Entities.RegionKpi;
using Lte.MySqlFramework.Support.View;

namespace Lte.Evaluations.DataService.RegionKpi
{
    public class TownHourCqiService
    {
        private readonly ITownHourCqiRepository _repository;
        private readonly ITownRepository _townRepository;
        private readonly IENodebRepository _eNodebRepository;
        private readonly IHourCqiRepository _usersRepository;
        private readonly ICellRepository _cellRepository;

        public TownHourCqiService(ITownHourCqiRepository repository, ITownRepository townRepository,
            IENodebRepository eNodebRepository, IHourCqiRepository usersRepository, ICellRepository cellRepository)
        {
            _repository = repository;
            _townRepository = townRepository;
            _eNodebRepository = eNodebRepository;
            _usersRepository = usersRepository;
            _cellRepository = cellRepository;
        }

        public IEnumerable<TownHourCqiView> QueryLastDateView(DateTime initialDate,
            FrequencyBandType frequency = FrequencyBandType.All)
        {
            var stats = _repository.QueryDate(initialDate,
                (repository, beginDate, endDate) =>
                    repository.GetAllList(x => x.StatDate >= beginDate && x.StatDate < endDate
                    && x.FrequencyBandType == frequency)
                            .OrderBy(x => x.StatDate)
                        .ToList());
            return stats.Select(x => x.ConstructView<TownHourCqi, TownHourCqiView>(_townRepository));
        }

        public IEnumerable<TownHourCqiView> QueryLastDateView(DateTime initialDate, string city, string district,
            FrequencyBandType frequency = FrequencyBandType.All)
        {
            var towns = _townRepository.GetAllList(x => x.CityName == city && x.DistrictName == district);
            var stats = _repository.QueryDate(initialDate,
                (repository, beginDate, endDate) =>
                    repository.GetAllList(x => x.StatDate >= beginDate && x.StatDate < endDate
                    && x.FrequencyBandType == frequency)
                            .OrderBy(x => x.StatDate)
                        .ToList());
            var filterStats = from s in stats join t in towns on s.TownId equals t.Id select s;
            return filterStats.Select(x => x.ConstructView<TownHourCqi, TownHourCqiView>(_townRepository));
        }

        public IEnumerable<TownHourCqi> QueryCurrentDateStats(DateTime currentDate, FrequencyBandType frequency)
        {
            var beginDate = currentDate.Date;
            var endDate = beginDate.AddDays(1);
            return
                _repository.GetAllList(
                    x => x.StatDate >= beginDate && x.StatDate < endDate && x.FrequencyBandType == frequency);
        }

        private IEnumerable<TownHourCqi> QueryDateSpanStats(DateTime begin, DateTime end, string city, string district,
            string townName)
        {
            var town =
                _townRepository.FirstOrDefault(
                    x => x.CityName == city && x.DistrictName == district && x.TownName == townName);
            return town == null
                ? new List<TownHourCqi>()
                : _repository.GetAllList(x => x.TownId == town.Id && x.StatDate >= begin && x.StatDate < end);
        }

        public IEnumerable<TownHourCqiView> QueryDateSpanViews(DateTime begin, DateTime end, string city, string district,
            string townName)
        {
            var views = QueryDateSpanStats(begin, end, city, district, townName).MapTo<List<TownHourCqiView>>();
            views.ForEach(view =>
            {
                view.City = city;
                view.District = district;
                view.Town = townName;
            });
            return views;
        }

        public IEnumerable<TownHourCqiView> QueryDateSpanGroupByFrequency(DateTime begin, DateTime end, string city,
            string district, string town)
        {
            var views =
                QueryDateSpanStats(begin, end, city, district, town)
                    .GroupBy(x => x.FrequencyBandType)
                    .Select(g => g.ArraySum()).MapTo<List<TownHourCqiView>>();
            views.ForEach(view =>
            {
                view.City = city;
                view.District = district;
                view.Town = town;
            });
            return views;
        }

        public IEnumerable<HourCqiRegionDateView> QueryDateSpanStats(DateTime begin, DateTime end, string city,
            FrequencyBandType frequency = FrequencyBandType.All)
        {
            var townViews = QueryTownCqiViews(begin, end, city, frequency);
            return from view in townViews
                   group view by view.StatDate into g
                   select new HourCqiRegionDateView
                   {
                       StatDate = g.Key,
                       TownViews = g.Select(x => x),
                       DistrictViews = g.Select(x => x).Merge(v => v.MapTo<DistrictHourCqiView>())
                   };
        }

        public List<TownHourCqi> GetTownCqiStats(DateTime statDate, FrequencyBandType bandType)
        {
            var end = statDate.AddDays(1);
            var userses = _usersRepository.GetAllList(x => x.StatTime >= statDate && x.StatTime < end);
            var townStatList = userses.GetTownFrequencyItems<HourCqi, TownHourCqi>(bandType, _cellRepository, _eNodebRepository);
            return townStatList.ToList();
        }

        private List<TownHourCqiView> QueryTownCqiViews(DateTime begin, DateTime end, string city, FrequencyBandType frequency)
        {
            var query =
                _repository.GetAllList(x => x.StatDate >= begin && x.StatDate < end && x.FrequencyBandType == frequency)
                    .OrderBy(x => x.StatDate)
                    .ToList();
            var townViews = query.QueryTownStat<TownHourCqi, TownHourCqiView>(_townRepository, city);
            return townViews;
        }

        public List<TownHourCqi> QueryTownCqiViews(DateTime begin, DateTime end, int townId, FrequencyBandType frequency)
        {
            var query =
                _repository.GetAllList(
                        x =>
                            x.StatDate >= begin && x.StatDate < end && x.FrequencyBandType == frequency &&
                            x.TownId == townId)
                    .OrderBy(x => x.StatDate)
                    .ToList();
            return query;
        }

        public TownHourCqi QueryOneDateBandStat(DateTime statDate, FrequencyBandType frequency)
        {
            var end = statDate.AddDays(1);
            var result = _repository
                .GetAllList(x => x.StatDate >= statDate && x.StatDate < end && x.FrequencyBandType == frequency);
            return result.Any() ? result.ArraySum() : null;
        }

        public TownHourCqi Update(TownHourCqi stat)
        {
            return _repository.ImportOne(stat);
        }
    }
}
