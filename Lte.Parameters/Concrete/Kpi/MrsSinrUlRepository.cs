using System;
using System.Collections.Generic;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract.Kpi;
using Lte.Parameters.Entities.Kpi;
using MongoDB.Bson;

namespace Lte.Parameters.Concrete.Kpi
{
    public class MrsSinrUlRepository : MongoDbRepositoryBase<MrsSinrUlStat, ObjectId>, IMrsSinrUlRepository
    {
        public MrsSinrUlRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "mrs_SinrUL_combined";
        }
        public MrsSinrUlRepository() : this(new MyMongoProvider("ouyh"))
        {

        }

        public MrsSinrUlStat Get(string cellId, DateTime statDate)
        {
            return this.QueryLastDate(cellId, statDate);
        }

        public IEnumerable<MrsSinrUlStat> GetList(string cellId, DateTime begin, DateTime end)
        {
            return this.QueryLastDate(cellId, begin, end);
        }
    }
}