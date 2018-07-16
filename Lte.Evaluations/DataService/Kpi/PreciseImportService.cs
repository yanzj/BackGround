using AutoMapper;
using Lte.Domain.LinqToCsv.Context;
using Lte.Domain.LinqToCsv.Description;
using Lte.Evaluations.ViewModels.Precise;
using Lte.Parameters.Abstract.Kpi;
using Lte.Parameters.Entities.Kpi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Lte.Domain.Excel;
using Lte.Evaluations.ViewModels.RegionKpi;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;

namespace Lte.Evaluations.DataService.Kpi
{
    public class PreciseImportService
    {
        private readonly IPreciseCoverage4GRepository _repository;
        private readonly ITownPreciseCoverage4GStatRepository _regionRepository;
        private readonly IENodebRepository _eNodebRepository;
        private readonly ITownRepository _townRepository;
        private readonly IPreciseMongoRepository _mongoRepository;
        private readonly IMrsRsrpRepository _mrsRsrpRepository;
        private readonly ITownMrsRsrpRepository _townMrsRsrpRepository;
        private readonly ITopMrsRsrpRepository _topMrsRsrpRepository;

        public static Stack<PreciseCoverage4G> PreciseCoverage4Gs { get; set; } 
        
        public PreciseImportService(IPreciseCoverage4GRepository repository,
            ITownPreciseCoverage4GStatRepository regionRepository,
            IENodebRepository eNodebRepository, ITownRepository townRepository,
            IPreciseMongoRepository mongoRepository, IMrsRsrpRepository mrsRsrpRepository,
            ITownMrsRsrpRepository townMrsRsrpRepository, ITopMrsRsrpRepository topMrsRsrpRepository)
        {
            _repository = repository;
            _regionRepository = regionRepository;
            _eNodebRepository = eNodebRepository;
            _townRepository = townRepository;
            _mongoRepository = mongoRepository;
            _mrsRsrpRepository = mrsRsrpRepository;
            _townMrsRsrpRepository = townMrsRsrpRepository;
            _topMrsRsrpRepository = topMrsRsrpRepository;
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

        public IEnumerable<TownPreciseView> GetMergeStats(DateTime statTime)
        {
            var stats = _repository.GetAllList(statTime.Date, statTime.Date.AddDays(1));
            var townStats = GetTownStats(stats);
            
            var mergeStats = from stat in townStats
                group stat by stat.TownId
                into g
                select new TownPreciseCoverage4GStat
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
                    StatTime = statTime
                };
            return mergeStats.Select(x => x.ConstructView<TownPreciseCoverage4GStat, TownPreciseView>(_townRepository));
        }

        public IEnumerable<TownMrsRsrp> GetMergeMrsStats(DateTime statTime)
        {
            var end = statTime.AddDays(1);
            var stats = _mrsRsrpRepository.GetAllList(x => x.StatDate >= statTime && x.StatDate < end);
            var townStats = GetTownMrsStats(stats);

            var mergeStats = from stat in townStats
                             group stat by stat.TownId
                into g
                             select new TownMrsRsrp
                             {
                                 TownId = g.Key,
                                 StatDate = statTime,
                                 Rsrp100To95 = g.Sum(x => x.Rsrp100To95),
                                 Rsrp105To100 = g.Sum(x => x.Rsrp105To100),
                                 Rsrp110To105 = g.Sum(x => x.Rsrp110To105),
                                 Rsrp115To110 = g.Sum(x => x.Rsrp115To110),
                                 Rsrp120To115 = g.Sum(x => x.Rsrp120To115),
                                 Rsrp70To60 = g.Sum(x => x.Rsrp70To60),
                                 Rsrp80To70 = g.Sum(x => x.Rsrp80To70),
                                 Rsrp90To80 = g.Sum(x => x.Rsrp90To80),
                                 Rsrp95To90 = g.Sum(x => x.Rsrp95To90),
                                 RsrpAbove60 = g.Sum(x => x.RsrpAbove60),
                                 RsrpBelow120 = g.Sum(x => x.RsrpBelow120)
                             };
            return mergeStats;
        }

