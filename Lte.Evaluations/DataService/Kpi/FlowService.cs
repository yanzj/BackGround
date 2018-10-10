using AutoMapper;
using Lte.Domain.Common.Geo;
using Lte.Domain.Regular;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common;
using Lte.Domain.Common.Wireless.Cell;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Kpi;
using Lte.MySqlFramework.Abstract.RegionKpi;
using Lte.Parameters.Entities.Kpi;
using Abp.EntityFramework.AutoMapper;

namespace Lte.Evaluations.DataService.Kpi
{
    public class FlowService
    {
        private readonly IFlowHuaweiRepository _huaweiRepository;
        private readonly IFlowZteRepository _zteRepository;
        private readonly IRrcZteRepository _rrcZteRepository;
        private readonly IRrcHuaweiRepository _rrcHuaweiRepository;
        private readonly IQciZteRepository _qciZteRepository;
        private readonly IQciHuaweiRepository _qciHuaweiRepository;
        private readonly ICqiZteRepository _cqiZteRepository;
        private readonly ICqiHuaweiRepository _cqiHuaweiRepository;
        private readonly IDoubleFlowHuaweiRepository _doubleFlowHuaweiRepository;
        private readonly IDoubleFlowZteRepository _doubleFlowZteRepository;
        private readonly ITownFlowRepository _townFlowRepository;
        private readonly IENodebRepository _eNodebRepository;
        private readonly ITownRrcRepository _townRrcRepository;
        private readonly ITownQciRepository _townQciRepository;
        private readonly ITownCqiRepository _townCqiRepository;
        private readonly ITownDoubleFlowRepository _townDoubleFlowRepository;
        private readonly ICellRepository _cellRepository;
        private readonly IPrbHuaweiRepository _prbHuaweiRepository;
        private readonly IPrbZteRepository _prbZteRepository;
        private readonly ITownPrbRepository _townPrbRepository;
        private readonly IRssiHuaweiRepository _rssiHuaweiRepository;
        private readonly IRssiZteRepository _rssiZteRepository;


        private static Stack<Tuple<FlowHuawei, RrcHuawei, QciHuawei, PrbHuawei>> FlowHuaweis { get; set; }

        private static Stack<Tuple<FlowZte, RrcZte, QciZte, PrbZte, CqiZte, DoubleFlowZte, RssiZte>> FlowZtes { get; set; }

        private static Stack<Tuple<CqiHuawei, DoubleFlowHuawei>> CqiHuaweis { get; set; }

        private static Stack<RssiHuawei> RssiHuaweis { get; set; }

        public int FlowHuaweiCount => FlowHuaweis.Count;

        public int FlowZteCount => FlowZtes.Count;

        public int FlowHuaweiCqiCount => CqiHuaweis.Count;

        public int RssiHuaweiCount => RssiHuaweis.Count;

        public FlowService(
            IFlowHuaweiRepository huaweiRepositroy, IFlowZteRepository zteRepository,
            IRrcZteRepository rrcZteRepository, IRrcHuaweiRepository rrcHuaweiRepository,
            IQciZteRepository qciZteRepository, IQciHuaweiRepository qciHuaweiRepository,
            ICqiZteRepository cqiZteRepository, ICqiHuaweiRepository cqiHuaweiRepository,
            IDoubleFlowHuaweiRepository doubleFlowHuaweiRepository, IDoubleFlowZteRepository doubleFlowZteRepository,
            ITownFlowRepository townFlowRepository, IENodebRepository eNodebRepository,
            ITownRrcRepository townRrcRepository, ITownQciRepository townQciRepository,
            ITownCqiRepository townCqiRepository, ITownDoubleFlowRepository townDoubleFlowRepository,
            ICellRepository cellRepository, IPrbHuaweiRepository prbHuaweiRepository,
            IPrbZteRepository prbZteRepository, ITownPrbRepository townPrbRepository,
            IRssiHuaweiRepository rssiHuaweiRepository, IRssiZteRepository rssiZteRepository)
        {
            _huaweiRepository = huaweiRepositroy;
            _zteRepository = zteRepository;
            _rrcZteRepository = rrcZteRepository;
            _rrcHuaweiRepository = rrcHuaweiRepository;
            _qciZteRepository = qciZteRepository;
            _qciHuaweiRepository = qciHuaweiRepository;
            _cqiZteRepository = cqiZteRepository;
            _cqiHuaweiRepository = cqiHuaweiRepository;
            _doubleFlowHuaweiRepository = doubleFlowHuaweiRepository;
            _doubleFlowZteRepository = doubleFlowZteRepository;
            _townFlowRepository = townFlowRepository;
            _eNodebRepository = eNodebRepository;
            _townRrcRepository = townRrcRepository;
            _townQciRepository = townQciRepository;
            _townCqiRepository = townCqiRepository;
            _townDoubleFlowRepository = townDoubleFlowRepository;
            _cellRepository = cellRepository;
            _prbHuaweiRepository = prbHuaweiRepository;
            _prbZteRepository = prbZteRepository;
            _townPrbRepository = townPrbRepository;
            _rssiHuaweiRepository = rssiHuaweiRepository;
            _rssiZteRepository = rssiZteRepository;

            if (FlowHuaweis == null) FlowHuaweis = new Stack<Tuple<FlowHuawei, RrcHuawei, QciHuawei, PrbHuawei>>();
            if (FlowZtes == null) FlowZtes = new Stack<Tuple<FlowZte, RrcZte, QciZte, PrbZte, CqiZte, DoubleFlowZte, RssiZte>>();
            if (CqiHuaweis == null) CqiHuaweis = new Stack<Tuple<CqiHuawei, DoubleFlowHuawei>>();
            if (RssiHuaweis == null) RssiHuaweis = new Stack<RssiHuawei>();
        }

