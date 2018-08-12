using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;

namespace Abp.EntityFramework.Entities.Mr
{
    [AutoMapFrom(typeof(DpiGridKpiDto))]
    public class DpiGridKpi : Entity
    {
        public int X { get; set; }

        public int Y { get; set; }

        public double FirstPacketDelay { get; set; }

        public double FirstPacketDelayClass { get; set; }

        public double PageOpenDelay { get; set; }

        public double PageOpenDelayClass { get; set; }
    }
}