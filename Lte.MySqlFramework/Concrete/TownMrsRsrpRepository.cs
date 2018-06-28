using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;

namespace Lte.MySqlFramework.Concrete
{
    public class TownMrsRsrpRepository : EfRepositorySave<MySqlContext, TownMrsRsrp>, ITownMrsRsrpRepository
    {
        public TownMrsRsrpRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}