using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Complain;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;

namespace Lte.MySqlFramework.Abstract.Complain
{
    public interface IComplainItemRepository
        : IRepository<ComplainItem>,
            IMatchRepository<ComplainItem, ComplainExcel>,
            IMatchRepository<ComplainItem, ComplainDto>,
            IMatchRepository<ComplainItem, ComplainSupplyExcel>,
            IDateSpanRepository<ComplainItem>,
            ISaveChanges
    {
        ComplainItem Get(string serialNumber);
    }
}