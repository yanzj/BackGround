using AutoMapper;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Infrastructure;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Station;
using Lte.Evaluations.ViewModels.Precise;
using Lte.MySqlFramework.Abstract.Infrastructure;

namespace Lte.Evaluations.DataService.Basic
{
    public class CellService
    {
        private readonly ICellRepository _repository;
        private readonly IENodebRepository _eNodebRepository;

        public CellService(ICellRepository repository, IENodebRepository eNodebRepository)
        {
            _repository = repository;
            _eNodebRepository = eNodebRepository;
        }

        public CellView GetCell(int eNodebId, byte sectorId)
        {
            var cell = _repository.GetBySectorId(eNodebId, sectorId);
            return cell == null ? null : CellView.ConstructView(cell, _eNodebRepository);
        }

        public IEnumerable<SectorView> GetCells(double west, double east, double south, double north)
        {
            var cells = _repository.GetAllList(west, east, south, north);
            return cells.Any()
                ? Mapper.Map<IEnumerable<CellView>, IEnumerable<SectorView>>(
                    cells.Select(x => CellView.ConstructView(x, _eNodebRepository)))
                : new List<SectorView>();
        }

        public IEnumerable<CellView> GetNearbyCellsWithPci(int eNodebId, byte sectorId, short pci)
        {
            var cell = _repository.GetBySectorId(eNodebId, sectorId);
            if (cell == null) return new List<CellView>();
            return
                _repository.GetAllList(cell.Longtitute - 0.2, cell.Longtitute + 0.2, cell.Lattitute - 0.2,
                        cell.Lattitute + 0.2)
                    .Where(x => x.Pci == pci)
                    .Select(x => CellView.ConstructView(x, _eNodebRepository));
        }

        public IEnumerable<CellView> GetNearbyCellsWithPci(int eNodebId, byte sectorId, short pci, int frequency)
        {
            var cell = _repository.GetBySectorId(eNodebId, sectorId);
            if (cell == null) return new List<CellView>();
            return
                _repository.GetAllList(cell.Longtitute - 0.2, cell.Longtitute + 0.2, cell.Lattitute - 0.2,
                        cell.Lattitute + 0.2)
                    .Where(x => x.Pci == pci && x.Frequency == frequency)
                    .Select(x => CellView.ConstructView(x, _eNodebRepository));
        }

        public List<byte> GetSectorIds(string eNodebName)
        {
            var eNodeb = _eNodebRepository.GetByName(eNodebName);
            return eNodeb == null
                ? null
                : _repository.GetAll().Where(x => x.ENodebId == eNodeb.ENodebId).Select(x => x.SectorId).ToList();
        }

        public IEnumerable<SectorView> QuerySectors(int eNodebId)
        {
            var cells = _repository.GetAllList(eNodebId);
            return cells.Any()
                ? Mapper.Map<IEnumerable<CellView>, IEnumerable<SectorView>>(
                    cells.Select(x => CellView.ConstructView(x, _eNodebRepository)))
                : new List<SectorView>();
        }

        public IEnumerable<SectorView> QuerySectorsInUse(int eNodebId)
        {
            var cells = _repository.GetAllInUseList().Where(x => x.ENodebId == eNodebId).ToList();
            return cells.Any()
                ? Mapper.Map<IEnumerable<CellView>, IEnumerable<SectorView>>(
                    cells.Select(x => CellView.ConstructView(x, _eNodebRepository)))
                : new List<SectorView>();
        }

        public IEnumerable<SectorView> QuerySectors(double west, double east, double south, double north)
        {
            var cells = _repository.GetAllList(west, east, south, north);
            return cells.Any()
                ? Mapper.Map<IEnumerable<CellView>, IEnumerable<SectorView>>(
                    cells.Select(x => CellView.ConstructView(x, _eNodebRepository)))
                : new List<SectorView>();
        }

        public IEnumerable<SectorView> QuerySectors(SectorRangeContainer container)
        {
            var cells =
                _repository.GetAllList(container.West, container.East, container.South, container.North)
                    .Where(x => x.IsInUse).ToList();
            if (container.ExcludedCells.Any())
            {
                var excludeCells = from cell in cells
                    join sector in container.ExcludedCells on new
                    {
                        CellId = cell.ENodebId,
                        cell.SectorId
                    } equals new
                    {
                        sector.CellId,
                        sector.SectorId
                    }
                    select cell;
                cells = cells.Except(excludeCells).ToList();
            }

            return cells.Any()
                ? Mapper.Map<IEnumerable<CellView>, IEnumerable<SectorView>>(
                    cells.Select(x => CellView.ConstructView(x, _eNodebRepository)))
                : new List<SectorView>();
        }

        public IEnumerable<Precise4GSector> QuerySectors(TopPreciseViewContainer container)
        {
            return container.Views.Select(x => x.ConstructSector(_repository));
        }

        public IEnumerable<GeoPoint> QueryOutdoorCellSites(IEnumerable<ENodeb> eNodebs, NetworkType type = NetworkType.With4G)
        {
            var cellList = type == NetworkType.With4G
                ? _repository.GetAllList(x => x.IsOutdoor && (x.SectorId < 16 || (x.SectorId >= 48 && x.SectorId < 64)))
                : (type == NetworkType.NbIot
                    ? _repository.GetAllList(x => x.IsOutdoor && x.SectorId >= 80)
                    : _repository.GetAllList(x => x.IsOutdoor && (x.SectorId >= 16 && x.SectorId < 32)));
            var cellSites = cellList.Select(x => new
            {
                x.Longtitute,
                x.Lattitute,
                x.ENodebId
            }).Distinct();
            var results = from e in eNodebs
                join c in cellSites on e.ENodebId equals c.ENodebId
                select new GeoPoint(e.Longtitute, e.Lattitute);
            return results;
        }

        public IEnumerable<GeoPoint> QueryIndoorCellSites(IEnumerable<ENodeb> eNodebs)
        {
            var cellSites = _repository.GetAllList(x => !x.IsOutdoor).Select(x => new
            {
                x.Longtitute,
                x.Lattitute,
                x.ENodebId
            }).Distinct();
            var results = from e in eNodebs
                          join c in cellSites on e.ENodebId equals c.ENodebId
                          select new GeoPoint(e.Longtitute, e.Lattitute);
            return results;
        }

    }
}
