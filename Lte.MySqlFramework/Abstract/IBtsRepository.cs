using System.Collections.Generic;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Cdma;

namespace Lte.MySqlFramework.Abstract
{
    public interface IBtsRepository : IRepository<CdmaBts>
    {
        CdmaBts GetByBtsId(int btsId);

        CdmaBts GetByName(string name);

        List<CdmaBts> GetAllInUseList();

        List<CdmaBts> GetAllList(double west, double east, double south, double north);

        int SaveChanges();
    }
}