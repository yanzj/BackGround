using System.Collections.Generic;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.College;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract.College
{
    public interface ICollegeYearRepository : IRepository<CollegeYearInfo>, ISaveChanges
    {
        CollegeYearInfo GetByCollegeAndYear(int collegeId, int year);

        List<CollegeYearInfo> GetAllList(int year);
    }
}