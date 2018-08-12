using System.Collections.Generic;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Complain;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;

namespace Lte.MySqlFramework.Abstract
{
    public interface IComplainProcessRepository
        : IRepository<ComplainProcess>,
            IMatchRepository<ComplainProcess, OnlineSustainExcel>,
            ISaveChanges
    {
        List<ComplainProcess> GetAllList(string serialNumber);
    }
}