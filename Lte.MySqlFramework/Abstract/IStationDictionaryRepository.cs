using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Station;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Entities;

namespace Lte.MySqlFramework.Abstract
{
    public interface IStationDictionaryRepository : IRepository<StationDictionary>,
        IMatchRepository<StationDictionary, StationDictionaryExcel>, ISaveChanges
    {
        
    }
}