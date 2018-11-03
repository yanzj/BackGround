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
    public class MrsSinrUlImportService
    {
        private readonly IMrsSinrUlRepository _mrsSinrUlRepository;
        private readonly IENodebRepository _eNodebRepository;
        private readonly ITopMrsSinrUlRepository _topMrsSinrUlRepository;
        private readonly ITownMrsSinrUlRepository _townMrsSinrUlRepository;
        private readonly ICellRepository _cellRepository;

        private static Stack<TopMrsSinrUl> TopStats { get; set; }

        public MrsSinrUlImportService(IMrsSinrUlRepository mrsSinrUlRepository,
            IENodebRepository eNodebRepository, ITownMrsSinrUlRepository townMrsSinrUlRepository,
            ICellRepository cellRepository, ITopMrsSinrUlRepository topRepository)
        {
            _mrsSinrUlRepository = mrsSinrUlRepository;
            _eNodebRepository = eNodebRepository;
            _topMrsSinrUlRepository = topRepository;
            _townMrsSinrUlRepository = townMrsSinrUlRepository;
            _cellRepository = cellRepository;
            if (TopStats == null) TopStats = new Stack<TopMrsSinrUl>();
        }

        public IEnumerable<TownMrsSinrUl> GetMergeMrsStats(DateTime statTime, FrequencyBandType bandType)
        {
            var end = statTime.AddDays(1);
            var stats = _mrsSinrUlRepository.GetAllList(x => x.StatDate >= statTime && x.StatDate < end);
            var townStats = stats.GetTownFrequencyStats<MrsSinrUlStat, TownMrsSinrUlDto, TownMrsSinrUl>(bandType,
                _cellRepository, _eNodebRepository);

            var mergeStats = from stat in townStats
                             group stat by stat.TownId
                into g
                             select new TownMrsSinrUl
                             {
                                 TownId = g.Key,
                                 StatDate = statTime,
                                 SinrUlBelowM9 = g.Sum(x => x.SinrUlBelowM9),
                                 SinrUlM9ToM6 = g.Sum(x => x.SinrUlM9ToM6),
                                 SinrUlM6ToM3 = g.Sum(x => x.SinrUlM6ToM3),
                                 SinrUlM3To0 = g.Sum(x => x.SinrUlM3To0),
                                 SinrUl0To3 = g.Sum(x => x.SinrUl0To3),
                                 SinrUl3To6 = g.Sum(x => x.SinrUl3To6),
                                 SinrUl6To9 = g.Sum(x => x.SinrUl6To9),
                                 SinrUl9To12 = g.Sum(x => x.SinrUl9To12),
                                 SinrUl12To15 = g.Sum(x => x.SinrUl12To15),
                                 SinrUl15To18 = g.Sum(x => x.SinrUl15To18),
                                 SinrUl18To21 = g.Sum(x => x.SinrUl18To21),
                                 SinrUl21To24 = g.Sum(x => x.SinrUl21To24),
                                 SinrUlAbove24 = g.Sum(x => x.SinrUlAbove24),
                                 FrequencyBandType = bandType
                             };
            return mergeStats;
        }

        public IEnumerable<SinrHistory> GetSinrHistories(DateTime begin, DateTime end)
        {
            var results = new List<SinrHistory>();
            while (begin < end.AddDays(1))
            {
                var beginDate = begin.Date;
                var endDate = beginDate.AddDays(1);
                var townSinrUlItems =
                    _townMrsSinrUlRepository.GetAllList(x =>
                        x.StatDate >= beginDate && x.StatDate < endDate &&
                        x.FrequencyBandType == FrequencyBandType.All);
                var collegeSinrUlItems =
                    _townMrsSinrUlRepository.GetAllList(x =>
                        x.StatDate >= beginDate && x.StatDate < endDate &&
                        x.FrequencyBandType == FrequencyBandType.College);
                var townSinrUlItems800 =
                    _townMrsSinrUlRepository.GetAllList(x =>
                        x.StatDate >= beginDate && x.StatDate < endDate &&
                        x.FrequencyBandType == FrequencyBandType.Band800VoLte);
                var townSinrUlItems1800 =
                    _townMrsSinrUlRepository.GetAllList(x =>
                        x.StatDate >= beginDate && x.StatDate < endDate &&
                        x.FrequencyBandType == FrequencyBandType.Band1800);
                var townSinrUlItems2100 =
                    _townMrsSinrUlRepository.GetAllList(x =>
                        x.StatDate >= beginDate && x.StatDate < endDate &&
                        x.FrequencyBandType == FrequencyBandType.Band2100);
                var topSinrUlItems = _topMrsSinrUlRepository.GetAllList(x => x.StatDate >= beginDate && x.StatDate < endDate);
                results.Add(new SinrHistory
                {
                    DateString = begin.ToShortDateString(),
                    StatDate = begin.Date,
                    TownSinrUlStats = townSinrUlItems.Count,
                    CollegeSinrUlStats = collegeSinrUlItems.Count,
                    TownSinrUlStats800 = townSinrUlItems800.Count,
                    TownSinrUlStats1800 = townSinrUlItems1800.Count,
                    TownSinrUlStats2100 = townSinrUlItems2100.Count,
                    TopSinrUlStats = topSinrUlItems.Count
                });
                begin = begin.AddDays(1);
            }
            return results;
        }

        public int GetTopMrsSinrUls(DateTime statTime)
        {
            var end = statTime.AddDays(1);
            var stats = _mrsSinrUlRepository.GetAllList(x => x.StatDate >= statTime && x.StatDate < end);
            var dtos = stats
                .Where(x => x.SinrUL_00 + x.SinrUL_01 + x.SinrUL_02 > 2000)
                .MapTo<List<CellMrsSinrUlDto>>();
            var items = dtos.MapTo<IEnumerable<TopMrsSinrUl>>();
            foreach (var topMrsRsrp in items)
            {
                TopStats.Push(topMrsRsrp);
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

        public async Task DumpTownStats(TownSinrViewContainer container)
        {
            var mrsSinrUlStats = container.MrsSinrUls
                .Concat(container.MrsSinrUls800).Concat(container.MrsSinrUls1800).Concat(container.MrsSinrUls2100);
            await _townMrsSinrUlRepository.UpdateMany(mrsSinrUlStats);
            if (container.CollegeMrsSinrUls.Any())
                await _townMrsSinrUlRepository.UpdateMany(container.CollegeMrsSinrUls);
        }

        public bool DumpOneStat()
        {
            var stat = TopStats.Pop();
            if (stat == null) return false;
            _topMrsSinrUlRepository.ImportOne(stat);
            return true;
        }
    }
}
