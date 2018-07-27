using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Abstract.Infrastructure;
using Lte.Parameters.Abstract.Switch;
using Lte.Parameters.Entities.Switch;

namespace Lte.Evaluations.DataService.Switch
{
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
}