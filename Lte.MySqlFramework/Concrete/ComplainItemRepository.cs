using System;
using System.Collections.Generic;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
{
    public class ComplainItemRepository : EfRepositorySave<MySqlContext, ComplainItem>,
        IComplainItemRepository
    {
        public ComplainItemRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public ComplainItem Match(ComplainExcel stat)
        {
            return Get(stat.SerialNumber);
        }

        public List<ComplainItem> GetAllList(DateTime begin, DateTime end)
        {
            return GetAllList(x => x.BeginTime >= begin && x.BeginTime < end);
        }

        public List<ComplainItem> GetAllList(int townId, DateTime begin, DateTime end)
        {
            return GetAllList(x => x.BeginTime >= begin && x.BeginTime < end && x.TownId == townId);
        }
        
        public ComplainItem Get(string serialNumber)
        {
            return FirstOrDefault(x => x.SerialNumber == serialNumber);
        }

        public ComplainItem Match(ComplainDto stat)
        {
            return FirstOrDefault(x => x.SerialNumber == stat.SerialNumber);
        }

        public ComplainItem Match(ComplainSupplyExcel stat)
        {
            return FirstOrDefault(x => x.SerialNumber == stat.SerialNumber);
        }
    }
}