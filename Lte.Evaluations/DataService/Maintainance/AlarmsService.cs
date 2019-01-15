using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Abp.EntityFramework.Entities.Maintainence;
using AutoMapper;
using Lte.Domain.Common.Wireless.Alarm;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Common.Wireless.Work;
using Lte.Domain.LinqToCsv.Context;
using Lte.Domain.LinqToCsv.Description;
using Lte.Domain.Regular;
using Lte.Evaluations.DataService.Basic;
using Lte.MySqlFramework.Abstract.Maintainence;
using Lte.MySqlFramework.Abstract.RegionKpi;
using Lte.MySqlFramework.Abstract.Test;
using Lte.MySqlFramework.Entities.Maintainence;

namespace Lte.Evaluations.DataService.Maintainance
{
    public class AlarmsService
    {
        private readonly IAlarmRepository _repository;
        private readonly ICoverageStatRepository _coverageStatRepository;
        private readonly ITownCoverageRepository _townCoverageRepository;
        private readonly IZhangshangyouQualityRepository _zhangshangyouQualityRepository;
        private readonly IZhangshangyouCoverageRepository _zhangshangyouCoverageRepository;

        public AlarmsService(IAlarmRepository repository, ICoverageStatRepository coverageStatRepository,
            ITownCoverageRepository townCoverageRepository, IZhangshangyouQualityRepository zhangshangyouQualityRepository,
            IZhangshangyouCoverageRepository zhangshangyouCoverageRepository)
        {
            _repository = repository;
            _coverageStatRepository = coverageStatRepository;
            _townCoverageRepository = townCoverageRepository;
            _zhangshangyouQualityRepository = zhangshangyouQualityRepository;
            _zhangshangyouCoverageRepository = zhangshangyouCoverageRepository;
            if (AlarmStats == null)
                AlarmStats = new Stack<AlarmStat>();
        }

        public IEnumerable<AlarmView> Get(int eNodebId, DateTime begin, DateTime end)
        {
            var stats = _repository.GetAllList(begin, end, eNodebId);
            return Mapper.Map<IEnumerable<AlarmStat>, IEnumerable<AlarmView>>(stats);
        }

        public IEnumerable<AlarmView> Get(int eNodebId, byte sectorId, DateTime begin, DateTime end)
        {
            var stats = _repository.GetAllList(begin, end, eNodebId, sectorId);
            return Mapper.Map<IEnumerable<AlarmStat>, IEnumerable<AlarmView>>(stats);
        }

        public IEnumerable<AlarmView> Get(int eNodebId, DateTime begin, DateTime end, string levelDescription)
        {
            var stats = _repository.GetAllList(begin, end, eNodebId);
            IEnumerable<AlarmStat> refinedStats;
            switch (levelDescription)
            {
                case "���ظ澯":
                    refinedStats =
                        stats.Where(x => x.AlarmLevel == AlarmLevel.Serious || x.AlarmLevel == AlarmLevel.Urgent);
                    break;
                case "��Ҫ���ϸ澯":
                    refinedStats =
                        stats.Where(
                            x =>
                                x.AlarmLevel == AlarmLevel.Serious || x.AlarmLevel == AlarmLevel.Urgent ||
                                x.AlarmLevel == AlarmLevel.Primary || x.AlarmLevel == AlarmLevel.Important);
                    break;
                default:
                    refinedStats = stats;
                    break;
            }
            return Mapper.Map<IEnumerable<AlarmStat>, IEnumerable<AlarmView>>(refinedStats);
        }

        public int GetCounts(int eNodebId, DateTime begin, DateTime end)
        {
            return _repository.Count(begin, end, eNodebId);
        }

        private static Stack<AlarmStat> AlarmStats { get; set; }