        public void UploadFlowHuaweis(StreamReader reader)
        {
            var originCsvs = FlowHuaweiCsv.ReadFlowHuaweiCsvs(reader);
            var mergedCsvs = (from item in originCsvs
                group item by new
                {
                    item.StatTime.Date,
                    item.CellInfo
                }
                into g
                select g.ArrayAggration(stat =>
                {
                    stat.StatTime = g.Key.Date;
                    stat.CellInfo = g.Key.CellInfo;
                })).ToList();
            foreach (var csv in mergedCsvs)
            {
                FlowHuaweis.Push(
                    new Tuple<FlowHuawei, RrcHuawei, QciHuawei, PrbHuawei>(Mapper.Map<FlowHuaweiCsv, FlowHuawei>(csv),
                        Mapper.Map<FlowHuaweiCsv, RrcHuawei>(csv), Mapper.Map<FlowHuaweiCsv, QciHuawei>(csv),
                        Mapper.Map<FlowHuaweiCsv, PrbHuawei>(csv)));
            }
        }

        public void UploadFlowZtes(StreamReader reader)
        {
            var csvs = FlowZteCsv.ReadFlowZteCsvs(reader);
            foreach (var csv in csvs)
            {
                FlowZtes.Push(new Tuple<FlowZte, RrcZte, QciZte, PrbZte, CqiZte, DoubleFlowZte, RssiZte>(
                    Mapper.Map<FlowZteCsv, FlowZte>(csv),
                    Mapper.Map<FlowZteCsv, RrcZte>(csv), 
                    Mapper.Map<FlowZteCsv, QciZte>(csv),
                    Mapper.Map<FlowZteCsv, PrbZte>(csv),
                    Mapper.Map<FlowZteCsv, CqiZte>(csv),
                    Mapper.Map<FlowZteCsv, DoubleFlowZte>(csv),
                    Mapper.Map<FlowZteCsv, RssiZte>(csv)
                    ));
            }
        }

        public void UploadCqiHuaweis(StreamReader reader)
        {
            var originCsvs = CqiHuaweiCsv.ReadFlowHuaweiCsvs(reader);
            var mergedCsvs = (from item in originCsvs
                              group item by new
                              {
                                  item.StatTime.Date,
                                  item.CellInfo
                              }
                into g
                              select g.ArrayAggration(stat =>
                              {
                                  stat.StatTime = g.Key.Date;
                                  stat.CellInfo = g.Key.CellInfo;
                              })).ToList();
            foreach (var csv in mergedCsvs)
            {
                CqiHuaweis.Push(new Tuple<CqiHuawei, DoubleFlowHuawei>(
                    Mapper.Map<CqiHuaweiCsv, CqiHuawei>(csv),
                    Mapper.Map<CqiHuaweiCsv, DoubleFlowHuawei>(csv)));
            }
        }

