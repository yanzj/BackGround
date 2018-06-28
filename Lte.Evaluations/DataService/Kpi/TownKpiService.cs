using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Lte.Domain.Common.Wireless;
using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.DataService.Switch;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;

namespace Lte.Evaluations.DataService.Kpi
{
    public class TownKpiService
    {
        private readonly BandCellService _cellService;
        private readonly IENodebRepository _eNodebRepository;
        private readonly IFlowHuaweiRepository _huaweiRepository;
        private readonly IFlowZteRepository _zteRepository;
        private readonly IRrcHuaweiRepository _rrcHuaweiRepository;
        private readonly IRrcZteRepository _rrcZteRepository;
        private readonly IQciZteRepository _qciZteRepository;
        private readonly IQciHuaweiRepository _qciHuaweiRepository;
        private readonly ICqiZteRepository _cqiZteRepository;
        private readonly ICqiHuaweiRepository _cqiHuaweiRepository;
        private readonly IPrbHuaweiRepository _prbHuaweiRepository;
        private readonly IPrbZteRepository _prbZteRepository;
        private readonly IDoubleFlowHuaweiRepository _doubleFlowHuaweiRepository;
        private readonly IDoubleFlowZteRepository _doubleFlowZteRepository;
        private readonly ITownFlowRepository _townFlowRepository;
        private readonly ITownRrcRepository _townRrcRepository;
        private readonly ITownQciRepository _townQciRepository;
        private readonly ITownCqiRepository _townCqiRepository;
        private readonly ITownDoubleFlowRepository _townDoubleFlowRepository;
        private readonly ITownPrbRepository _townPrbRepository;

        public TownKpiService(
            BandCellService cellService, IFlowZteRepository zteRepository,
            IENodebRepository eNodebRepository, IFlowHuaweiRepository huaweiRepositroy,
            IRrcHuaweiRepository rrcHuaweiRepository, IRrcZteRepository rrcZteRepository,
            IQciZteRepository qciZteRepository, IQciHuaweiRepository qciHuaweiRepository,
            ICqiZteRepository cqiZteRepository, ICqiHuaweiRepository cqiHuaweiRepository,
            IPrbZteRepository prbZteRepository, IPrbHuaweiRepository prbHuaweiRepository,
            IDoubleFlowHuaweiRepository doubleFlowHuaweiRepository, IDoubleFlowZteRepository doubleFlowZteRepository,
            ITownFlowRepository townFlowRepository, ITownPrbRepository townPrbRepository,
            ITownCqiRepository townCqiRepository, ITownDoubleFlowRepository townDoubleFlowRepository,
            ITownRrcRepository townRrcRepository, ITownQciRepository townQciRepository)
        {
            _cellService = cellService;
            _eNodebRepository = eNodebRepository;
            _zteRepository = zteRepository;
            _huaweiRepository = huaweiRepositroy;
            _rrcHuaweiRepository = rrcHuaweiRepository;
            _rrcZteRepository = rrcZteRepository;
            _qciZteRepository = qciZteRepository;
            _qciHuaweiRepository = qciHuaweiRepository;
            _cqiZteRepository = cqiZteRepository;
            _cqiHuaweiRepository = cqiHuaweiRepository;
            _prbHuaweiRepository = prbHuaweiRepository;
            _prbZteRepository = prbZteRepository;
            _doubleFlowHuaweiRepository = doubleFlowHuaweiRepository;
            _doubleFlowZteRepository = doubleFlowZteRepository;
            _townFlowRepository = townFlowRepository;
            _townRrcRepository = townRrcRepository;
            _townQciRepository = townQciRepository;
            _townCqiRepository = townCqiRepository;
            _townDoubleFlowRepository = townDoubleFlowRepository;
            _townPrbRepository = townPrbRepository;
        }

