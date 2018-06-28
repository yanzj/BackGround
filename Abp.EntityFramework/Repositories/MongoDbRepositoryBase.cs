using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Dependency;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;

namespace Abp.EntityFramework.Repositories
{
    /// <summary>
    /// Implements IRepository for MongoDB.
    /// </summary>
    /// <typeparam name="TEntity">Type of the Entity for this repository</typeparam>
    public class MongoDbRepositoryBase<TEntity> : MongoDbRepositoryBase<TEntity, int>, IRepository<TEntity>
        where TEntity : class, IEntity<int>
    {
        public MongoDbRepositoryBase(IMongoDatabaseProvider databaseProvider)
            : base(databaseProvider)
        {
        }
    }

    /// <summary>
    /// Implements IRepository for MongoDB.
    /// </summary>
    /// <typeparam name="TEntity">Type of the Entity for this repository</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key of the entity</typeparam>
    public class MongoDbRepositoryBase<TEntity, TPrimaryKey> : AbpRepositoryBase<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        private readonly IMongoDatabaseProvider _databaseProvider;

        protected MongoDatabase Database => _databaseProvider.Database;

        protected string CollectionName { get; set; } = typeof (TEntity).Name;

        protected MongoCollection<TEntity> Collection => _databaseProvider.Database.GetCollection<TEntity>(CollectionName);

        public MongoDbRepositoryBase(IMongoDatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
        }

        public IQueryable<TEntity> QueryCursor(IMongoQuery query)
        {
            return Collection.Find(query).AsQueryable();
        }

        public TEntity QueryOne(IMongoQuery query)
        {
            return Collection.FindOne(query);
        }

        public override IQueryable<TEntity> GetAll()
        {
            return Collection.AsQueryable();
        }

        public override TEntity Get(TPrimaryKey id)
        {
            var query = MongoDB.Driver.Builders.Query<TEntity>.EQ(e => e.Id, id);
            return Collection.FindOne(query); //TODO: What if no entity with id?
        }

        public override TEntity FirstOrDefault(TPrimaryKey id)
        {
            var query = MongoDB.Driver.Builders.Query<TEntity>.EQ(e => e.Id, id);
            return Collection.FindOne(query); //TODO: What if no entity with id?
        }

        public override TEntity Insert(TEntity entity)
        {
            Collection.Insert(entity);
            return entity;
        }
        public override TEntity Update(TEntity entity)
        {
            Collection.Save(entity);
            return entity;
        }

        public override void Delete(TEntity entity)
        {
            Delete(entity.Id);
        }

        public override void Delete(TPrimaryKey id)
        {
            var query = MongoDB.Driver.Builders.Query<TEntity>.EQ(e => e.Id, id);
            Collection.Remove(query);
        }
    }
}