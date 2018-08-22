using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.Infrastructure;
using AutoMapper;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Entities;
using Lte.MySqlFramework.Entities.Kpi;

namespace Lte.Evaluations.DataService.RegionKpi
{
    public static class TownStatQueries
    {
        public static IEnumerable<TTownStat> GetTownStats<TStat, TTownStat>(this IEnumerable<TStat> stats,
            IENodebRepository eNodebRepository)
            where TStat : IENodebId
            where TTownStat : ITownId
        {
            var query = from stat in stats
                join eNodeb in eNodebRepository.GetAllList() on stat.ENodebId equals eNodeb.ENodebId
                select
                new
                {
                    Stat = stat,
                    eNodeb.TownId
                };
            var townStats = query.Select(x =>
            {
                var townStat = Mapper.Map<TStat, TTownStat>(x.Stat);
                townStat.TownId = x.TownId;
                return townStat;
            });
            return townStats;
        }

        public static IEnumerable<TTownStat> GetTownDateStats<TStat, TTownStat>(this IEnumerable<TStat> stats,
            IENodebRepository eNodebRepository)
            where TStat : IENodebId, IStatTime
            where TTownStat : ITownId, IStatDate
        {
            var query = from stat in stats
                join eNodeb in eNodebRepository.GetAllList() on stat.ENodebId equals eNodeb.ENodebId
                select
                    new
                    {
                        Stat = stat,
                        eNodeb.TownId
                    };
            var townStats = query.Select(x =>
            {
                var townStat = Mapper.Map<TStat, TTownStat>(x.Stat);
                townStat.TownId = x.TownId;
                townStat.StatDate = x.Stat.StatTime.Date;
                return townStat;
            });
            return townStats;
        }

        public static IEnumerable<TENodebStat> GetENodebStats<TStat, TENodebStat>(this IEnumerable<TStat> stats,
            IEnumerable<ENodeb> eNodebs, Func<IGrouping<int, TStat>, TStat> filterFlow)
            where TStat : IENodebId
            where TENodebStat : IENodebId, IGeoPoint<double>
        {
            var query = from stat in stats.GroupBy(x => x.ENodebId)
                join eNodeb in eNodebs on stat.Key equals eNodeb.ENodebId
                select
                new
                {
                    Stat = stat,
                    eNodeb.Longtitute,
                    eNodeb.Lattitute
                };
            var eNodebStats = query.Select(x =>
            {
                var eNodebStat = Mapper.Map<TStat, TENodebStat>(filterFlow(x.Stat));
                eNodebStat.Longtitute = x.Longtitute;
                eNodebStat.Lattitute = x.Lattitute;
                return eNodebStat;
            });
            return eNodebStats;
        }

        public static IEnumerable<TCellStat> GetCellStats<TStat, TCellStat>(this IEnumerable<TStat> stats,
            IEnumerable<Cell> cells)
            where TStat : ILteCellQuery
        {
            return from stat in stats.GroupBy(x => new { x.ENodebId, x.SectorId})
                join cell in cells on new {stat.Key.ENodebId, stat.Key.SectorId}
                equals new {cell.ENodebId, cell.SectorId}
                select cell.MapTo(Mapper.Map<TStat, TCellStat>(stat.Select(x => x).First()));
        }

        public static IEnumerable<TCellStat> GetLocalCellStats<TStat, TCellStat>(this IEnumerable<TStat> stats,
            IEnumerable<Cell> cells)
            where TStat : ILocalCellQuery
        {
            return from stat in stats.GroupBy(x => new {x.ENodebId, LocalSectorId = x.LocalCellId})
                join cell in cells on new {stat.Key.ENodebId, stat.Key.LocalSectorId}
                equals new {cell.ENodebId, cell.LocalSectorId}
                select cell.MapTo(Mapper.Map<TStat, TCellStat>(stat.Select(x => x).First()));
        }

        public static IEnumerable<TTownStat> GetPositionMergeStats<TTownStat>(this IEnumerable<TTownStat> townStats, DateTime statTime)
            where TTownStat : class, ITownId, IStatTime, new()
        {
            var mergeStats = from stat in townStats
                group stat by stat.TownId
                into g
                select new
                {
                    TownId = g.Key,
                    Value = g.ArraySum()
                };
            return mergeStats.Select(x =>
            {
                var stat = x.Value;
                stat.TownId = x.TownId;
                stat.StatTime = statTime;
                return stat;
            });
        }


        public static IEnumerable<TTownStat> GetDateMergeStats<TTownStat>(this IEnumerable<TTownStat> townStats, DateTime statTime)
            where TTownStat : class, ITownId, IStatDate, new()
        {
            var mergeStats = from stat in townStats
                             group stat by stat.TownId
                into g
                             select new
                             {
                                 TownId = g.Key,
                                 Value = g.ArraySum()
                             };
            return mergeStats.Select(x =>
            {
                var stat = x.Value;
                stat.TownId = x.TownId;
                stat.StatDate = statTime;
                return stat;
            });
        }

        public static IEnumerable<ENodebFlowView> GetPositionMergeStats(this IEnumerable<ENodebFlowView> eNodebStats)
        {
            var mergeStats = from stat in eNodebStats
                group stat by new
                {
                    stat.Longtitute,
                    stat.Lattitute
                }
                into g
                select new
                {
                    g.Key.Longtitute,
                    g.Key.Lattitute,
                    Value = g.ArraySum()
                };
            return mergeStats.Select(x =>
            {
                var stat = x.Value;
                stat.Longtitute = x.Longtitute;
                stat.Lattitute = x.Lattitute;
                return stat;
            });
        }

        public static IEnumerable<CellFlowView> GetPositionMergeStats(this IEnumerable<CellFlowView> cellStats)
        {
            var mergeStats = from stat in cellStats
                group stat by new
                {
                    stat.Longtitute,
                    stat.Lattitute,
                    stat.Azimuth
                }
                into g
                select new
                {
                    g.Key.Longtitute,
                    g.Key.Lattitute,
                    g.Key.Azimuth,
                    Value = g.ArraySum()
                };
            return mergeStats.Select(x =>
            {
                var stat = x.Value;
                stat.Longtitute = x.Longtitute;
                stat.Lattitute = x.Lattitute;
                stat.Azimuth = x.Azimuth;
                return stat;
            });
        }

    }
}