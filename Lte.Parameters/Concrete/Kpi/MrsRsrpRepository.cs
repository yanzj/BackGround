using System;
using System.Collections.Generic;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.Mr;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract.Kpi;
using MongoDB.Bson;

namespace Lte.Parameters.Concrete.Kpi
{
    public class MrsRsrpRepository : MongoDbRepositoryBase<MrsRsrpStat, ObjectId>, IMrsRsrpRepository
    {
        public MrsRsrpRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "mrs_RSRP_combined";
        }

        public MrsRsrpRepository() : this(new MyMongoProvider("ouyh"))
        {

        }

        public MrsRsrpStat Get(string cellId, DateTime statDate)
        {
            return this.QueryLastDate(cellId, statDate);
        }

        public IEnumerable<MrsRsrpStat> GetList(string cellId, DateTime begin, DateTime end)
        {
            return this.QueryLastDate(cellId, begin, end);
        }
    }
}