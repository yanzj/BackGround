using Abp.EntityFramework.AutoMapper;
using AutoMapper;
using Lte.Domain.Regular;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Abstract.Infrastructure;
using Lte.Parameters.Abstract.Kpi;
using Lte.Parameters.Entities.Kpi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lte.Domain.Common.Wireless;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;

namespace Lte.Evaluations.DataService.Mr
{
    public class InterferenceMatrixService
    {
        private readonly IInterferenceMatrixRepository _repository;

        private static Stack<InterferenceMatrixStat> InterferenceMatrixStats { get; set; }

        public static List<PciCell> PciCellList { get; private set; }
        
        public InterferenceMatrixService(IInterferenceMatrixRepository repository, ICellRepository cellRepository)
        {
            _repository = repository;
            if (InterferenceMatrixStats == null)
                InterferenceMatrixStats = new Stack<InterferenceMatrixStat>();
            if (PciCellList == null)
            {
                PciCellList = cellRepository.GetAllList().MapTo<List<PciCell>>();
            }
        }

        public int QueryExistedStatsCount(int eNodebId, byte sectorId, DateTime date)
        {
            var beginDay = date.Date;
            var nextDay = date.AddDays(1).Date;
            var cellId = eNodebId + "-" + sectorId;
            return _repository.Count(x =>
                    x.CellId == cellId && x.StatDate >= beginDay && x.StatDate < nextDay);
        }
        
        public int DumpMongoStats(InterferenceMatrixStat stat)
        {
            stat.StatDate = stat.StatDate.Date;
            var cellId = stat.ENodebId + "-" + stat.SectorId;
            var existedStat =
                _repository.FirstOrDefault(
                    x => x.CellId == cellId && x.NeighborPci == stat.NeighborPci && x.StatDate == stat.StatDate);
            if (existedStat == null)
                _repository.Insert(stat);
            else
            {
                existedStat.DestENodebId = stat.DestENodebId;
                existedStat.DestSectorId = stat.DestSectorId;
            }

            return _repository.SaveChanges();
        }
        
        public async Task<bool> DumpOneStat()
        {
            var stat = InterferenceMatrixStats.Pop();
            if (stat == null) return false;
            var cellId = stat.ENodebId + "-" + stat.SectorId;
            var item =
                _repository.FirstOrDefault(
                    x => x.CellId == cellId && x.NeighborPci == stat.NeighborPci && x.StatDate == stat.StatDate);
            if (item == null)
            {
                await _repository.InsertAsync(stat);
            }
            _repository.SaveChanges();
            return true;
        }
        
        public int GetStatsToBeDump()
        {
            return InterferenceMatrixStats.Count;
        }

        public void ClearStats()
        {
            InterferenceMatrixStats.Clear();
        }
    }

    public class InterferenceMongoService
    {
        private readonly IInterferenceMongoRepository _mongoRepository;

        public InterferenceMongoService(IInterferenceMongoRepository mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public InterferenceMatrixMongo QueryMongo(int eNodebId, byte sectorId)
        {
            return _mongoRepository.GetOne(eNodebId + "-" + sectorId);
        }

        private static List<InterferenceMatrixStat> GenereateOverallStatList(IEnumerable<InterferenceMatrixMongo> statList)
        {
            var results = Mapper.Map<IEnumerable<InterferenceMatrixMongo>, IEnumerable<InterferenceMatrixStat>>(statList);
            return (from s in results
                group s by new { s.NeighborPci, s.ENodebId }
                into g
                select g.Select(x => x).ArraySum()).ToList();
        }

        private static List<InterferenceMatrixStat> GenereateStatList(List<InterferenceMatrixMongo> statList)
        {
            var results = Mapper.Map<List<InterferenceMatrixMongo>, IEnumerable<InterferenceMatrixStat>>(statList);
            return (from s in results
                group s by new { s.NeighborPci, s.ENodebId, RecordDate = s.StatDate.Date }
                into g
                select g.Select(x => x).ArraySum()).ToList();
        }

        public async Task<List<InterferenceMatrixMongo>> QueryMongoList(int eNodebId, byte sectorId, DateTime date)
        {
            var cellList = await _mongoRepository.GetListAsync(eNodebId + "-" + sectorId, date);
            return cellList;
        }

        public async Task<IEnumerable<NeighborRsrpView>> QueryNeiborRsrpViews(int eNodebId, byte sectorId, DateTime date)
        {
            var stats= await _mongoRepository.GetListAsync(eNodebId + "-" + sectorId, date);
            var groups = stats.GroupBy(x => x.NeighborEarfcn);
            return groups.Select(x =>
            {
                var view = x.Select(g => g).MapTo<IEnumerable<NeighborRsrpView>>().ArraySum();
                view.StatDate = date.Date;
                view.NeighborEarfcn = x.Key;
                return view;
            });
        }

        public async Task<List<InterferenceMatrixStat>> QueryStats(int eNodebId, byte sectorId, DateTime time)
        {
            var statList = await _mongoRepository.GetListAsync(eNodebId + "-" + sectorId, time);
            return !statList.Any() ? new List<InterferenceMatrixStat>() : GenereateOverallStatList(statList);
        }

        public InterferenceMatrixStat QueryStat(int eNodebId, byte sectorId, short neighborPci, DateTime time)
        {
            var stat = _mongoRepository.Get(eNodebId + "-" + sectorId, neighborPci, time);
            return stat?.MapTo<InterferenceMatrixStat>();
        }

        public async Task<List<InterferenceMatrixStat>> QueryStats(int eNodebId, byte sectorId)
        {
            var stats = await _mongoRepository.GetListAsync(eNodebId + "-" + sectorId);
            return !stats.Any() ? new List<InterferenceMatrixStat>() : GenereateStatList(stats);
        }
    }

