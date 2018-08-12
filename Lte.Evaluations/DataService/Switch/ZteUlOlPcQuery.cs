using AutoMapper;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Entities.Channel;

namespace Lte.Evaluations.DataService.Switch
{
    internal class ZteUlOlPcQuery : IMongoQuery<CellOpenLoopPcView>
    {
        private readonly IPowerControlULZteRepository _ztePcRepository;
        private readonly int _eNodebId;
        private readonly byte _sectorId;

        public ZteUlOlPcQuery(IPowerControlULZteRepository zteRepository, int eNodebId, byte sectorId)
        {
            _ztePcRepository = zteRepository;
            _eNodebId = eNodebId;
            _sectorId = sectorId;
        }

        public CellOpenLoopPcView Query()
        {
            var stat = _ztePcRepository.GetRecent(_eNodebId, _sectorId);
            var view = Mapper.Map<CellOpenLoopPcView>(stat);
            if (view != null)
            {
                view.ENodebId = _eNodebId;
                view.SectorId = _sectorId;
            }
            return view;
        }
    }
}