        public void UploadZteAlarms(StreamReader reader)
        {
            try
            {
                var stats = CsvContext.Read<AlarmStatCsv>(reader, CsvFileDescription.CommaDescription).ToList();
                foreach (var stat in stats)
                {
                    AlarmStats.Push(Mapper.Map<AlarmStatCsv, AlarmStat>(stat));
                }
            }
            catch
            {
                //ignore.
            }
        }

        public void UploadHwAlarms(StreamReader reader)
        {
            try
            {
                var stats = CsvContext.Read<AlarmStatHuawei>(reader, CsvFileDescription.CommaDescription).ToList();
                foreach (var stat in stats)
                {
                    AlarmStats.Push(Mapper.Map<AlarmStatHuawei, AlarmStat>(stat));
                }
            }
            catch
            {
                // ignored
            }
        }

        public bool DumpOneStat()
        {
            var stat = AlarmStats.Pop();
            if (stat == null) throw new NullReferenceException("alarm stat is null!");
            var item =
                _repository.FirstOrDefault(
                    x =>
                        x.HappenTime == stat.HappenTime && x.ENodebId == stat.ENodebId && x.SectorId == stat.SectorId &&
                        x.AlarmId == stat.AlarmId);
            if (item == null)
            {
                _repository.Insert(stat);
            }
            else
            {
                item.RecoverTime = stat.RecoverTime;
            }
            _repository.SaveChanges();
            return true;
        }

        public int GetAlarmsToBeDump()
        {
            return AlarmStats.Count;
        }

        public IEnumerable<AlarmStat> GetAlarmsToBeDump(int begin, int range)
        {
            return AlarmStats.Skip(begin).Take(range);
        }

        public IEnumerable<AlarmStat> QueryAlarmStats()
        {
            return AlarmStats;
        }

        public void ClearAlarmStats()
        {
            AlarmStats.Clear();
        }

        public IEnumerable<AlarmHistory> GetAlarmHistories(DateTime begin, DateTime end)
        {
            var results = new List<AlarmHistory>();
            while (begin < end.AddDays(1))
            {
                var beginDate = begin.Date;
                var endDate = begin.Date.AddDays(1);
                results.Add(new AlarmHistory
                {
                    DateString = beginDate.ToShortDateString(),
                    Alarms = _repository.Count(x => x.HappenTime >= beginDate && x.HappenTime < endDate),
                    ZhangshangyouQualities =
                        _zhangshangyouQualityRepository.Count(x => x.StatTime >= beginDate && x.StatTime < endDate),
                    ZhangshangyouCoverages =
                        _zhangshangyouCoverageRepository.Count(x => x.StatTime >= beginDate && x.StatTime < endDate)
                });
                begin = begin.AddDays(1);
            }
            return results;
        }
        
        public IEnumerable<CoverageHistory> GetCoverageHistories(DateTime begin, DateTime end)
        {
            var results = new List<CoverageHistory>();
            while (begin < end.AddDays(1))
            {
                var beginDate = begin.Date;
                var endDate = begin.Date.AddDays(1);
                results.Add(new CoverageHistory
                {
                    DateString = beginDate.ToShortDateString(),
                    CoverageStats = _coverageStatRepository.Count(x => x.StatDate >= beginDate && x.StatDate < endDate),
                    TownCoverageStats =
                        _townCoverageRepository.Count(x =>
                            x.StatDate >= beginDate && x.StatDate < endDate &&
                            x.FrequencyBandType == FrequencyBandType.All),
                    TownCoverage800 =
                        _townCoverageRepository.Count(x =>
                            x.StatDate >= beginDate && x.StatDate < endDate &&
                            x.FrequencyBandType == FrequencyBandType.Band800VoLte),
                    TownCoverage1800 =
                        _townCoverageRepository.Count(x =>
                            x.StatDate >= beginDate && x.StatDate < endDate &&
                            x.FrequencyBandType == FrequencyBandType.Band1800),
                    TownCoverage2100 =
                        _townCoverageRepository.Count(x =>
                            x.StatDate >= beginDate && x.StatDate < endDate &&
                            x.FrequencyBandType == FrequencyBandType.Band2100),
                    CollegeCoverageStats = 
                        _townCoverageRepository.Count(x =>
                            x.StatDate >= beginDate && x.StatDate < endDate &&
                            x.FrequencyBandType == FrequencyBandType.College),
                    MarketCoverageStats = 
                        _townCoverageRepository.Count(x =>
                            x.StatDate >= beginDate && x.StatDate < endDate &&
                            x.FrequencyBandType == FrequencyBandType.Market),
                    TransportationCoverageStats = 
                        _townCoverageRepository.Count(x =>
                            x.StatDate >= beginDate && x.StatDate < endDate &&
                            x.FrequencyBandType == FrequencyBandType.Transportation)
                });
                begin = begin.AddDays(1);
            }
            return results;
        }