    public class InterferenceNeighborService
    {
        private readonly IInterferenceMatrixRepository _repository;
        private readonly INearestPciCellRepository _neighborRepository;
        private readonly IENodebRepository _eNodebRepository;
        private readonly ICellRepository _cellRepository;

        public InterferenceNeighborService(IInterferenceMatrixRepository repository,
            INearestPciCellRepository neighboRepository, IENodebRepository eNodebRepository,
            ICellRepository cellRepository)
        {
            _repository = repository;
            _neighborRepository = neighboRepository;
            _eNodebRepository = eNodebRepository;
            _cellRepository = cellRepository;
        }

        public async Task<int> UpdateNeighbors(int cellId, byte sectorId)
        {
            var count = 0;
            var neighborList = _neighborRepository.GetAllList(cellId, sectorId);
            foreach (var cell in neighborList)
            {
                count += await _repository.UpdateItemsAsync(cellId, sectorId, cell.Pci, cell.NearestCellId, cell.NearestSectorId);
            }
            return _repository.SaveChanges() + count;
        }

        public IEnumerable<InterferenceMatrixView> QueryViews(DateTime begin, DateTime end, int cellId, byte sectorId)
        {
            var statList = _repository.GetAllList(begin, end, cellId, sectorId);
            var results = from stat in statList
                group stat by new
                {
                    stat.ENodebId,
                    stat.SectorId,
                    stat.NeighborPci,
                    stat.NeighborEarfcn,
                    stat.DestENodebId,
                    stat.DestSectorId,
                    stat.Earfcn
                }
                into g
                select new InterferenceMatrixView
                {
                    DestPci = g.Key.NeighborPci,
                    DestENodebId = g.Key.DestENodebId,
                    DestSectorId = g.Key.DestSectorId,
                    NeighborEarfcn = g.Key.NeighborEarfcn,
                    Mod3Interferences = g.Key.Earfcn == g.Key.NeighborEarfcn
                        ? g.Average(x =>
                            x.Pci % 3 == x.NeighborPci % 3 ? (x.Diff0 + x.Diff3 + x.Diff6) : 0)
                        : 0,
                    Mod6Interferences = g.Key.Earfcn == g.Key.NeighborEarfcn
                        ? g.Average(x =>
                            x.Pci % 6 == x.NeighborPci % 6 ? (x.Diff0 + x.Diff3 + x.Diff6) : 0)
                        : 0,
                    OverInterferences10Db = g.Average(x => (x.Diff0 + x.Diff3 + x.Diff6 + x.Diff9)),
                    OverInterferences6Db = g.Average(x => (x.Diff0 + x.Diff3 + x.Diff6)),
                    InterferenceLevel =
                        g.Average(
                            x =>
                                (x.Diff0 + x.Diff3 + x.Diff6) *
                                (x?.NeighborRsrpBelow120 ??
                                 x.RsrpBelow120 + x?.NeighborRsrpBetween120110 ??
                                 x.RsrpBetween120110 + x?.NeighborRsrpBetween110105 ?? x.RsrpBetween110105)),
                    NeighborCellName = "未匹配小区"
                };
            var views = results as InterferenceMatrixView[] ?? results.ToArray();
            foreach (var result in views.Where(x => x.DestENodebId > 0))
            {
                var eNodeb = _eNodebRepository.FirstOrDefault(x => x.ENodebId == result.DestENodebId);
                result.NeighborCellName = eNodeb?.Name + "-" + result.DestSectorId;
            }
            return views;
        }

        public IEnumerable<InterferenceVictimView> QueryVictimViews(DateTime begin, DateTime end, int cellId,
            byte sectorId)
        {
            var statList = _repository.GetAllVictims(begin, end, cellId, sectorId);
            var results = from stat in statList
                group stat by new {stat.ENodebId, stat.SectorId, stat.NeighborPci, stat.DestENodebId, stat.DestSectorId}
                into g
                select new InterferenceVictimView
                {
                    VictimENodebId = g.Key.ENodebId,
                    VictimSectorId = g.Key.SectorId,
                    Mod3Interferences =
                        g.Average(x => x.Pci % 3 == x.NeighborPci % 3 ? (x.Diff0 + x.Diff3 + x.Diff6) : 0),
                    Mod6Interferences =
                        g.Average(x => x.Pci % 6 == x.NeighborPci % 6 ? (x.Diff0 + x.Diff3 + x.Diff6) : 0),
                    OverInterferences10Db = g.Average(x => (x.Diff0 + x.Diff3 + x.Diff6 + x.Diff9)),
                    OverInterferences6Db = g.Average(x => (x.Diff0 + x.Diff3 + x.Diff6)),
                    InterferenceLevel =
                        g.Average(
                            x =>
                                (x.Diff0 + x.Diff3 + x.Diff6) *
                                (x?.NeighborRsrpBelow120 ??
                                 x.RsrpBelow120 + x?.NeighborRsrpBetween120110 ??
                                 x.RsrpBetween120110 + x?.NeighborRsrpBetween110105 ?? x.RsrpBetween110105)),
                    VictimCellName = "未匹配小区"
                };
            var victims = results as InterferenceVictimView[] ?? results.ToArray();
            foreach (var victim in victims)
            {
                var eNodeb = _eNodebRepository.FirstOrDefault(x => x.ENodebId == victim.VictimENodebId);
                victim.VictimCellName = eNodeb?.Name + "-" + victim.VictimSectorId;
                var cell = _cellRepository.GetBySectorId(victim.VictimENodebId, victim.VictimSectorId);
                victim.VictimPci = cell?.Pci ?? 0;
            }
            return victims;
        }
    }
}
