using AutoMapper;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Abstract.Switch;
using Lte.Parameters.Entities.Switch;
using System.Collections.Generic;
using System.Linq;
using Lte.MySqlFramework.Abstract;
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

    internal class ZteInterFreqENodebQuery : IMongoQuery<ENodebInterFreqHoView>
    {
        private readonly IUeEUtranMeasurementRepository _zteMeasurementRepository;
        private readonly ICellMeasGroupZteRepository _zteGroupRepository;
        private readonly int _eNodebId;

        public ZteInterFreqENodebQuery(IUeEUtranMeasurementRepository zteMeasurementRepository,
            ICellMeasGroupZteRepository zteGroupRepository, int eNodebId)
        {
            _zteMeasurementRepository = zteMeasurementRepository;
            _zteGroupRepository = zteGroupRepository;
            _eNodebId = eNodebId;
        }

        public ENodebInterFreqHoView Query()
        {
            var view = new ENodebInterFreqHoView
            {
                ENodebId = _eNodebId
            };
            var zteGroup = _zteGroupRepository.GetRecent(_eNodebId);
            var a1ConfigId = zteGroup == null ? 20 : int.Parse(zteGroup.closedInterFMeasCfg.Split(',')[0]);
            var a1Congig = _zteMeasurementRepository.GetRecent(_eNodebId, a1ConfigId);
            view.InterFreqHoA1TrigQuan = view.A3InterFreqHoA1TrigQuan = a1Congig?.triggerQuantity ?? 0;
            var a2ConfigId = zteGroup == null ? 30 : int.Parse(zteGroup.openInterFMeasCfg.Split(',')[0]);
            var a2Config = _zteMeasurementRepository.GetRecent(_eNodebId, a2ConfigId);
            view.InterFreqHoA1TrigQuan = view.A3InterFreqHoA2TrigQuan = a2Config?.triggerQuantity ?? 0;
            var hoEventId = zteGroup == null ? 70 : int.Parse(zteGroup.interFHOMeasCfg.Split(',')[0]);
            var hoEvent = _zteMeasurementRepository.GetRecent(_eNodebId, hoEventId);
            view.InterFreqHoA4TrigQuan = hoEvent?.triggerQuantity ?? 0;
            view.InterFreqHoA4RprtQuan = hoEvent?.reportQuantity ?? 0;
            view.InterFreqHoRprtInterval = hoEvent?.reportInterval ?? 0;
            return view;
        }
    }

    internal class HuaweiInterFreqCellQuery : HuaweiCellMongoQuery<List<CellInterFreqHoView>>
    {
        private readonly IInterFreqHoGroupRepository _huaweiCellHoRepository;
        private readonly IEutranInterNFreqRepository _huaweiNFreqRepository;
        private readonly IIntraFreqHoGroupRepository _intraFreqHoGroupRepository;

        public HuaweiInterFreqCellQuery(IInterFreqHoGroupRepository huaweiCellHoRepository,
            ICellHuaweiMongoRepository huaweiCellRepository, IEutranInterNFreqRepository huaweiNFreqRepository,
            IIntraFreqHoGroupRepository intraFreqHoGroupRepository, int eNodebId, byte sectorId)
            : base(huaweiCellRepository, eNodebId, sectorId)
        {
            _huaweiCellHoRepository = huaweiCellHoRepository;
            _huaweiNFreqRepository = huaweiNFreqRepository;
            _intraFreqHoGroupRepository = intraFreqHoGroupRepository;
        }

        protected override List<CellInterFreqHoView> QueryByLocalCellId(int localCellId)
        {

            var results = new List<CellInterFreqHoView>();
            var nFreqs = _huaweiNFreqRepository.GetRecentList(ENodebId, localCellId);
            var hoGroup = _huaweiCellHoRepository.GetRecent(ENodebId, localCellId);
            if (hoGroup == null) return null;
            var intraFreqConfig = _intraFreqHoGroupRepository.GetRecent(ENodebId, localCellId);
            foreach (var config in nFreqs.Select(freq => new CellInterFreqHoView
            {
                Earfcn = freq.DlEarfcn,
                InterFreqHoEventType = freq.InterFreqHoEventType,
                InterFreqEventA1 = Mapper.Map<InterFreqHoGroup, InterFreqEventA1>(hoGroup),
                InterFreqEventA2 = Mapper.Map<InterFreqHoGroup, InterFreqEventA2>(hoGroup),
                InterFreqEventA3 = Mapper.Map<InterFreqHoGroup, InterFreqEventA3>(hoGroup),
                InterFreqEventA4 = Mapper.Map<InterFreqHoGroup, InterFreqEventA4>(hoGroup),
                InterFreqEventA5 = Mapper.Map<InterFreqHoGroup, InterFreqEventA5>(hoGroup)
            }))
            {
                if (intraFreqConfig != null)
                {
                    config.InterFreqEventA3.Hysteresis = intraFreqConfig.IntraFreqHoA3Hyst;
                    config.InterFreqEventA3.TimeToTrigger = intraFreqConfig.IntraFreqHoA3TimeToTrig;
                }
                switch (config.InterFreqHoEventType)
                {
                    case 0:
                        config.InterFreqEventA1.ThresholdOfRsrp = hoGroup.A3InterFreqHoA1ThdRsrp;
                        config.InterFreqEventA2.ThresholdOfRsrp = hoGroup.A3InterFreqHoA2ThdRsrp;
                        config.InterFreqEventA1.ThresholdOfRsrq = hoGroup.A3InterFreqHoA1ThdRsrq;
                        config.InterFreqEventA2.ThresholdOfRsrq = hoGroup.A3InterFreqHoA2ThdRsrq;
                        break;
                    default:
                        config.InterFreqEventA1.ThresholdOfRsrp = hoGroup.InterFreqHoA1ThdRsrp;
                        config.InterFreqEventA2.ThresholdOfRsrp = hoGroup.InterFreqHoA2ThdRsrp;
                        config.InterFreqEventA1.ThresholdOfRsrq = hoGroup.InterFreqHoA1ThdRsrq;
                        config.InterFreqEventA2.ThresholdOfRsrq = hoGroup.InterFreqHoA2ThdRsrq;
                        break;
                }
                results.Add(config);
            }
            return results;
        }
    }

    internal class ZteInterFreqCellQuery : IMongoQuery<List<CellInterFreqHoView>>
    {
        private readonly IUeEUtranMeasurementRepository _zteMeasurementRepository;
        private readonly ICellMeasGroupZteRepository _zteGroupRepository;
        private readonly IEUtranCellMeasurementZteRepository _zteCellGroupRepository;
        private readonly int _eNodebId;
        private readonly byte _sectorId;

        public ZteInterFreqCellQuery(IUeEUtranMeasurementRepository zteMeasurementRepository,
            ICellMeasGroupZteRepository zteGroupRepository, IEUtranCellMeasurementZteRepository zteCellGroupRepository,
            int eNodebId, byte sectorId)
        {
            _zteMeasurementRepository = zteMeasurementRepository;
            _zteGroupRepository = zteGroupRepository;
            _zteCellGroupRepository = zteCellGroupRepository;
            _eNodebId = eNodebId;
            _sectorId = sectorId;
        }

        public List<CellInterFreqHoView> Query()
        {
            var zteGroup = _zteGroupRepository.GetRecent(_eNodebId);
            var zteCellGroup = _zteCellGroupRepository.GetRecentList(_eNodebId, _sectorId);
            var idA1 = zteGroup == null ? 10 : int.Parse(zteGroup.closedInterFMeasCfg.Split(',')[0]);
            var idA2 = zteGroup == null ? 20 : int.Parse(zteGroup.openInterFMeasCfg.Split(',')[0]);
            var meas = _zteMeasurementRepository.GetRecent(_eNodebId, idA1);
            var eventA1= Mapper.Map<UeEUtranMeasurementZte, InterFreqEventA1>(meas);
            meas = _zteMeasurementRepository.GetRecent(_eNodebId, idA2);
            var eventA2 = Mapper.Map<UeEUtranMeasurementZte, InterFreqEventA2>(meas);
            if (zteCellGroup.Count > 1)
                return zteCellGroup.Select(group =>
                {
                    var view = new CellInterFreqHoView
                    {
                        Earfcn = (int) group.eutranMeasParas_interCarriFreq,
                        InterFreqEventA1 = eventA1,
                        InterFreqEventA2 = eventA2
                    };

                    var configId = int.Parse(group.interFHOMeasCfg.Split(',')[0]);
                    var measurement = _zteMeasurementRepository.GetRecent(_eNodebId, configId);
                    if (measurement == null) return view;
                    view.InterFreqHoEventType = measurement.eventId;
                    view.InterFreqEventA3 = Mapper.Map<UeEUtranMeasurementZte, InterFreqEventA3>(measurement);
                    view.InterFreqEventA4 = Mapper.Map<UeEUtranMeasurementZte, InterFreqEventA4>(measurement);
                    view.InterFreqEventA5 = Mapper.Map<UeEUtranMeasurementZte, InterFreqEventA5>(measurement);
                    return view;
                }).ToList();
            if (zteCellGroup.Count==0) return new List<CellInterFreqHoView>();
            var configIds = zteCellGroup[0].interFHOMeasCfg.Split(',').Select(int.Parse).Distinct();
            return configIds.Select(id =>
            {
                var view = new CellInterFreqHoView
                {
                    Earfcn = (int)zteCellGroup[0].eutranMeasParas_interCarriFreq,
                    InterFreqEventA1 = eventA1,
                    InterFreqEventA2 = eventA2
                };
                
                var measurement = _zteMeasurementRepository.GetRecent(_eNodebId, id);
                if (measurement != null)
                {
                    view.InterFreqHoEventType = measurement.eventId;
                    view.InterFreqEventA3 = Mapper.Map<UeEUtranMeasurementZte, InterFreqEventA3>(measurement);
                    view.InterFreqEventA4 = Mapper.Map<UeEUtranMeasurementZte, InterFreqEventA4>(measurement);
                    view.InterFreqEventA5 = Mapper.Map<UeEUtranMeasurementZte, InterFreqEventA5>(measurement);
                }
                return view;
            }).ToList();
        }
    }
}
