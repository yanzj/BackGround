using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract
{
    public interface IBranchDemandRepository
        : IRepository<BranchDemand>, IMatchRepository<BranchDemand, BranchDemandExcel>,
            IDateSpanQuery<BranchDemand>, ISaveChanges
    {
        
    }
}