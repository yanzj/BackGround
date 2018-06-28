using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract
{
    public interface ICollege4GTestRepository : IRepository<College4GTestResults>, ISaveChanges
    {
    }
}