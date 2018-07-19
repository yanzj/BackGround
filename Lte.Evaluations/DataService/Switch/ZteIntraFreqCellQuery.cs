using AutoMapper;
using Lte.Domain.Regular;
using Lte.Parameters.Abstract.Switch;
using Lte.Parameters.Entities.Switch;

namespace Lte.Evaluations.DataService.Switch
{
    internal class ZteIntraFreqCellQuery : IMongoQuery<CellIntraFreqHoView>
    {
        private readonly IUeEUtranMeasurementRepository _zteMeasurementRepository;
        private readonly ICellMeasGroupZteRepository _zteGroupRepository;
        private readonly IEUtranCellMeasurementZteRepository _zteCellGroupRepository;
        private readonly int _eNodebId;
        private readonly byte _sectorId;

        public ZteIntraFreqCellQuery(IUeEUtranMeasurementRepository zteMeasurementRepository,
            ICellMeasGroupZteRepository zteGroupRepository, IEUtranCellMeasurementZteRepository zteCellGroupRepository, 
            int eNodebId, byte sectorId)
        {
            _zteGroupRepository = zteGroupRepository;
            _zteMeasurementRepository = zteMeasurementRepository;
            _zteCellGroupRepository = zteCellGroupRepository;
            _eNodebId = eNodebId;
            _sectorId = sectorId;
        }

        public CellIntraFreqHoView Query()
        {
            var zteCellGroup = _zteCellGroupRepository.GetRecentList(_eNodebId, _sectorId);
            int configId;
            if (zteCellGroup != null && zteCellGroup.Count > 0)
            {
                var fields = zteCellGroup[0].intraFHOMeasCfg.Split(',');
                configId = fields.Length > 0 ? fields[0].ConvertToInt(0) : 0;
            }
            else
            {
                var zteGroup = _zteGroupRepository.GetRecent(_eNodebId);
                var fields = zteGroup?.intraFHOMeasCfg.Split(',');
                configId = fields != null && fields.Length > 0 ? fields[0].ConvertToInt(0) : 0;
            }

            var ztePara = _zteMeasurementRepository.GetRecent(_eNodebId, configId);
            return ztePara == null ? null : Mapper.Map<UeEUtranMeasurementZte, CellIntraFreqHoView>(ztePara);
        }
    }
}