        public void UploadRssiHuaweis(StreamReader reader)
        {
            var originCsvs = RssiHuaweiCsv.ReadRssiHuaweiCsvs(reader).MapTo<List<RssiHuawei>>();
            var mergedCsvs = (from item in originCsvs
                              group item by new
                              {
                                  item.StatTime.Date,
                                  item.ENodebId,
                                  item.SectorId
                              }
                into g
                              select g.ArrayAggration(stat =>
                              {
                                  stat.StatTime = g.Key.Date;
                                  stat.ENodebId = g.Key.ENodebId;
                                  stat.SectorId = g.Key.SectorId;
                              })).ToList();
            foreach(var csv in mergedCsvs)
            {
                RssiHuaweis.Push(csv);
            }
        }

        public async Task<bool> DumpOneHuaweiStat()
        {
            var stat = FlowHuaweis.Pop();
            if (stat.Item1 != null)
            {
                await _huaweiRepository.ImportOneAsync(stat.Item1);
            }
            if (stat.Item2 != null)
            {
                await _rrcHuaweiRepository.ImportOneAsync(stat.Item2);
            }
            if (stat.Item3 != null)
            {
                await _qciHuaweiRepository.ImportOneAsync(stat.Item3);
            }
            if (stat.Item4 != null)
            {
                await _prbHuaweiRepository.ImportOneAsync(stat.Item4);
            }

            return true;
        }

        public async Task<bool> DumpOneZteStat()
        {
            var stat = FlowZtes.Pop();
            if (stat.Item1 != null)
            {
                await _zteRepository.ImportOneAsync(stat.Item1);
            }
            if (stat.Item2 != null)
            {
                await _rrcZteRepository.ImportOneAsync(stat.Item2);
            }
            if (stat.Item3 != null)
            {
                await _qciZteRepository.ImportOneAsync(stat.Item3);
            }
            if (stat.Item4 != null)
            {
                await _prbZteRepository.ImportOneAsync(stat.Item4);
            }
            if (stat.Item5 != null)
            {
                await _cqiZteRepository.ImportOneAsync(stat.Item5);
            }
            if (stat.Item6 != null)
            {
                await _doubleFlowZteRepository.ImportOneAsync(stat.Item6);
            }
            if (stat.Item7 !=null)
            {
                await _rssiZteRepository.ImportOneAsync(stat.Item7);
            }

            return true;
        }

        public async Task<bool> DumpOneHuaweiCqiStat()
        {
            var stat = CqiHuaweis.Pop();
            if (stat.Item1 != null)
            {
                await _cqiHuaweiRepository.ImportOneAsync(stat.Item1);
            }
            if (stat.Item2 != null)
            {
                await _doubleFlowHuaweiRepository.ImportOneAsync(stat.Item2);
            }
            return true;
        }

        public async Task<bool> DumpOneHuaweiRssiStat()
        {
            var stat = RssiHuaweis.Pop();
            if (stat != null)
            {
                await _rssiHuaweiRepository.ImportOneAsync(stat);
            }
            return true;
        }

        public void ClearHuaweiStats()
        {
            FlowHuaweis.Clear();
        }

        public void ClearZteStats()
        {
            FlowZtes.Clear();
        }

        public void ClearHuaweiCqiStats()
        {
            CqiHuaweis.Clear();
        }

        public void ClearHuaweiRssiStats()
        {
            RssiHuaweis.Clear();
        }

