using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Maintainence;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract.Maintainence
{
    public interface IModuleUsageRecordRepository : IRepository<ModuleUsageRecord>, ISaveChanges
    {
        
    }
}