using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Entities.Mr;
using Abp.EntityFramework.Entities.Test;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Kpi;
using Lte.Domain.Regular;
using Lte.MySqlFramework.Entities;
using Lte.MySqlFramework.Entities.Cdma;
using Lte.MySqlFramework.Properties;

namespace Lte.MySqlFramework.Support
{
    public static class OrderService
    {
        public static IEnumerable<TopPrecise4GContainer> GenerateContainers(this IEnumerable<PreciseCoverage4G> stats)
        {
            return from q in stats
                group q by new
                {
                    q.CellId,
                    q.SectorId
                }
                into g
                select new TopPrecise4GContainer
                {
                    PreciseCoverage4G = g.ArrayAggration(stat =>
                    {
                        stat.CellId = g.Key.CellId;
                        stat.SectorId = g.Key.SectorId;
                        stat.StatTime = g.First().StatTime;
                    }),
                    TopDates = g.Count()
                };
        }

        public static IEnumerable<TContainer> GenerateContainers<TStat, TContainer>(
            this IEnumerable<TStat> stats)
            where TStat : class, IStatDate, ILteCellQuery, new()
            where TContainer : ITopKpiContainer<TStat>, new()
        {
            return from q in stats
                group q by new
                {
                    q.ENodebId,
                    q.SectorId
                }
                into g
                select new TContainer
                {
                    TopStat = g.ArrayAggration(stat =>
                    {
                        stat.ENodebId = g.Key.ENodebId;
                        stat.SectorId = g.Key.SectorId;
                        stat.StatDate = g.First().StatDate;
                    }),
                    TopDates = g.Count()
                };
        }

        public static List<TopPrecise4GContainer> Order(this IEnumerable<TopPrecise4GContainer> result, 
            OrderPreciseStatPolicy policy, int topCount)
        {
            switch (policy)
            {
                case OrderPreciseStatPolicy.OrderBySecondRate:
                    return result.OrderByDescending(x => x.PreciseCoverage4G.SecondRate)
                        .Take(topCount).ToList();
                case OrderPreciseStatPolicy.OrderBySecondNeighborsDescending:
                    return result
                        .Where(x => x.PreciseCoverage4G.SecondRate > Settings.Default.PreciseRateThreshold)
                        .OrderByDescending(x => x.PreciseCoverage4G.SecondNeighbors)
                        .Take(topCount).ToList();
                case OrderPreciseStatPolicy.OrderByFirstRate:
                    return result.OrderByDescending(x => x.PreciseCoverage4G.FirstRate)
                        .Take(topCount).ToList();
                case OrderPreciseStatPolicy.OrderByFirstNeighborsDescending:
                    return result
                        .Where(x => x.PreciseCoverage4G.SecondRate > Settings.Default.PreciseRateThreshold)
                        .OrderByDescending(x => x.PreciseCoverage4G.FirstNeighbors)
                        .Take(topCount).ToList();
                case OrderPreciseStatPolicy.OrderByTopDatesDescending:
                    return result
                        .Where(x => x.PreciseCoverage4G.SecondRate > Settings.Default.PreciseRateThreshold)
                        .OrderByDescending(x => x.TopDates).Take(topCount).ToList();
                default:
                    return result
                        .Where(x => x.PreciseCoverage4G.SecondRate > Settings.Default.PreciseRateThreshold)
                        .OrderByDescending(x => x.PreciseCoverage4G.TotalMrs)
                        .Take(topCount).ToList();
            }
        }

        public static IEnumerable<TopMrsRsrp> Order(this IEnumerable<TopMrsRsrp> stats, OrderMrsRsrpPolicy policy,
            int topCount)
        {
            switch (policy)
            {
                case OrderMrsRsrpPolicy.OrderBy105Rate:
                    return
                        stats.OrderByDescending(x => (double)(x.RsrpBelow120 + x.Rsrp120To115 + x.Rsrp115To110 + x.Rsrp110To105)
                                                     /
                                                     (x.RsrpBelow120 + x.Rsrp120To115 + x.Rsrp115To110 + x.Rsrp110To105 +
                                                      x.Rsrp105To100 + x.Rsrp100To95
                                                      + x.Rsrp95To90 + x.Rsrp90To80 + x.Rsrp90To80 + x.Rsrp80To70 +
                                                      x.Rsrp70To60 + x.RsrpAbove60)).Take(topCount).ToList();
                case OrderMrsRsrpPolicy.OrderBy105TimesDescending:
                    return
                        stats.OrderByDescending(x => x.RsrpBelow120 + x.Rsrp120To115 + x.Rsrp115To110 + x.Rsrp110To105)
                            .Take(topCount)
                            .ToList();
                case OrderMrsRsrpPolicy.OrderBy110Rate:
                    return
                        stats.OrderByDescending(x => (double)(x.RsrpBelow120 + x.Rsrp120To115 + x.Rsrp115To110)
                                                     /
                                                     (x.RsrpBelow120 + x.Rsrp120To115 + x.Rsrp115To110 + x.Rsrp110To105 +
                                                      x.Rsrp105To100 + x.Rsrp100To95
                                                      + x.Rsrp95To90 + x.Rsrp90To80 + x.Rsrp90To80 + x.Rsrp80To70 +
                                                      x.Rsrp70To60 + x.RsrpAbove60)).Take(topCount).ToList();
                default:
                    return
                        stats.OrderByDescending(x => x.RsrpBelow120 + x.Rsrp120To115 + x.Rsrp115To110)
                            .Take(topCount)
                            .ToList();
            }
        }

