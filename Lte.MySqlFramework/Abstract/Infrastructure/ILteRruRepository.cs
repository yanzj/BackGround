using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Infrastructure;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;

namespace Lte.MySqlFramework.Abstract.Infrastructure
{
    public interface ILteRruRepository : IRepository<LteRru>,
        IMatchRepository<LteRru, CellExcel>, ISaveChanges
    {
        LteRru Get(int eNodebId, byte localSectorId);
    }
}