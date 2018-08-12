using System;
using System.Collections.Generic;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Complain;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Abstract.Complain;

namespace Lte.MySqlFramework.Concrete.Complain
{
    public class OnlineSustainRepository : EfRepositorySave<MySqlContext, OnlineSustain>, IOnlineSustainRepository
    {
        public OnlineSustainRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public OnlineSustain Match(OnlineSustainExcel stat)
        {
            return FirstOrDefault(x => x.SerialNumber == stat.SerialNumber);
        }

        public List<OnlineSustain> GetAllList(DateTime begin, DateTime end)
        {
            return GetAllList(x => x.BeginDate >= begin && x.BeginDate < end);
        }

        public List<OnlineSustain> GetAllList(int townId, DateTime begin, DateTime end)
        {
            return GetAllList(x => x.BeginDate >= begin && x.BeginDate < end && x.TownId == townId);
        }
    }
}