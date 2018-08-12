using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Entities.Channel;

namespace Lte.Evaluations.DataService.Switch
{
    public class UlOpenLoopPcService
    {
        private readonly IPowerControlULZteRepository _ztePcRepository;
        private readonly ICellUlpcCommRepository _huaweiPcRepository;
        private readonly ICellHuaweiMongoRepository _huaweiCellRepository;
        private readonly IENodebRepository _eNodebRepository;

        public UlOpenLoopPcService(IPowerControlULZteRepository ztePcRepository,
            ICellUlpcCommRepository huaweiPcRepository, ICellHuaweiMongoRepository huaweiCellRepository,
            IENodebRepository eNodebRepository)
        {
            _ztePcRepository = ztePcRepository;
            _huaweiPcRepository = huaweiPcRepository;
            _huaweiCellRepository = huaweiCellRepository;
            _eNodebRepository = eNodebRepository;
        }

        private IMongoQuery<CellOpenLoopPcView> ConstructCellQuery(int eNodebId, byte sectorId)
        {
            var eNodeb = _eNodebRepository.FirstOrDefault(x => x.ENodebId == eNodebId);
            if (eNodeb == null) return null;
            return eNodeb.Factory == "华为"
                ? (IMongoQuery<CellOpenLoopPcView>)
                    new HuaweiUlOlPcQuery(_huaweiCellRepository, _huaweiPcRepository, eNodebId, sectorId)
                : new ZteUlOlPcQuery(_ztePcRepository, eNodebId, sectorId);
        }

        public CellOpenLoopPcView Query(int eNodebId, byte sectorId)
        {
            var query = ConstructCellQuery(eNodebId, sectorId);
            return query?.Query();
        }
    }
}
