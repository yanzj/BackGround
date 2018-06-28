using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
{
    public class StationRruRepository : EfRepositorySave<MySqlContext, StationRru>, IStationRruRepository
    {
        public StationRruRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public StationRru Match(StationRruExcel stat)
        {
            return FirstOrDefault(x => x.RruSerialNum == stat.RruSerialNum);
        }
    }
}
