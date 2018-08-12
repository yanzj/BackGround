using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Test;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.Test;

namespace Lte.MySqlFramework.Concrete.Test
{
    public class RasterInfoRepository : EfRepositorySave<MySqlContext, RasterInfo>, IRasterInfoRepository
    {
        public RasterInfoRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}