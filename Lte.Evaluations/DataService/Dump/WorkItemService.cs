using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Excel;
using Lte.Domain.LinqToExcel;
using Lte.Evaluations.Policy;
using Lte.Evaluations.ViewModels.Precise;
using Lte.Evaluations.ViewModels.RegionKpi;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;

namespace Lte.Evaluations.DataService.Dump
{
    public class WorkItemService
    {
        private readonly IWorkItemRepository _repository;
        private readonly IENodebRepository _eNodebRepository;
        private readonly IBtsRepository _btsRepository;
        private readonly ITownRepository _townRepository;
        private readonly IAlarmWorkItemRepository _alarmWorkItemRepository;
        private readonly ICheckingProjectRepository _checkingProjectRepository;
        private readonly ICheckingBasicRepository _checkingBasicRepository;
        private readonly ICheckingDetailsRepository _checkingDetailsRepository;

        public WorkItemService(IWorkItemRepository repository, IENodebRepository eNodebRepository,
            IBtsRepository btsRepository, ITownRepository townRepository, IAlarmWorkItemRepository alarmWorkItemRepository,
            ICheckingProjectRepository checkingProjectRepository, ICheckingBasicRepository checkingBasicRepository,
            ICheckingDetailsRepository checkingDetailsRepository)
        {
            _repository = repository;
            _eNodebRepository = eNodebRepository;
            _btsRepository = btsRepository;
            _townRepository = townRepository;
            _alarmWorkItemRepository = alarmWorkItemRepository;
            _checkingProjectRepository = checkingProjectRepository;
            _checkingBasicRepository = checkingBasicRepository;
            _checkingDetailsRepository = checkingDetailsRepository;
        }

        public WorkItemView Query(string serialNumber)
        {
            var item = _repository.GetAll().FirstOrDefault(x => x.SerialNumber == serialNumber);
            var result = Mapper.Map<WorkItem, WorkItemView>(item);
            result.UpdateTown(_eNodebRepository, _btsRepository, _townRepository);
            return result;
        }

        public int UpdateLteSectorIds()
        {
            return 0;
        }

        private static Stack<WorkItemExcel> WorkItemInfos { get; }=new Stack<WorkItemExcel>();

        public string ImportExcelFiles(string path)
        {
            var factory = new ExcelQueryFactory { FileName = path };
            const string sheetName = "工单查询结果";
            var infos = (from c in factory.Worksheet<WorkItemExcel>(sheetName)
                        select c).ToList();
            WorkItemInfos.Clear();
            foreach (var info in infos)
            {
                WorkItemInfos.Push(info);
               
            }

            return "完成工单读取：" + WorkItemInfos.Count + "条";
        }

        public string ImportAlarmExcelFiles(string path)
        {
            var factory = new ExcelQueryFactory { FileName = path };
            const string sheetName = "数据页1";
            var infos = (from c in factory.Worksheet<AlarmWorkItemExcel>(sheetName)
                select c).ToList();
            var count =
                _alarmWorkItemRepository.Import<IAlarmWorkItemRepository, AlarmWorkItem, AlarmWorkItemExcel>(infos);

            return "完成故障工单读取：" + count + "条";
        }

        public string ImportCheckingProjectExcelFiles(string path)
        {
            var factory = new ExcelQueryFactory { FileName = path };
            const string sheetName = "sheet1";
            var infos = (from c in factory.Worksheet<CheckingProjectExcel>(sheetName)
                         select c).ToList();
            var count =
                _checkingProjectRepository.Import<ICheckingProjectRepository, CheckingProject, CheckingProjectExcel>(infos);

            return "完成巡检计划读取：" + count + "条";
        }

        public string ImportCheckingResultExcelFiles(string path)
        {
            var factory = new ExcelQueryFactory { FileName = path };
            const string sheetName = "基本信息";
            var infos = (from c in factory.Worksheet<CheckingBasicExcel>(sheetName)
                select c).ToList();
            var count =
                _checkingBasicRepository.Import<ICheckingBasicRepository, CheckingBasic, CheckingBasicExcel>(infos);
            var keys = new[]
            {
                "机房上_上塔RRU_上塔RRU",
                "机房上_天馈线_GPS及性能",
                "机房上_天馈线_天线测量",
                "机房上_天馈线_馈线质量",
                "机房上_天馈线_馈线质量",
                "机房内_传输配套_传输配套",
                "机房内_空调_空调",
                "机房内_动力配套_照明与插座",
                "机房内_动力配套_蓄电池",
                "机房内_动力配套_开关电源",
                "机房内_动力配套_固定式油机",
                "机房内_动力配套_防雷与接地",
                "机房内_动力配套_低压配电",
                "机房内_动力配套_UPS",
                "机房内_主设备_设备风扇",
                "机房内_主设备_板卡状态",
                "机房内_主设备_LTE机框",
                "机房内_内部环境_温湿度",
                "机房内_内部环境_维护标识",
                "机房内_内部环境_基站消防",
                "机房内_内部环境_机房土建",
                "机房内_内部环境_环控核查",
                "机房内_内部环境_环控核查",
                "机房外_电光线缆_电光线缆",
                "机房外_空调室外机_空调室外机",
                "机房外_变_稳压器_变_稳压器",
                "机房外_电表读数_电表读数",
                "机房外_信号测试_信号测试",
                "机房外_周边环境_周边环境"
            };
            foreach (var key in keys)
            {
                var detailses = (from c in factory.Worksheet<CheckingDetailsExcel>(key)
                    select c).ToList();
                _checkingDetailsRepository.Import<ICheckingDetailsRepository, CheckingDetails, CheckingDetailsExcel>(
                    detailses, key, (item, k) => { item.CheckingTheme = k; });
            }

            return "完成巡检结果读取：" + count + "条";
        }

