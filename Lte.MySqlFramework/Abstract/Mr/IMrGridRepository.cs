using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract.Mr
{
    public interface IMrGridRepository : IRepository<MrGrid>, ISaveChanges
    {
        
    }
}