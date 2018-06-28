using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Excel
{
    public class AlarmWorkItemExcel
    {
        [ExcelColumn("故障单号")]
        public string SerialNumber { get; set; }

        [ExcelColumn("故障标题")]
        public string Title { get; set; }

        [ExcelColumn("故障种类")]
        public string AlarmCategory { get; set; }

        public string[] CategoryFields => AlarmCategory.GetSplittedFields('/');

        public string Category1 => CategoryFields.Length > 0 ? CategoryFields[0] : "";

        public bool IsNetworkAlarm => Category1 == "网络侧故障";

        public string Category3Description => CategoryFields.Length > 2 ? CategoryFields[2] : "";

        public string Category4Description => CategoryFields.Length > 3 ? CategoryFields[3] : "";

        public string Category5Description => CategoryFields.Length > 4 ? CategoryFields[4] : "";
        
        [ExcelColumn("故障等级")]
        public string AlarmClass { get; set; }

        [ExcelColumn("故障内容")]
        public string Contents { get; set; }

        [ExcelColumn("预处理")]
        public string PreProcess { get; set; }

        [ExcelColumn("故障状态")]
        public string StateDescription { get; set; }

        [ExcelColumn("告警总数")]
        public int TotalAlarms { get; set; }

        [ExcelColumn("影响基站总数")]
        public int AffectBtss { get; set; }

        [ExcelColumn("影响光路总数")]
        public int AffectFibers { get; set; }

        [ExcelColumn("发生时间")]
        public DateTime HappenTime { get; set; }

        [ExcelColumn("建单时间")]
        public DateTime BeginTime { get; set; }

        [ExcelColumn("故障修复时间")]
        public DateTime? FinishTime { get; set; }

        [ExcelColumn("是否超时")]
        public string Expire { get; set; }

        [ExcelColumn("是否修复超时")]
        public string FixExpire { get; set; }

        [ExcelColumn("故障清除时间")]
        public DateTime? ClearTime { get; set; }

        [ExcelColumn("网元名称")]
        public string BtsName { get; set; }
    }
}
