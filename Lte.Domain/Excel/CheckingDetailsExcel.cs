using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Excel
{
    [TypeDoc("巡检结果详细信息")]
    public class CheckingDetailsExcel
    {
        [ExcelColumn("巡检流水号")]
        public string CheckingFlowNumber { get; set; }

        [ExcelColumn("检查结果")]
        public string Results { get; set; }

        [ExcelColumn("是否需要整治")]
        public string NeedFixing { get; set; }

        [ExcelColumn("是否有图片")]
        public string ContainPicture { get; set; }

        [ExcelColumn("图片存放目录")]
        public string Directory { get; set; }

        [ExcelColumn("状态")]
        public string ComplainStateDescription { get; set; }

    }
}
