using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract.Kpi;
using Lte.Parameters.Entities.Kpi;
using MongoDB.Bson;

namespace Lte.Parameters.Concrete.Kpi
{
    public class PreciseMongoRepository : MongoDbRepositoryBase<PreciseMongo, ObjectId>, IPreciseMongoRepository
    {
        public PreciseMongoRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "precise_combined";
        }

        public PreciseMongoRepository() : this(new MyMongoProvider("ouyh"))
        {

        }

        public List<PreciseMongo> GetAllList(DateTime statDate)
        {
            var nextDate = statDate.AddDays(1);
            var query =
                MongoDB.Driver.Builders.Query<InterferenceMatrixMongo>.Where(e =>
                    e.StatDate >= statDate && e.StatDate < nextDate);
            return Collection.Find(query).AsQueryable().ToList();
        }
    }
}