using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
{
    public class AreaTestInfoRepository : EfRepositorySave<MySqlContext, AreaTestInfo>, IAreaTestInfoRepository
    {
        public AreaTestInfoRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}