using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Excel
{
    [TypeDoc("巡检计划")]
    public class CheckingProjectExcel
    {
        [ExcelColumn("区县")]
        public string District { get; set; }

        [ExcelColumn("站址编码")]
        public string StationSerialNumber { get; set; }

        [ExcelColumn("维护单位")]
        public string Company { get; set; }

        [ExcelColumn("基站维护人")]
        public string Maintainer { get; set; }

        [ExcelColumn("巡检流水号")]
        public string CheckingFlowNumber { get; set; }

        [ExcelColumn("计划开始日期")]
        public DateTime BeginDate { get; set; }

        [ExcelColumn("计划结束日期")]
        public DateTime EndDate { get; set; }

        [ExcelColumn("计划名称")]
        public string ProjectName { get; set; }

        [ExcelColumn("归档时间")]
        public DateTime? FinalDate { get; set; }

        [ExcelColumn("巡检单状态")]
        public string WorkItemStateString { get; set; }

        [ExcelColumn("合同站点名称")]
        public string StationName { get; set; }

        [ExcelColumn("备注")]
        public string Comments { get; set; }
    }
}
