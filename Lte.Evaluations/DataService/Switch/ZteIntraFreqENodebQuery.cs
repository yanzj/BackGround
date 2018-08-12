using Lte.Parameters.Abstract.Switch;
using Lte.Parameters.Entities.Switch;

namespace Lte.Evaluations.DataService.Switch
{
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