using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Region;
using Lte.Domain.Common.Wireless;
using Lte.MySqlFramework.Abstract;

namespace Lte.Evaluations.DataService.Basic
{
    public class TownQueryService
    {
        private readonly ITownRepository _repository;
        private readonly IOptimzeRegionRepository _optimzeRegionRepository;
        private readonly IENodebRepository _eNodebRepository;
        private readonly IBtsRepository _btsRepository;
        private readonly ICellRepository _cellRepository;
        private readonly ICdmaCellRepository _cdmaCellRepository;

        public TownQueryService(ITownRepository repository, IOptimzeRegionRepository optimzeRegionRepository,
            IENodebRepository eNodebRepositroy, IBtsRepository btsRepository,
            ICellRepository cellRepository, ICdmaCellRepository cdmaCellRepository)
        {
            _repository = repository;
            _optimzeRegionRepository = optimzeRegionRepository;
            _eNodebRepository = eNodebRepositroy;
            _btsRepository = btsRepository;
            _cellRepository = cellRepository;
            _cdmaCellRepository = cdmaCellRepository;
        }

        public List<string> GetCities()
        {
            return _repository.GetAll().Select(x => x.CityName).Distinct().ToList();
        }

        public IEnumerable<string> GetRegions(string city)
        {
            return _optimzeRegionRepository.GetAllList().Where(x => x.City == city)
                .Select(x => x.Region).Distinct().OrderBy(x => x);
        }

        public List<string> GetDistricts(string city)
        {
            return _repository.GetAllList().Where(x => x.CityName == city)
                .Select(x => x.DistrictName).Distinct().ToList();
        }

        public List<DistrictIndoorStat> QueryDistrictIndoorStats(string city)
        {
            var eNodebTownIds = _eNodebRepository.GetAllInUseList().Select(x => new
            {
                x.TownId,
                x.ENodebId
            });
            var allCells = _cellRepository.GetAllInUseList();
            return (from district in GetDistricts(city)
                let townList = _repository.GetAllList().Where(x => x.CityName == city && x.DistrictName == district)
                let cells = (from t in townList
                    join e in eNodebTownIds on t.Id equals e.TownId
                    join c in allCells on e.ENodebId equals c.ENodebId
                    select c)
                select new DistrictIndoorStat
                {
                    District = district,
                    TotalIndoorCells = cells.Count(x => !x.IsOutdoor),
                    TotalOutdoorCells = cells.Count(x => x.IsOutdoor)
                }).ToList();
        }

        public List<DistrictBandClassStat> QueryDistrictBandStats(string city)
        {
            var eNodebTownIds = _eNodebRepository.GetAllList().Select(x => new
            {
                x.TownId,
                x.ENodebId
            });
            var allCells = _cellRepository.GetAllInUseList();
            return (from district in GetDistricts(city)
                let townList = _repository.GetAllList().Where(x => x.CityName == city && x.DistrictName == district)
                let cells = (from t in townList
                    join e in eNodebTownIds on t.Id equals e.TownId
                    join c in allCells on e.ENodebId equals c.ENodebId
                    select c)
                select new DistrictBandClassStat
                {
                    District = district,
                    Band1Cells = cells.Count(x => x.BandClass == 1),
                    Band3Cells = cells.Count(x => x.BandClass == 3),
                    Band5Cells = cells.Count(x => x.BandClass == 5 && x.Frequency != 2506),
                    NbIotCells = cells.Count(x => x.BandClass == 5 && x.Frequency == 2506),
                    Band41Cells = cells.Count(x => x.BandClass == 41)
                }).ToList();
        }

