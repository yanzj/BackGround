using System;
using System.Collections.Generic;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
{
    public class QciZteRepository : EfRepositorySave<MySqlContext, QciZte>, IQciZteRepository
    {
        public QciZteRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public QciZte Match(QciZte stat)
        {
            return FirstOrDefault(x =>
                x.StatTime == stat.StatTime && x.ENodebId == stat.ENodebId &&
                x.SectorId == stat.SectorId);
        }

        public List<QciZte> FilterTopList(DateTime begin, DateTime end)
        {
            return GetAllList(
                x => x.StatTime >= begin && x.StatTime < end
                     && x.Cqi0Times + x.Cqi1Times + x.Cqi2Times + x.Cqi3Times + x.Cqi4Times
                     + x.Cqi5Times + x.Cqi6Times >
                     3000000);
        }
    }
}