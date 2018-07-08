using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
{
    public class EFIndoorDistributionRepository
        : EfRepositorySave<MySqlContext, IndoorDistribution>, IIndoorDistributionRepository
    {
        public EFIndoorDistributionRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}