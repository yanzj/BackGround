using Lte.Evaluations.DataService.Switch;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Entities.Basic;

namespace Lte.Evaluations.DataService.Basic
{
    internal class ZteCellPowerQuery : IMongoQuery<CellPower>
    {
        private readonly IEUtranCellFDDZteRepository _ztePbRepository;
        private readonly IPowerControlDLZteRepository _ztePaRepository;
        private readonly int _eNodebId;
        private readonly byte _sectorId;

        public ZteCellPowerQuery(IEUtranCellFDDZteRepository ztePbRepository,
            IPowerControlDLZteRepository ztePaRepository, int eNodebId, byte sectorId)
        {
            _ztePbRepository = ztePbRepository;
            _ztePaRepository = ztePaRepository;
            _eNodebId = eNodebId;
            _sectorId = sectorId;
        }

        public CellPower Query()
        {
            var pbCfg = _ztePbRepository.GetRecent(_eNodebId, _sectorId);
            var paCfg = _ztePaRepository.GetRecent(_eNodebId, _sectorId);
            return pbCfg == null || paCfg == null ? null : new CellPower(pbCfg, paCfg) { SectorId = _sectorId };
        }
    }
}