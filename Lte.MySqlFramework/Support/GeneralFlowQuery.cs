using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Infrastructure;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Cell;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Region;

namespace Lte.MySqlFramework.Support
{
    public static class GeneralFlowQuery
    {
        public static List<TView> QueryZteViews<TView, T>(this List<ENodeb> eNodebs, List<T> zteStats)
            where T : ILteCellQuery
            where TView : class, IENodebName, new()
        {
            return
                (from eNodeb in eNodebs join stat in zteStats on eNodeb.ENodebId equals stat.ENodebId select stat)
                .MapTo<List<TView>>();

        }

        public static List<Tuple<TView, byte>> QueryHuaweiViews<TView, T>(this List<ENodeb> eNodebs, List<T> huaweiStats,
            ICellRepository huaweiCellRepository)
            where T : ILocalCellQuery
            where TView : class, IENodebName, ILteCellQuery, new()
        {
            var cells = (from eNodeb in eNodebs
                join cell in huaweiCellRepository.GetAllList() on eNodeb.ENodebId equals cell.ENodebId
                select cell).ToList();
            return cells.QueryHuaweiTuples<TView, T>(huaweiStats);
        }

        private static List<Tuple<TView, byte>> QueryHuaweiTuples<TView, T>(this List<Cell> cells, List<T> huaweiStats)
            where T : ILocalCellQuery
            where TView : class, IENodebName, ILteCellQuery, new()
        {
            return (from cell in cells
                join stat in huaweiStats on new {cell.ENodebId, cell.LocalSectorId} equals
                new {stat.ENodebId, LocalSectorId = stat.LocalCellId}
                select new Tuple<TView, byte>(stat.MapTo<TView>(), cell.SectorId)).ToList();
        }

        public static IEnumerable<TView> QueryDistrictFlowViews<TView, TZte, THuawei>(this ICellRepository huaweiCellRepository,
            string city, string district, List<TZte> zteStats, List<THuawei> huaweiStats,
            ITownRepository townRepository, IENodebRepository eNodebRepository)
            where TZte : ILteCellQuery
            where THuawei : ILocalCellQuery
            where TView : class, IENodebName, ILteCellQuery, new()
        {
            var eNodebs = townRepository.QueryENodebs(eNodebRepository, city, district);
            if (!eNodebs.Any())
            {
                return new List<TView>();
            }

            var zteViews = eNodebs.QueryZteViews<TView, TZte>(zteStats);
            var huaweiViews = eNodebs.QueryHuaweiViews<TView, THuawei>(huaweiStats, huaweiCellRepository).Select(v =>
            {
                var view = v.Item1;
                view.SectorId = v.Item2;
                return view;
            });

            return zteViews.Concat(huaweiViews).ToList();
        }

        public static IEnumerable<TView> QueryDistrictKpiViews<TView, TZte, THuawei>(this IENodebRepository eNodebRepository,
            string city, string district, List<TZte> zteStats, List<THuawei> huaweiStats,
            ITownRepository townRepository)
            where TZte : ILteCellQuery
            where THuawei : ILteCellQuery
            where TView : class, IENodebName, ILteCellQuery, new()
        {
            var eNodebs = townRepository.QueryENodebs(eNodebRepository, city, district);
            if (!eNodebs.Any())
            {
                return new List<TView>();
            }

            var zteViews = eNodebs.QueryZteViews<TView, TZte>(zteStats);
            var huaweiViews = eNodebs.QueryZteViews<TView, THuawei>(huaweiStats);

            return zteViews.Concat(huaweiViews).ToList();
        }

        public static IEnumerable<TView> QueryTownFlowViews<TView, TZte, THuawei>(this ICellRepository huaweiCellRepository,
            string city, string district, string town, List<TZte> zteStats, List<THuawei> huaweiStats,
            FrequencyBandType frequency,
            ITownRepository townRepository, IENodebRepository eNodebRepository)
            where TZte : ILteCellQuery
            where THuawei : ILocalCellQuery
            where TView : class, IENodebName, ILteCellQuery, new()
        {
            var eNodebs = townRepository.QueryENodebs(eNodebRepository, city, district, town);
            if (!eNodebs.Any())
            {
                return new List<TView>();
            }

            var zteViews = eNodebs.QueryZteViews<TView, TZte>(zteStats);
            var huaweiList = eNodebs.QueryHuaweiViews<TView, THuawei>(huaweiStats, huaweiCellRepository);

            IEnumerable<Tuple<TView, byte>> huaweiViews;
            switch (frequency)
            {
                case FrequencyBandType.All:
                    huaweiViews = huaweiList;
                    break;
                case FrequencyBandType.Band2100:
                    huaweiViews = huaweiList.Where(x => x.Item2 < 16);
                    break;
                case FrequencyBandType.Band1800:
                    huaweiViews = huaweiList.Where(x => x.Item2 >= 48 && x.Item2 < 64);
                    break;
                default:
                    huaweiViews = huaweiList.Where(x => x.Item2 >= 16 && x.Item2 < 32);
                    break;
            }
            return zteViews.Concat(huaweiViews.Select(v =>
                {
                    var view = v.Item1;
                    view.SectorId = v.Item2;
                    return view;
                })).ToList();
        }

        public static IEnumerable<TView> QueryAllFlowViews<TView, TZte, THuawei>(this ICellRepository huaweiCellRepository,
            List<TZte> zteStats, List<THuawei> huaweiStats)
            where TZte : ILteCellQuery
            where THuawei : ILocalCellQuery
            where TView : class, IENodebName, ILteCellQuery, new()
        {
            var zteViews = zteStats.MapTo<List<TView>>();
            var huaweiViews =
                huaweiCellRepository.GetAllList().QueryHuaweiTuples<TView, THuawei>(huaweiStats)
                    .Select(v =>
                    {
                        var view = v.Item1;
                        view.SectorId = v.Item2;
                        return view;
                    });

            return zteViews.Concat(huaweiViews).ToList();
        }

        public static List<TStat> QueryZteFlows<TStat, TRepository>(this TRepository zteRepository,
            FrequencyBandType frequency, DateTime begin, DateTime end)
            where TStat: Entity, IStatTime, ILteCellQuery
            where TRepository: IRepository<TStat>
        {
            switch (frequency)
            {
                case FrequencyBandType.All:
                    return zteRepository.GetAllList(x => x.StatTime >= begin && x.StatTime < end);
                case FrequencyBandType.Band2100:
                    return
                        zteRepository.GetAllList(
                            x => x.StatTime >= begin && x.StatTime < end && x.SectorId < 16);
                case FrequencyBandType.Band1800:
                    return
                        zteRepository.GetAllList(
                            x => x.StatTime >= begin && x.StatTime < end && x.SectorId >= 48 && x.SectorId < 64);
                default:
                    return
                        zteRepository.GetAllList(
                            x => x.StatTime >= begin && x.StatTime < end && x.SectorId >= 16 && x.SectorId < 32);
            }
        }

    }
}