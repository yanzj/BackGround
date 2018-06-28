using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;

namespace Lte.MySqlFramework.Abstract
{
    public interface IOnlineSustainRepository
        : IRepository<OnlineSustain>, IMatchRepository<OnlineSustain, OnlineSustainExcel>,
            IDateSpanQuery<OnlineSustain>, ISaveChanges
    {
        
    }
}