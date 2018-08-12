using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Station;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Abstract.Station;

namespace Lte.MySqlFramework.Concrete.Station
{
    public class DistributionRepository : EfRepositorySave<MySqlContext, IndoorDistribution>, IDistributionRepository
    {
        public DistributionRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public IndoorDistribution Match(IndoorDistributionExcel stat)
        {
            return FirstOrDefault(x => x.IndoorSerialNum == stat.IndoorSerialNum);
        }
    }
}