using System;
using System.Collections.Generic;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Test;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Entities;

namespace Lte.MySqlFramework.Abstract
{
    public interface ICollege3GTestRepository : IRepository<College3GTestResults>, ISaveChanges
    {
        List<College3GTestResults> GetAllList(DateTime begin, DateTime end);
    }
}