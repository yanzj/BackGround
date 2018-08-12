using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Infrastructure;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract.Infrastructure
{
    public interface IMicroAddressRepository : IRepository<MicroAddress>, ISaveChanges
    {
        
    }
}