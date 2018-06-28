using AutoMapper;
using Lte.MySqlFramework.Abstract;
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

    internal class HuaweiUlOlPcQuery : HuaweiCellMongoQuery<CellOpenLoopPcView>
    {
        private readonly ICellUlpcCommRepository _huaweiPcRepository;

        public HuaweiUlOlPcQuery(ICellHuaweiMongoRepository huaweiCellRepository,
            ICellUlpcCommRepository huaweiPcRepository, int eNodebId, byte sectorId)
            : base(huaweiCellRepository, eNodebId, sectorId)
        {
            _huaweiPcRepository = huaweiPcRepository;
        }

        protected override CellOpenLoopPcView QueryByLocalCellId(int localCellId)
        {
            var stat = _huaweiPcRepository.GetRecent(ENodebId, localCellId);
            var view = Mapper.Map<CellOpenLoopPcView>(stat);
            if (view != null)
            {
                view.ENodebId = ENodebId;
                view.SectorId = SectorId;
            }
            return view;
        }
    }

    internal class ZteUlOlPcQuery : IMongoQuery<CellOpenLoopPcView>
    {
        private readonly IPowerControlULZteRepository _ztePcRepository;
        private readonly int _eNodebId;
        private readonly byte _sectorId;

        public ZteUlOlPcQuery(IPowerControlULZteRepository zteRepository, int eNodebId, byte sectorId)
        {
            _ztePcRepository = zteRepository;
            _eNodebId = eNodebId;
            _sectorId = sectorId;
        }

        public CellOpenLoopPcView Query()
        {
            var stat = _ztePcRepository.GetRecent(_eNodebId, _sectorId);
            var view = Mapper.Map<CellOpenLoopPcView>(stat);
            if (view != null)
            {
                view.ENodebId = _eNodebId;
                view.SectorId = _sectorId;
            }
            return view;
        }
    }
}
