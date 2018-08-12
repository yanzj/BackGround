using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Region;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.Region;

namespace Lte.MySqlFramework.Concrete.Region
{
    public class TownBoundaryRepository : EfRepositorySave<MySqlContext, TownBoundary>, ITownBoundaryRepository
    {
        public TownBoundaryRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}