        private List<TownFlowStat> GetTownFlowStats(DateTime statDate,
            FrequencyBandType frequency = FrequencyBandType.All)
        {
            var end = statDate.AddDays(1);
            var townStatList = new List<TownFlowStat>();
            var huaweiFlows = _huaweiRepository.GetAllList(x => x.StatTime >= statDate && x.StatTime < end);
            var zteFlows = _zteRepository.QueryZteFlows<FlowZte, IFlowZteRepository>(frequency, statDate, end);
            if (frequency != FrequencyBandType.All)
            {
                var cells = _cellService.GetHuaweiCellsByBandType(frequency);
                huaweiFlows = (from f in huaweiFlows
                               join c in cells on new { f.ENodebId, f.LocalCellId } equals
                               new { c.ENodebId, LocalCellId = c.LocalSectorId }
                               select f).ToList();
            }
            townStatList.AddRange(huaweiFlows.GetTownStats<FlowHuawei, TownFlowStat>(_eNodebRepository));
            townStatList.AddRange(zteFlows.GetTownStats<FlowZte, TownFlowStat>(_eNodebRepository));
            return townStatList;
        }

        private List<TownRrcStat> GetTownRrcStats(DateTime statDate)
        {
            var end = statDate.AddDays(1);
            var townStatList = new List<TownRrcStat>();
            var huaweiRrcs = _rrcHuaweiRepository.GetAllList(x => x.StatTime >= statDate && x.StatTime < end);
            townStatList.AddRange(huaweiRrcs.GetTownStats<RrcHuawei, TownRrcStat>(_eNodebRepository));
            var zteRrcs = _rrcZteRepository.GetAllList(x => x.StatTime >= statDate && x.StatTime < end);
            townStatList.AddRange(zteRrcs.GetTownStats<RrcZte, TownRrcStat>(_eNodebRepository));
            return townStatList;
        }

        private List<TownQciStat> GetTownQciStats(DateTime statDate)
        {
            var end = statDate.AddDays(1);
            var townStatList = new List<TownQciStat>();
            var huaweiQcis = _qciHuaweiRepository.GetAllList(x => x.StatTime >= statDate && x.StatTime < end);
            townStatList.AddRange(huaweiQcis.GetTownStats<QciHuawei, TownQciStat>(_eNodebRepository));
            var zteQcis = _qciZteRepository.GetAllList(x => x.StatTime >= statDate && x.StatTime < end);
            townStatList.AddRange(zteQcis.GetTownStats<QciZte, TownQciStat>(_eNodebRepository));
            return townStatList;
        }

        private List<TownCqiStat> GetTownCqiStats(DateTime statDate)
        {
            var end = statDate.AddDays(1);
            var townStatList = new List<TownCqiStat>();
            var huaweiCqis = _cqiHuaweiRepository.GetAllList(x => x.StatTime >= statDate && x.StatTime < end);
            townStatList.AddRange(huaweiCqis.GetTownStats<CqiHuawei, TownCqiStat>(_eNodebRepository));
            var zteCqis = _cqiZteRepository.GetAllList(x => x.StatTime >= statDate && x.StatTime < end);
            townStatList.AddRange(zteCqis.GetTownStats<CqiZte, TownCqiStat>(_eNodebRepository));
            return townStatList;
        }

        private List<TownPrbStat> GetTownPrbStats(DateTime statDate)
        {
            var end = statDate.AddDays(1);
            var townStatList = new List<TownPrbStat>();
            var huaweiPrbs = _prbHuaweiRepository.GetAllList(x => x.StatTime >= statDate && x.StatTime < end);
            townStatList.AddRange(huaweiPrbs.GetTownStats<PrbHuawei, TownPrbStat>(_eNodebRepository));
            var ztePrbs = _prbZteRepository.GetAllList(x => x.StatTime >= statDate && x.StatTime < end);
            townStatList.AddRange(ztePrbs.GetTownStats<PrbZte, TownPrbStat>(_eNodebRepository));
            return townStatList;
        }

