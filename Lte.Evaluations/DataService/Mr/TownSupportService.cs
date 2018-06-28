using System.Collections.Generic;
using System.Linq;
using Lte.Domain.Common.Geo;
using Lte.MySqlFramework.Abstract;

namespace Lte.Evaluations.DataService.Mr
{
    public class TownSupportService
    {
        private readonly ITownRepository _townRepository;
        private readonly ITownBoundaryRepository _boundaryRepository;

        public TownSupportService(ITownRepository townRepository, ITownBoundaryRepository boundaryRepository)
        {
            _townRepository = townRepository;
            _boundaryRepository = boundaryRepository;
        }
        
        public List<List<GeoPoint>> QueryTownBoundaries(string district, string town)
        {
            var townItem = _townRepository.QueryTown(district, town);
            if (townItem == null) return null;
            return _boundaryRepository.GetAllList(x => x.TownId == townItem.Id).Select(x => x.CoorList()).ToList();
        }
        
    }
}