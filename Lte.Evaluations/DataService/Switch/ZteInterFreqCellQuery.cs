using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Lte.Parameters.Abstract.Switch;
using Lte.Parameters.Entities.Switch;

namespace Lte.Evaluations.DataService.Switch
{
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