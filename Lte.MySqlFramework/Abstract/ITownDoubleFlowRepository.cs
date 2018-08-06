using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Entities;

namespace Lte.MySqlFramework.Abstract
{
    public interface ITownDoubleFlowRepository : IRepository<TownDoubleFlow>, ISaveChanges
    {
        
    }
}