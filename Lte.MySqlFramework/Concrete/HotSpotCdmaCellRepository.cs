using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.College;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
{
    public class HotSpotCdmaCellRepository : EfRepositorySave<MySqlContext, HotSpotCdmaCellId>,
        IHotSpotCdmaCellRepository
    {
        public HotSpotCdmaCellRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}