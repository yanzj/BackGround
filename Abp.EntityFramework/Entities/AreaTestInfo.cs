using Abp.Domain.Entities;
using Lte.Domain.Common.Wireless;

namespace Abp.EntityFramework.Entities
{
    public class AreaTestInfo : Entity, ITownId
    {
        public int FileId { get; set; }

        public double Distance { get; set; }

        public int Count { get; set; }

        public int CoverageCount { get; set; }

        public double CoverageRate => Count == 0 ? 0 : 100 * (double)CoverageCount / Count;

        public int TownId { get; set; }
    }
}