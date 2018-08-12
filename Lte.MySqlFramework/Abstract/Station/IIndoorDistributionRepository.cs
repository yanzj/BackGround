using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Station;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract.Station
{
    public interface IIndoorDistributionRepository : IRepository<IndoorDistribution>, ISaveChanges
    {
    }
}