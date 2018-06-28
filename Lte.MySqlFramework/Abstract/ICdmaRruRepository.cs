using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;

namespace Lte.MySqlFramework.Abstract
{
    public interface ICdmaRruRepository : IRepository<CdmaRru>,
        IMatchRepository<CdmaRru, CdmaCellExcel>, ISaveChanges
    {
        CdmaRru Get(int btsId, byte sectorId);
    }
}