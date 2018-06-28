using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
{
    public class LteProblemRepository : EfRepositorySave<MySqlContext, LteProblem>, ILteProblemRepository
    {
        public LteProblemRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
        
        public LteProblem Match(StandarProblemExcel stat)
        {
            return FirstOrDefault(x => x.Body == stat.Body);
        }

        public LteProblem Match(ChoiceProblemExcel stat)
        {
            return FirstOrDefault(x => x.Body == stat.Body);
        }
    }
}