using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Excel
{
    [TypeDoc("巡检结果基本信息")]
    public class CheckingBasicExcel : IGeoPoint<double>, IBeginDate
    {
        [ExcelColumn("巡检流水号")]
        public string CheckingFlowNumber { get; set; }

        [ExcelColumn("开始时间")]
        public DateTime BeginDate { get; set; }

        [ExcelColumn("结束时间")]
        public DateTime? EndDate { get; set; }
    
        [ExcelColumn("巡检经度")]
        public double Longtitute { get; set; }

        [ExcelColumn("巡检纬度")]
        public double Lattitute { get; set; }

        [ExcelColumn("巡检距离")]
        public double Distance { get; set; }
    }
}
