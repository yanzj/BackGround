using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Complain;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;

namespace Lte.MySqlFramework.Abstract.Complain
{
    public interface IOnlineSustainRepository
        : IRepository<OnlineSustain>, IMatchRepository<OnlineSustain, OnlineSustainExcel>,
            IDateSpanQuery<OnlineSustain>, ISaveChanges
    {
        
    }
}