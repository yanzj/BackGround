using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;

namespace Lte.MySqlFramework.Abstract
{
    public interface IComplainItemRepository
        : IRepository<ComplainItem>,
            IMatchRepository<ComplainItem, ComplainExcel>,
            IMatchRepository<ComplainItem, ComplainDto>,
            IMatchRepository<ComplainItem, ComplainSupplyExcel>,
            IDateSpanQuery<ComplainItem>,
            ISaveChanges
    {
        ComplainItem Get(string serialNumber);
    }
}