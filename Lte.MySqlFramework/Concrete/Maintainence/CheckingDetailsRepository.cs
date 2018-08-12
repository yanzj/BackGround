using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Maintainence;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Abstract.Maintainence;

namespace Lte.MySqlFramework.Concrete.Maintainence
{
    public class CheckingDetailsRepository : EfRepositorySave<MySqlContext, CheckingDetails>, ICheckingDetailsRepository
    {
        public CheckingDetailsRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public CheckingDetails Match(CheckingDetailsExcel stat, string key)
        {
            return FirstOrDefault(x => x.CheckingFlowNumber == stat.CheckingFlowNumber && x.CheckingTheme == key);
        }
    }
}
