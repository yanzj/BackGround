using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
{
    public class ZhangshangyouQualityRepository : EfRepositorySave<MySqlContext, ZhangshangyouQuality>,
        IZhangshangyouQualityRepository
    {
        public ZhangshangyouQualityRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(
            dbContextProvider)
        {
        }

        public ZhangshangyouQuality Match(ZhangshangyouQualityCsv stat)
        {
            return FirstOrDefault(x => x.SerialNumber == stat.SerialNumber);
        }
    }
}
