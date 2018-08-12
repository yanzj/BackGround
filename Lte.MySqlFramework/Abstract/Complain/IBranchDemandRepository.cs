using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Complain;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;

namespace Lte.MySqlFramework.Abstract.Complain
{
    public interface IBranchDemandRepository
        : IRepository<BranchDemand>, IMatchRepository<BranchDemand, BranchDemandExcel>,
            IDateSpanRepository<BranchDemand>, ISaveChanges
    {
        
    }
}