using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Repositories;
using AutoMapper;
using Lte.Domain.Common.Wireless;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities;
using Lte.MySqlFramework.Support;

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
            where TRepository : IDateSpanQuery<TEntity>
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
            where TRepository : IDateSpanQuery<TEntity>
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
            where TRepository : IDateSpanQuery<TEntity>
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

        public static async Task<Tuple<List<string>, List<int>>> QueryCounts<TService, TItem>(this TService service,
            DateTime today)
            where TService : IDateSpanService<TItem>
        {
            var months = new List<string>();
            var counts = new List<int>();
            for (var i = 0; i < 12; i++)
            {
                var date = today.AddMonths(i - 12);
                var begin = new DateTime(date.Year, date.Month, 1);
                months.Add(begin.ToString("yyyy-MM"));
                counts.Add(await service.QueryCount(begin, begin.AddMonths(1)));
            }
            return new Tuple<List<string>, List<int>>(months, counts);
        }
    }

    public class EmergencyFiberService
    {
        private readonly IEmergencyFiberWorkItemRepository _repository;

        public EmergencyFiberService(IEmergencyFiberWorkItemRepository repository)
        {
            _repository = repository;
        }

        public EmergencyFiberWorkItem Create(EmergencyFiberWorkItem item)
        {
            item.BeginDate = DateTime.Now;
            return _repository.ImportOne(item);
        }

        public async Task<int> Finish(EmergencyFiberWorkItem item)
        {
            var info =
                _repository.FirstOrDefault(
                    x => x.EmergencyId == item.EmergencyId && x.WorkItemNumber == item.WorkItemNumber);
            info.FinishDate = DateTime.Now;
            await _repository.UpdateAsync(info);
            return _repository.SaveChanges();
        }

        public List<EmergencyFiberWorkItem> Query(int id)
        {
            return _repository.GetAllList(id);
        }
    }

    public interface IDateSpanService<TItem>
    {
        List<TItem> QueryItems(DateTime begin, DateTime end);

        Task<int> QueryCount(DateTime begin, DateTime end);
    }

    public class ComplainService : IDateSpanService<ComplainItem>
    {
        private readonly IComplainItemRepository _repository;
        private readonly ITownRepository _townRepository;

        public ComplainService(IComplainItemRepository repository, ITownRepository townRepository)
        {
            _repository = repository;
            _townRepository = townRepository;
        }

        public IEnumerable<ComplainPositionDto> QueryPositionDtos(DateTime begin, DateTime end)
        {
            return
                _repository.GetAllList(begin, end).Where(x => x.TownId == 0).MapTo<IEnumerable<ComplainPositionDto>>();
        }

        public List<ComplainItem> QueryItems(DateTime begin, DateTime end)
        {
            return _repository.GetAllList(begin, end);
        }

        public List<ComplainDto> Query(DateTime begin, DateTime end)
        {
            var items = _repository.GetAllList(begin, end);
            return Mapper.Map<List<ComplainItem>, List<ComplainDto>>(items);
        }

        public List<ComplainDto> QueryDate(DateTime begin, DateTime end, string district)
        {
            if (district=="佛山")
                return Query(begin.Date, end.Date);
            var items =
                _repository.GetAllList(
                    x => x.BeginDate > begin.Date && x.BeginDate <= end.Date && x.District == district);
            return Mapper.Map<List<ComplainItem>, List<ComplainDto>>(items);
        } 
        
        public async Task<int> UpdateTown(ComplainPositionDto dto)
        {
            var item = _repository.Get(dto.SerialNumber);
            if (item == null) return 0;
            var town = _townRepository.GetAllList()
                .FirstOrDefault(x => x.CityName == dto.City && x.DistrictName == dto.District && x.TownName == dto.Town);
            if (town == null) return 0;
            item.TownId = town.Id;
            await _repository.UpdateAsync(item);
            return _repository.SaveChanges();
        }

        public async Task<int> QueryCount(DateTime begin, DateTime end)
        {
            return await _repository.CountAsync(x => x.BeginTime >= begin && x.BeginTime < end);
        }

        public ComplainDto Query(string serialNumber)
        {
            var item = _repository.FirstOrDefault(x => x.SerialNumber == serialNumber);
            return Mapper.Map<ComplainItem, ComplainDto>(item);
        }
        
        public async Task<int> UpdateAsync(ComplainDto dto)
        {
            return await _repository.UpdateOne<IComplainItemRepository, ComplainItem, ComplainDto>(dto);
        }

        public IEnumerable<ComplainDto> QueryList(DateTime today, string city, string district)
        {
            var items =
                this.QueryItems<ComplainService, ComplainItem>(today);
            return items.QueryItemViews<ComplainItem, ComplainDto>(city, district, _townRepository);
        }

        public DistrictComplainDateView QueryLastDateStat(DateTime initialDate)
        {
            var stats = _repository.QueryBeginDate(initialDate.Date,
                (repository, beginDate, endDate) => _repository.GetAllList(beginDate, endDate)).ToList();
            if (!stats.Any())
                return new DistrictComplainDateView
                {
                    StatDate = initialDate.Date,
                    DistrictComplainViews = new List<DistrictComplainView>()
                };
            return DistrictComplainDateView.GenerateDistrictComplainDateView(stats, _townRepository.GetFoshanDistricts());
        }

        public List<DistrictComplainView> QueryDateSpanStats(DateTime begin, DateTime end)
        {
            var stats = _repository.GetAllList(begin, end);
            if (!stats.Any()) return new List<DistrictComplainView>();
            return DistrictComplainDateView.GenerateDistrictComplainList(stats, _townRepository.GetFoshanDistricts());
        }
    }

    public class BranchDemandService : IDateSpanService<BranchDemand>
    {
        private readonly IBranchDemandRepository _repository;

        public BranchDemandService(IBranchDemandRepository repository)
        {
            _repository = repository;
        }

        public List<BranchDemand> QueryItems(DateTime begin, DateTime end)
        {
            return _repository.GetAllList(begin, end);
        }

        public async Task<int> QueryCount(DateTime begin, DateTime end)
        {
            return await _repository.CountAsync(x => x.BeginDate >= begin && x.BeginDate < end);
        }

        public List<BranchDemandDto> QueryList(DateTime begin, DateTime end)
        {
            var items = _repository.GetAllList(begin, end);
            return Mapper.Map<List<BranchDemand>, List<BranchDemandDto>>(items);
        }
    }

    public class OnlineSustainService : IDateSpanService<OnlineSustain>
    {
        private readonly IOnlineSustainRepository _repository;
        private readonly ITownRepository _townRepository;

        public OnlineSustainService(IOnlineSustainRepository repository, ITownRepository townRepository)
        {
            _repository = repository;
            _townRepository = townRepository;
        }

        public List<OnlineSustain> QueryItems(DateTime begin, DateTime end)
        {
            return _repository.GetAllList(begin, end);
        }

        public async Task<int> QueryCount(DateTime begin, DateTime end)
        {
            return await _repository.CountAsync(x => x.BeginDate >= begin && x.BeginDate < end);
        }

        public List<OnlineSustainDto> QueryList(DateTime begin, DateTime end)
        {
            var items = _repository.GetAllList(begin, end);
            var views = items.MapTo<List<OnlineSustainDto>>();
            views.ForEach(x =>
            {
                var town = _townRepository.Get(x.TownId);
                x.City = town?.CityName;
                x.District = town?.DistrictName;
                x.Town = town?.TownName;
            });
            return views;
        }

        public IEnumerable<OnlineSustainDto> QueryList(DateTime today)
        {
            var views =
                this.QueryItems<OnlineSustainService, OnlineSustain>(today).MapTo<List<OnlineSustainDto>>();
            views.ForEach(x =>
            {
                var town = _townRepository.Get(x.TownId);
                x.City = town?.CityName;
                x.District = town?.DistrictName;
                x.Town = town?.TownName;
            });
            return views;
        }

        public IEnumerable<OnlineSustainDto> QueryList(DateTime today, string city, string district)
        {
            var items =
                this.QueryItems<OnlineSustainService, OnlineSustain>(today);
            return items.QueryItemViews<OnlineSustain, OnlineSustainDto>(city, district, _townRepository);
        }

        public IEnumerable<OnlineSustainDto> QueryList(double west, double east, double south, double north)
        {
            var items = _repository.GetAllList(x => x.Longtitute >= west && x.Longtitute <= east
                                                    && x.Lattitute >= south && x.Lattitute <= north);
            var views = items.MapTo<List<OnlineSustainDto>>();
            views.ForEach(x =>
            {
                var town = _townRepository.Get(x.TownId);
                x.City = town?.CityName;
                x.District = town?.DistrictName;
                x.Town = town?.TownName;
            });
            return views;
        } 
    }

    public class MicroAmplifierService
    {
        private readonly IMicroItemRepository _repository;
        private readonly IMicroAddressRepository _addressRepository;

        public MicroAmplifierService(IMicroItemRepository repository, IMicroAddressRepository addressRepository)
        {
            _repository = repository;
            _addressRepository = addressRepository;
        }

        public IEnumerable<MicroAmplifierView> QueryMicroAmplifierViews()
        {
            var list =
                _addressRepository.GetAllList(
                    x => x.Longtitute > 112 && x.Longtitute < 114 && x.Lattitute > 22 && x.Lattitute < 24)
                    .MapTo<List<MicroAmplifierView>>();
            list.ForEach(item =>
            {
                item.MicroItems = _repository.GetAllList(x => x.AddressNumber == item.AddressNumber);
            });
            return list;
        }
    }
}
