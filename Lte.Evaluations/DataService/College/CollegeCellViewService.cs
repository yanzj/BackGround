using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.EntityFramework.Entities.College;
using Abp.EntityFramework.Entities.Infrastructure;
using AutoMapper;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Distribution;
using Lte.Domain.Regular;
using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.ViewModels.Precise;
using Lte.MySqlFramework.Abstract.College;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Entities.Infrastructure;

namespace Lte.Evaluations.DataService.College
{
    public class CollegeCellViewService
    {
        private readonly IHotSpotCellRepository _repository;
        private readonly ICellRepository _cellRepository;
        private readonly IENodebRepository _eNodebRepository;
        private readonly ILteRruRepository _rruRepository;

        public CollegeCellViewService(IHotSpotCellRepository repository, ICellRepository cellRepoistory,
            IENodebRepository eNodebRepository, ILteRruRepository rruRepository)
        {
            _repository = repository;
            _cellRepository = cellRepoistory;
            _eNodebRepository = eNodebRepository;
            _rruRepository = rruRepository;
        }

        public List<Cell> GetCollegeCells(string collegeName)
        {
            var ids =
                _repository.GetAllList(
                    x =>
                        x.HotspotType == HotspotType.College && x.HotspotName == collegeName &&
                        x.InfrastructureType == InfrastructureType.Cell);
            var cellList = _cellRepository.GetAllList();
            return 
                (from id in ids
                    join c in cellList on new {id.ENodebId, id.SectorId} equals new {c.ENodebId, c.SectorId}
                    select c).ToList();
        }
        
        public IEnumerable<CellRruView> GetCollegeViews(string collegeName)
        {
            var cells = GetCollegeCells(collegeName);
            return cells.Any()
                ? cells.Select(x => x.ConstructCellRruView(_eNodebRepository, _rruRepository))
                : new List<CellRruView>();
        }

        public IEnumerable<CellRruView> GetHotSpotViews(string name)
        {
            var ids = _repository.GetAllList(
                x =>
                    x.HotspotName == name && x.InfrastructureType == InfrastructureType.Cell);
            var query = ids.Select(x => _cellRepository.GetBySectorId(x.ENodebId, x.SectorId)).Where(cell => cell != null).ToList();
            return query.Any()
                ? query.Select(x => x.ConstructCellRruView(_eNodebRepository, _rruRepository))
                : new List<CellRruView>();
        }

        public IEnumerable<SectorView> QueryCollegeSectors(string collegeName)
        {
            var ids =
                _repository.GetAllList(
                    x =>
                        x.HotspotName == collegeName && x.HotspotType == HotspotType.College &&
                        x.InfrastructureType == InfrastructureType.Cell);
            var query =
                ids.Select(x => _cellRepository.GetBySectorId(x.ENodebId, x.SectorId))
                    .Where(cell => cell != null)
                    .ToList();
            return query.Any()
                ? Mapper.Map<IEnumerable<CellView>, IEnumerable<SectorView>>(
                    query.Select(x => CellView.ConstructView(x, _eNodebRepository)))
                : null;
        }

        public IEnumerable<SectorView> QueryHotSpotSectors(string name)
        {
            var ids =
                _repository.GetAllList(
                    x => x.HotspotName == name && x.InfrastructureType == InfrastructureType.Cell);
            var query =
                ids.Select(x => _cellRepository.GetBySectorId(x.ENodebId, x.SectorId))
                    .Where(cell => cell != null)
                    .ToList();
            return query.Any()
                ? Mapper.Map<IEnumerable<CellView>, IEnumerable<SectorView>>(
                    query.Select(x => CellView.ConstructView(x, _eNodebRepository)))
                : null;
        }

        public async Task<int> UpdateHotSpotCells(CollegeCellNamesContainer container)
        {
            foreach (var cell in from cellName in container.CellNames
                select cellName.GetSplittedFields('-')
                into fields
                where fields.Length > 1
                let eNodeb = _eNodebRepository.GetByName(fields[0])
                where eNodeb != null
                select _cellRepository.GetBySectorId(eNodeb.ENodebId, fields[1].ConvertToByte(0))
                into cell
                where cell != null
                select cell)
            {
                await _repository.InsertAsync(new HotSpotCellId
                {
                    ENodebId = cell.ENodebId,
                    SectorId = cell.SectorId,
                    HotspotType = HotspotType.College,
                    HotspotName = container.CollegeName
                });
            }
            return _repository.SaveChanges();
        }
    }
}