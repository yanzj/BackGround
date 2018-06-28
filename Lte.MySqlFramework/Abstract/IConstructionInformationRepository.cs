using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;

namespace Lte.MySqlFramework.Abstract
{
    public interface IConstructionInformationRepository : IRepository<ConstructionInformation>, ISaveChanges,
        IMatchRepository<ConstructionInformation, ConstructionExcel>
    {
    }
}