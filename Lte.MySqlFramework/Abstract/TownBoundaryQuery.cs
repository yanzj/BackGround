using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Entities;
using Lte.Domain.Common.Geo;

namespace Lte.MySqlFramework.Abstract
{
    public static class TownBoundaryQuery
    {
        public static bool IsInTownRange(this List<TownBoundary> coors, IGeoPoint<double> point)
        {
            return coors.Any(coor => point.IsInPolygon(coor.CoorList()));
        }

        public static bool IsInTownRange(this TownBoundary coor, IGeoPoint<double> point)
        {
            return point.IsInPolygon(coor.CoorList());
        }
    }
}