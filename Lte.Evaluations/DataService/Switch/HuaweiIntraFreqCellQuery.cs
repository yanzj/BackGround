using AutoMapper;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Abstract.Switch;
using Lte.Parameters.Entities.Switch;

namespace Lte.Evaluations.DataService.Switch
{
    internal class HuaweiIntraFreqCellQuery : HuaweiCellMongoQuery<CellIntraFreqHoView>
    {
        private readonly IIntraFreqHoGroupRepository _huaweiCellHoRepository;

        public HuaweiIntraFreqCellQuery(ICellHuaweiMongoRepository huaweiCellRepository,
            IIntraFreqHoGroupRepository huaweiCellHoRepository, int eNodebId, byte sectorId)
            : base(huaweiCellRepository, eNodebId, sectorId)
        {
            _huaweiCellHoRepository = huaweiCellHoRepository;
        }

        protected override CellIntraFreqHoView QueryByLocalCellId(int localCellId)
        {
            var huaweiPara = _huaweiCellHoRepository.GetRecent(ENodebId, localCellId);
            return huaweiPara == null ? null : Mapper.Map<IntraFreqHoGroup, CellIntraFreqHoView>(huaweiPara);
        }
    }
}