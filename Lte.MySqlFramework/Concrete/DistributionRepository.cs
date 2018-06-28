using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
{
    public class DistributionRepository : EfRepositorySave<MySqlContext, IndoorDistribution>, IDistributionRepository
    {
        public DistributionRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public IndoorDistribution Match(IndoorDistributionExcel stat)
        {
            return FirstOrDefault(x => x.IndoorSerialNum == stat.IndoorSerialNum && x.BuildingName == stat.BuildingName);
        }
    }
}