using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Infrastructure;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Entities.Mr;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common.Wireless;
using Lte.Domain.LinqToCsv.Context;
using Lte.Evaluations.DataService.Basic;
using Lte.MySqlFramework.Entities;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Mr;

namespace Lte.Evaluations.DataService.Mr
{
    public class NearestPciCellService
    {
        private readonly INearestPciCellRepository _repository;
        private readonly ICellRepository _cellRepository;
        private readonly IENodebRepository _eNodebRepository;
        private readonly IAgisDtPointRepository _agisRepository;
      
        private readonly IMrGridKpiRepository _mrGridKpiRepository;

        private static Stack<NearestPciCell> NearestCells { get; set; }

        public NearestPciCellService(INearestPciCellRepository repository, ICellRepository cellRepository,
            IENodebRepository eNodebRepository, IAgisDtPointRepository agisRepository,
            IMrGridKpiRepository mrGridKpiRepository)
        {
            _repository = repository;
            _cellRepository = cellRepository;
            _eNodebRepository = eNodebRepository;
            _agisRepository = agisRepository;
          
            _mrGridKpiRepository = mrGridKpiRepository;
            
            if (NearestCells == null)
                NearestCells = new Stack<NearestPciCell>();
        }

        public List<NearestPciCellView> QueryCells(int cellId, byte sectorId)
        {
            return
                _repository.GetAllList(cellId, sectorId)
                    .Select(
                        x =>
                            x.ConstructView(_eNodebRepository))
                    .ToList();
        }

        public List<NearestPciCell> QueryNeighbors(int cellId, byte sectorId)
        {
            return _repository.GetAllList(cellId, sectorId);
        }

        public NearestPciCell QueryNearestPciCell(int cellId, byte sectorId, short pci)
        {
            return _repository.GetNearestPciCell(cellId, sectorId, pci);
        }

        public int UpdateNeighborPcis(int cellId, byte sectorId)
        {
            var neighborList = _repository.GetAllList(cellId, sectorId);
            foreach (var pciCell in neighborList)
            {
                var cell = _cellRepository.GetBySectorId(pciCell.NearestCellId, pciCell.NearestSectorId);
                if (cell == null || pciCell.Pci == cell.Pci) continue;
                pciCell.Pci = cell.Pci;
                _repository.Update(pciCell);
                neighborList = _repository.GetAllList(pciCell.NearestCellId, pciCell.NearestSectorId);
                foreach (var nearestPciCell in neighborList)
                {
                    cell = _cellRepository.GetBySectorId(nearestPciCell.NearestCellId, nearestPciCell.NearestSectorId);
                    if (cell==null||nearestPciCell.Pci==cell.Pci) continue;
                    nearestPciCell.Pci = cell.Pci;
                    _repository.Update(nearestPciCell);
                }
            }
            return _repository.SaveChanges();
        }

        public void UpdateNeighborCell(NearestPciCell cell)
        {
            var item = _repository.GetNearestPciCell(cell.CellId, cell.SectorId, cell.Pci);
            if (item != null)
            {
                item.NearestCellId = cell.NearestCellId;
                item.NearestSectorId = cell.NearestSectorId;
                _repository.Update(item);
            }
            else
            {
                cell.TotalTimes = 98;
                _repository.Insert(cell);
            }
            _repository.SaveChanges();
        }

        public async Task<int> UploadMrGridKpiPoints(StreamReader reader)
        {
            var csvs = CsvContext.Read<MrGridKpiDto>(reader);
            return await _mrGridKpiRepository.UpdateMany<IMrGridKpiRepository, MrGridKpi, MrGridKpiDto>(csvs);

        }

        public IEnumerable<AgisDtPoint> QueryAgisDtPoints(DateTime begin, DateTime end)
        {
            var points = _agisRepository.GetAllList(x => x.StatDate > begin && x.StatDate <= end);
            if (!points.Any()) return new List<AgisDtPoint>();
            return points.GroupBy(x => new {x.X, x.Y}).Select(g =>
            {
                var telecomGroups = g.Where(x => x.TelecomRsrp > 0).ToList();
                var mobileGroups = g.Where(x => x.MobileRsrp > 0).ToList();
                var unicomGroups = g.Where(x => x.UnicomRsrp > 0).ToList();
                var stat = new AgisDtPoint
                {
                    X = g.Key.X,
                    Y = g.Key.Y,
                    Longtitute = g.First().Longtitute,
                    Lattitute = g.First().Lattitute,
                    StatDate = g.First().StatDate
                };
                if (telecomGroups.Any())
                {
                    stat.TelecomRsrp = telecomGroups.Average(x => x.TelecomRsrp);
                    stat.TelecomRate100 = telecomGroups.Average(x => x.TelecomRate100);
                    stat.TelecomRate105 = telecomGroups.Average(x => x.TelecomRate105);
                    stat.TelecomRate110 = telecomGroups.Average(x => x.TelecomRate110);
                }
                if (mobileGroups.Any())
                {
                    stat.MobileRsrp = mobileGroups.Average(x => x.MobileRsrp);
                    stat.MobileRate100 = mobileGroups.Average(x => x.MobileRate100);
                    stat.MobileRate105 = mobileGroups.Average(x => x.MobileRate105);
                    stat.MobileRate110 = mobileGroups.Average(x => x.MobileRate110);
                }
                if (unicomGroups.Any())
                {
                    stat.UnicomRsrp = unicomGroups.Average(x => x.UnicomRsrp);
                    stat.UnicomRate100 = unicomGroups.Average(x => x.UnicomRate100);
                    stat.UnicomRate105 = unicomGroups.Average(x => x.UnicomRate105);
                    stat.UnicomRate110 = unicomGroups.Average(x => x.UnicomRate110);
                }
                return stat;
            });
        }

        public IEnumerable<AgisDtPoint> QueryAgisDtPoints(DateTime begin, DateTime end, string topic)
        {
            var points = _agisRepository.GetAllList(x => x.StatDate > begin && x.StatDate <= end && x.Operator == topic);
            return points;
        }

    }
}
