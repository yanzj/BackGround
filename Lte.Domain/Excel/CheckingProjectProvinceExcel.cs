using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Excel
{
    [TypeDoc("省公司巡检计划")]
    public class CheckingProjectProvinceExcel
    {
        [ExcelColumn("任务编号")]
        public string WorkItemNumber { get; set; }

        [ExcelColumn("地市")]
        public string City { get; set; }

        [ExcelColumn("任务名称")]
        public string WorkItemName { get; set; }

        [ExcelColumn("任务状态")]
        public string WorkItemStateDescription { get; set; }

        [ExcelColumn("计划名称")]
        public string ProjectName { get; set; }

        [ExcelColumn("当前处理人员")]
        public string Processor { get; set; }

        [ExcelColumn("接收班组")]
        public string Receiver { get; set; }

        [ExcelColumn("任务开始时间")]
        public DateTime BeginDate { get; set; }

        [ExcelColumn("任务完成时限")]
        public DateTime FinishDate { get; set; }
    }
}
