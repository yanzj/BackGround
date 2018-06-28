using Lte.Evaluations.DataService.Switch;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Abstract.Switch;
using Lte.Parameters.Entities.Basic;

namespace Lte.Evaluations.DataService.Basic
{
    internal class ZteCellQuery : IMongoQuery<CellHuaweiMongo>
    {
        private readonly IEUtranCellFDDZteRepository _zteCellRepository;
        private readonly IEUtranCellMeasurementZteRepository _zteMeasRepository;
        private readonly IPrachFDDZteRepository _ztePrachRepository;
        private readonly int _eNodebId;
        private readonly byte _sectorId;

        public ZteCellQuery(IEUtranCellFDDZteRepository zteCellRepository,
            IEUtranCellMeasurementZteRepository zteMeasRepository,
            IPrachFDDZteRepository ztePrachRepository, int eNodebId, byte sectorId)
        {
            _zteCellRepository = zteCellRepository;
            _zteMeasRepository = zteMeasRepository;
            _ztePrachRepository = ztePrachRepository;
            _eNodebId = eNodebId;
            _sectorId = sectorId;
        }

        public CellHuaweiMongo Query()
        {
            var zteCell = _zteCellRepository.GetRecent(_eNodebId, _sectorId);
            var zteMeas = _zteMeasRepository.GetRecentList(_eNodebId, _sectorId);
            var ztePrach = _ztePrachRepository.GetRecent(_eNodebId, _sectorId);

            return new CellHuaweiMongo
            {
                PhyCellId = zteCell?.pci ?? 0,
                CellSpecificOffset = zteCell?.ocs ?? 15,
                QoffsetFreq = zteMeas[0]?.eutranMeasParas_offsetFreq ?? 15,
                RootSequenceIdx = ztePrach?.rootSequenceIndex ?? -1
            };
        }
    }
}