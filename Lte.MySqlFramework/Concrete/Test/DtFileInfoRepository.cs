using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Test;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.Test;

namespace Lte.MySqlFramework.Concrete.Test
{
    public class DtFileInfoRepository : EfRepositorySave<MySqlContext, CsvFilesInfo>, IDtFileInfoRepository
    {
        public DtFileInfoRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}