        public static IEnumerable<TopMrsSinrUl> Order(this IEnumerable<TopMrsSinrUl> stats, OrderMrsSinrUlPolicy policy,
            int topCount)
        {
            switch (policy)
            {
                case OrderMrsSinrUlPolicy.OrderByM3Rate:
                    return
                        stats.OrderByDescending(x => (double)(x.SinrUlBelowM9 + x.SinrUlM9ToM6 + x.SinrUlM6ToM3)
                                                     /
                                                     (x.SinrUlBelowM9 + x.SinrUlM9ToM6 + x.SinrUlM6ToM3 + x.SinrUlM3To0 +
                                                      x.SinrUl0To3 + x.SinrUl3To6 + x.SinrUl6To9 + x.SinrUl9To12
                                                      + x.SinrUl12To15 + x.SinrUl15To18 + x.SinrUl18To21
                                                      + x.SinrUl21To24 + x.SinrUlAbove24)).Take(topCount).ToList();
                case OrderMrsSinrUlPolicy.OrderByM3TimesDescending:
                    return
                        stats.OrderByDescending(x => x.SinrUlBelowM9 + x.SinrUlM9ToM6 + x.SinrUlM6ToM3)
                            .Take(topCount)
                            .ToList();
                case OrderMrsSinrUlPolicy.OrderBy0Rate:
                    return
                        stats.OrderByDescending(x => (double)(x.SinrUlBelowM9 + x.SinrUlM9ToM6 + x.SinrUlM6ToM3 + x.SinrUlM3To0)
                                                     /
                                                     (x.SinrUlBelowM9 + x.SinrUlM9ToM6 + x.SinrUlM6ToM3 + x.SinrUlM3To0 +
                                                      x.SinrUl0To3 + x.SinrUl3To6 + x.SinrUl6To9 + x.SinrUl9To12
                                                      + x.SinrUl12To15 + x.SinrUl15To18 + x.SinrUl18To21
                                                      + x.SinrUl21To24 + x.SinrUlAbove24)).Take(topCount).ToList();
                case OrderMrsSinrUlPolicy.OrderBy0TimesDescending:
                    return
                        stats.OrderByDescending(x => x.SinrUlBelowM9 + x.SinrUlM9ToM6 + x.SinrUlM6ToM3 + x.SinrUlM3To0)
                            .Take(topCount)
                            .ToList();
                case OrderMrsSinrUlPolicy.OrderBy3Rate:
                    return
                        stats.OrderByDescending(x => (double)(x.SinrUlBelowM9 + x.SinrUlM9ToM6 + x.SinrUlM6ToM3 + x.SinrUlM3To0 + x.SinrUl0To3)
                                                     /
                                                     (x.SinrUlBelowM9 + x.SinrUlM9ToM6 + x.SinrUlM6ToM3 + x.SinrUlM3To0 +
                                                      x.SinrUl0To3 + x.SinrUl3To6 + x.SinrUl6To9 + x.SinrUl9To12
                                                      + x.SinrUl12To15 + x.SinrUl15To18 + x.SinrUl18To21
                                                      + x.SinrUl21To24 + x.SinrUlAbove24)).Take(topCount).ToList();
                default:
                    return
                        stats.OrderByDescending(x => x.SinrUlBelowM9 + x.SinrUlM9ToM6 + x.SinrUlM6ToM3 + x.SinrUlM3To0 + x.SinrUl0To3)
                            .Take(topCount)
                            .ToList();
            }
        }

        public static IEnumerable<CoverageStat> Order(this IEnumerable<CoverageStat> stats, OrderMrsRsrpPolicy policy,
            int topCount)
        {
            switch (policy)
            {
                case OrderMrsRsrpPolicy.OrderBy105Rate:
                    return
                        stats.OrderBy(x => (double)x.TelecomAbove105 / x.TelecomMrs).Take(topCount).ToList();
                case OrderMrsRsrpPolicy.OrderBy105TimesDescending:
                    return
                        stats.OrderBy(x => x.TelecomAbove105)
                            .Take(topCount)
                            .ToList();
                case OrderMrsRsrpPolicy.OrderBy110Rate:
                    return
                        stats.OrderBy(x => (double)x.TelecomAbove110 / x.TelecomMrs).Take(topCount).ToList();
                default:
                    return
                        stats.OrderBy(x => x.TelecomAbove110)
                            .Take(topCount)
                            .ToList();
            }
        }

        public static IEnumerable<TopConnection3GTrendView> Order(this IEnumerable<TopConnection3GTrendView> stats,
            OrderTopConnection3GPolicy policy,
            int topCount)
        {
            switch (policy)
            {
                case OrderTopConnection3GPolicy.OrderByConnectionFailsDescending:
                    return stats.OrderByDescending(x => x.ConnectionFails).Take(topCount);
                case OrderTopConnection3GPolicy.OrderByConnectionRate:
                    return stats.OrderBy(x => x.ConnectionRate).Take(topCount);
                default:
                    return stats.OrderByDescending(x => x.TopDates).Take(topCount);
            }
        }

        public static IEnumerable<TopDrop2GTrendView> Order(this IEnumerable<TopDrop2GTrendView> stats,
            OrderTopDrop2GPolicy policy,
            int topCount)
        {
            switch (policy)
            {
                case OrderTopDrop2GPolicy.OrderByDropRateDescending:
                    return stats.OrderByDescending(x => x.DropRate).Take(topCount);
                case OrderTopDrop2GPolicy.OrderByDropsDescending:
                    return stats.OrderByDescending(x => x.TotalDrops).Take(topCount);
                default:
                    return stats.OrderByDescending(x => x.TopDates).Take(topCount);
            }
        }

    }
}
