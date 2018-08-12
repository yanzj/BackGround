using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Maintainence;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Abstract.Maintainence;

namespace Lte.MySqlFramework.Concrete.Maintainence
{
    public class CheckingProjectRepository : EfRepositorySave<MySqlContext, CheckingProject>, ICheckingProjectRepository
    {
        public CheckingProjectRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public CheckingProject Match(CheckingProjectExcel stat)
        {
            return FirstOrDefault(x => x.CheckingFlowNumber == stat.CheckingFlowNumber);
        }
    }
}
