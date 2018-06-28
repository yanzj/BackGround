using Abp.Domain.Entities;

namespace Abp.EntityFramework.Entities
{
    public class OptimizeRegion : Entity
    {
        public string City { get; set; }

        public string Region { get; set; }

        public string District { get; set; }
    }
}