        public int DumpHuaweiAlarmInfo(HuaweiLocalCellDef cellDef)
        {
            var items = _repository.GetAllList(DateTime.Now.AddDays(-100), DateTime.Now, cellDef.ENodebId);
            foreach (var item in items.Where(x => x.SectorId == 0))
            {
                if (item.AlarmCategory == AlarmCategory.Huawei)
                {
                    switch (item.AlarmType)
                    {
                        //eNodeB����=������������LBBU6, ����С����ʶ=3, С��˫��ģʽ=FDD, С������=�������Ĺ㳡���_3, eNodeB��ʶ=501157, С����ʶ=51, ��������=��Ƶ��Ԫ�쳣
                        case AlarmType.CellDown://AlarmType=4
                            item.AlarmCategory = AlarmCategory.Qos;
                            item.SectorId =
                                byte.Parse(
                                    item.Details.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)[5].Split('=')
                                        [1]);
                            break;
                        //eNodeB����=��̳����LBBU13, ����С����ʶ=2, PCIֵ=212, ����Ƶ��=1825, С��˫��ģʽ=FDD, ��ͻ����=����, С������=��̳��ĶԱ����R_2, eNodeB��ʶ=500578, С����ʶ=50
                        case AlarmType.PciCrack://AlarmType=46
                            item.AlarmCategory = AlarmCategory.Qos;
                            item.SectorId =
                                byte.Parse(
                                    item.Details.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)[8].Split('=')
                                        [1]);
                            break;
                        //eNodeB����=�ִӰ�����, ����С����ʶ=0, С��˫��ģʽ=FDD, С����ǰʹ�÷���ͨ����=2, С����ǰʹ�ý���ͨ����=4, С������=�ִӰ�����_0, eNodeB��ʶ=499773, С����ʶ=0, ��������=��������
                        //eNodeB����=�ִӶ�ƽ�Ž�, ����С����ʶ=4, С��˫��ģʽ=FDD, С����ǰʹ�÷���ͨ����=1, С����ǰʹ�ý���ͨ����=2, С������=�ִӶ�ƽ�Ž�_4, eNodeB��ʶ=500264, С����ʶ=4, ��������=ͨ���쳣
                        //eNodeB����=�ݹ𻪿ڽ������LBBU2, ����С����ʶ=1, С��˫��ģʽ=FDD, С����ǰʹ�÷���ͨ����=2, С����ǰʹ�ý���ͨ����=4, С������=�ݹ�ҵ·R_1, eNodeB��ʶ=552563, С����ʶ=49, ��������=��������
                        case AlarmType.BadPerformance://AlarmType=43
                            item.SectorId =
                                byte.Parse(
                                    item.Details.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)[7].Split('=')
                                        [1]);
                            item.AlarmCategory = AlarmCategory.Apparatus;
                            break;
                        default:
                            item.SectorId = 255;
                            break;
                    }
                }
                else
                    item.SectorId = 255;
                _repository.Update(item);
            }
            return _repository.SaveChanges();
        }
    }
}