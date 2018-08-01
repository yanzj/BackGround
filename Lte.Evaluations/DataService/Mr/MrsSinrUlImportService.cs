using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using AutoMapper;
using Lte.Evaluations.ViewModels.Precise;
using Lte.MySqlFramework.Abstract;
using Lte.Parameters.Abstract.Kpi;
using Lte.Parameters.Entities.Kpi;

namespace Lte.Evaluations.DataService.Mr
{
    public class MrsSinrUlImportService
    {
        private readonly IMrsSinrUlRepository _mrsSinrUlRepository;
        private readonly IENodebRepository _eNodebRepository;
        private readonly ITopMrsSinrUlRepository _topMrsSinrULRepository;

        private static Stack<TopMrsSinrUl> TopStats { get; set; }

        public MrsSinrUlImportService(IMrsSinrUlRepository mrsSinrUlRepository,
            IENodebRepository eNodebRepository, ITopMrsSinrUlRepository topRepository)
        {
            _mrsSinrUlRepository = mrsSinrUlRepository;
            _eNodebRepository = eNodebRepository;
            _topMrsSinrULRepository = topRepository;
            if (TopStats == null) TopStats = new Stack<TopMrsSinrUl>();
        }

        private IEnumerable<TownMrsSinrUlDto> GetTownMrsStats(List<MrsSinrUlStat> stats)
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
                var townStat = Mapper.Map<MrsSinrUlStat, TownMrsSinrUlDto>(x.Stat);
                townStat.TownId = x.TownId;
                return townStat;
            });
            return townStats;
        }

        public IEnumerable<TownMrsSinrUl> GetMergeMrsStats(DateTime statTime)
        {
            var end = statTime.AddDays(1);
            var stats = _mrsSinrUlRepository.GetAllList(x => x.StatDate >= statTime && x.StatDate < end);
            var townStats = GetTownMrsStats(stats);

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
                                 SinrUlAbove24 = g.Sum(x => x.SinrUlAbove24)
                             };
            return mergeStats;
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

        public bool DumpOneStat()
        {
            var stat = TopStats.Pop();
            if (stat == null) return false;
            _topMrsSinrULRepository.ImportOne(stat);
            return true;
        }
    }
}
