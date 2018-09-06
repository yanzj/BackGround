using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Test;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.Test;

namespace Lte.MySqlFramework.Concrete.College
{
    public class EFCollege4GTestRepository : EfRepositorySave<MySqlContext, College4GTestResults>, ICollege4GTestRepository
    {
        public EFCollege4GTestRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}