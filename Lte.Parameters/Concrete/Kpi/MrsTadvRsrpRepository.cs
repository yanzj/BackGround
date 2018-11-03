using System;
using System.Collections.Generic;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract.Kpi;
using Lte.Parameters.Entities.Kpi;
using MongoDB.Bson;

namespace Lte.Parameters.Concrete.Kpi
{
    public class MrsTadvRsrpRepository : MongoDbRepositoryBase<MrsTadvRsrpStat, ObjectId>, IMrsTadvRsrpRepository
    {
        public MrsTadvRsrpRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "mrs_TadvRsrp_combined";
        }

        public MrsTadvRsrpRepository() : this(new MyMongoProvider("ouyh"))
        {

        }

        public MrsTadvRsrpStat Get(string cellId, DateTime statDate)
        {
            return this.QueryLastDate(cellId, statDate);
        }

        public IEnumerable<MrsTadvRsrpStat> GetList(string cellId, DateTime begin, DateTime end)
        {
            return this.QueryLastDate(cellId, begin, end);
        }
    }
}