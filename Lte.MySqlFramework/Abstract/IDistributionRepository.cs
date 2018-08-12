using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Station;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;

namespace Lte.MySqlFramework.Abstract
{
    public interface IDistributionRepository : IRepository<IndoorDistribution>,
        IMatchRepository<IndoorDistribution, IndoorDistributionExcel>, ISaveChanges
    {
        
    }
}