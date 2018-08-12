using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Maintainence;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Abstract.Maintainence;

namespace Lte.MySqlFramework.Concrete.Maintainence
{
    public class CheckingBasicRepository : EfRepositorySave<MySqlContext, CheckingBasic>, ICheckingBasicRepository
    {
        public CheckingBasicRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public CheckingBasic Match(CheckingBasicExcel stat)
        {
            return FirstOrDefault(x => x.CheckingFlowNumber == stat.CheckingFlowNumber);
        }
    }
}
