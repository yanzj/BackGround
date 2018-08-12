using System.Collections.Generic;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Region;

namespace Lte.MySqlFramework.Abstract.Region
{
    public interface ITownRepository : IRepository<Town>
    {
        Town QueryTown(string district, string town);
        
        IEnumerable<string> GetFoshanDistricts();
    }
}