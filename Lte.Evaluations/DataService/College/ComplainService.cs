using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.Complain;
using Abp.EntityFramework.Repositories;
using AutoMapper;
using Lte.MySqlFramework.Abstract.Complain;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Support.View;

namespace Lte.Evaluations.DataService.College
{
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
        
        public List<ComplainDto> Query(DateTime begin, DateTime end, string text)
        {
            var items = _repository.GetAllList(x =>
                x.BeginTime >= begin && x.BeginTime < end && x.ComplainContents.Contains(text));
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
}