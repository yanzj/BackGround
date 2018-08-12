using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.College;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract
{
    public interface IHotSpotENodebRepository : IRepository<HotSpotENodebId>, ISaveChanges
    {
        
    }
}