using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.College;
using Abp.EntityFramework.Entities.Maintainence;
using Abp.EntityFramework.Entities.Test;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Region;

namespace Lte.MySqlFramework.Concrete
{
    public class CollegeRepository : EfRepositorySave<MySqlContext, CollegeInfo>, ICollegeRepository
    {
        public CollegeRegion GetRegion(int id)
        {
            return GetAll().Select(x => new {x.Id, x.CollegeRegion}).FirstOrDefault(x => x.Id == id)?.CollegeRegion;
        }

        public CollegeInfo GetByName(string name)
        {
            return FirstOrDefault(x => x.Name == name);
        }

        public RectangleRange GetRange(string name)
        {
            var college = GetByName(name);
            return college == null ? null : GetRegion(college.Id)?.RectangleRange;
        }
        
        public CollegeRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }

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
    
    public class EFCollege3GTestRepository : EfRepositorySave<MySqlContext, College3GTestResults>, ICollege3GTestRepository
    {
        public List<College3GTestResults> GetAllList(DateTime begin, DateTime end)
        {
            return GetAllList(x => x.TestTime >= begin && x.TestTime < end);
        }
        
        public EFCollege3GTestRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }

    public class EFCollege4GTestRepository : EfRepositorySave<MySqlContext, College4GTestResults>, ICollege4GTestRepository
    {
        public EFCollege4GTestRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }

    public class UserLoginSessionInfoRepository : EfRepositorySave<MySqlContext, UserLoginSessionInfo>,
        IUserLoginSessionInfoRepository
    {
        public UserLoginSessionInfoRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }

    public class ModuleUsageRecordRepository : EfRepositorySave<MySqlContext, ModuleUsageRecord>,
        IModuleUsageRecordRepository
    {
        public ModuleUsageRecordRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
