﻿using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Station;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;

namespace Lte.MySqlFramework.Abstract.Station
{
    public interface IStationAntennaRepository : IRepository<StationAntenna>, ISaveChanges,
        IMatchRepository<StationAntenna, StationAntennaExcel>
    {
    }
}
