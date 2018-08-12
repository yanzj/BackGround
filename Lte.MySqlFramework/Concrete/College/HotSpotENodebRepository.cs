using Abp.EntityFramework;
using Abp.EntityFramework.Entities.College;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.College;

namespace Lte.MySqlFramework.Concrete.College
{
    public class HotSpotENodebRepository : EfRepositorySave<MySqlContext, HotSpotENodebId>, IHotSpotENodebRepository
    {
        public HotSpotENodebRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}