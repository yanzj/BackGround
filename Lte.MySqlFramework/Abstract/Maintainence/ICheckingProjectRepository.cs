﻿using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Maintainence;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;

namespace Lte.MySqlFramework.Abstract.Maintainence
{
    public interface ICheckingProjectRepository : IRepository<CheckingProject>, ISaveChanges, 
        IMatchRepository<CheckingProject, CheckingProjectExcel>
    {
    }
}
