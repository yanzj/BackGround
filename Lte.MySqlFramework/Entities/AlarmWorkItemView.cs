using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular.Attributes;

namespace Lte.MySqlFramework.Entities
{
    [AutoMapFrom(typeof(AlarmWorkItem))]
    [TypeDoc("故障工单视图")]
    public class AlarmWorkItemView : ILteCellQuery
    {
        [MemberDoc("故障单号")]
        public string SerialNumber { get; set; }

        [MemberDoc("故障标题")]
        public string Title { get; set; }

        [MemberDoc("是否为网络故障")]
        [AutoMapPropertyResolve("IsNetworkAlarm", typeof(AlarmWorkItem), typeof(YesNoTransform))]
        public string NetworkAlarm { get; set; }

        [AutoMapPropertyResolve("NetworkType", typeof(AlarmWorkItem), typeof(NetworkTypeDescriptionTransform))]
        public string NetworkTypeDescription { get; set; }

        [AutoMapPropertyResolve("InfrastructureType", typeof(AlarmWorkItem), typeof(InfrastructureTypeDescriptionTransform))]
        public string InfrastructureTypeDescription { get; set; }

        [AutoMapPropertyResolve("AlarmType", typeof(AlarmWorkItem), typeof(AlarmTypeDescriptionTransform))]
        public string AlarmTypeDescription { get; set; }

        [MemberDoc("故障等级")]
        public string AlarmClass { get; set; }

        [MemberDoc("故障内容")]
        public string Contents { get; set; }

        [MemberDoc("预处理")]
        public string PreProcess { get; set; }

        [MemberDoc("工单状态")]
        [AutoMapPropertyResolve("State", typeof(AlarmWorkItem), typeof(WorkItemStateDescriptionTransform))]
        public string StateDescription { get; set; }

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
        [AutoMapPropertyResolve("IsExpire", typeof(AlarmWorkItem), typeof(YesNoTransform))]
        public string Expire { get; set; }

        [MemberDoc("是否修复超时")]
        [AutoMapPropertyResolve("IsFixExpire", typeof(AlarmWorkItem), typeof(YesNoTransform))]
        public string FixExpire { get; set; }

        [MemberDoc("故障清除时间")]
        public DateTime? ClearTime { get; set; }

        [MemberDoc("网元名称")]
        public string BtsName { get; set; }

        [MemberDoc("扇区编号")]
        public byte SectorId { get; set; }

        [MemberDoc("基站编号")]
        public int ENodebId { get; set; }
    }
}
