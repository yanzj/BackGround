using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Test;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
{
    public class DtFileInfoRepository : EfRepositorySave<MySqlContext, CsvFilesInfo>, IDtFileInfoRepository
    {
        public DtFileInfoRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}