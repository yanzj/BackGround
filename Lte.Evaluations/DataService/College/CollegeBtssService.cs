using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.EntityFramework.Entities.Cdma;
using Abp.EntityFramework.Entities.College;
using AutoMapper;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Distribution;
using Lte.MySqlFramework.Abstract.Cdma;
using Lte.MySqlFramework.Abstract.College;
using Lte.MySqlFramework.Abstract.Region;

namespace Lte.Evaluations.DataService.College
{
    public class CollegeBtssService
    {
        private readonly IHotSpotBtsRepository _repository;
        private readonly IBtsRepository _btsRepository;
        private readonly ITownRepository _townRepository;

        public CollegeBtssService(IHotSpotBtsRepository repository, IBtsRepository btsRepository, ITownRepository townRepository)
        {
            _repository = repository;
            _btsRepository = btsRepository;
            _townRepository = townRepository;
        }

        public IEnumerable<CdmaBtsView> Query(string collegeName)
        {
            var ids =
                _repository.GetAllList(
                    x =>
                        x.HotspotName == collegeName && x.HotspotType == HotspotType.College &&
                        x.InfrastructureType == InfrastructureType.CdmaBts);
            var btss = ids.Select(x => _btsRepository.GetByBtsId(x.BtsId)).Where(bts => bts != null).ToList();
            var views = Mapper.Map<List<CdmaBts>, List<CdmaBtsView>>(btss);
            views.ForEach(x =>
            {
                var town = _townRepository.Get(x.TownId);
                if (town != null)
                {
                    x.DistrictName = town.DistrictName;
                    x.TownName = town.TownName;
                }
            });
            return views;
        }

        public async Task<int> UpdateBtss(CollegeBtsIdsContainer container)
        {
            foreach (var btsId in container.BtsIds)
            {
                await _repository.InsertAsync(new HotSpotBtsId
                {
                    BtsId = btsId,
                    HotspotName = container.CollegeName,
                    HotspotType = HotspotType.College
                });
            }
            return _repository.SaveChanges();
        }
    }
}