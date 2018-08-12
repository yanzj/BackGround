using System;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Maintainence;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Work;
using Lte.Domain.Regular.Attributes;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Entities
{
    [TypeDoc("工单信息视图")]
    [AutoMapFrom(typeof(WorkItem))]
    public class WorkItemView
    {
        [MemberDoc("工单编号")]
        public string SerialNumber { get; set; }

        [MemberDoc("工单类型")]
        [AutoMapPropertyResolve("Type", typeof(WorkItem), typeof(WorkItemTypeDescriptionTransform))]
        public string WorkItemType { get; set; }

        [MemberDoc("工单子类型")]
        [AutoMapPropertyResolve("Subtype", typeof(WorkItem), typeof(WorkItemSubtypeDescriptionTransform))]
        public string WorkItemSubType { get; set; }

        [MemberDoc("基站编号")]
        public int ENodebId { get; set; }

        [MemberDoc("扇区编号")]
        public byte SectorId { get; set; }

        [MemberDoc("城市")]
        public string City { get; set; }

        [MemberDoc("区域")]
        public string District { get; set; }

        [MemberDoc("镇区")]
        public string Town { get; set; }

        [MemberDoc("派单时间")]
        public DateTime BeginTime { get; set; }

        [MemberDoc("回单期限")]
        public DateTime Deadline { get; set; }

        [MemberDoc("重复次数")]
        public short RepeatTimes { get; set; }

        [MemberDoc("驳回次数")]
        public short RejectTimes { get; set; }

        [MemberDoc("责任人")]
        public string StaffName { get; set; }

        [MemberDoc("最近反馈时间")]
        public DateTime? FeedbackTime { get; set; }

        [MemberDoc("完成时间")]
        public DateTime? FinishTime { get; set; }

        [MemberDoc("定位原因")]
        [AutoMapPropertyResolve("Cause", typeof(WorkItem), typeof(WorkItemCauseDescriptionTransform))]
        public string WorkItemCause { get; set; }

        [MemberDoc("工单状态")]
        [AutoMapPropertyResolve("State", typeof(WorkItem), typeof(WorkItemStateDescriptionTransform))]
        public string WorkItemState { get; set; }

        [MemberDoc("省中心平台反馈信息")]
        public string Comments { get; set; }

        [MemberDoc("本平台反馈信息")]
        public string FeedbackContents { get; set; }

        [MemberDoc("基站名称")]
        public string ENodebName { get; set; }

        public void UpdateTown(IENodebRepository eNodebRepository, IBtsRepository btsRepository,
            ITownRepository townRepository)
        {
            if (ENodebId > 10000)
            {
                UpdateTown(eNodebRepository, townRepository);
            }
            var bts = btsRepository.GetByBtsId(ENodebId);
            if (bts == null) return;
            ENodebName = bts.Name;
            var town = bts.TownId == -1 ? null : townRepository.Get(bts.TownId);
            if (town == null) return;
            City = town.CityName;
            District = town.DistrictName;
            Town = town.TownName;
        }

        public void UpdateTown(IENodebRepository eNodebRepository, ITownRepository townRepository)
        {
            var eNodeb = eNodebRepository.FirstOrDefault(x => x.ENodebId == ENodebId);
            if (eNodeb == null) return;
            ENodebName = eNodeb.Name;
            var town = eNodeb.TownId == -1 ? null : townRepository.Get(eNodeb.TownId);
            if (town == null) return;
            City = town.CityName;
            District = town.DistrictName;
            Town = town.TownName;
        }
    }
}