        private List<TownDoubleFlow> GetTownDoubleFlowStats(DateTime statDate)
        {
            var end = statDate.AddDays(1);
            var townStatList = new List<TownDoubleFlow>();
            var huaweiDoubleFlows = _doubleFlowHuaweiRepository.GetAllList(x => x.StatTime >= statDate && x.StatTime < end);
            townStatList.AddRange(huaweiDoubleFlows.GetTownStats<DoubleFlowHuawei, TownDoubleFlow>(_eNodebRepository));
            var zteDoubleFlows = _doubleFlowZteRepository.GetAllList(x => x.StatTime >= statDate && x.StatTime < end);
            townStatList.AddRange(zteDoubleFlows.GetTownStats<DoubleFlowZte, TownDoubleFlow>(_eNodebRepository));
            return townStatList;
        }

        public async Task<int> GenerateTownCqis(DateTime statTime)
        {
            var end = statTime.AddDays(1);
            var count = _townCqiRepository.Count(x => x.StatTime >= statTime && x.StatTime < end);
            if (count == 0)
            {
                var townCqiList = GetTownCqiStats(statTime);
                foreach (var stat in townCqiList.GetPositionMergeStats(statTime))
                {
                    await _townCqiRepository.InsertAsync(stat);
                }
                count = _townCqiRepository.SaveChanges();
            }
            return count;
        }

