using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Maintainence;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.Maintainence;

namespace Lte.MySqlFramework.Concrete.College
{
    public class ModuleUsageRecordRepository : EfRepositorySave<MySqlContext, ModuleUsageRecord>,
        IModuleUsageRecordRepository
    {
        public ModuleUsageRecordRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}