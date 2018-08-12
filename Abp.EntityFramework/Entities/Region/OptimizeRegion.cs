using Abp.Domain.Entities;

namespace Abp.EntityFramework.Entities.Region
{
    public class OptimizeRegion : Entity
    {
        public string City { get; set; }

        public string Region { get; set; }

        public string District { get; set; }
    }
}