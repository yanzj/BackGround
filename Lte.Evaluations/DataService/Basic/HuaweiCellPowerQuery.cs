using Lte.Evaluations.DataService.Switch;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Entities.Basic;

namespace Lte.Evaluations.DataService.Basic
{
    internal class HuaweiCellPowerQuery : HuaweiCellMongoQuery<CellPower>
    {
        private readonly IPDSCHCfgRepository _huaweiPbRepository;
        private readonly ICellDlpcPdschPaRepository _huaweiPaRepository;

        public HuaweiCellPowerQuery(ICellHuaweiMongoRepository huaweiCellRepository,
            IPDSCHCfgRepository huaweiPbRepository, ICellDlpcPdschPaRepository huaweiPaRepository, int eNodebId,
            byte sectorId) : base(huaweiCellRepository, eNodebId, sectorId)
        {
            _huaweiPbRepository = huaweiPbRepository;
            _huaweiPaRepository = huaweiPaRepository;
        }

        protected override CellPower QueryByLocalCellId(int localCellId)
        {
            var pbCfg = _huaweiPbRepository.GetRecent(ENodebId, localCellId);
            var paCfg = _huaweiPaRepository.GetRecent(ENodebId, localCellId);
            return pbCfg == null || paCfg == null ? null : new CellPower(pbCfg, paCfg) { SectorId = SectorId };
        }
    }
}