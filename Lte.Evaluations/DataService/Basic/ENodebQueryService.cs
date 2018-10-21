using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Regular;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Entities.Infrastructure;
using Abp.EntityFramework.Entities.Station;
using Lte.Domain.Common.Geo;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Abstract.Station;
using Lte.MySqlFramework.Entities.Infrastructure;

namespace Lte.Evaluations.DataService.Basic
{
    public class ENodebQueryService
    {
        private readonly ITownRepository _townRepository;
        private readonly IENodebRepository _eNodebRepository;
        private readonly IStationDictionaryRepository _stationDictionaryRepository;
        private readonly IDistributionRepository _distributionRepository;
        private readonly ITownBoundaryRepository _boundaryRepository;

        public ENodebQueryService(ITownRepository townRepository, IENodebRepository eNodebRepository,
            IStationDictionaryRepository stationDictionaryRepository,
            IDistributionRepository distributionRepository, ITownBoundaryRepository boundaryRepository)
        {
            _townRepository = townRepository;
            _eNodebRepository = eNodebRepository;
            _stationDictionaryRepository = stationDictionaryRepository;
            _distributionRepository = distributionRepository;
            _boundaryRepository = boundaryRepository;
        }

        public IEnumerable<ENodebView> GetByTownNames(string city, string district, string town)
        {
            var townItem = _townRepository.GetAllList()
                    .FirstOrDefault(x => x.CityName == city && x.DistrictName == district && x.TownName == town);
            if (townItem == null) return new List<ENodebView>();
            var list = _eNodebRepository.GetAllList(x => x.TownId == townItem.Id).MapTo<List<ENodebView>>();

            list.ForEach(x=>
            {
                x.CityName = city;
                x.DistrictName = district;
                x.TownName = town;
            });
            return list;
        }

        public IEnumerable<ENodebView> GetByTownArea(string city, string district, string town)
        {
            var list = new List<ENodebView>();
            var townItem = _townRepository.GetAllList()
                    .FirstOrDefault(x => x.CityName == city && x.DistrictName == district && x.TownName == town);
            if (townItem == null) return list;
            var boudary = _boundaryRepository.FirstOrDefault(x => x.TownId == townItem.Id);
            if (boudary == null) return list;
            foreach (var townEntity in _townRepository.GetAllList(x=>x.CityName==city&&x.DistrictName==district&&x.TownName!=town))
            {
                var views =
                    _eNodebRepository.GetAllList(x => x.TownId == townEntity.Id)
                        .Where(x => boudary.IsInTownRange(x))
                        .MapTo<List<ENodebView>>();
                views.ForEach(x =>
                {
                    x.CityName = city;
                    x.DistrictName = district;
                    x.TownName = townEntity.TownName;
                    x.TownId = townItem.Id;
                });
                list.AddRange(views);
            }
            return list;
        }

        public IEnumerable<ENodebView> GetByTownNamesInUse(string city, string district, string town)
        {
            return GetByTownNames(city, district, town).Where(x => x.IsInUse);
        }

        public List<ENodeb> GetENodebsByDistrict(string city, string district)
        {
            var towns = _townRepository.GetAllList().Where(x => x.CityName == city && x.DistrictName == district);
            return (from town in towns
                join eNodeb in _eNodebRepository.GetAllList() on town.Id equals eNodeb.TownId
                select eNodeb).ToList();
        }
        
        public IEnumerable<ENodebView> GetByDistrictNames(string city, string district)
        {
            var list = GetENodebsByDistrict(city, district).ToList().MapTo<List<ENodebView>>();
            list.ForEach(x =>
            {
                x.CityName = city;
                x.DistrictName = district;
            });
            return list;
        } 
        
        public IEnumerable<string> GetENodebNames(string city)
        {
            var towns = _townRepository.GetAllList().Where(x => x.CityName == city);
            var list = (from town in towns
                join eNodeb in _eNodebRepository.GetAllList().MapTo<List<ENodebView>>() on town.Id equals eNodeb.TownId
                select eNodeb.Name).Distinct().ToList();
            return list;
        } 

