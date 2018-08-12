using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Complain;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;

namespace Lte.MySqlFramework.Abstract
{
    public interface IBranchDemandRepository
        : IRepository<BranchDemand>, IMatchRepository<BranchDemand, BranchDemandExcel>,
            IDateSpanQuery<BranchDemand>, ISaveChanges
    {
        
    }
}