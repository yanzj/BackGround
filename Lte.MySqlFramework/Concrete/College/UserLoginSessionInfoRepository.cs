using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Maintainence;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.Maintainence;

namespace Lte.MySqlFramework.Concrete.College
{
    public class UserLoginSessionInfoRepository : EfRepositorySave<MySqlContext, UserLoginSessionInfo>,
        IUserLoginSessionInfoRepository
    {
        public UserLoginSessionInfoRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}