using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Wireless;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;
using System.Linq;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.College;
using Abp.EntityFramework.Entities.Infrastructure;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Distribution;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Region;
using System;

namespace Lte.Evaluations.DataService.College
{
    public class HotSpotService
    {
        private readonly IInfrastructureRepository _repository;
        private readonly ITownBoundaryRepository _boundaryRepository;

        public HotSpotService(IInfrastructureRepository repository, ITownBoundaryRepository boundaryRepository)
        {
            _repository = repository;
            _boundaryRepository = boundaryRepository;
        }

        public async Task<int> SaveBuildingHotSpot(HotSpotView dto)
        {
            var info =
                _repository.FirstOrDefault(
                    x => x.HotspotName == dto.HotspotName && x.InfrastructureType == InfrastructureType.HotSpot);
            if (info == null)
            {
                info = dto.MapTo<InfrastructureInfo>();
                info.InfrastructureType = InfrastructureType.HotSpot;
                await _repository.InsertAsync(info);
            }
            else
            {
                dto.MapTo(info);
            }
            
            return _repository.SaveChanges();
        }

        public IEnumerable<HotSpotView> QueryHotSpotViews()
        {
            var results =
                _repository.GetAllList(x => x.InfrastructureType == InfrastructureType.HotSpot)
                    .MapTo<IEnumerable<HotSpotView>>();
            return results;
        }

        public IEnumerable<HotSpotView> QueryHotSpotViews(string typeDescription)
        {
            var type = typeDescription.GetEnumType<HotspotType>();
            return
                _repository.GetAllList(
                    x =>
                        x.InfrastructureType == InfrastructureType.HotSpot &&
                        x.HotspotType == type).MapTo<IEnumerable<HotSpotView>>();
        }

        public HotSpotView QueryMarketView(string name)
        {
            var item = _repository.FirstOrDefault(x =>
                x.InfrastructureType == InfrastructureType.HotSpot && x.HotspotType == HotspotType.Marketing &&
                x.HotspotName == name);
            return item == null ? null : item.MapTo<HotSpotView>();
        }

        public HotSpotView QueryTransportationView(string name)
        {
            var item = _repository.FirstOrDefault(x =>
                x.InfrastructureType == InfrastructureType.HotSpot && x.HotspotType == HotspotType.Transportation &&
                x.HotspotName == name);
            return item == null ? null : item.MapTo<HotSpotView>();
        }

        public IEnumerable<HighwayView> QueryAllHighwayViews()
        {
            var highways = _repository.GetAllList(x => x.HotspotType == HotspotType.Highway);
            return highways.Select(info =>
            {
                var highway = info.MapTo<HighwayView>();
                var boundary = _boundaryRepository.FirstOrDefault(x => x.TownId == info.Id);
                if (boundary != null) boundary.MapTo(highway);
                highway.CoorList = boundary.CoorList();
                return highway;
            });
        }

        public InfrastructureInfo QueryHotSpot(string name, HotspotType type)
        {
            return
                _repository.FirstOrDefault(
                    x =>
                        x.InfrastructureType == InfrastructureType.HotSpot && x.HotspotName == name &&
                        x.HotspotType == type);
        }

    }
}