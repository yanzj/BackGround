using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Cdma;
using Abp.EntityFramework.Repositories;
using AutoMapper;
using Lte.Domain.Common.Geo;
using Lte.Domain.Excel;
using Lte.Evaluations.DataService.Basic;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;

namespace Lte.Evaluations.DataService.Dump
{
    public class CdmaCellDumpService
    {
        private readonly ICdmaCellRepository _cellRepository;
        private readonly ICdmaRruRepository _rruRepository;

        public CdmaCellDumpService(ICdmaCellRepository cellRepository, ICdmaRruRepository rruRepository)
        {
            _cellRepository = cellRepository;
            _rruRepository = rruRepository;
        }

        public int DumpNewCellExcels(IEnumerable<CdmaCellExcel> infos)
        {
            var cellList = Mapper.Map<IEnumerable<CdmaCellExcel>, List<CdmaCell>>(infos);
            if (!cellList.Any()) return 0;
            var count = 0;
            foreach (var cell in cellList)
            {
                var item = _cellRepository.GetBySectorId(cell.BtsId, cell.SectorId);
                if (item == null)
                {
                    if (_cellRepository.Insert(cell) != null) count++;
                }
                else
                {
                    item.Pn = cell.Pn;
                    item.IsInUse = true;
                    _cellRepository.Update(item);
                    count++;
                }
            }
            _cellRepository.SaveChanges();
            return count;
        }

        public bool DumpSingleCellExcel(CdmaCellExcel info)
        {
            var cell = _cellRepository.GetBySectorIdAndCellType(info.BtsId, info.SectorId, info.CellType);
            if (cell == null)
            {
                cell = Mapper.Map<CdmaCellExcel, CdmaCell>(info);
                cell.Import(info);
                _cellRepository.SaveChanges();
                return _cellRepository.Insert(cell) != null;
            }
            cell.Import(info);
            cell.IsInUse = true;
            _cellRepository.Update(cell);
            _cellRepository.SaveChanges();
            return true;
        }

        public void VanishCells(CdmaCellIdsContainer container)
        {
            foreach (
                var cell in
                container.CellIdPairs.Select(
                        cellIdPair => _cellRepository.GetBySectorIdAndCellType(cellIdPair.CellId, cellIdPair.SectorId, cellIdPair.CellType))
                    .Where(cell => cell != null))
            {
                cell.IsInUse = false;
                _cellRepository.Update(cell);
            }
            _cellRepository.SaveChanges();
        }

        public async Task<int> ImportRru()
        {
            var info = BasicImportContainer.CdmaCellExcels[BasicImportContainer.CdmaRruIndex];
            await _rruRepository.UpdateOne<ICdmaRruRepository, CdmaRru, CdmaCellExcel>(info);
            BasicImportContainer.CdmaRruIndex++;
            return BasicImportContainer.CdmaRruIndex < BasicImportContainer.CdmaCellExcels.Count
                ? BasicImportContainer.CdmaRruIndex
                : -1;
        }
    }
}