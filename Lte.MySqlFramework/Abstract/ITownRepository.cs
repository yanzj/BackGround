using System.Collections.Generic;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Region;

namespace Lte.MySqlFramework.Abstract
{
    public interface ITownRepository : IRepository<Town>
    {
        IEnumerable<Town> QueryTowns(string city, string district, string town);
        
        Town QueryTown(string district, string town);
        
        IEnumerable<string> GetFoshanDistricts();
    }
}