        public IEnumerable<ENodebView> GetByGeneralName(string name)
        {
            var items =
                _eNodebRepository.GetAllList().Where(x => x.Name.Contains(name.Trim())).ToArray();
            if (items.Any()) return items.Select(item => ENodebView.ConstructView(item, _townRepository));
            var eNodebId = name.Trim().ConvertToInt(0);
            if (eNodebId > 0)
            {
                items = _eNodebRepository.GetAllList(x => x.ENodebId == eNodebId).ToArray();
                if (items.Any()) return items.Select(item => ENodebView.ConstructView(item, _townRepository));
            }
            items =
                _eNodebRepository.GetAllList()
                    .Where(
                        x =>
                            (!string.IsNullOrEmpty(x.Address) && x.Address.Contains(name.Trim()))  
                            || (!string.IsNullOrEmpty(x.PlanNum) && x.PlanNum.Contains(name.Trim())))
                    .ToArray();
            return items.Any()
                ? items.Select(item => ENodebView.ConstructView(item, _townRepository))
                : new List<ENodebView>();
        }

        public IEnumerable<ENodebView> GetByGeneralNameInUse(string name)
        {
            return GetByGeneralName(name).Where(x => x.IsInUse);
        } 

        public ENodebView GetByENodebId(int eNodebId)
        {
            var item = _eNodebRepository.FirstOrDefault(x => x.ENodebId == eNodebId);
            return ENodebView.ConstructView(item, _townRepository);
        }
        
        public ENodebView GetByPlanNum(string planNum)
        {
            var item = _eNodebRepository.FirstOrDefault(x => x.PlanNum == planNum);
            return ENodebView.ConstructView(item, _townRepository);
        }
        
        public IEnumerable<ENodebView> QueryENodebViews(double west, double east, double south, double north)
        {
            var eNodebs = _eNodebRepository.GetAllList(x => x.Longtitute >= west && x.Longtitute <= east
                && x.Lattitute >= south && x.Lattitute <= north);
            return eNodebs.Any()
                ? eNodebs.Select(e => ENodebView.ConstructView(e, _townRepository))
                : new List<ENodebView>();
        }

        public IEnumerable<IndoorDistribution> QueryDistributionSystems(double west, double east, double south,
            double north)
        {
            return
                _distributionRepository.GetAllList(
                    x => x.Longtitute >= west && x.Longtitute <= east && x.Lattitute >= south && x.Lattitute <= north);
        }

        public IEnumerable<ENodebView> QueryENodebViews(ENodebRangeContainer container)
        {
            var eNodebs =
                _eNodebRepository.GetAllList(x => x.Longtitute >= container.West && x.Longtitute <= container.East
                                                  && x.Lattitute >= container.South && x.Lattitute <= container.North)
                    .Where(x => x.IsInUse)
                    .ToList();
            var excludedENodebs = from eNodeb in eNodebs
                join id in container.ExcludedIds on eNodeb.ENodebId equals id
                select eNodeb;
            eNodebs = eNodebs.Except(excludedENodebs).ToList();
            return eNodebs.Any()
                ? eNodebs.Select(e => ENodebView.ConstructView(e, _townRepository))
                : new List<ENodebView>();
        }

        public IEnumerable<IndoorDistribution> QueryDistributionSystems(string district)
        {
            return _distributionRepository.GetAllList(x => x.StationDistrict == district);
        }

        public ENodeb UpdateTownInfo(int eNodebId, int townId)
        {
            var eNodeb = _eNodebRepository.FirstOrDefault(x => x.ENodebId == eNodebId);
            if (eNodeb == null) return null;
            eNodeb.TownId = townId;
            _eNodebRepository.SaveChanges();
            return eNodeb;
        }
    }
}
