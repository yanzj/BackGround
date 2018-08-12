﻿using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Abstract.Switch;
using Lte.Parameters.Entities.Switch;

namespace Lte.Evaluations.DataService.Switch
{
    public class IntraFreqHoService
    {
        private readonly IUeEUtranMeasurementRepository _zteMeasurementRepository;
        private readonly ICellMeasGroupZteRepository _zteGroupRepository;
        private readonly IEUtranCellMeasurementZteRepository _zteCellGroupRepository;
        private readonly IIntraFreqHoGroupRepository _huaweiCellHoRepository;
        private readonly IIntraRatHoCommRepository _huaweiENodebHoRepository;
        private readonly ICellHuaweiMongoRepository _huaweiCellRepository;
        private readonly IENodebRepository _eNodebRepository;

        public IntraFreqHoService(IUeEUtranMeasurementRepository zteMeasurementRepository,
            ICellMeasGroupZteRepository zteGroupRepository, IEUtranCellMeasurementZteRepository zteCellGroupRepository,
            IIntraFreqHoGroupRepository huaweiCellHoRepository, IIntraRatHoCommRepository huaweiENodebHoRepository,
            ICellHuaweiMongoRepository huaweiCellRepository, IENodebRepository eNodebRepository)
        {
            _zteMeasurementRepository = zteMeasurementRepository;
            _zteGroupRepository = zteGroupRepository;
            _zteCellGroupRepository = zteCellGroupRepository;
            _huaweiCellHoRepository = huaweiCellHoRepository;
            _huaweiENodebHoRepository = huaweiENodebHoRepository;
            _huaweiCellRepository = huaweiCellRepository;
            _eNodebRepository = eNodebRepository;
        }

        private IMongoQuery<ENodebIntraFreqHoView> ConstructENodebQuery(int eNodebId)
        {
            var eNodeb = _eNodebRepository.FirstOrDefault(x => x.ENodebId == eNodebId);
            if (eNodeb == null) return null;
            return eNodeb.Factory == "华为"
                ? (IMongoQuery<ENodebIntraFreqHoView>) new HuaweiIntraFreqENodebMongoQuery(_huaweiENodebHoRepository, eNodebId)
                : new ZteIntraFreqENodebQuery(_zteGroupRepository, _zteMeasurementRepository, eNodebId);
        }

        public ENodebIntraFreqHoView QueryENodebHo(int eNodebId)
        {
            var query = ConstructENodebQuery(eNodebId);
            return query?.Query();
        }

        private IMongoQuery<CellIntraFreqHoView> ConstructCellQuery(int eNodebId, byte sectorId)
        {
            var eNodeb = _eNodebRepository.FirstOrDefault(x => x.ENodebId == eNodebId);
            if (eNodeb == null) return null;
            return eNodeb.Factory == "华为"
                ? (IMongoQuery<CellIntraFreqHoView>)
                    new HuaweiIntraFreqCellQuery(_huaweiCellRepository, _huaweiCellHoRepository, eNodebId, sectorId)
                : new ZteIntraFreqCellQuery(_zteMeasurementRepository, _zteGroupRepository, _zteCellGroupRepository, eNodebId,
                    sectorId);
        }

        public CellIntraFreqHoView QueryCellHo(int eNodebId, byte sectorId)
        {
            var query = ConstructCellQuery(eNodebId, sectorId);
            return query?.Query();
        }
    }

    internal class HuaweiIntraFreqENodebMongoQuery : HuaweiENodebMongoQuery<IntraRatHoComm, ENodebIntraFreqHoView, IIntraRatHoCommRepository>
    {
        public HuaweiIntraFreqENodebMongoQuery(IIntraRatHoCommRepository repository, int eNodebId) : base(repository, eNodebId)
        {
        }
    }

    internal class ZteIntraFreqENodebQuery : ZteGeneralENodebQuery<UeEUtranMeasurementZte, ENodebIntraFreqHoView>
    {
        private readonly ICellMeasGroupZteRepository _zteGroupRepository;
        private readonly IUeEUtranMeasurementRepository _zteMeasurementRepository;

        public ZteIntraFreqENodebQuery(ICellMeasGroupZteRepository zteGroupRepository,
            IUeEUtranMeasurementRepository zteMeasurementRepository, int eNodebId) 
            : base(eNodebId)
        {
            _zteGroupRepository = zteGroupRepository;
            _zteMeasurementRepository = zteMeasurementRepository;
        }

        protected override UeEUtranMeasurementZte QueryStat()
        {
            if (UeEUtranMeasurementZte.IntraFreqHoConfigId < 0)
            {
                var zteGroup = _zteGroupRepository.GetRecent(ENodebId);
                UeEUtranMeasurementZte.IntraFreqHoConfigId = zteGroup == null
                    ? 50
                    : int.Parse(zteGroup.intraFHOMeasCfg.Split(',')[0]);
            }

            return _zteMeasurementRepository.GetRecent(ENodebId, UeEUtranMeasurementZte.IntraFreqHoConfigId);
        }
    }
}
