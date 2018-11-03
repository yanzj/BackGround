using System;
using System.Collections.Generic;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract.Kpi;
using Lte.Parameters.Entities.Kpi;
using MongoDB.Bson;

namespace Lte.Parameters.Concrete.Kpi
{
    public class MrsTadvRepository : MongoDbRepositoryBase<MrsTadvStat, ObjectId>, IMrsTadvRepository
    {
        public MrsTadvRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "mrs_Tadv_combined";
        }

        public MrsTadvRepository() : this(new MyMongoProvider("ouyh"))
        {

        }

        public MrsTadvStat Get(string cellId, DateTime statDate)
        {
            return this.QueryLastDate(cellId, statDate);
        }

        public IEnumerable<MrsTadvStat> GetList(string cellId, DateTime begin, DateTime end)
        {
            return this.QueryLastDate(cellId, begin, end);
        }
    }
}