        public async Task<int[]> GenerateTownStats(DateTime statDate)
        {
            var end = statDate.AddDays(1);
            var item1 =
                _townFlowRepository.Count(
                    x => x.StatTime >= statDate && x.StatTime < end && x.FrequencyBandType == FrequencyBandType.All);
            if (item1 < 44)
            {
                var townStatList = GetTownFlowStats(statDate);
                foreach (var stat in townStatList.GetPositionMergeStats(statDate))
                {
                    stat.FrequencyBandType = FrequencyBandType.All;
                    var subItem = _townFlowRepository.FirstOrDefault(
                        x => x.StatTime >= statDate && x.StatTime < end && x.TownId == stat.TownId
                        && x.FrequencyBandType == FrequencyBandType.All);
                    if (subItem == null)
                        await _townFlowRepository.InsertAsync(stat);
                    else
                    {
                        var oldId = subItem.Id;
                        stat.MapTo(subItem);
                        subItem.Id = oldId;
                    }
                }
                item1 += _townFlowRepository.SaveChanges();
            }
            var item2 = _townRrcRepository.Count(x => x.StatTime >= statDate && x.StatTime < end);
            if (item2 < 44)
            {
                var townRrcList = GetTownRrcStats(statDate);
                foreach (var stat in townRrcList.GetPositionMergeStats(statDate))
                {
                    var subItem = _townRrcRepository.FirstOrDefault(
                        x => x.StatTime >= statDate && x.StatTime < end && x.TownId == stat.TownId);
                    if (subItem == null)
                        await _townRrcRepository.InsertAsync(stat);
                    else
                    {
                        var oldId = subItem.Id;
                        stat.MapTo(subItem);
                        subItem.Id = oldId;
                    }
                }
                item2 += _townRrcRepository.SaveChanges();
            }
            var item3 =
                _townFlowRepository.Count(
                    x => x.StatTime >= statDate && x.StatTime < end && x.FrequencyBandType == FrequencyBandType.Band2100);
            if (item3 < 44)
            {
                var townStatList = GetTownFlowStats(statDate, FrequencyBandType.Band2100);
                foreach (var stat in townStatList.GetPositionMergeStats(statDate))
                {
                    stat.FrequencyBandType = FrequencyBandType.Band2100;
                    var subItem = _townFlowRepository.FirstOrDefault(
                        x => x.StatTime >= statDate && x.StatTime < end && x.TownId == stat.TownId
                        && x.FrequencyBandType == FrequencyBandType.Band2100);
                    if (subItem == null)
                        await _townFlowRepository.InsertAsync(stat);
                    else
                    {
                        var oldId = subItem.Id;
                        stat.MapTo(subItem);
                        subItem.Id = oldId;
                    }
                }
                item3 += _townFlowRepository.SaveChanges();
            }
            var item4 =
                _townFlowRepository.Count(
                    x => x.StatTime >= statDate && x.StatTime < end && x.FrequencyBandType == FrequencyBandType.Band1800);
            if (item4 < 44)
            {
                var townStatList = GetTownFlowStats(statDate, FrequencyBandType.Band1800);
                foreach (var stat in townStatList.GetPositionMergeStats(statDate))
                {
                    stat.FrequencyBandType = FrequencyBandType.Band1800;
                    var subItem = _townFlowRepository.FirstOrDefault(
                        x => x.StatTime >= statDate && x.StatTime < end && x.TownId == stat.TownId
                        && x.FrequencyBandType == FrequencyBandType.Band1800);
                    if (subItem == null)
                        await _townFlowRepository.InsertAsync(stat);
                    else
                    {
                        var oldId = subItem.Id;
                        stat.MapTo(subItem);
                        subItem.Id = oldId;
                    }
                }
                item4 += _townFlowRepository.SaveChanges();
            }
            var item5 =
                _townFlowRepository.Count(
                    x => x.StatTime >= statDate && x.StatTime < end && x.FrequencyBandType == FrequencyBandType.Band800VoLte);
            if (item5 < 44)
            {
                var townStatList = GetTownFlowStats(statDate, FrequencyBandType.Band800VoLte);
                foreach (var stat in townStatList.GetPositionMergeStats(statDate))
                {
                    stat.FrequencyBandType = FrequencyBandType.Band800VoLte;
                    var subItem = _townFlowRepository.FirstOrDefault(
                        x => x.StatTime >= statDate && x.StatTime < end && x.TownId == stat.TownId
                        && x.FrequencyBandType == FrequencyBandType.Band800VoLte);
                    if (subItem == null)
                        await _townFlowRepository.InsertAsync(stat);
                    else
                    {
                        var oldId = subItem.Id;
                        stat.MapTo(subItem);
                        subItem.Id = oldId;
                    }
                }
                item5 += _townFlowRepository.SaveChanges();
            }
            var item6 = _townQciRepository.Count(x => x.StatTime >= statDate && x.StatTime < end);
            if (item6 < 44)
            {
                var townQciList = GetTownQciStats(statDate);
                foreach (var stat in townQciList.GetPositionMergeStats(statDate))
                {
                    var subItem = _townQciRepository.FirstOrDefault(
                        x => x.StatTime >= statDate && x.StatTime < end && x.TownId == stat.TownId);
                    if (subItem == null)
                        await _townQciRepository.InsertAsync(stat);
                    else
                    {
                        var oldId = subItem.Id;
                        stat.MapTo(subItem);
                        subItem.Id = oldId;
                    }
                }
                item6 += _townQciRepository.SaveChanges();
            }
            var item7 = _townPrbRepository.Count(x => x.StatTime >= statDate && x.StatTime < end);
            if (item7 < 44)
            {
                var townPrbList = GetTownPrbStats(statDate);
                foreach (var stat in townPrbList.GetPositionMergeStats(statDate))
                {
                    var subItem = _townPrbRepository.FirstOrDefault(
                        x => x.StatTime >= statDate && x.StatTime < end && x.TownId == stat.TownId);
                    if (subItem == null)
                        await _townPrbRepository.InsertAsync(stat);
                    else
                    {
                        var oldId = subItem.Id;
                        stat.MapTo(subItem);
                        subItem.Id = oldId;
                    }
                }
                item7 += _townPrbRepository.SaveChanges();
            }
            var item8 = _townDoubleFlowRepository.Count(x => x.StatTime >= statDate && x.StatTime < end);
            if (item8 < 44)
            {
                var townDoubleFlowList = GetTownDoubleFlowStats(statDate);
                foreach (var stat in townDoubleFlowList.GetPositionMergeStats(statDate))
                {
                    var subItem =
                        _townDoubleFlowRepository.FirstOrDefault(
                            x => x.StatTime >= statDate && x.StatTime < end && x.TownId == stat.TownId);
                    if (subItem == null)
                        await _townDoubleFlowRepository.InsertAsync(stat);
                    else
                    {
                        var oldId = subItem.Id;
                        stat.MapTo(subItem);
                        subItem.Id = oldId;
                    }
                }
                item8 += _townDoubleFlowRepository.SaveChanges();
            }
            return new [] {item1, item2, item3, item4, item5, item6, item7, item8};
        }

    }
}
