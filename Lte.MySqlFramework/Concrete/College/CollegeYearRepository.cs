using System.Collections.Generic;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities.College;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.College;

namespace Lte.MySqlFramework.Concrete.College
{
    public class CollegeYearRepository : EfRepositorySave<MySqlContext, CollegeYearInfo>, ICollegeYearRepository
    {
        public CollegeYearRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public CollegeYearInfo GetByCollegeAndYear(int collegeId, int year)
        {
            return FirstOrDefault(x => x.CollegeId == collegeId && x.Year == year);
        }

        public List<CollegeYearInfo> GetAllList(int year)
        {
            return GetAllList(x => x.Year == year);
        }
    }
}