using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.College;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;

namespace Lte.MySqlFramework.Abstract
{
    public interface IHotSpotCellRepository : IRepository<HotSpotCellId>, ISaveChanges, IMatchRepository<HotSpotCellId, HotSpotCellExcel>
    {
        
    }
}