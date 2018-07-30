using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;

namespace Lte.MySqlFramework.Concrete
{
    public class TownCqiRepository : EfRepositorySave<MySqlContext, TownCqiStat>, ITownCqiRepository
    {
        public TownCqiRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}