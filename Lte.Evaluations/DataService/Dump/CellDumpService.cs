using Abp.EntityFramework.Repositories;
using AutoMapper;
using Lte.Domain.Regular;
using Lte.Evaluations.DataService.Basic;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Infrastructure;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Abstract.Cdma;
using Lte.MySqlFramework.Abstract.Infrastructure;

namespace Lte.Evaluations.DataService.Dump
{
    public class CellDumpService
    {
        private readonly IBtsRepository _btsRepository;
        private readonly ICellRepository _cellRepository;
        private readonly ILteRruRepository _rruRepository;

        public CellDumpService(IBtsRepository btsRepository, ICellRepository cellRepository, ILteRruRepository rruRepository)
        {
            _btsRepository = btsRepository;
            _cellRepository = cellRepository;
            _rruRepository = rruRepository;
        }

        public int DumpNewCellExcels(IEnumerable<CellExcel> infos)
        {
            var cellList = Mapper.Map<IEnumerable<CellExcel>, List<Cell>>(infos);
            if (!cellList.Any()) return 0;
            var count = 0;
            foreach (var cell in cellList)
            {
                var item = _cellRepository.GetBySectorId(cell.ENodebId, cell.SectorId);
                if (item == null)
                {
                    if (_cellRepository.Insert(cell) != null) count++;
                }
                else
                {
                    item.Pci = cell.Pci;
                    item.Frequency = cell.Frequency;
                    item.BandClass = cell.BandClass;
                    item.IsInUse = true;
                    _cellRepository.SaveChanges();
                    count++;
                }
            }
            _cellRepository.SaveChanges();
            return count;
        }

        public void UpdateENodebBtsIds(IEnumerable<CellExcel> infos)
        {
            var idPairs =
                Mapper.Map<IEnumerable<CellExcel>, IEnumerable<ENodebBtsIdPair>>(infos)
                    .Where(x => x.BtsId != -1)
                    .Distinct(new ENodebBtsIdPairEquator())
                    .ToList();
            if (!idPairs.Any()) return;
            idPairs.ForEach(x =>
            {
                var bts = _btsRepository.GetByBtsId(x.BtsId);
                if (bts == null) return;
                bts.ENodebId = x.ENodebId;
                _btsRepository.Update(bts);
            });
        }

        public bool DumpSingleCellExcel(CellExcel info)
        {
            var cell = _cellRepository.GetBySectorId(info.ENodebId, info.SectorId);
            if (cell == null)
            {
                cell = Mapper.Map<CellExcel, Cell>(info);
                var fields = info.ShareCdmaInfo.GetSplittedFields('_');
                var btsId = (fields.Length > 2) ? fields[1].ConvertToInt(-1) : -1;
                if (btsId > 0)
                {
                    var bts = _btsRepository.GetByBtsId(btsId);
                    if (bts != null)
                    {
                        bts.ENodebId = info.ENodebId;
                        _btsRepository.Update(bts);
                    }
                }
                var result = _cellRepository.Insert(cell);
                if (result == null) return false;
                var item =
                    BasicImportContainer.CellExcels.FirstOrDefault(
                        x => x.ENodebId == info.ENodebId && x.SectorId == info.SectorId);
                if (item != null)
                {
                    BasicImportContainer.CellExcels.Remove(item);
                }
                _cellRepository.SaveChanges();
                return true;
            }
            cell.Pci = info.Pci;
            cell.BandClass = info.BandClass;
            cell.Frequency = info.Frequency;
            cell.IsInUse = true;
            _cellRepository.SaveChanges();
            return true;
        }

        public void VanishCells(CellIdsContainer container)
        {
            foreach (
                var cell in
                    container.CellIdPairs.Select(
                        cellIdPair => _cellRepository.GetBySectorId(cellIdPair.CellId, cellIdPair.SectorId))
                        .Where(cell => cell != null))
            {
                cell.IsInUse = false;
                _cellRepository.Update(cell);
            }
            _cellRepository.SaveChanges();
        }

        public async Task<int> ImportRru()
        {
            var info = BasicImportContainer.CellExcels[BasicImportContainer.LteRruIndex];
            await _rruRepository.UpdateOne<ILteRruRepository, LteRru, CellExcel>(info);
            BasicImportContainer.LteRruIndex++;
            return BasicImportContainer.LteRruIndex < BasicImportContainer.CellExcels.Count
                ? BasicImportContainer.LteRruIndex
                : -1;
        }

        public async Task<int> UpdateCells()
        {
            var info = BasicImportContainer.CellExcels[BasicImportContainer.LteCellIndex];
            await _cellRepository.UpdateOnly<ICellRepository, Cell, CellExcel>(info);
            BasicImportContainer.LteCellIndex++;
            return BasicImportContainer.LteCellIndex < BasicImportContainer.CellExcels.Count
                ? BasicImportContainer.LteCellIndex
                : -1;
        } 
    }
}
