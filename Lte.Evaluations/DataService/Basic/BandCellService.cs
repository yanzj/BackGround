using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Infrastructure;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Cell;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;

namespace Lte.Evaluations.DataService.Basic
{
    public class BandCellService
    {
        private readonly ICellRepository _repository;
        private readonly IENodebRepository _eNodebRepository;
        private readonly ITownRepository _townRepository;

        public BandCellService(ICellRepository repository, IENodebRepository eNodebRepository,
            ITownRepository townRepository)
        {
            _repository = repository;
            _eNodebRepository = eNodebRepository;
            _townRepository = townRepository;
        }

        public List<Cell> GetHuaweiCellsByBandType(FrequencyBandType frequency)
        {
            switch (frequency)
            {
                case FrequencyBandType.Band2100:
                    return
                        _repository.GetAllList(
                            x =>
                                x.BandClass == 1 && 
                                ((x.ENodebId >= 499712 && x.ENodebId < 501248) || (x.ENodebId >= 552448 &&
                                x.ENodebId < 552960) || (x.ENodebId >= 870144 && x.ENodebId < 870460)));
                case FrequencyBandType.Band1800:
                    return _repository.GetAllList(
                            x =>
                                x.BandClass == 3 &&
                                ((x.ENodebId >= 499712 && x.ENodebId < 501248) || (x.ENodebId >= 552448 &&
                                x.ENodebId < 552960) || (x.ENodebId >= 870144 && x.ENodebId < 870460)));
                default:
                    return _repository.GetAllList(
                            x =>
                                x.BandClass == 5 &&
                                ((x.ENodebId >= 499712 && x.ENodebId < 501248) || (x.ENodebId >= 552448 &&
                                x.ENodebId < 552960) || (x.ENodebId >= 870144 && x.ENodebId < 870460)));
            }
        }

        public List<ENodeb> GetDistrictENodebs(string city, string district)
        {
            var towns = _townRepository.GetAllList(x => x.CityName == city && x.DistrictName == district);
            var eNodebs = _eNodebRepository.GetAllList();
            return (from t in towns join e in eNodebs on t.Id equals e.TownId select e).ToList();
        }

        public List<ENodeb> GetTownENodebs(string city, string district, string town)
        {
            var towns = _townRepository
                .GetAllList(x => x.CityName == city && x.DistrictName == district && x.TownName == town);
            var eNodebs = _eNodebRepository.GetAllList();
            return (from t in towns join e in eNodebs on t.Id equals e.TownId select e).ToList();
        }

        public List<Cell> GetOutdoorTownCells(string city, string district, string town, FrequencyBandType frequency)
        {
            var towns = _townRepository
                .GetAllList(x => x.CityName == city && x.DistrictName == district && x.TownName == town);
            var eNodebs = _eNodebRepository.GetAllList();
            List<Cell> cells;
            switch (frequency)
            {
                case FrequencyBandType.All:
                    cells = _repository.GetAllList(x => x.IsOutdoor);
                    break;
                case FrequencyBandType.Band2100:
                    cells = _repository.GetAllList(x => x.IsOutdoor && x.BandClass == 1);
                    break;
                case FrequencyBandType.Band1800:
                    cells = _repository.GetAllList(x => x.IsOutdoor && x.BandClass == 3);
                    break;
                default:
                    cells = _repository.GetAllList(x => x.IsOutdoor && x.BandClass == 5);
                    break;
            }
            var subENodebs =
            (from t in towns
                join e in eNodebs on t.Id equals e.TownId
                select e
            ).ToList();
            return (from e in subENodebs join c in cells on e.ENodebId equals c.ENodebId select c).ToList();
        }

        public List<Cell> GetIndoorTownCells(string city, string district, string town)
        {
            var towns = _townRepository
                .GetAllList(x => x.CityName == city && x.DistrictName == district && x.TownName == town);
            var eNodebs = _eNodebRepository.GetAllList();
            var cells = _repository.GetAllList(x => !x.IsOutdoor);
            var subENodebs =
            (from t in towns
             join e in eNodebs on t.Id equals e.TownId
             select e
            ).ToList();
            return (from e in subENodebs join c in cells on e.ENodebId equals c.ENodebId select c).ToList();
        }

    }
}
