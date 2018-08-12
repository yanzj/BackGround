using AutoMapper;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Entities.Channel;

namespace Lte.Evaluations.DataService.Switch
{
    internal class HuaweiUlOlPcQuery : HuaweiCellMongoQuery<CellOpenLoopPcView>
    {
        private readonly ICellUlpcCommRepository _huaweiPcRepository;

        public HuaweiUlOlPcQuery(ICellHuaweiMongoRepository huaweiCellRepository,
            ICellUlpcCommRepository huaweiPcRepository, int eNodebId, byte sectorId)
            : base(huaweiCellRepository, eNodebId, sectorId)
        {
            _huaweiPcRepository = huaweiPcRepository;
        }

        protected override CellOpenLoopPcView QueryByLocalCellId(int localCellId)
        {
            var stat = _huaweiPcRepository.GetRecent(ENodebId, localCellId);
            var view = Mapper.Map<CellOpenLoopPcView>(stat);
            if (view != null)
            {
                view.ENodebId = ENodebId;
                view.SectorId = SectorId;
            }
            return view;
        }
    }
}