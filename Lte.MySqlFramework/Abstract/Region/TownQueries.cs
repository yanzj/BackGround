using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Region;
using AutoMapper;
using Lte.Domain.Common.Wireless;

namespace Lte.MySqlFramework.Abstract.Region
{
    public static class TownQueries
    {
        public static TView ConstructView<TStat, TView>(this TStat stat, ITownRepository repository)
            where TStat : ITownId
            where TView : ICityDistrictTown
        {
            var town = stat.TownId == -1 ? null : repository.Get(stat.TownId);
            var view = Mapper.Map<TStat, TView>(stat);
            view.City = town?.CityName;
            view.District = town?.DistrictName;
            view.Town = town?.TownName;
            return view;
        }

        public static TView ConstructView<TStat, TView>(this TStat stat, Town town)
            where TStat : ITownId
            where TView : ICityDistrictTown
        {
            var view = Mapper.Map<TStat, TView>(stat);
            view.City = town.CityName;
            view.District = town.DistrictName;
            view.Town = town.TownName;
            return view;
        }

        public static List<TView> ConstructViews<TStat, TView>(this IEnumerable<TStat> stats, ITownRepository townRepository)
            where TStat : ITownId
            where TView : ICityDistrictTown
        {
            return stats.Select(x => ConstructView<TStat, TView>(x, townRepository)).ToList();
        }

        public static TView ConstructAreaView<TStat, TView>(this TStat stat, ITownRepository repository)
            where TStat : IArea
        {
            var town =
                repository.FirstOrDefault(
                    x => x.TownName == (stat.Area == "±±½Ñ" ? "±±œò" : stat.Area) || x.DistrictName == stat.Area);
            var view = Mapper.Map<TStat, TView>(stat);
            if (town != null)
            {
                town.MapTo(view);
            }
            return view;
        }

        public static List<TView> QueryTownStat<TTownStat, TView>(this IEnumerable<TTownStat> query,
            ITownRepository townRepository, string city)
            where TTownStat : ITownId
            where TView : ICityDistrictTown
        {
            var towns = townRepository.GetAllList(x => x.CityName == city);
            return (from q in query
                join t in towns on q.TownId equals t.Id
                select q.ConstructView<TTownStat, TView>(t)).ToList();
        }
    }
}