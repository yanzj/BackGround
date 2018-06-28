using System;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Excel
{
    public class PlanningSiteExcel : IDistrictTown
    {
        [ExcelColumn("区县")]
        public string District { get; set; }

        [ExcelColumn("分局")]
        public string Town { get; set; }

        [ExcelColumn("编号")]
        public string PlanNum { get; set; }

        [ExcelColumn("规划名")]
        public string PlanName { get; set; }

        [ExcelColumn("铁塔编号")]
        public string TowerNum { get; set; }

        [ExcelColumn("铁塔站名")]
        public string TowerName { get; set; }

        [ExcelColumn("电信出图站名")]
        public string FormalName { get; set; }

        [ExcelColumn("备注（清单来源）")]
        public string SiteSource { get; set; }

        [ExcelColumn("建设类型")]
        public string SiteCategory { get; set; }

        [ExcelColumn("受阻说明")]
        public string ShouzuShuoming { get; set; }

        [ExcelColumn("规划经度")]
        public double PlanLongtitute { get; set; }

        [ExcelColumn("规划纬度")]
        public double PlanLattitute { get; set; }

        [ExcelColumn("选址经度")]
        public double? FinalLongtitute { get; set; }

        [ExcelColumn("选址纬度")]
        public double? FinalLattitute { get; set; }

        public double Longtitute => FinalLongtitute ?? PlanLongtitute;

        public double Lattitute => FinalLattitute ?? PlanLattitute;

        [ExcelColumn("杆塔类型")]
        public string TowerType { get; set; }

        [ExcelColumn("天线挂高")]
        public double? AntennaHeight { get; set; }

        [ExcelColumn("整体完工时间")]
        public DateTime? CompleteDate { get; set; }

        [ExcelColumn("验收交付时间")]
        public DateTime? YanshouDate { get; set; }

        [ExcelColumn("谈点状态")]
        public string GottenState { get; set; }

        public bool IsGotton => GottenState == "已谈点";

        [ExcelColumn("谈点完成日期")]
        public DateTime? GottenDate { get; set; }

        [ExcelColumn("铁塔对接联系人及联系方式")]
        public string TowerContaction { get; set; }

        [ExcelColumn("合同签订日期")]
        public DateTime? ContractDate { get; set; }

        [ExcelColumn("开通日期")]
        public DateTime? FinishedDate { get; set; }

        [ExcelColumn("铁塔盖章方案")]
        public string TowerScheme { get; set; }

        [ExcelColumn("对应提供给铁塔规划需求名（固定）")]
        public string TowerSiteName { get; set; }

        [ExcelColumn("设计天线类型")]
        public string AntennaType { get; set; }
    }
}