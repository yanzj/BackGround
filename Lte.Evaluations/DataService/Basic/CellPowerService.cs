using Lte.Evaluations.DataService.Switch;
using Lte.MySqlFramework.Abstract;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Entities.Basic;

namespace Lte.Evaluations.DataService.Basic
{
    public class CellPowerService : ICellPowerService
    {
        private readonly IEUtranCellFDDZteRepository _ztePbRepository;
        private readonly IPowerControlDLZteRepository _ztePaRepository;
        private readonly IPDSCHCfgRepository _huaweiPbRepository;
        private readonly ICellDlpcPdschPaRepository _huaweiPaRepository;
        private readonly ICellHuaweiMongoRepository _huaweiCellRepository;
        private readonly IENodebRepository _eNodebRepository;

        public CellPowerService(IEUtranCellFDDZteRepository ztePbRepository,
            IPowerControlDLZteRepository ztePaRepository, IPDSCHCfgRepository huaweiPbRepository,
            ICellDlpcPdschPaRepository huaweiPaRepository, ICellHuaweiMongoRepository huaweiCellRepository,
            IENodebRepository eNodebRepository)
        {
            _ztePbRepository = ztePbRepository;
            _ztePaRepository = ztePaRepository;
            _huaweiPbRepository = huaweiPbRepository;
            _huaweiPaRepository = huaweiPaRepository;
            _huaweiCellRepository = huaweiCellRepository;
            _eNodebRepository = eNodebRepository;
        }

        private IMongoQuery<CellPower> ConstructQuery(int eNodebId, byte sectorId)
        {
            var eNodeb = _eNodebRepository.FirstOrDefault(x => x.ENodebId == eNodebId);
            if (eNodeb == null) return null;
            return eNodeb.Factory == "华为"
                ? (IMongoQuery<CellPower>)
                new HuaweiCellPowerQuery(_huaweiCellRepository, _huaweiPbRepository, _huaweiPaRepository, eNodebId,
                    sectorId)
                : new ZteCellPowerQuery(_ztePbRepository, _ztePaRepository, eNodebId, sectorId);
        }

        public CellPower Query(int eNodebId, byte sectorId)
        {
            var query = ConstructQuery(eNodebId, sectorId);
            return query?.Query();
        }
    }
}