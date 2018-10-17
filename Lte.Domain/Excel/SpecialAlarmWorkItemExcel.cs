using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Excel
{
    [TypeDoc("专业网管告警工单信息")]
    public class SpecialAlarmWorkItemExcel: IBeginDate
    {
        [ExcelColumn("告警标识")]
        public string AlarmNumber { get;set; }

        [ExcelColumn("电路名称")]
        public string CircuitName { get; set; }

        [ExcelColumn("电路编号")]
        public string CircuitNumber { get; set; }

        [ExcelColumn("故障单单号")]
        public string WorkItemNumber { get; set; }

        [ExcelColumn("故障单状态")]
        public string WorkItemStateDescription { get; set; }

        public bool WorkItemState => WorkItemStateDescription == "已申请";

        [ExcelColumn("告警信息")]
        public string AlarmInfo { get; set; }

        [ExcelColumn("告警位置")]
        public string AlarmPosition { get; set; }

        [ExcelColumn("厂家网元时间")]
        public DateTime BeginDate { get; set; }

        [ExcelColumn("网元清除时间")]
        public DateTime? RecoverTime { get; set; }

        [ExcelColumn("机房名称")]
        public string StationName { get; set; }

        [ExcelColumn("告警附加信息")]
        public string AdditionalInfo { get; set; }

        [ExcelColumn("设备类型")]
        public string ApplianceType { get; set; }

        [ExcelColumn("告警级别")]
        public string AlarmLevel { get; set; }

        [ExcelColumn("告警对象名称")]
        public string ApplianceName { get; set; }
    }
}
