using Lte.Evaluations.DataService.Switch;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Entities.Basic;

namespace Lte.Evaluations.DataService.Basic
{
    internal class HuaweiCellQuery : IMongoQuery<CellHuaweiMongo>
    {
        private readonly ICellHuaweiMongoRepository _repository;
        private readonly int _eNodebId;
        private readonly byte _sectorId;

        public HuaweiCellQuery(ICellHuaweiMongoRepository repository, int eNodebId, byte sectorId)
        {
            _repository = repository;
            _eNodebId = eNodebId;
            _sectorId = sectorId;
        }

        public CellHuaweiMongo Query()
        {
            return _repository.GetRecent(_eNodebId, _sectorId);
        }
    }
}