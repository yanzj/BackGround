using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Excel;
using Lte.Domain.Regular;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities
{
    [TypeDoc("故障工单定义")]
    [AutoMapFrom(typeof(AlarmWorkItemExcel))]
    public class AlarmWorkItem : Entity
    {
        [MemberDoc("故障单号")]
        public string SerialNumber { get; set; }

        [MemberDoc("故障标题")]
        public string Title { get; set; }

        public bool IsNetworkAlarm { get; set; }

        [AutoMapPropertyResolve("Category3Description", typeof(AlarmWorkItemExcel), typeof(NetworkTypeTransform))]
        public NetworkType NetworkType { get; set; }

        [AutoMapPropertyResolve("Category4Description", typeof(AlarmWorkItemExcel), typeof(InfrastructureTypeTransform))]
        public InfrastructureType InfrastructureType { get; set; }

        [AutoMapPropertyResolve("Category5Description", typeof(AlarmWorkItemExcel), typeof(AlarmZteTypeTransform))]
        public AlarmType AlarmType { get; set; }

        [MemberDoc("故障等级")]
        public string AlarmClass { get; set; }

        [MemberDoc("故障内容")]
        public string Contents { get; set; }

        [MemberDoc("预处理")]
        public string PreProcess { get; set; }

        [MemberDoc("工单状态")]
        [AutoMapPropertyResolve("StateDescription", typeof(AlarmWorkItemExcel), typeof(WorkItemStateTransform))]
        public WorkItemState State { get; set; }

        [MemberDoc("告警总数")]
        public int TotalAlarms { get; set; }

        [MemberDoc("影响基站总数")]
        public int AffectBtss { get; set; }

        [MemberDoc("影响光路总数")]
        public int AffectFibers { get; set; }

        [MemberDoc("发生时间")]
        public DateTime HappenTime { get; set; }

        [MemberDoc("建单时间")]
        public DateTime BeginTime { get; set; }

        [MemberDoc("故障修复时间")]
        public DateTime? FinishTime { get; set; }

        [MemberDoc("是否超时")]
        [AutoMapPropertyResolve("Expire", typeof(AlarmWorkItemExcel), typeof(YesToBoolTransform))]
        public bool IsExpire { get; set; }

        [MemberDoc("是否修复超时")]
        [AutoMapPropertyResolve("FixExpire", typeof(AlarmWorkItemExcel), typeof(YesToBoolTransform))]
        public bool IsFixExpire { get; set; }

        [MemberDoc("故障清除时间")]
        public DateTime? ClearTime { get; set; }

        [MemberDoc("网元名称")]
        public string BtsName { get; set; }

        public byte SectorId => Contents.GetSectorIdByString();

        public int ENodebId => Contents.GetENodebIdByString();
    }
}
