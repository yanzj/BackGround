using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Mr;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Evaluations.DataService.RegionKpi;
using Lte.Evaluations.ViewModels.Mr;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Mr;
using Lte.MySqlFramework.Support.Container;
using Lte.Parameters.Abstract.Kpi;
using Lte.Parameters.Entities.Kpi;

namespace Lte.Evaluations.DataService.Mr
{
    public class MrsTadvImportService
    {
        private readonly IMrsTadvRepository _mrsTadvRepository;
        private readonly IENodebRepository _eNodebRepository;
        private readonly ITopMrsTadvRepository _topMrsTadvRepository;
        private readonly ICellRepository _cellRepository;
        private readonly ITownMrsTadvRepository _townMrsTadvRepository;

        private static Stack<TopMrsTadv> TopStats { get; set; }

        public MrsTadvImportService(IMrsTadvRepository mrsTadvRepository, IENodebRepository eNodebRepository,
            ICellRepository cellRepository, ITopMrsTadvRepository topRepository,
            ITownMrsTadvRepository townMrsTadvRepository)
        {
            _mrsTadvRepository = mrsTadvRepository;
            _eNodebRepository = eNodebRepository;
            _topMrsTadvRepository = topRepository;
            _townMrsTadvRepository = townMrsTadvRepository;
            _cellRepository = cellRepository;
            if (TopStats == null) TopStats = new Stack<TopMrsTadv>();
        }

        public IEnumerable<TownMrsTadv> GetMergeMrsStats(DateTime statTime, FrequencyBandType bandType)
        {
            var end = statTime.AddDays(1);
            var stats = _mrsTadvRepository.GetAllList(x => x.StatDate >= statTime && x.StatDate < end);
            var townStats =
                stats.GetTownFrequencyStats<MrsTadvStat, TownMrsTadvDto, TownMrsTadv>(bandType, _cellRepository,
                    _eNodebRepository);

            var mergeStats = from stat in townStats
                group stat by stat.TownId
                into g
                select new TownMrsTadv
                {
                    TownId = g.Key,
                    StatDate = statTime,
                    TadvBelow1 = g.Sum(x => x.TadvBelow1),
                    Tadv1To2 = g.Sum(x => x.Tadv1To2),
                    Tadv2To3 = g.Sum(x => x.Tadv2To3),
                    Tadv3To4 = g.Sum(x => x.Tadv3To4),
                    Tadv4To6 = g.Sum(x => x.Tadv4To6),
                    Tadv6To8 = g.Sum(x => x.Tadv6To8),
                    Tadv8To10 = g.Sum(x => x.Tadv8To10),
                    Tadv10To12 = g.Sum(x => x.Tadv10To12),
                    Tadv12To14 = g.Sum(x => x.Tadv12To14),
                    Tadv14To16 = g.Sum(x => x.Tadv14To16),
                    Tadv16To18 = g.Sum(x => x.Tadv16To18),
                    Tadv18To20 = g.Sum(x => x.Tadv18To20),
                    Tadv20To24 = g.Sum(x => x.Tadv20To24),
                    Tadv24To28 = g.Sum(x => x.Tadv24To28),
                    Tadv28To32 = g.Sum(x => x.Tadv28To32),
                    Tadv32To36 = g.Sum(x => x.Tadv32To36),
                    Tadv36To42 = g.Sum(x => x.Tadv36To42),
                    Tadv42To48 = g.Sum(x => x.Tadv42To48),
                    Tadv48To54 = g.Sum(x => x.Tadv48To54),
                    Tadv54To60 = g.Sum(x => x.Tadv54To60),
                    Tadv60To80 = g.Sum(x => x.Tadv60To80),
                    Tadv80To112 = g.Sum(x => x.Tadv80To112),
                    Tadv112To192 = g.Sum(x => x.Tadv112To192),
                    TadvAbove192 = g.Sum(x => x.TadvAbove192),
                    FrequencyBandType = bandType,
                    Id = 0
                };
            return mergeStats;
        }
        
        public IEnumerable<TadvHistory> GetTadvHistories(DateTime begin, DateTime end)
        {
            var results = new List<TadvHistory>();
            while (begin < end.AddDays(1))
            {
                var beginDate = begin.Date;
                var endDate = beginDate.AddDays(1);
                var townTadvItems =
                    _townMrsTadvRepository.GetAllList(x =>
                        x.StatDate >= beginDate && x.StatDate < endDate &&
                        x.FrequencyBandType == FrequencyBandType.All);
                var collegeTadvItems =
                    _townMrsTadvRepository.GetAllList(x =>
                        x.StatDate >= beginDate && x.StatDate < endDate &&
                        x.FrequencyBandType == FrequencyBandType.College);
                var townTadvItems800 =
                    _townMrsTadvRepository.GetAllList(x =>
                        x.StatDate >= beginDate && x.StatDate < endDate &&
                        x.FrequencyBandType == FrequencyBandType.Band800VoLte);
                var townTadvItems1800 =
                    _townMrsTadvRepository.GetAllList(x =>
                        x.StatDate >= beginDate && x.StatDate < endDate &&
                        x.FrequencyBandType == FrequencyBandType.Band1800);
                var townTadvItems2100 =
                    _townMrsTadvRepository.GetAllList(x =>
                        x.StatDate >= beginDate && x.StatDate < endDate &&
                        x.FrequencyBandType == FrequencyBandType.Band2100);
                var topTadvItems =
                    _topMrsTadvRepository.GetAllList(x => x.StatDate >= beginDate && x.StatDate < endDate);
                results.Add(new TadvHistory
                {
                    DateString = begin.ToShortDateString(),
                    StatDate = begin.Date,
                    TownTadvStats = townTadvItems.Count,
                    CollegeTadvStats = collegeTadvItems.Count,
                    TownTadvStats800 = townTadvItems800.Count,
                    TownTadvStats1800 = townTadvItems1800.Count,
                    TownTadvStats2100 = townTadvItems2100.Count,
                    TopTadvStats = topTadvItems.Count
                });
                begin = begin.AddDays(1);
            }
            return results;
        }

        public int GetTopMrsTadvs(DateTime statTime)
        {
            var end = statTime.AddDays(1);
            var stats = _mrsTadvRepository.GetAllList(x => x.StatDate >= statTime && x.StatDate < end);
            var dtos = stats
                .Where(x => x.Tadv_21 + x.Tadv_22 + x.Tadv_23 + x.Tadv_24 + x.Tadv_25 + x.Tadv_26 +
                            x.Tadv_27 + x.Tadv_28 > 1000).MapTo<List<CellMrsTadvDto>>();
            var items = dtos.MapTo<IEnumerable<TopMrsTadv>>();
            foreach (var topMrsTadv in items)
            {
                TopStats.Push(topMrsTadv);
            }

            return TopStats.Count;
        }

        public int GetStatsToBeDump()
        {
            return TopStats.Count;
        }

        public void ClearStats()
        {
            TopStats.Clear();
        }
        
        public async Task DumpTownStats(TownTadvViewContainer container)
        {
            var mrsTadvStats = container.MrsTadvs
                .Concat(container.MrsTadvs800).Concat(container.MrsTadvs1800).Concat(container.MrsTadvs2100);
            await _townMrsTadvRepository.UpdateMany(mrsTadvStats);
            if (container.CollegeMrsTadvs.Any())
                await _townMrsTadvRepository.UpdateMany(container.CollegeMrsTadvs);
        }

        public bool DumpOneStat()
        {
            var stat = TopStats.Pop();
            if (stat == null) return false;
            _topMrsTadvRepository.ImportOne(stat);
            return true;
        }

    }
}
