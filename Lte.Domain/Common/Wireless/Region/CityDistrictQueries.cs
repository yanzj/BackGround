using System;
using System.Collections.Generic;
using System.Linq;
using Lte.Domain.Regular;

namespace Lte.Domain.Common.Wireless.Region
{
    public static class CityDistrictQueries
    {
        public static IEnumerable<TDistrictView> Merge<TDistrictView, TTownView>(this IEnumerable<TTownView> townPreciseViews,
            Func<TTownView, TDistrictView> constructView)
            where TDistrictView : ICityDistrict
            where TTownView : class, ICityDistrictTown, new()
        {
            var preciseViews = townPreciseViews as TTownView[] ?? townPreciseViews.ToArray();
            if (!preciseViews.Any()) return null;
            var districts = preciseViews.Select(x => x.District).Distinct();
            var city = preciseViews.ElementAt(0).City;
            return districts.Select(district =>
            {
                var view =
                    constructView(preciseViews.Where(x => x.District == district).ArraySum());
                view.City = city;
                view.District = district;
                return view;
            }).ToList();
        }
    }
}