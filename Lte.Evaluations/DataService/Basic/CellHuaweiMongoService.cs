using System.Linq;
using Lte.Evaluations.DataService.Switch;
using Lte.MySqlFramework.Abstract;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Abstract.Switch;
using Lte.Parameters.Entities.Basic;

namespace Lte.Evaluations.DataService.Basic
{
    public class CellHuaweiMongoService
    {
        private readonly ICellHuaweiMongoRepository _repository;
        private readonly IEUtranCellFDDZteRepository _zteCellRepository;
        private readonly IENodebRepository _eNodebRepository;
        private readonly IEUtranCellMeasurementZteRepository _zteMeasRepository;
        private readonly IPrachFDDZteRepository _ztePrachRepository;

        public CellHuaweiMongoService(ICellHuaweiMongoRepository repository,
            IEUtranCellFDDZteRepository zteCellRepository, IENodebRepository eNodebRepository,
            IEUtranCellMeasurementZteRepository zteMeasRepository,
            IPrachFDDZteRepository ztePrachRepository)
        {
            _repository = repository;
            _zteCellRepository = zteCellRepository;
            _eNodebRepository = eNodebRepository;
            _zteMeasRepository = zteMeasRepository;
            _ztePrachRepository = ztePrachRepository;
        }

        private IMongoQuery<CellHuaweiMongo> ConstructQuery(int eNodebId, byte sectorId)
        {
            var eNodeb = _eNodebRepository.FirstOrDefault(x => x.ENodebId == eNodebId);
            if (eNodeb == null) return null;
            return eNodeb.Factory == "华为"
                ? (IMongoQuery<CellHuaweiMongo>)new HuaweiCellQuery(_repository, eNodebId, sectorId)
                : new ZteCellQuery(_zteCellRepository, _zteMeasRepository, _ztePrachRepository, eNodebId, sectorId);
        }

        public CellHuaweiMongo QueryRecentCellInfo(int eNodebId, byte sectorId)
        {
            var query = ConstructQuery(eNodebId, sectorId);
            return query?.Query();
        }

        public HuaweiLocalCellDef QueryLocalCellDef(int eNodebId)
        {
            var eNodeb = _eNodebRepository.FirstOrDefault(x => x.ENodebId == eNodebId);
            if (eNodeb == null || eNodeb.Factory != "华为") return null;
            var cells = _repository.GetRecentList(eNodebId);
            return new HuaweiLocalCellDef
            {
                ENodebId = eNodebId,
                LocalCellDict = cells.ToDictionary(x => x.LocalCellId, y => y.CellId)
            };
        }
    }
}