        public List<DistrictStat> QueryDistrictStats(string city)
        {
            var eNodebTownIds = _eNodebRepository.GetAllInUseList().Select(x => new
            {
                x.TownId,
                x.ENodebId
            });
            var btsTownIds = _btsRepository.GetAllInUseList().Select(x => new
            {
                x.TownId,
                x.BtsId
            });
            var cellENodebs = _cellRepository.GetAllInUseList();
            var cdmaCellBtsIds = _cdmaCellRepository.GetAllInUseList().Select(x => x.BtsId);
            return (from district in GetDistricts(city)
                let townList = _repository.GetAllList().Where(x => x.CityName == city && x.DistrictName == district)
                let eNodebs = (from t in townList join e in eNodebTownIds on t.Id equals e.TownId select e)
                let btss = (from t in townList join b in btsTownIds on t.Id equals b.TownId select b)
                let cells = (from t in townList
                    join e in eNodebTownIds on t.Id equals e.TownId
                    join c in cellENodebs on e.ENodebId equals c.ENodebId
                    select c)
                let cdmaCells =
                (from t in townList
                    join b in btsTownIds on t.Id equals b.TownId
                    join c in cdmaCellBtsIds on b.BtsId equals c
                    select c)
                select new DistrictStat
                {
                    District = district,
                    TotalLteENodebs = eNodebs.Count(),
                    TotalLteCells = cells.Count(x => x.Frequency != 2506),
                    Lte800Cells = cells.Count(x => x.BandClass == 5 && x.Frequency != 2506),
                    TotalNbIotCells = cells.Count(x => x.Frequency == 2506),
                    TotalCdmaBts = btss.Count(),
                    TotalCdmaCells = cdmaCells.Count()
                }).ToList();
        }

        public List<string> GetTowns(string city, string district)
        {
            return
                _repository.GetAllList()
                    .Where(x => x.CityName == city && x.DistrictName == district)
                    .Select(x => x.TownName)
                    .Distinct()
                    .ToList();
        }

        public List<TownStat> QueryTownStats(string city, string district)
        {
            var eNodebTownIds = _eNodebRepository.GetAllInUseList().Select(x => new
            {
                x.TownId,
                x.ENodebId
            });
            var btsTownIds = _btsRepository.GetAllInUseList().Select(x => new
            {
                x.TownId,
                x.BtsId
            });
            var cellENodebs = _cellRepository.GetAllInUseList();
            var cdmaCellBtsIds = _cdmaCellRepository.GetAllInUseList().Select(x => x.BtsId);
            return (from town in _repository.GetAllList().Where(x => x.CityName == city && x.DistrictName == district)
                let eNodebs = eNodebTownIds.Where(x => x.TownId == town.Id)
                let btss = btsTownIds.Where(x => x.TownId == town.Id)
                let cells = (from e in eNodebs join c in cellENodebs on e.ENodebId equals c.ENodebId select c)
                let cdmaCells = (from b in btss join c in cdmaCellBtsIds on b.BtsId equals c select c)
                select new TownStat
                {
                    Town = town.TownName,
                    TotalLteENodebs = eNodebs.Count(),
                    TotalLteCells = cells.Count(x => x.Frequency != 2506),
                    Lte800Cells = cells.Count(x => x.BandClass == 5 && x.Frequency != 2506),
                    TotalNbIotCells = cells.Count(x => x.Frequency == 2506),
                    TotalCdmaBts = btss.Count(),
                    TotalCdmaCells = cdmaCells.Count()
                }).ToList();
        }

        public LteCellStat QueryLteCellStat(string city, string district, string town, bool isIndoor)
        {
            var townId = _repository.GetAllList().FirstOrDefault(
                x => x.CityName == city && x.DistrictName == district && x.TownName == town)?.Id;
            if (townId == null) return null;
            var eNodebs = _eNodebRepository.GetAllList(x => x.TownId == townId);
            var cells =
            (from cell in _cellRepository.GetAllList(x => x.IsOutdoor != isIndoor)
                join eNodeb in eNodebs on cell.ENodebId equals eNodeb.ENodebId
                select cell).ToList();
            return new LteCellStat
            {
                Lte1800Cells = cells.Count(x => x.BandClass == 3),
                Lte2100Cells = cells.Count(x => x.BandClass == 1),
                Lte800Cells = cells.Count(x => x.BandClass == 5 && x.Frequency != 2506),
                TotalNbIotCells = cells.Count(x => x.Frequency == 2506),
                Lte2600Cells = cells.Count(x => x.BandClass == 41)
            };
        }

        public TownView GetTown(string city, string district, string town)
        {
            return
                _repository.GetAllList().FirstOrDefault(
                        x => x.CityName == city && x.DistrictName == district && x.TownName == town)
                    .MapTo<TownView>();
        }
        
        public Tuple<string, string, string> GetTownNamesByENodebId(int eNodebId)
        {
            var item = _eNodebRepository.FirstOrDefault(x => x.ENodebId == eNodebId);
            var town = item == null ? null : _repository.Get(item.TownId);
            return town == null
                ? null
                : new Tuple<string, string, string>(town.CityName, town.DistrictName, town.TownName);
        }

    }
}