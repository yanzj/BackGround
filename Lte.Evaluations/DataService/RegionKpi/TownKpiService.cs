using System;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Evaluations.DataService.Dump;
using Lte.MySqlFramework.Abstract.RegionKpi;

namespace Lte.Evaluations.DataService.RegionKpi
{
    public class TownKpiService
    {
        private readonly ITownFlowRepository _townFlowRepository;
        private readonly ITownRrcRepository _townRrcRepository;
        private readonly ITownQciRepository _townQciRepository;
        private readonly ITownCqiRepository _townCqiRepository;
        private readonly ITownDoubleFlowRepository _townDoubleFlowRepository;
        private readonly ITownPrbRepository _townPrbRepository;
        private readonly RegionTownFlowService _flowService;
        private readonly TownRrcService _rrcService;
        private readonly TownQciService _qciService;
        private readonly DumpCqiService _cqiService;
        private readonly TownPrbService _prbService;
        private readonly TownDoubleFlowService _doubleFlowService;

        public TownKpiService(ITownFlowRepository townFlowRepository, ITownPrbRepository townPrbRepository,
            ITownCqiRepository townCqiRepository, ITownDoubleFlowRepository townDoubleFlowRepository,
            ITownRrcRepository townRrcRepository, ITownQciRepository townQciRepository,
            RegionTownFlowService flowService, TownRrcService rrcService,
            TownQciService qciService, DumpCqiService cqiService, TownPrbService prbService,
            TownDoubleFlowService doubleFlowService)
        {
            _townFlowRepository = townFlowRepository;
            _townRrcRepository = townRrcRepository;
            _townQciRepository = townQciRepository;
            _townCqiRepository = townCqiRepository;
            _townDoubleFlowRepository = townDoubleFlowRepository;
            _townPrbRepository = townPrbRepository;
            _flowService = flowService;
            _rrcService = rrcService;
            _qciService = qciService;
            _cqiService = cqiService;
            _prbService = prbService;
            _doubleFlowService = doubleFlowService;
        }

        public async Task<int[]> GenerateTownCqis(DateTime statTime)
        {
            var end = statTime.AddDays(1);
            var count0 = _townCqiRepository.Count(x => x.StatTime >= statTime && x.StatTime < end && x.FrequencyBandType == FrequencyBandType.All);
            if (count0 == 0)
            {
                var townCqiList = _cqiService.GetTownCqiStats(statTime);
                foreach (var stat in townCqiList.GetPositionMergeStats(statTime))
                {
                    await _townCqiRepository.InsertAsync(stat);
                }
                count0 = _townCqiRepository.SaveChanges();
            }
            var count1 = _townCqiRepository.Count(x => x.StatTime >= statTime && x.StatTime < end && x.FrequencyBandType == FrequencyBandType.Band2100);
            if (count1 == 0)
            {
                var townCqiList = _cqiService.GetTownCqiStats(statTime);
                foreach (var stat in townCqiList.GetPositionMergeStats(statTime))
                {
                    await _townCqiRepository.InsertAsync(stat);
                }
                count1 = _townCqiRepository.SaveChanges();
            }
            var count2 = _townCqiRepository.Count(x => x.StatTime >= statTime && x.StatTime < end && x.FrequencyBandType == FrequencyBandType.Band1800);
            if (count2 == 0)
            {
                var townCqiList = _cqiService.GetTownCqiStats(statTime);
                foreach (var stat in townCqiList.GetPositionMergeStats(statTime))
                {
                    await _townCqiRepository.InsertAsync(stat);
                }
                count2 = _townCqiRepository.SaveChanges();
            }
            var count3 = _townCqiRepository.Count(x => x.StatTime >= statTime && x.StatTime < end && x.FrequencyBandType == FrequencyBandType.Band800VoLte);
            if (count3 == 0)
            {
                var townCqiList = _cqiService.GetTownCqiStats(statTime);
                foreach (var stat in townCqiList.GetPositionMergeStats(statTime))
                {
                    await _townCqiRepository.InsertAsync(stat);
                }
                count3 = _townCqiRepository.SaveChanges();
            }
            return new[] { count0, count1, count2, count3 };
        }

        public async Task<int[]> GenerateTownStats(DateTime statDate)
        {
            var end = statDate.AddDays(1);
            var item1 =
                _townFlowRepository.Count(
                    x => x.StatTime >= statDate && x.StatTime < end && x.FrequencyBandType == FrequencyBandType.All);
            if (item1 < 44)
            {
                var townStatList = _flowService.GetTownFlowStats(statDate);
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
                var townRrcList = _rrcService.GetTownRrcStats(statDate);
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
                var townStatList = _flowService.GetTownFlowStats(statDate, FrequencyBandType.Band2100);
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
                var townStatList = _flowService.GetTownFlowStats(statDate, FrequencyBandType.Band1800);
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
                var townStatList = _flowService.GetTownFlowStats(statDate, FrequencyBandType.Band800VoLte);
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
                var townQciList = _qciService.GetTownQciStats(statDate);
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
                var townPrbList = _prbService.GetTownPrbStats(statDate);
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
                var townDoubleFlowList = _doubleFlowService.GetTownDoubleFlowStats(statDate);
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
