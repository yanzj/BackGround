using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Complain;
using Lte.MySqlFramework.Abstract.Complain;
using Lte.MySqlFramework.Abstract.Region;

namespace Lte.Evaluations.DataService.College
{
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
}