        public async Task<IEnumerable<FlowHistory>> GetFlowHistories(DateTime begin, DateTime end)
        {
            var results = new List<FlowHistory>();
            while (begin < end.AddDays(1))
            {
                var beginDate = begin;
                var endDate = begin.AddDays(1);
                var huaweiItems = await _huaweiRepository.CountAsync(x => x.StatTime >= beginDate && x.StatTime < endDate);
                var huaweiCqis =
                    await _cqiHuaweiRepository.CountAsync(x => x.StatTime >= beginDate && x.StatTime < endDate);
                var huaweiPrbs =
                    await _prbHuaweiRepository.CountAsync(x => x.StatTime >= beginDate && x.StatTime < endDate);
                var huaweiRssis =
                    await _rssiHuaweiRepository.CountAsync(x => x.StatTime >= beginDate && x.StatTime < endDate);
                var zteItems = await _zteRepository.CountAsync(x => x.StatTime >= beginDate && x.StatTime < endDate);
                var zteCqis = await _cqiZteRepository.CountAsync(x => x.StatTime >= beginDate && x.StatTime < endDate);
                var ztePrbs = await _prbZteRepository.CountAsync(x => x.StatTime >= beginDate && x.StatTime < endDate);
                var zteRssis = await _rssiZteRepository.CountAsync(x => x.StatTime >= beginDate && x.StatTime < endDate);
                var townItems =
                    await _townFlowRepository.CountAsync(
                        x =>
                            x.StatTime >= beginDate && x.StatTime < endDate &&
                            x.FrequencyBandType == FrequencyBandType.All); 
                var townItems2100 =
                    await _townFlowRepository.CountAsync(
                        x =>
                            x.StatTime >= beginDate && x.StatTime < endDate &&
                            x.FrequencyBandType == FrequencyBandType.Band2100);
                var townItems1800 =
                    await _townFlowRepository.CountAsync(
                        x =>
                            x.StatTime >= beginDate && x.StatTime < endDate &&
                            x.FrequencyBandType == FrequencyBandType.Band1800);
                var townItems800 =
                    await _townFlowRepository.CountAsync(
                        x =>
                            x.StatTime >= beginDate && x.StatTime < endDate &&
                            x.FrequencyBandType == FrequencyBandType.Band800VoLte);
                var townRrcs = await _townRrcRepository.CountAsync(x => x.StatTime >= beginDate && x.StatTime < endDate);
                var townQcis = await _townQciRepository.CountAsync(x => x.StatTime >= beginDate && x.StatTime < endDate);
                var townCqis = await _townCqiRepository.CountAsync(x => x.StatTime >= beginDate && x.StatTime < endDate 
                    && x.FrequencyBandType == FrequencyBandType.All);
                var townCqis2100 =
                    await _townCqiRepository.CountAsync(
                        x =>
                            x.StatTime >= beginDate && x.StatTime < endDate &&
                            x.FrequencyBandType == FrequencyBandType.Band2100);
                var townCqis1800 =
                    await _townCqiRepository.CountAsync(
                        x =>
                            x.StatTime >= beginDate && x.StatTime < endDate &&
                            x.FrequencyBandType == FrequencyBandType.Band1800);
                var townCqis800 =
                    await _townCqiRepository.CountAsync(
                        x =>
                            x.StatTime >= beginDate && x.StatTime < endDate &&
                            x.FrequencyBandType == FrequencyBandType.Band800VoLte);
                var townPrbs = await _townPrbRepository.CountAsync(x => x.StatTime >= beginDate && x.StatTime < endDate);
                var townDoubleFlows =
                    await _townDoubleFlowRepository.CountAsync(x => x.StatTime >= beginDate && x.StatTime < endDate);
                results.Add(new FlowHistory
                {
                    DateString = begin.ToShortDateString(),
                    HuaweiItems = huaweiItems,
                    HuaweiCqis = huaweiCqis,
                    HuaweiPrbs = huaweiPrbs,
                    HuaweiRssis = huaweiRssis,
                    ZteItems = zteItems,
                    ZteCqis = zteCqis,
                    ZtePrbs = ztePrbs,
                    ZteRssis = zteRssis,
                    TownStats = townItems,
                    TownStats2100 = townItems2100,
                    TownStats1800 = townItems1800,
                    TownStats800VoLte = townItems800,
                    TownRrcs = townRrcs,
                    TownQcis = townQcis,
                    TownCqis = townCqis,
                    TownCqis2100 = townCqis2100,
                    TownCqis1800 = townCqis1800,
                    TownCqis800VoLte = townCqis800,
                    TownPrbs = townPrbs,
                    TownDoubleFlows = townDoubleFlows
                });
                begin = begin.AddDays(1);
            }
            return results;
        }

        public IEnumerable<CellIdPair> QueryUnmatchedHuaweis(int townId, DateTime date)
        {
            var eNodebs = _eNodebRepository.GetAllList(x => x.TownId == townId);
            var cells = from cell in _cellRepository.GetAllList()
                        join eNodeb in eNodebs on cell.ENodebId equals eNodeb.ENodebId
                        select cell;
            var stats = _huaweiRepository.GetAllList(x => x.StatTime >= date);
            var townStats = from stat in stats
                join eNodeb in eNodebs on stat.ENodebId equals eNodeb.ENodebId
                select stat;
            return from stat in townStats
                join cell in cells on new {stat.ENodebId, stat.LocalCellId} equals
                new {cell.ENodebId, LocalCellId = cell.LocalSectorId}
                into match
                from m in match.DefaultIfEmpty()
                   where m == null
                select new CellIdPair
                {
                    CellId = stat.ENodebId,
                    SectorId = stat.LocalCellId
                };
        }

    }
}

