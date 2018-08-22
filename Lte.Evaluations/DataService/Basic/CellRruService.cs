using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Infrastructure;
using Lte.Domain.Common.Types;
using Lte.Domain.Regular;
using Lte.Evaluations.ViewModels.Precise;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Station;
using Lte.MySqlFramework.Entities;
using Lte.MySqlFramework.Entities.Infrastructure;

namespace Lte.Evaluations.DataService.Basic
{
    public class CellRruService
    {
        private readonly ICellRepository _repository;
        private readonly ILteRruRepository _rruRepository;
        private readonly IENodebRepository _eNodebRepository;
        private readonly IStationDictionaryRepository _stationDictionaryRepository;

        public CellRruService(ICellRepository repository, IENodebRepository eNodebRepository, 
            ILteRruRepository rruRepository,
            IStationDictionaryRepository stationDictionaryRepository)
        {
            _repository = repository;
            _eNodebRepository = eNodebRepository;
            _rruRepository = rruRepository;
            _stationDictionaryRepository = stationDictionaryRepository;
        }

        public CellRruView GetCellRruView(int eNodebId, byte sectorId)
        {
            var cell = _repository.GetBySectorId(eNodebId, sectorId);
            return cell == null ? null : cell.ConstructCellRruView(_eNodebRepository, _rruRepository);
        }

        public IEnumerable<CellRruView> GetCellViews(int eNodebId)
        {
            var cells = _repository.GetAllList(eNodebId);
            return cells.Any()
                ? cells.Select(x => x.ConstructCellRruView(_eNodebRepository, _rruRepository))
                : new List<CellRruView>();
        }
        
        public IEnumerable<CellRruView> GetByPlanNum(string planNum)
        {
            var rrus = _rruRepository.GetAllList(x => x.PlanNum == planNum);
            if (!rrus.Any()) return new List<CellRruView>();
            return rrus.Select(rru =>
            {
                var cell =
                    _repository.FirstOrDefault(x => x.ENodebId == rru.ENodebId && x.LocalSectorId == rru.LocalSectorId);
                return cell == null ? null : cell.ConstructCellRruView(_eNodebRepository, rru);
            }).Where(rru => rru != null);
        }

        public IEnumerable<CellRruView> GetByRruName(string rruName)
        {
            var rrus = _rruRepository.GetAllList(x => x.RruName == rruName);
            if (!rrus.Any()) return new List<CellRruView>();
            return rrus.Select(rru =>
            {
                var cell =
                    _repository.FirstOrDefault(x => x.ENodebId == rru.ENodebId && x.LocalSectorId == rru.LocalSectorId);
                return cell == null ? null : cell.ConstructCellRruView(_eNodebRepository, rru);
            }).Where(rru => rru != null);
        }

        public LteRru QueryRru(string cellName)
        {
            var fields = cellName.GetSplittedFields('-');
            if (fields.Length < 2) return null;
            var eNodebName = fields[0];
            var sectorId = fields[1].ConvertToByte(0);
            var eNodeb = _eNodebRepository.GetByName(eNodebName);
            if (eNodeb == null) return null;
            var cell = _repository.GetBySectorId(eNodeb.ENodebId, sectorId);
            if (cell == null) return null;
            return _rruRepository.Get(eNodeb.ENodebId, cell.LocalSectorId);
        }

        public IEnumerable<CellView> QueryByRruName(string rruName)
        {
            var rrus = _rruRepository.GetAllList(x => x.RruName.Contains(rruName));
            return from rru in rrus
                   select _repository.FirstOrDefault(x => x.ENodebId == rru.ENodebId && x.LocalSectorId == rru.LocalSectorId)
                   into cell
                   where cell != null
                   select CellView.ConstructView(cell, _eNodebRepository);
        }

    }
}
