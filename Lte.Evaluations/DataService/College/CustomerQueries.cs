using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Repositories;
using AutoMapper;
using Lte.Domain.Common.Wireless;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.EntityFramework.Entities.Region;
using Lte.MySqlFramework.Abstract.Region;

namespace Lte.Evaluations.DataService.College
{
    public static class CustomerQueries
    {
        public static int DumpItem<TRepository, TEntity, TItem>(this TRepository repository, TItem dto, 
            ITownRepository townRepository)
            where TRepository : IRepository<TEntity>, IMatchRepository<TEntity, TItem>, ISaveChanges
            where TEntity : Entity
            where TItem : IDistrictTown, ITownId
        {
            dto.TownId =
                townRepository.QueryTown(dto.District, dto.Town)?.Id ?? 1;
            return
                repository
                    .ImportOne<TRepository, TEntity, TItem>(dto);
        }

        public static List<TDto> Query<TRepository, TEntity, TDto>(this TRepository repository,
            ITownRepository townRepository, DateTime begin, DateTime end)
            where TRepository : IDateSpanRepository<TEntity>
            where TDto : IDistrictTown, ITownId
        {
            var results = Mapper.Map<List<TEntity>, List<TDto>>(repository.GetAllList(begin, end));
            var towns = townRepository.GetAllList();
            results.ForEach(x =>
            {
                UpdateTown(towns, x);
            });
            return results;
        }

        public static List<TDto> Query<TRepository, TEntity, TDto>(this TRepository repository,
            List<Town> towns, List<TEntity> list)
            where TRepository : IDateSpanRepository<TEntity>
            where TDto : IDistrictTown, ITownId
        {
            var results = Mapper.Map<List<TEntity>, List<TDto>>(list);
            results.ForEach(x =>
            {
                UpdateTown(towns, x);
            });
            return results;
        }

        public static TDto Query<TRepository, TEntity, TDto>(this TRepository repository,
            List<Town> towns, int id)
            where TRepository : IRepository<TEntity>
            where TEntity: Entity
            where TDto : IDistrictTown, ITownId
        {
            var result = Mapper.Map<TEntity, TDto>(repository.Get(id));
            UpdateTown(towns, result);
            return result;
        }

        private static void UpdateTown<TDto>(List<Town> towns, TDto x) 
            where TDto : IDistrictTown, ITownId
        {
            if (x.TownId <= 0)
            {
                x.District = "未知";
                x.Town = "未知";
            }
            else
            {
                var town = towns.FirstOrDefault(t => t.Id == x.TownId);
                if (town != null)
                {
                    x.District = town.DistrictName;
                    x.Town = town.TownName;
                }
            }
        }

        public static List<TDto> Query<TRepository, TEntity, TDto>(this TRepository repository,
            ITownRepository townRepository, string district, string town, DateTime begin, DateTime end)
            where TRepository : IDateSpanRepository<TEntity>
            where TDto : IDistrictTown
        {
            var townId = townRepository.QueryTown(district, town)?.Id ?? 1;
            var results = Mapper.Map<List<TEntity>, List<TDto>>(repository.GetAllList(townId, begin, end));
            results.ForEach(x =>
            {
                x.District = district;
                x.Town = town;
            });
            return results;
        }

        public static Tuple<IEnumerable<string>, IEnumerable<int>> Query<TService, TItem>(this TService service, 
            DateTime date, Func<TItem, DateTime> queryDate)
            where TService : IDateSpanService<TItem>
        {
            var begin = new DateTime(date.Year, date.Month, 1);
            var start = new DateTime(date.Year, date.Month, 1);
            var end = new DateTime(date.Year, date.Month + 1, 1);
            var items = service.QueryItems(begin, end);
            var dateStrings = new List<string>();
            var counts = new List<int>();
            while (begin < end)
            {
                dateStrings.Add(begin.ToShortDateString());
                counts.Add(items.Count(x => queryDate(x) > start && queryDate(x) < begin.AddDays(1)));
                begin = begin.AddDays(1);
            }
            return new Tuple<IEnumerable<string>, IEnumerable<int>>(dateStrings, counts);
        }

        public static IEnumerable<TItem> QueryItems<TService, TItem>(this TService service, DateTime today)
            where TService : IDateSpanService<TItem>
        {
            var begin = new DateTime(today.Year, today.Month, 1).AddMonths(-1);
            var end = today;
            var result = service.QueryItems(begin, end);
            if (!result.Any())
            {
                begin = begin.AddMonths(-1);
                result= service.QueryItems(begin, end);
            }
            return result;
        }
        
        public static IEnumerable<TView> QueryItemViews<TItem, TView>(this IEnumerable<TItem> items, string city,
            string district, ITownRepository repository)
            where TItem: ITownId
            where TView: ICityDistrictTown
        {
            var views = district != "其他"
                ? from item in items
                join town in repository.GetAllList(x => x.CityName == city && x.DistrictName == district) on
                item.TownId equals town.Id
                select new
                {
                    Town = town,
                    Item = item
                }
                : items.Where(x => x.TownId == 0).Select(item => new
                {
                    Town = new Town {TownName = "其他"},
                    Item = item
                });
            return views.Select(x =>
            {
                var view = x.Item.MapTo<TView>();
                view.City = city;
                view.District = district;
                view.Town = x.Town.TownName;
                return view;
            });
        }

        public static async Task<int> QueryCount<TService, TItem>(this TService service, DateTime today)
            where TService : IDateSpanService<TItem>
        {
            var begin = new DateTime(today.Year, today.Month, 1).AddMonths(-1);
            var end = new DateTime(today.Year, today.Month, 1);
            return await service.QueryCount(begin, end);
        }

        public static async Task<int> QueryThisMonthCount<TService, TItem>(this TService service, DateTime today)
            where TService : IDateSpanService<TItem>
        {
            var begin = new DateTime(today.Year, today.Month, 1);
            return await service.QueryCount(begin, today);
        }
        
        public static async Task<Tuple<List<string>, List<int>, List<int>>> QueryCounts<TService, TItem, TService1, TItem1>(
            this TService service, TService1 service1, DateTime today)
            where TService : IDateSpanService<TItem>
            where TService1 : IDateSpanService<TItem1>
        {
            var months = new List<string>();
            var counts = new List<int>();
            var counts1 = new List<int>();
            for (var i = 0; i < 12; i++)
            {
                var date = today.AddMonths(i - 12);
                var begin = new DateTime(date.Year, date.Month, 1);
                months.Add(begin.ToString("yyyy-MM"));
                counts.Add(await service.QueryCount(begin, begin.AddMonths(1)));
                counts1.Add(await service1.QueryCount(begin, begin.AddMonths(1)));
            }

            return new Tuple<List<string>, List<int>, List<int>>(months, counts, counts1);
        }
    }
}
