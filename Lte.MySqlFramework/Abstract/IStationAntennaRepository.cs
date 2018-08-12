using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Station;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Entities;

namespace Lte.MySqlFramework.Abstract
{
    public interface IStationAntennaRepository : IRepository<StationAntenna>, ISaveChanges,
        IMatchRepository<StationAntenna, StationAntennaExcel>
    {
    }
}