        public async Task<bool> DumpOne()
        {
            var info = WorkItemInfos.Pop();
            if (info == null) return false;
            var item = _repository.FirstOrDefault(x => x.SerialNumber == info.SerialNumber);
            if (item != null)
            {
                if (item.State == WorkItemState.Finished)
                {
                    item.FinishTime = info.FinishTime;
                    item.State = info.StateDescription.GetEnumType<WorkItemState>();
                    item.Cause = info.CauseDescription.GetEnumType<WorkItemCause>();
                    item.ENodebId = info.ENodebId;
                    item.SectorId = info.SectorId;
                    item.Comments = info.Comments;
                    await _repository.UpdateAsync(item);
                }

            }
            else
            {
                await _repository.InsertAsync(Mapper.Map<WorkItemExcel, WorkItem>(info));
            }
            _repository.SaveChanges();
            return true;
        }

        public void ClearDumpItems()
        {
            WorkItemInfos.Clear();
        }

        public int QueryDumpItems()
        {
            return WorkItemInfos.Count;
        }
        
        public async Task<Tuple<int, int, int>> QueryTotalItemsThisMonth()
        {
            var lastMonthDate = DateTime.Today.Day < 26 ? DateTime.Today.AddMonths(-1) : DateTime.Today;
            var begin = new DateTime(lastMonthDate.Year, lastMonthDate.Month, 26);
            var end = begin.AddMonths(1);
            var items = await _repository.GetAllListAsync(x => x.BeginTime >= begin && x.BeginTime < end);
            return new Tuple<int, int, int>(items.Count, items.Count(x => x.FinishTime != null),
                items.Count(x => (x.Deadline < DateTime.Today && x.FinishTime == null) ||
                (x.FinishTime != null && x.FinishTime > x.Deadline)));
        }

        public IEnumerable<WorkItemView> QueryViews(string statCondition, string typeCondition)
        {
            var predict = (statCondition + '_' + typeCondition).GetWorkItemFilter();
            var stats = predict == null ? _repository.GetAllList() : _repository.GetAllList(predict);
            var views = Mapper.Map<List<WorkItem>, List<WorkItemView>>(stats);
            views.ForEach(x => x.UpdateTown(_eNodebRepository, _btsRepository, _townRepository));
            return views;
        }

        public IEnumerable<WorkItemView> QueryViews(string statCondition, string typeCondition, string district)
        {
            return QueryViews(statCondition, typeCondition).Where(x => x.District == district);
        }

        public async Task<IEnumerable<WorkItemView>> QueryViews(int eNodebId)
        {
            var statList = await _repository.GetAllListAsync(eNodebId);
            var views = Mapper.Map<List<WorkItem>, List<WorkItemView>>(statList);
            views.ForEach(x => x.UpdateTown(_eNodebRepository, _btsRepository, _townRepository));
            return views;
        }

        public async Task<IEnumerable<WorkItemView>> QueryViews(int eNodebId, byte sectorId)
        {
            var statList = await _repository.GetAllListAsync(eNodebId, sectorId);
            var views = Mapper.Map<List<WorkItem>, List<WorkItemView>>(statList);
            views.ForEach(x => x.UpdateTown(_eNodebRepository, _btsRepository, _townRepository));
            return views;
        }

        public async Task<IEnumerable<WorkItemView>> QueryPreciseViews(DateTime begin, DateTime end)
        {
            var statList = await _repository.GetUnfinishedPreciseListAsync(begin, end);
            var views = Mapper.Map<List<WorkItem>, List<WorkItemView>>(statList);
            views.ForEach(x => x.UpdateTown(_eNodebRepository, _btsRepository, _townRepository));
            return views;
        }

        public async Task<IEnumerable<WorkItemView>> QueryPreciseViews(DateTime begin, DateTime end, string district)
        {
            var statList = await _repository.GetUnfinishedPreciseListAsync(begin, end);
            var views = Mapper.Map<List<WorkItem>, List<WorkItemView>>(statList);
            views.ForEach(x => x.UpdateTown(_eNodebRepository, _btsRepository, _townRepository));
            return views.Where(x => x.District == district);
        }

        private static IEnumerable<WorkItemChartView> WorkItemChartViews { get; set; }

        private static DateTime ChartDate { get; set; } = DateTime.Today;

