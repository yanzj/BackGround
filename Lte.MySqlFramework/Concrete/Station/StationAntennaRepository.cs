using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Station;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Abstract.Station;

namespace Lte.MySqlFramework.Concrete.Station
{
    public class StationAntennaRepository : EfRepositorySave<MySqlContext, StationAntenna>, IStationAntennaRepository
    {
        public StationAntennaRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public StationAntenna Match(StationAntennaExcel stat)
        {
            return FirstOrDefault(x => x.AntennaNum == stat.AntennaNum);
        }
    }
}
