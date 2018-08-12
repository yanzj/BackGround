using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Test;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.Test;

namespace Lte.MySqlFramework.Concrete.Test
{
    public class RasterTestInfoRepository : EfRepositorySave<MySqlContext, RasterTestInfo>, IRasterTestInfoRepository
    {
        public RasterTestInfoRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}