using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract
{
    public interface IRrcZteRepository : IRepository<RrcZte>, ISaveChanges, IMatchRepository<RrcZte>
    {
        
    }
}