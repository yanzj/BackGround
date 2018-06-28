using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Wireless;

namespace Lte.Parameters.Entities.Kpi
{
    [AutoMapFrom(typeof(TownPreciseView))]
    public class DistrictPreciseView : ICityDistrict
    {
        public string City { get; set; } = "-";

        public string District { get; set; } = "-";

        public int TotalMrs { get; set; }

        public int SecondNeighbors { get; set; }

        public int FirstNeighbors { get; set; }

        public int ThirdNeighbors { get; set; }

        public double PreciseRate => 100 - (double)SecondNeighbors * 100 / TotalMrs;

        public double FirstRate => 100 - (double)FirstNeighbors * 100 / TotalMrs;

        public double ThirdRate => 100 - (double)ThirdNeighbors * 100 / TotalMrs;

        public int NeighborsMore { get; set; }

        public int InterFirstNeighbors { get; set; }

        public int InterSecondNeighbors { get; set; }

        public int InterThirdNeighbors { get; set; }

        public static DistrictPreciseView ConstructView(TownPreciseView townView)
        {
            return townView.MapTo<DistrictPreciseView>();
        }
    }
}