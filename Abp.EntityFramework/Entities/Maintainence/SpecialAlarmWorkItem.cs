using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Excel;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Maintainence
{
    [TypeDoc("专业网管告警工单信息")]
    [AutoMapFrom(typeof(SpecialAlarmWorkItemExcel))]
    public class SpecialAlarmWorkItem: Entity, IBeginDate
    {
        [MemberDoc("告警标识")]
        public string AlarmNumber { get;set; }

        [MemberDoc("电路名称")]
        public string CircuitName { get; set; }

        [MemberDoc("电路编号")]
        public string CircuitNumber { get; set; }

        [MemberDoc("故障单单号")]
        public string WorkItemNumber { get; set; }

        [MemberDoc("故障单状态")]
        public bool WorkItemState { get; set; }

        [MemberDoc("告警信息")]
        public string AlarmInfo { get; set; }

        [MemberDoc("告警位置")]
        public string AlarmPosition { get; set; }

        [MemberDoc("厂家网元时间")]
        public DateTime BeginDate { get; set; }

        [MemberDoc("网元清除时间")]
        public DateTime? RecoverTime { get; set; }

        [MemberDoc("机房名称")]
        public string StationName { get; set; }

        [MemberDoc("告警附加信息")]
        public string AdditionalInfo { get; set; }

        [MemberDoc("设备类型")]
        public string ApplianceType { get; set; }

        [MemberDoc("告警级别")]
        public string AlarmLevel { get; set; }

        [MemberDoc("告警对象名称")]
        public string ApplianceName { get; set; }
    }
}
