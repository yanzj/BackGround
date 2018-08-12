using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.Mr;

namespace Lte.MySqlFramework.Concrete.Mr
{
    public class AgisDtPointRepository : EfRepositorySave<MySqlContext, AgisDtPoint>, IAgisDtPointRepository
    {
        public AgisDtPointRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