        public IEnumerable<TopMrsRsrp> GetTopMrsRsrps(DateTime statTime)
        {
            var end = statTime.AddDays(1);
            var stats = _mrsRsrpRepository.GetAllList(x => x.StatDate >= statTime && x.StatDate < end);
            var dtos = stats.Where(x => x.RSRP_00 + x.RSRP_01 > 5000).MapTo<List<CellMrsRsrpDto>>();
            return dtos.MapTo<IEnumerable<TopMrsRsrp>>();
        }

        private IEnumerable<TownPreciseCoverage4GStat> GetTownStats(List<PreciseCoverage4G> stats)
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
                var townStat = Mapper.Map<PreciseCoverage4G, TownPreciseCoverage4GStat>(x.Stat);
                townStat.TownId = x.TownId;
                return townStat;
            });
            return townStats;
        }

        private IEnumerable<TownMrsRsrpDto> GetTownMrsStats(List<MrsRsrpStat> stats)
        {
            var query = from stat in stats
                        join eNodeb in _eNodebRepository.GetAllList() on stat.ENodebId equals eNodeb.ENodebId
                        select
                            new
                            {
                                Stat = stat,
                                eNodeb.TownId
                            };
            var townStats = query.Select(x =>
            {
                var townStat = Mapper.Map<MrsRsrpStat, TownMrsRsrpDto>(x.Stat);
                townStat.TownId = x.TownId;
                return townStat;
            });
            return townStats;
        }

        public void DumpTownStats(TownPreciseViewContainer container)
        {
            var stats = Mapper.Map<IEnumerable<TownPreciseView>, IEnumerable<TownPreciseCoverage4GStat>>(container.Views);
            foreach (var stat in stats)
            {
                var endTime = stat.StatTime.AddDays(1);
                var item =
                    _regionRepository.FirstOrDefault(
                        x => x.TownId == stat.TownId && x.StatTime >= stat.StatTime && x.StatTime < endTime);
                if (item == null)
                {
                    _regionRepository.Insert(stat);
                }
                else
                {
                    stat.MapTo(item);
                }
            }
            _regionRepository.SaveChanges();

            var mrsStats = container.MrsRsrps;
            foreach (var stat in mrsStats)
            {
                var endTime = stat.StatDate.AddDays(1);
                var item =
                    _townMrsRsrpRepository.FirstOrDefault(
                        x => x.TownId == stat.TownId && x.StatDate >= stat.StatDate && x.StatDate < endTime);
                if (item == null)
                {
                    _townMrsRsrpRepository.Insert(stat);
                }
                else
                {
                    stat.MapTo(item);
                }
            }
            _townMrsRsrpRepository.SaveChanges();

            var topMrsStats = container.TopMrsRsrps;
            foreach (var stat in topMrsStats)
            {
                var endTime = stat.StatDate.AddDays(1);
                var item =
                    _topMrsRsrpRepository.FirstOrDefault(
                        x =>
                            x.ENodebId == stat.ENodebId && x.SectorId == stat.SectorId && x.StatDate >= stat.StatDate &&
                            x.StatDate < endTime);
                if (item == null)
                {
                    _topMrsRsrpRepository.Insert(stat);
                }
                else
                {
                    stat.MapTo(item);
                }
            }
            _topMrsRsrpRepository.SaveChanges();
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
                var items = _repository.GetAllList(beginDate, endDate);
                var townItems = _regionRepository.GetAllList(x => x.StatTime >= beginDate && x.StatTime < endDate);
                var townMrsItems =
                    _townMrsRsrpRepository.GetAllList(x => x.StatDate >= beginDate && x.StatDate < endDate);
                var topMrsItems = _topMrsRsrpRepository.GetAllList(x => x.StatDate >= beginDate && x.StatDate < endDate);
                results.Add(new PreciseHistory
                {
                    DateString = begin.ToShortDateString(),
                    StatDate = begin.Date,
                    PreciseStats = items.Count,
                    TownPreciseStats = townItems.Count,
                    TownMrsStats = townMrsItems.Count,
                    TopMrsStats = topMrsItems.Count
                });
                begin = begin.AddDays(1);
            }
            return results;
        }
    }
}
