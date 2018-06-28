using Lte.Parameters.Abstract.Basic;

namespace Lte.Evaluations.DataService.Switch
{
    public abstract class HuaweiCellMongoQuery<TView> : IMongoQuery<TView>
    {
        private readonly ICellHuaweiMongoRepository _huaweiCellRepository;
        protected readonly int ENodebId;
        protected readonly byte SectorId;

        protected HuaweiCellMongoQuery(ICellHuaweiMongoRepository huaweiCellRepository, int eNodebId, byte sectorId)
        {
            _huaweiCellRepository = huaweiCellRepository;
            ENodebId = eNodebId;
            SectorId = sectorId;
        }

        protected abstract TView QueryByLocalCellId(int localCellId);

        public TView Query()
        {
            var huaweiCell = _huaweiCellRepository.GetRecent(ENodebId, SectorId);
            var localCellId = huaweiCell?.LocalCellId ?? SectorId;
            return QueryByLocalCellId(localCellId);
        }
    }
}