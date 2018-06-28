using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
{
    public class TopMrsRsrpRepository : EfRepositorySave<MySqlContext, TopMrsRsrp>, ITopMrsRsrpRepository
    {
        public TopMrsRsrpRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}