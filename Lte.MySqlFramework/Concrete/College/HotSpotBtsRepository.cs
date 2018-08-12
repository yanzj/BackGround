using Abp.EntityFramework;
using Abp.EntityFramework.Entities.College;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.College;

namespace Lte.MySqlFramework.Concrete.College
{
    public class HotSpotBtsRepository : EfRepositorySave<MySqlContext, HotSpotBtsId>, IHotSpotBtsRepository
    {
        public HotSpotBtsRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}