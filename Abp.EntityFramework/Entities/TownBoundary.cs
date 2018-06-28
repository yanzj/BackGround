using System.Collections.Generic;
using Abp.Domain.Entities;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular;

namespace Abp.EntityFramework.Entities
{
    public class TownBoundary : Entity, ITownId
    {
        public int TownId { get; set; }

        public string AreaName { get; set; }

        public string Boundary { get; set; }

        public List<GeoPoint> CoorList()
        {
            var coors = Boundary.GetSplittedFields(' ');
            var coorList = new List<GeoPoint>();
            for (var i = 0; i < coors.Length / 2; i++)
            {
                coorList.Add(new GeoPoint(coors[i * 2].ConvertToDouble(0), coors[i * 2 + 1].ConvertToDouble(0)));
            }
            return coorList;
        }
    }
}