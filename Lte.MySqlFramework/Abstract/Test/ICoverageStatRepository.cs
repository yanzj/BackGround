using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Test;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract.Test
{
    public interface ICoverageStatRepository : IRepository<CoverageStat>, IMatchRepository<CoverageStat>, ISaveChanges
    {
        
    }
}