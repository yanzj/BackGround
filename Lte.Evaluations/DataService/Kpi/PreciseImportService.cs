using AutoMapper;
using Lte.Evaluations.ViewModels.Precise;
using Lte.Parameters.Abstract.Kpi;
using Lte.Parameters.Entities.Kpi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Entities.RegionKpi;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common.Wireless.Cell;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Mr;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Abstract.RegionKpi;
using Lte.MySqlFramework.Support.Container;

namespace Lte.Evaluations.DataService.Kpi
{
    public class PreciseImportService
    {
        private readonly IPreciseCoverage4GRepository _repository;
        private readonly ITownPreciseCoverageRepository _regionRepository;
        private readonly IENodebRepository _eNodebRepository;
        private readonly ITownRepository _townRepository;
        private readonly IPreciseMongoRepository _mongoRepository;
        private readonly ITownMrsRsrpRepository _townMrsRsrpRepository;
        private readonly ITopMrsRsrpRepository _topMrsRsrpRepository;
        private readonly ITownMrsSinrUlRepository _townMrsSinrUlRepository;
        private readonly ITopMrsSinrUlRepository _topMrsSinrUlRepository;

        public static Stack<PreciseCoverage4G> PreciseCoverage4Gs { get; set; } 
        
        public PreciseImportService(IPreciseCoverage4GRepository repository,
            ITownPreciseCoverageRepository regionRepository,
            IENodebRepository eNodebRepository, ITownRepository townRepository,
            IPreciseMongoRepository mongoRepository,
            ITownMrsRsrpRepository townMrsRsrpRepository, ITopMrsRsrpRepository topMrsRsrpRepository,
            ITownMrsSinrUlRepository townMrsSinrUlRepository, ITopMrsSinrUlRepository topMrsSinrUlRepository)
        {
            _repository = repository;
            _regionRepository = regionRepository;
            _eNodebRepository = eNodebRepository;
            _townRepository = townRepository;
            _mongoRepository = mongoRepository;
            _townMrsRsrpRepository = townMrsRsrpRepository;
            _topMrsRsrpRepository = topMrsRsrpRepository;
            _townMrsSinrUlRepository = townMrsSinrUlRepository;
            _topMrsSinrUlRepository = topMrsSinrUlRepository;
            if (PreciseCoverage4Gs == null)
                PreciseCoverage4Gs = new Stack<PreciseCoverage4G>();
        }

        public int UpdateItems(DateTime statDate)
        {
            var stats = _mongoRepository.GetAllList(statDate.Date);
            PreciseCoverage4Gs = new Stack<PreciseCoverage4G>();
            foreach (var stat in stats)
            {
                PreciseCoverage4Gs.Push(Mapper.Map<PreciseMongo, PreciseCoverage4G>(stat));
            }

            return PreciseCoverage4Gs.Count;
        }

        public IEnumerable<TownPreciseView> GetMergeStats(DateTime statTime,
            FrequencyBandType frequency = FrequencyBandType.All)
        {
            var stats = _repository.GetAllList(statTime.Date, statTime.Date.AddDays(1), frequency);
            var townStats = GetTownStats(stats);
            
            var mergeStats = from stat in townStats
                group stat by stat.TownId
                into g
                select new TownPreciseStat
                {
                    TownId = g.Key,
                    FirstNeighbors = g.Sum(x => x.FirstNeighbors),
                    SecondNeighbors = g.Sum(x => x.SecondNeighbors),
                    ThirdNeighbors = g.Sum(x => x.ThirdNeighbors),
                    TotalMrs = g.Sum(x => x.TotalMrs),
                    InterFirstNeighbors = g.Sum(x => x.InterFirstNeighbors),
                    InterSecondNeighbors = g.Sum(x => x.InterSecondNeighbors),
                    InterThirdNeighbors = g.Sum(x => x.InterThirdNeighbors),
                    NeighborsMore = g.Sum(x => x.NeighborsMore),
                    StatTime = statTime,
                    FrequencyBandType = frequency
                };
            return mergeStats.Select(x => x.ConstructView<TownPreciseStat, TownPreciseView>(_townRepository));
        }

        private IEnumerable<TownPreciseStat> GetTownStats(List<PreciseCoverage4G> stats)
        {
            var query = from stat in stats
                join eNodeb in _eNodebRepository.GetAllList() on stat.CellId equals eNodeb.ENodebId
                select
                    new
                    {
                        Stat = stat,
                        eNodeb.TownId
                    };
            var townStats = query.Select(x =>
            {
                var townStat = Mapper.Map<PreciseCoverage4G, TownPreciseStat>(x.Stat);
                townStat.TownId = x.TownId;
                return townStat;
            });
            return townStats;
        }

