using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Infrastructure;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.Infrastructure;

namespace Lte.MySqlFramework.Concrete.Infrastructure
{
    public class MicroAddressRepository : EfRepositorySave<MySqlContext, MicroAddress>, IMicroAddressRepository
    {
        public MicroAddressRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}