using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Abstract.Switch;
using Lte.Parameters.Entities.Switch;
using System.Collections.Generic;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.Parameters.Abstract.Infrastructure;

namespace Lte.Evaluations.DataService.Switch
{
    public class InterFreqHoService
    {
        private readonly IUeEUtranMeasurementRepository _zteMeasurementRepository;
        private readonly ICellMeasGroupZteRepository _zteGroupRepository;
        private readonly IEUtranCellMeasurementZteRepository _zteCellGroupRepository;
        private readonly IIntraRatHoCommRepository _huaweiENodebHoRepository;
        private readonly IInterFreqHoGroupRepository _huaweiCellHoRepository;
        private readonly ICellHuaweiMongoRepository _huaweiCellRepository;
        private readonly IENodebRepository _eNodebRepository;
        private readonly IEutranInterNFreqRepository _huaweiNFreqRepository;
        private readonly IIntraFreqHoGroupRepository _intraFreqHoGroupRepository;

        public InterFreqHoService(IUeEUtranMeasurementRepository zteMeasurementRepository,
            ICellMeasGroupZteRepository zteGroupRepository, IEUtranCellMeasurementZteRepository zteCellGroupRepository,
            IIntraRatHoCommRepository huaweiENodebHoRepository, IInterFreqHoGroupRepository huaweiCellHoRepository,
            ICellHuaweiMongoRepository huaweiCellRepository, IENodebRepository eNodebRepository,
            IEutranInterNFreqRepository huaweiNFreqRepository, IIntraFreqHoGroupRepository intraFreqHoGroupRepository)
        {
            _zteMeasurementRepository = zteMeasurementRepository;
            _zteGroupRepository = zteGroupRepository;
            _zteCellGroupRepository = zteCellGroupRepository;
            _huaweiENodebHoRepository = huaweiENodebHoRepository;
            _huaweiCellHoRepository = huaweiCellHoRepository;
            _huaweiCellRepository = huaweiCellRepository;
            _eNodebRepository = eNodebRepository;
            _huaweiNFreqRepository = huaweiNFreqRepository;
            _intraFreqHoGroupRepository = intraFreqHoGroupRepository;
        }

        private IMongoQuery<ENodebInterFreqHoView> ConstructENodebQuery(int eNodebId)
        {
            var eNodeb = _eNodebRepository.FirstOrDefault(x => x.ENodebId == eNodebId);
            if (eNodeb == null) return null;
            return eNodeb.Factory == "华为"
                ? (IMongoQuery<ENodebInterFreqHoView>)new HuaweiInterFreqENodebMongoQuery(_huaweiENodebHoRepository, eNodebId)
                : new ZteInterFreqENodebQuery(_zteMeasurementRepository, _zteGroupRepository, eNodebId);
        }

        public ENodebInterFreqHoView QueryENodebHo(int eNodebId)
        {
            var query = ConstructENodebQuery(eNodebId);
            return query?.Query();
        }

        private IMongoQuery<List<CellInterFreqHoView>> ConstructCellQuery(int eNodebId, byte sectorId)
        {
            var eNodeb = _eNodebRepository.FirstOrDefault(x => x.ENodebId == eNodebId);
            if (eNodeb == null) return null;
            return eNodeb.Factory == "华为"
                ? (IMongoQuery<List<CellInterFreqHoView>>)
                    new HuaweiInterFreqCellQuery(_huaweiCellHoRepository, _huaweiCellRepository, _huaweiNFreqRepository,
                        _intraFreqHoGroupRepository, eNodebId, sectorId)
                : new ZteInterFreqCellQuery(_zteMeasurementRepository, _zteGroupRepository, _zteCellGroupRepository,
                    eNodebId, sectorId);
        }

        public List<CellInterFreqHoView> QueryCellHo(int eNodebId, byte sectorId)
        {
            var query = ConstructCellQuery(eNodebId, sectorId);
            return query?.Query();
        }
    }

    internal class HuaweiInterFreqENodebMongoQuery : HuaweiENodebMongoQuery<IntraRatHoComm, ENodebInterFreqHoView, IIntraRatHoCommRepository>
    {
        public HuaweiInterFreqENodebMongoQuery(IIntraRatHoCommRepository repository, int eNodebId)
            : base(repository, eNodebId)
        {
        }
    }
}
