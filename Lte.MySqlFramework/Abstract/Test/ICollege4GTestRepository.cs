using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Test;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract.Test
{
    public interface ICollege4GTestRepository : IRepository<College4GTestResults>, ISaveChanges
    {
    }
}