using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Geo;

namespace Abp.EntityFramework.Entities.Mr
{
    [AutoMapFrom(typeof(DpiGridKpi))]
    public class DpiGridKpiDto : IGeoGridPoint<double>
    {
        public int X { get; set; }
        
        public int Y { get; set; }

        public double Longtitute => 112 + X * 0.00049;

        public double Lattitute => 22 + Y * 0.00045;

        public double FirstPacketDelay { get; set; }

        public double FirstPacketDelayClass { get; set; }

        public double PageOpenDelay { get; set; }

        public double PageOpenDelayClass { get; set; }
    }
}