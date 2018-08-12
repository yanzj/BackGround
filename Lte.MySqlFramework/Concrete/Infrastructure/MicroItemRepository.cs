using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Infrastructure;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.Infrastructure;

namespace Lte.MySqlFramework.Concrete.Infrastructure
{
    public class MicroItemRepository : EfRepositorySave<MySqlContext, MicroItem>, IMicroItemRepository
    {
        public MicroItemRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}