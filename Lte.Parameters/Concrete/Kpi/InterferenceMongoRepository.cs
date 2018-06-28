using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract.Kpi;
using Lte.Parameters.Entities.Kpi;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lte.Parameters.Concrete.Kpi
{
    public class InterferenceMongoRepository : MongoDbRepositoryBase<InterferenceMatrixMongo, ObjectId>, IInterferenceMongoRepository
    {
        public InterferenceMongoRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "mro_combined";
        }

        public InterferenceMongoRepository() : this(new MyMongoProvider("ouyh"))
        {
            
        }

        public InterferenceMatrixMongo GetOne(string cellId)
        {
            var query =
                MongoDB.Driver.Builders.Query<InterferenceMatrixMongo>.Where(e => e.CellId == cellId);
            return Collection.FindOne(query);
        }
        
        public List<InterferenceMatrixMongo> GetList(string cellId)
        {
            var query =
                MongoDB.Driver.Builders.Query<InterferenceMatrixMongo>.Where(e => e.CellId == cellId);
            return Collection.Find(query).AsQueryable().ToList();
        }

        public async Task<List<InterferenceMatrixMongo>> GetListAsync(string cellId)
        {
            var query =
                MongoDB.Driver.Builders.Query<InterferenceMatrixMongo>.Where(e => e.CellId == cellId);
            return await Task.Run(() => Collection.Find(query).AsQueryable().ToList());
        }

        public InterferenceMatrixMongo Get(string cellId, short neighborPci, DateTime date)
        {
            var nextDate = date.AddDays(1);
            var query =
                MongoDB.Driver.Builders.Query<InterferenceMatrixMongo>.Where(
                    e => e.CellId == cellId && e.NeighborPci == neighborPci
                         && e.StatDate >= date && e.StatDate < nextDate);
            return Collection.FindOne(query);
        }

        public async Task<List<InterferenceMatrixMongo>> GetListAsync(string cellId, DateTime date)
        {
            var nextDate = date.AddDays(1);
            var query =
                MongoDB.Driver.Builders.Query<InterferenceMatrixMongo>.Where(
                    e => e.CellId == cellId && e.StatDate >= date && e.StatDate < nextDate);
            return await Task.Run(() => Collection.Find(query).AsQueryable().ToList());
        }
    }
}
