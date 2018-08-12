using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Station;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.Station;

namespace Lte.MySqlFramework.Concrete.Station
{
    public class IndoorDistributionRepository
        : EfRepositorySave<MySqlContext, IndoorDistribution>, IIndoorDistributionRepository
    {
        public IndoorDistributionRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}