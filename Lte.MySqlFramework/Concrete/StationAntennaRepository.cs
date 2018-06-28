using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
{
    public class StationAntennaRepository : EfRepositorySave<MySqlContext, StationAntenna>, IStationAntennaRepository
    {
        public StationAntennaRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public StationAntenna Match(StationAntennaExcel stat)
        {
            return FirstOrDefault(x => x.AntennaNum == stat.AntennaNum && x.StationNum == stat.StationNum);
        }
    }
}