        public IEnumerable<WorkItemChartTypeView> QueryChartTypeViews(string chartType)
        {
            if (WorkItemChartViews == null || ChartDate != DateTime.Today)
            {
                ChartDate = DateTime.Today;
                var views = Mapper.Map<List<WorkItem>, List<WorkItemView>>(_repository.GetAllList());
                views.ForEach(x => x.UpdateTown(_eNodebRepository, _btsRepository, _townRepository));
                WorkItemChartViews = views.MapTo<IEnumerable<WorkItemChartView>>();
            }
            switch (chartType)
            {
                case "type-subtype":
                    return WorkItemChartViews.GroupBy(x => new {x.WorkItemType, x.WorkItemSubType}).Select(g =>
                        new WorkItemChartTypeView
                        {
                            Type = g.Key.WorkItemType,
                            SubType = g.Key.WorkItemSubType,
                            Total = g.Count()
                        });
                case "state-subtype":
                    return WorkItemChartViews.GroupBy(x => new {x.WorkItemState, x.WorkItemSubType}).Select(g =>
                        new WorkItemChartTypeView
                        {
                            Type = g.Key.WorkItemState,
                            SubType = g.Key.WorkItemSubType,
                            Total = g.Count()
                        });
                case "district-town":
                    return WorkItemChartViews.GroupBy(x => new { x.District, x.Town }).Select(g =>
                          new WorkItemChartTypeView
                          {
                              Type = g.Key.District ?? "不分区",
                              SubType = g.Key.Town ?? "不分区",
                              Total = g.Count()
                          });
                default:
                    return new List<WorkItemChartTypeView>();
            }
        }

        public bool FeedBack(string userName, string message, string serialNumber)
        {
            var item = _repository.GetAll().FirstOrDefault(x => x.SerialNumber == serialNumber);
            if (item == null) return false;
            var now = DateTime.Now;
            item.FeedbackContents += "[" + now + "]" + userName + ":" + message;
            item.FeedbackTime = now;
            var result = _repository.Update(item) != null;
            _repository.SaveChanges();
            return result;
        }

        public async Task<string> ConstructPreciseWorkItem(Precise4GView view, DateTime begin, DateTime end, string userName)
        {
            var existedItem = await _repository.GetPreciseExistedAsync(view.CellId, view.SectorId);
            if (existedItem != null) return null;
            var serialNumber = "SELF-FS-Precise-" + view.CellId + "-" + view.SectorId + "-" + begin.ToString("yyyyMMdd") +
                               "-" + end.ToString("yyyyMMdd");
            existedItem = await _repository.FirstOrDefaultAsync(x => x.SerialNumber == serialNumber);
            if (existedItem != null) return null;
            var item = new WorkItem
            {
                BeginTime = end,
                Cause = WorkItemCause.WeakCoverage,
                SerialNumber = serialNumber,
                ENodebId = view.CellId,
                SectorId = view.SectorId,
                Deadline = end.AddMonths(1),
                StaffName = userName,
                Type = WorkItemType.SelfConstruction,
                Subtype = WorkItemSubtype.PreciseRate,
                State = WorkItemState.ToBeSigned,
                Comments =
                    "[" + DateTime.Now + "]" + userName + ": 创建工单" + serialNumber + ";精确覆盖率=" + view.SecondRate +
                    ";MR总数=" + view.TotalMrs + ";TOP天数=" + view.TopDates
            };
            var result = await _repository.InsertAsync(item);
            _repository.SaveChanges();
            return result?.SerialNumber;
        }

        public async Task<WorkItemView> SignInWorkItem(string serialNumber, string userName)
        {
            var existedItem = await _repository.FirstOrDefaultAsync(x => x.SerialNumber == serialNumber);
            if (existedItem == null) return null;
            existedItem.FeedbackContents += "[" + DateTime.Now + "]" + userName + ": 签收工单";
            existedItem.FeedbackTime = DateTime.Now;
            existedItem.State = WorkItemState.Processing;
            var result = await _repository.UpdateAsync(existedItem);
            var view = result == null ? null : Mapper.Map<WorkItem, WorkItemView>(result);
            view?.UpdateTown(_eNodebRepository, _btsRepository, _townRepository);
            _repository.SaveChanges();
            return view;
        }

        public async Task<WorkItemView> FinishWorkItem(string serialNumber, string userName, string comments)
        {
            var existedItem = await _repository.FirstOrDefaultAsync(x => x.SerialNumber == serialNumber);
            if (existedItem == null) return null;
            existedItem.Comments += "[" + DateTime.Now + "]" + userName + ": " + comments;
            existedItem.FinishTime = DateTime.Now;
            existedItem.State = WorkItemState.Finished;
            var result = await _repository.UpdateAsync(existedItem);
            var view = result == null ? null : Mapper.Map<WorkItem, WorkItemView>(result);
            view?.UpdateTown(_eNodebRepository, _btsRepository, _townRepository);
            _repository.SaveChanges();
            return view;
        }
    }
}
