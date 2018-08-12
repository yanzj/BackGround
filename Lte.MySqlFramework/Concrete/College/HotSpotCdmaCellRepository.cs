using Abp.EntityFramework;
using Abp.EntityFramework.Entities.College;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.College;

namespace Lte.MySqlFramework.Concrete.College
{
    public class HotSpotCdmaCellRepository : EfRepositorySave<MySqlContext, HotSpotCdmaCellId>,
        IHotSpotCdmaCellRepository
    {
        public HotSpotCdmaCellRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}