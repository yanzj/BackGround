using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;

namespace Abp.EntityFramework.Entities.Mr
{
    [AutoMapFrom(typeof(MrGridKpiDto))]
    public class MrGridKpi : Entity
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int MrCount { get; set; }

        public int WeakCount { get; set; }

        public double Rsrp { get; set; }

        public int MrCountNormalize { get; set; }

        public int WeakCountNormalize { get; set; }

        public int RsrpNormalize { get; set; }

        public int ShortestDistance { get; set; }
    }
}