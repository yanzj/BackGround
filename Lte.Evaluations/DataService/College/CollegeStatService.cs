using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.College;
using Lte.MySqlFramework.Abstract.College;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Entities.College;
using Lte.MySqlFramework.Support.View;

namespace Lte.Evaluations.DataService.College
{
    public class CollegeStatService
    {
        private readonly ICollegeRepository _repository;
        private readonly IInfrastructureRepository _infrastructureRepository;
        private readonly ICollegeYearRepository _yearRepository;
        private readonly IHotSpotENodebRepository _eNodebRepository;
        private readonly IHotSpotCellRepository _cellRepository;
        private readonly IHotSpotBtsRepository _btsRepository;
        private readonly IHotSpotCdmaCellRepository _cdmaCellRepository;
        private readonly ITownRepository _townRepository;

        public CollegeStatService(ICollegeRepository repository, IInfrastructureRepository infrastructureRepository,
            ICollegeYearRepository yearRepository, IHotSpotENodebRepository eNodebRepository, IHotSpotCellRepository cellRepository,
            IHotSpotBtsRepository btsRepository, IHotSpotCdmaCellRepository cdmaCellRepository,
            ITownRepository townRepository)
        {
            _repository = repository;
            _infrastructureRepository = infrastructureRepository;
            _yearRepository = yearRepository;
            _eNodebRepository = eNodebRepository;
            _cellRepository = cellRepository;
            _btsRepository = btsRepository;
            _cdmaCellRepository = cdmaCellRepository;
            _townRepository = townRepository;
        }
        
        public List<CollegeView> QueryInfos()
        {
            var views = _repository.GetAllList().MapTo<List<CollegeView>>();
            views.ForEach(view =>
            {
                var region = _repository.GetRegion(view.Id);
                view.RectangleRange = region?.RectangleRange;
                var town = _townRepository.FirstOrDefault(x => x.Id == view.TownId);
                if (town == null) return;
                view.City = town.CityName;
                view.District = town.DistrictName;
                view.Town = town.TownName;
            });
            return views;
        } 

        public IEnumerable<CollegeYearView> QueryInfo(int id)
        {
            var infos = _yearRepository.GetAllList(x => x.CollegeId == id);
            return !infos.Any()
                ? new List<CollegeYearView>()
                : infos.Select(x =>
                {
                    var stat = Mapper.Map<CollegeYearView>(x);
                    stat.Name = _repository.FirstOrDefault(c => c.Id == x.CollegeId)?.Name;
                    return stat;
                });
        }

        public CollegeView QueryInfo(string name)
        {
            var view = _repository.FirstOrDefault(x => x.Name == name).MapTo<CollegeView>();
            if (view == null) return null;
            var region = _repository.GetRegion(view.Id);
            view.RectangleRange = region?.RectangleRange;
            var town = _townRepository.FirstOrDefault(x => x.Id == view.TownId);
            if (town == null) return view;
            view.City = town.CityName;
            view.District = town.DistrictName;
            view.Town = town.TownName;
            return view;
        }

        public CollegeYearView QueryInfo(string name, int year)
        {
            var info = _repository.GetByName(name);
            if (info == null) return null;
            var view = _yearRepository.GetByCollegeAndYear(info.Id, year).MapTo<CollegeYearView>();
            view.Name = name;
            return view;
        }

        public IEnumerable<CollegeYearView> QueryYearViews(int year)
        {
            var infos = _yearRepository.GetAllList(year);
            return !infos.Any()
                ? new List<CollegeYearView>()
                : infos.Select(x =>
                {
                    var stat = Mapper.Map<CollegeYearView>(x);
                    stat.Name = _repository.FirstOrDefault(c => c.Id == x.CollegeId)?.Name;
                    return stat;
                });
        } 
        
        public async Task<int> SaveCollegeInfo(CollegeInfo info, long userId)
        {
            var item = _repository.GetByName(info.Name);
            if (item == null)
            {
                info.CreatorUserId = userId;
                await _repository.InsertAsync(info);
            }
            else
            {
                item.TownId = info.TownId;
                var areaItem = _repository.GetRegion(item.Id);
                if (areaItem == null)
                {
                    item.CollegeRegion = info.CollegeRegion;
                }
                else
                {
                    areaItem.Area = info.CollegeRegion.Area;
                    areaItem.Info = info.CollegeRegion.Info;
                    areaItem.RegionType = info.CollegeRegion.RegionType;
                }
            }
            return _repository.SaveChanges();
        } 

        public async Task<int> SaveYearInfo(CollegeYearInfo info)
        {
            var item = _yearRepository.GetByCollegeAndYear(info.CollegeId, info.Year);
            if (item != null) return 0;
            await _yearRepository.InsertAsync(info);
            return _yearRepository.SaveChanges();
        } 

        public CollegeStat QueryStat(int id, int year)
        {
            var info = _repository.Get(id);
            return info == null
                ? null
                : new CollegeStat(_repository, info, _yearRepository.GetByCollegeAndYear(id, year),
                    _infrastructureRepository, _eNodebRepository, _cellRepository, _btsRepository, _cdmaCellRepository);
        }

        public IEnumerable<CollegeStat> QueryStats(int year)
        {
            var infos = _repository.GetAllList();
            return !infos.Any()
                ? new List<CollegeStat>()
                : infos.Select(
                    x =>
                        new CollegeStat(_repository, x, _yearRepository.GetByCollegeAndYear(x.Id, year),
                            _infrastructureRepository, _eNodebRepository, _cellRepository, _btsRepository, _cdmaCellRepository))
                            .Where(x=>x.ExpectedSubscribers > 0);
        }

    }
}
