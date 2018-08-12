using System.Collections.Generic;
using System.Linq;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Distribution;
using Lte.Domain.Regular;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Entities;

namespace Lte.Evaluations.DataService.Basic
{

    public class TownBoundaryService
    {
        private readonly ITownBoundaryRepository _boundaryRepository;
        private readonly ITownRepository _repository;
        private readonly IInfrastructureRepository _infrastructureRepository;

        public TownBoundaryService(ITownRepository repository, ITownBoundaryRepository boundaryRepository,
            IInfrastructureRepository infrastructureRepository)
        {
            _repository = repository;
            _boundaryRepository = boundaryRepository;
            _infrastructureRepository = infrastructureRepository;
        }

        public IEnumerable<TownBoundaryView> GetTownBoundaryViews(string city, string district, string town)
        {
            var item =
                _repository.GetAllList()
                    .FirstOrDefault(x => x.CityName == city && x.DistrictName == district && x.TownName == town);
            if (item == null) return new List<TownBoundaryView>();
            var coors = _boundaryRepository.GetAllList(x => x.TownId == item.Id);
            return coors.Select(coor => new TownBoundaryView
            {
                Town = town,
                BoundaryGeoPoints = coor.CoorList()
            }).ToList();
        }

        public IEnumerable<TownBoundariesView> GetDistrictBoundaryViews(string city, string district)
        {
            var items =
                _repository.GetAllList(x => x.CityName == city && x.DistrictName == district);

            return items.Select(town => new TownBoundariesView
            {
                Town = town.TownName,
                BoundaryGeoPoints = _boundaryRepository.GetAllList(x => x.TownId == town.Id)
                .Select(coor => coor.CoorList())
            });
        }

        public IEnumerable<AreaBoundaryView> GetAreaBoundaryViews()
        {
            var areaDefs = _infrastructureRepository.GetAllList(x => x.HotspotType == HotspotType.AreaDef);
            return areaDefs.Select(area =>
            {
                var coor = _boundaryRepository.FirstOrDefault(x => x.TownId == area.Id);
                if (coor == null) return null;
                return new AreaBoundaryView
                {
                    AreaName = area.HotspotName,
                    AreaType = area.InfrastructureType.GetEnumDescription(),
                    BoundaryGeoPoints = coor.CoorList()
                };
            }).Where(x => x != null);
        }

        public bool IsInTownBoundaries(double longtitute, double lattitute, string city, string district, string town)
        {
            var item = _repository.GetAllList()
                    .FirstOrDefault(x => x.CityName == city && x.DistrictName == district && x.TownName == town);
            if (item == null) return false;
            var point = new GeoPoint(longtitute, lattitute);
            return _boundaryRepository.GetAllList(x => x.TownId == item.Id).IsInTownRange(point);
        }
    }
}