        public async Task DumpTownStats(TownPreciseViewContainer container)
        {
            var stats = Mapper.Map<IEnumerable<TownPreciseView>, IEnumerable<TownPreciseStat>>(
                container.Views.Concat(container.CollegeViews).Concat(container.Views800)
                    .Concat(container.Views1800).Concat(container.Views2100));
            await _regionRepository.UpdateMany(stats);

            var mrsStats = container.MrsRsrps;
            await _townMrsRsrpRepository.UpdateMany(mrsStats);

            var mrsSinrUlStats = container.MrsSinrUls;
            await _townMrsSinrUlRepository.UpdateMany(mrsSinrUlStats);

        }

        public bool DumpOneStat()
        {
            var stat = PreciseCoverage4Gs.Pop();
            if (stat == null) return false;
            var nextDate = stat.StatTime.Date.AddDays(1);
            var item =
                _repository.FirstOrDefault(
                    x =>
                        x.StatTime >= stat.StatTime.Date && x.StatTime < nextDate && x.CellId == stat.CellId && x.SectorId == stat.SectorId );
            if (item == null)
            {
                _repository.Insert(stat);
            }
            else
            {
                item.TotalMrs = stat.TotalMrs;
                item.FirstNeighbors = stat.FirstNeighbors;
                item.SecondNeighbors = stat.SecondNeighbors;
                item.ThirdNeighbors = stat.ThirdNeighbors;
                if (stat.Neighbors0 > 0) item.Neighbors0 = stat.Neighbors0;
                if (stat.Neighbors1 > 0) item.Neighbors1 = stat.Neighbors1;
                if (stat.Neighbors2 > 0) item.Neighbors2 = stat.Neighbors2;
                if (stat.Neighbors3 > 0) item.Neighbors3 = stat.Neighbors3;
                if (stat.NeighborsMore > 0) item.NeighborsMore = stat.NeighborsMore;
            }
            _repository.SaveChanges();
            return true;
        }

        public int GetStatsToBeDump()
        {
            return PreciseCoverage4Gs.Count;
        }

        public void ClearStats()
        {
            PreciseCoverage4Gs.Clear();
        }

        public IEnumerable<PreciseHistory> GetPreciseHistories(DateTime begin, DateTime end)
        {
            var results = new List<PreciseHistory>();
            while (begin < end.AddDays(1))
            {
                var beginDate = begin.Date;
                var endDate = beginDate.AddDays(1);
                var items = _repository.GetAllList(beginDate, endDate, FrequencyBandType.All);
                var townItems = _regionRepository.GetAllList(x =>
                    x.StatTime >= beginDate && x.StatTime < endDate && x.FrequencyBandType == FrequencyBandType.All);
                var collegeItems = _regionRepository.GetAllList(x =>
                    x.StatTime >= beginDate && x.StatTime < endDate && x.FrequencyBandType == FrequencyBandType.College);
                var townItems800 = _regionRepository.GetAllList(x =>
                    x.StatTime >= beginDate && x.StatTime < endDate && x.FrequencyBandType == FrequencyBandType.Band800VoLte);
                var townItems1800 = _regionRepository.GetAllList(x =>
                    x.StatTime >= beginDate && x.StatTime < endDate && x.FrequencyBandType == FrequencyBandType.Band1800);
                var townItems2100 = _regionRepository.GetAllList(x =>
                    x.StatTime >= beginDate && x.StatTime < endDate && x.FrequencyBandType == FrequencyBandType.Band2100);
                var townMrsItems =
                    _townMrsRsrpRepository.GetAllList(x => x.StatDate >= beginDate && x.StatDate < endDate);
                var topMrsItems = _topMrsRsrpRepository.GetAllList(x => x.StatDate >= beginDate && x.StatDate < endDate);
                var townSinrUlItems =
                    _townMrsSinrUlRepository.GetAllList(x => x.StatDate >= beginDate && x.StatDate < endDate);
                var topSinrUlItems = _topMrsSinrUlRepository.GetAllList(x => x.StatDate >= beginDate && x.StatDate < endDate);
                results.Add(new PreciseHistory
                {
                    DateString = begin.ToShortDateString(),
                    StatDate = begin.Date,
                    PreciseStats = items.Count,
                    TownPreciseStats = townItems.Count,
                    CollegePreciseStats = collegeItems.Count,
                    TownPrecise800Stats = townItems800.Count,
                    TownPrecise1800Stats = townItems1800.Count,
                    TownPrecise2100Stats = townItems2100.Count,
                    TownMrsStats = townMrsItems.Count,
                    TopMrsStats = topMrsItems.Count,
                    TownSinrUlStats = townSinrUlItems.Count,
                    TopSinrUlStats = topSinrUlItems.Count
                });
                begin = begin.AddDays(1);
            }
            return results;
        }
    }
}
