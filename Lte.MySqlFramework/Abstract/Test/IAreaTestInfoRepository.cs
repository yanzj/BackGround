using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract.Test
{
    public interface IAreaTestInfoRepository : IRepository<AreaTestInfo>, ISaveChanges
    {
        
    }
}