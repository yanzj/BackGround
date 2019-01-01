using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.EntityFramework.Entities.College;
using Abp.EntityFramework.Entities.Infrastructure;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Distribution;
using Lte.MySqlFramework.Abstract.College;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Entities.Infrastructure;

namespace Lte.Evaluations.DataService.College
{
    public class CollegeENodebService
    {
        private readonly IHotSpotENodebRepository _repository;
        private readonly IENodebRepository _eNodebRepository;

        public CollegeENodebService(IHotSpotENodebRepository repository, IENodebRepository eNodebRepository)
        {
            _repository = repository;
            _eNodebRepository = eNodebRepository;
        }

        public IEnumerable<ENodebView> Query(string collegeName)
        {
            var ids =
                _repository.GetAllList(
                    x =>
                        x.HotspotName == collegeName && x.HotspotType == HotspotType.College &&
                        x.InfrastructureType == InfrastructureType.ENodeb);
            return (from id in ids
                select _eNodebRepository.FirstOrDefault(x => x.ENodebId == id.ENodebId)
                into eNodeb
                where eNodeb != null
                select Mapper.Map<ENodeb, ENodebView>(eNodeb)).ToList();
        }

        public async Task<int> UpdateENodebs(CollegeENodebIdsContainer container)
        {
            foreach (var eNodebId in container.ENodebIds)
            {
                await _repository.InsertAsync(new HotSpotENodebId
                {
                    ENodebId = eNodebId,
                    HotspotType = HotspotType.College,
                    HotspotName = container.CollegeName
                });
            }
            return _repository.SaveChanges();
        } 
    }
}
