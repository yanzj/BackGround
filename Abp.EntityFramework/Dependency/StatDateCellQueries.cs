using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Region;
using MongoDB.Bson;

namespace Abp.EntityFramework.Dependency
{
    public static class StatDateCellQueries
    {
        public static IEnumerable<TRegionDateSpanView> QueryDateSpanViews<TRegionDateSpanView, TDistrictView, TTownView>
            (this IEnumerable<TTownView> townViews, Func<TTownView, TDistrictView> generateDistrictViewFunc)
            where TRegionDateSpanView : class, IRegionDateSpanView<TDistrictView, TTownView>, new()
            where TDistrictView : ICityDistrict
            where TTownView : class, ICityDistrictTown, IStatTime, new()
        {
            return from view in townViews
                group view by view.StatTime
                into g
                select new TRegionDateSpanView
                {
                    StatDate = g.Key,
                    TownViews = g.Select(x => x),
                    DistrictViews = g.Select(x => x).Merge(generateDistrictViewFunc)
                };
        }

        public static IEnumerable<TRegionDateSpanView> QueryDateDateViews<TRegionDateSpanView, TDistrictView, TTownView>
            (this IEnumerable<TTownView> townViews, Func<TTownView, TDistrictView> generateDistrictViewFunc)
            where TRegionDateSpanView : class, IRegionDateSpanView<TDistrictView, TTownView>, new()
            where TDistrictView : ICityDistrict
            where TTownView : class, ICityDistrictTown, IStatDate, new()
        {
            return from view in townViews
                group view by view.StatDate
                into g
                select new TRegionDateSpanView
                {
                    StatDate = g.Key,
                    TownViews = g.Select(x => x),
                    DistrictViews = g.Select(x => x).Merge(generateDistrictViewFunc)
                };
        }

        public static TRegionDateView QueryRegionDateView<TRegionDateView, TDistrictView, TTownView>(
            this List<TTownView> townViews, DateTime initialDate,
            Func<TTownView, TDistrictView> generateDistrictViewFunc)
            where TRegionDateView : class, IRegionDateSpanView<TDistrictView, TTownView>, new()
            where TDistrictView : ICityDistrict
            where TTownView : class, ICityDistrictTown, IStatTime, new()
        {
            return new TRegionDateView
            {
                StatDate = townViews.Any() ? townViews.First().StatTime : initialDate,
                TownViews = townViews,
                DistrictViews = townViews.Merge(generateDistrictViewFunc)
            };
        }

        public static TRegionDateView QueryRegionDateDateView<TRegionDateView, TDistrictView, TTownView>(
            this List<TTownView> townViews, DateTime initialDate,
            Func<TTownView, TDistrictView> generateDistrictViewFunc)
            where TRegionDateView : class, IRegionDateSpanView<TDistrictView, TTownView>, new()
            where TDistrictView : ICityDistrict
            where TTownView : class, ICityDistrictTown, IStatDate, new()
        {
            return new TRegionDateView
            {
                StatDate = townViews.Any() ? townViews.First().StatDate : initialDate,
                TownViews = townViews,
                DistrictViews = townViews.Merge(generateDistrictViewFunc)
            };
        }

        public static TEntity QueryLastDate<TEntity>(this MongoDbRepositoryBase<TEntity, ObjectId> repository,
            string cellId, DateTime statDate)
            where TEntity : class, IStatDateCell, IEntity<ObjectId>
        {
            var nextDate = statDate.AddDays(1);
            var query =
                MongoDB.Driver.Builders.Query<TEntity>.Where(
                    e => e.CellId == cellId && e.StatDate >= statDate && e.StatDate < nextDate);
            return repository.QueryOne(query);
        }

        public static IEnumerable<TEntity> QueryLastDate<TEntity>(this MongoDbRepositoryBase<TEntity, ObjectId> repository,
            string cellId, DateTime begin, DateTime end)
            where TEntity : class, IStatDateCell, IEntity<ObjectId>
        {
            var query =
                MongoDB.Driver.Builders.Query<TEntity>.Where(
                    e => e.CellId == cellId && e.StatDate >= begin && e.StatDate < end);
            return repository.QueryCursor(query);
        }

        public static async Task<IEnumerable<TEntity>> QueryAsync<TRepository, TEntity>(this TRepository repository,
            DateTime initialDate, Func<TRepository, DateTime, DateTime, Task<List<TEntity>>> queryDateFunc)
            where TEntity : Entity, IStatDate
            where TRepository : IRepository<TEntity>
        {
            var beginDate = initialDate;
            while (beginDate > initialDate.AddDays(-200))
            {
                var endDate = initialDate.AddDays(1);
                var result = await queryDateFunc(repository, beginDate, endDate);
                if (result.Any()) return result;
                beginDate = beginDate.AddDays(-1);
            }

            return new List<TEntity>();
        }

        public static IEnumerable<TEntity> QueryLastDate<TRepository, TEntity>(this TRepository repository,
            DateTime initialDate, Func<TRepository, DateTime, DateTime, List<TEntity>> queryDateFunc)
            where TEntity : Entity, IStatTime
            where TRepository : IRepository<TEntity>
        {
            var beginDate = initialDate;
            while (beginDate > initialDate.AddDays(-200))
            {
                var endDate = initialDate.AddDays(1);
                var result = queryDateFunc(repository, beginDate, endDate);
                if (result.Any()) return result;
                beginDate = beginDate.AddDays(-1);
            }

            return new List<TEntity>();
        }

        public static IEnumerable<TEntity> QueryDate<TRepository, TEntity>(this TRepository repository,
            DateTime initialDate, Func<TRepository, DateTime, DateTime, List<TEntity>> queryDateFunc)
            where TEntity : Entity, IStatDate
            where TRepository : IRepository<TEntity>
        {
            var beginDate = initialDate;
            while (beginDate > initialDate.AddDays(-200))
            {
                var endDate = initialDate.AddDays(1);
                var result = queryDateFunc(repository, beginDate, endDate);
                if (result.Any()) return result;
                beginDate = beginDate.AddDays(-1);
            }

            return new List<TEntity>();
        }

        public static IEnumerable<TEntity> QueryBeginDate<TRepository, TEntity>(this TRepository repository,
            DateTime initialDate, Func<TRepository, DateTime, DateTime, List<TEntity>> queryDateFunc)
            where TEntity : Entity, IBeginDate
            where TRepository : IRepository<TEntity>
        {
            var beginDate = initialDate;
            while (beginDate > initialDate.AddDays(-200))
            {
                var endDate = initialDate.AddDays(1);
                var result = queryDateFunc(repository, beginDate, endDate);
                if (result.Any()) return result;
                beginDate = beginDate.AddDays(-1);
            }

            return new List<TEntity>();
        }
    }
}