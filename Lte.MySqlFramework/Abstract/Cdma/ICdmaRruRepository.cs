using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Cdma;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;

namespace Lte.MySqlFramework.Abstract.Cdma
{
    public interface ICdmaRruRepository : IRepository<CdmaRru>,
        IMatchRepository<CdmaRru, CdmaCellExcel>, ISaveChanges
    {
        CdmaRru Get(int btsId, byte sectorId);
    }
}