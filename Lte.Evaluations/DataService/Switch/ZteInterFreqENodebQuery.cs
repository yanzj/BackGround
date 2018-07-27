using Lte.Domain.Regular;
using Lte.Parameters.Abstract.Switch;
using Lte.Parameters.Entities.Switch;

namespace Lte.Evaluations.DataService.Switch
{
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
            view.UpdateDate = hoEvent?.iDate.GetDateFromFileName();
            return view;
        }
    }
}