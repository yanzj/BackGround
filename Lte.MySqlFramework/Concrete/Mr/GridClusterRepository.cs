using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Mr;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.Mr;

namespace Lte.MySqlFramework.Concrete.Mr
{
    public class GridClusterRepository : EfRepositorySave<MySqlContext, GridCluster>, IGridClusterRepository
    {
        public GridClusterRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}