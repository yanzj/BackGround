using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;

namespace Lte.MySqlFramework.Abstract
{
    public interface IOptimzeRegionRepository : IRepository<OptimizeRegion>
    {
        List<OptimizeRegion> GetAllList(string city);

        Task<List<OptimizeRegion>> GetAllListAsync(string city);

        int SaveChanges();
    }
}