using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Wireless;

namespace Abp.EntityFramework.Entities.RegionKpi
{
    [AutoMapFrom(typeof(TownPreciseView))]
    public class DistrictPreciseView : ICityDistrict
    {
        public string City { get; set; } = "-";

        public string District { get; set; } = "-";

        public long TotalMrs { get; set; }

        public long SecondNeighbors { get; set; }

        public long FirstNeighbors { get; set; }

        public long ThirdNeighbors { get; set; }

        public double PreciseRate => 100 - (double)SecondNeighbors * 100 / TotalMrs;

        public double FirstRate => 100 - (double)FirstNeighbors * 100 / TotalMrs;

        public double ThirdRate => 100 - (double)ThirdNeighbors * 100 / TotalMrs;

        public long NeighborsMore { get; set; }

        public long InterFirstNeighbors { get; set; }

        public long InterSecondNeighbors { get; set; }

        public long InterThirdNeighbors { get; set; }

        public static DistrictPreciseView ConstructView(TownPreciseView townView)
        {
            return townView.MapTo<DistrictPreciseView>();
        }
    }
}