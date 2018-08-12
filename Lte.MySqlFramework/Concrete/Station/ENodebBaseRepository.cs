using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Station;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Abstract.Station;

namespace Lte.MySqlFramework.Concrete.Station
{
    public class ENodebBaseRepository : EfRepositorySave<MySqlContext, ENodebBase>, IENodebBaseRepository
    {
        public ENodebBaseRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public ENodebBase Match(ENodebBaseExcel stat)
        {
            return FirstOrDefault(x => x.ENodebId == stat.ENodebId);
        }
    }
}