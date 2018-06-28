using System;
using Lte.Domain.Common.Types;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Excel
{
    [TypeDoc("电子运维投诉工单Excel表结构")]
    public class OnlineSustainExcel : IBeginDate
    {
        [ExcelColumn("接单日期")]
        public DateTime BeginDate { get; set; }

        [ExcelColumn("故障单号（投诉单号）")]
        public string SerialNumber { get; set; }

        [ExcelColumn("故障来源")]
        public string ComplainSourceDescription { get; set; }

        [ExcelColumn("故障现象（申告现象）")]
        public string ComplainReason { get; set; }

        public string[] ReasonGroups => string.IsNullOrEmpty(ComplainReason) ? new string[1] : ComplainReason.GetSplittedFields('-');

        public string FirstReasonClass => ReasonGroups.Length > 0 ? ReasonGroups[0] : "其他";

        public string SecondReasonClass => ReasonGroups.Length > 1 ? ReasonGroups[1] : "其他";

        public string ThirdReasonClass => ReasonGroups.Length > 2 ? ReasonGroups[2] : "其他";

        [ExcelColumn("故障内容（投诉内容）")]
        public string Phenomenon { get; set; }

        [ExcelColumn("结单信息")]
        public string FeedbackInfo { get; set; }

        [ExcelColumn("投诉地点")]
        public string Site { get; set; }

        [ExcelColumn("故障种类（申告种类）")]
        public string ComplainCategoryDescription { get; set; }

        [ExcelColumn("申告级别")]
        public string Issue { get; set; }

        [ExcelColumn("所属区域")]
        public string District { get; set; }

        [ExcelColumn("镇区")]
        public string Town { get; set; }

        [ExcelColumn("申告时间")]//Process--
        public DateTime? ComplainTime { get; set; }

        [ExcelColumn("受理时间")]//Process--
        public DateTime? ReceiveTime { get; set; }

        [ExcelColumn("申告号码")]
        public string ComplainNumber { get; set; }

        [ExcelColumn("联系电话")]
        public string ContactPhone { get; set; }

        [ExcelColumn("协查单信息")]
        public string WorkItemNumber { get; set; }
        
        [ExcelColumn("覆盖类型")]//Process--
        public string CoverageTypeDescription { get; set; }

        [ExcelColumn(("工单信息"))]
        public string WorkItemInfo { get; set; }

        [ExcelColumn("处理日期")]//Process--
        public DateTime? ProcessDate { get; set; }

        [ExcelColumn("投诉点场所类型")]//Process--
        public string AreaTypeDescription { get; set; }

        [ExcelColumn("处理过程及建议")]//Process--
        public string ProcessSuggestion { get; set; }

        [ExcelColumn("测试地点")]
        public string Address { get; set; }

        [ExcelColumn("投诉点经度", TransformEnum.DoubleEmptyZero, 0)]
        public double Longtitute { get; set; }

        [ExcelColumn("投诉点纬度", TransformEnum.DoubleEmptyZero, 0)]
        public double Lattitute { get; set; }

        [ExcelColumn("主用基站名称")]//Process--
        public string BtsName { get; set; }
        
        [ExcelColumn("主用基站编号")]//Process--
        public int? BtsId { get; set; }

        [ExcelColumn("PN")]//Process--
        public short? Pn { get; set; }

        [ExcelColumn("RX")]//Process--
        public short? ReceiveLevel { get; set; }

        [ExcelColumn("TX")]//Process--
        public short? TransmitLevel { get; set; }

        [ExcelColumn("ECIO")]//Process--
        public double? EcIo { get; set; }
        
        [ExcelColumn("覆盖等级")]//Process--
        public byte? CoverageLevel { get; set; }

        [ExcelColumn("处理/测试人")]//Process--
        public string ProcessPerson { get; set; }

        [ExcelColumn("最终解决方案（分类）")]//Process--
        public string ResolveScheme { get; set; }

        [ExcelColumn("最终解决时间")]//Process--
        public DateTime? ResolveDate { get; set; }

        [ExcelColumn("当前进度（必选）")]//Process
        public string ComplainStateDescription { get; set; }

        public bool IsResolved => ComplainStateDescription == "已解决";

        [ExcelColumn("规划站点名称（新增资源必填）")]//Process--
        public string PlanSite { get; set; }

        [ExcelColumn("投诉原因（按需修正）")]
        public string CauseLocation { get; set; }

        public string[] CauseFields => string.IsNullOrEmpty(CauseLocation) ? null : CauseLocation.GetSplittedFields('/');

        public string WorkItemCause => CauseFields == null ? "" : CauseFields[0];

        public string WorkItemSubCause
            => CauseFields == null ? "" : (CauseFields.Length > 1 ? CauseFields[1] : CauseFields[0]);

        [ExcelColumn("测试报告名称（按规范命名，若无测试安报告需说明原因）")]//Process--
        public string ResolveCauseDescription { get; set; }

    }
}