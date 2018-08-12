using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Station;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
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