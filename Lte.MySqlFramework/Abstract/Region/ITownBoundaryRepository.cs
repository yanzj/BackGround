using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Region;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract.Region
{
    public interface ITownBoundaryRepository : IRepository<TownBoundary>, ISaveChanges
    {
        
    }
}