using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular;

namespace Abp.EntityFramework.Entities
{
    public class CollegeRegion
    {
        [Key]
        public int AreaId { get; set; }

        public double Area { get; set; }

        public RegionType RegionType { get; set; }

        public string Info { get; set; }

        public RectangleRange RectangleRange
        {
            get
            {
                var coordinates = Info.Split(';');
                switch (RegionType)
                {
                    case RegionType.Circle:
                        return new RectangleRange
                        {
                            West = coordinates[0].ConvertToDouble(112) - coordinates[2].ConvertToDouble(0.01),
                            East = coordinates[0].ConvertToDouble(112) + coordinates[2].ConvertToDouble(0.01),
                            South = coordinates[1].ConvertToDouble(23) - coordinates[2].ConvertToDouble(0.01),
                            North = coordinates[1].ConvertToDouble(23) - coordinates[2].ConvertToDouble(0.01)
                        };
                    case RegionType.Rectangle:
                        return new RectangleRange
                        {
                            West = coordinates[0].ConvertToDouble(112),
                            East = coordinates[2].ConvertToDouble(112.01),
                            South = coordinates[1].ConvertToDouble(23),
                            North = coordinates[3].ConvertToDouble(23.01)
                        };
                    default:
                        var xcoors = new List<double>();
                        var ycoors = new List<double>();
                        for (var i = 0; i < coordinates.Length; i += 2)
                        {
                            xcoors.Add(coordinates[i].ConvertToDouble(112));
                            ycoors.Add(coordinates[i + 1].ConvertToDouble(23));
                        }
                        return new RectangleRange
                        {
                            West = xcoors.Min() - 0.005,
                            East = xcoors.Max() + 0.005,
                            South = ycoors.Min() - 0.005,
                            North = ycoors.Max() + 0.005
                        };
                }
            }
        }
    }
}