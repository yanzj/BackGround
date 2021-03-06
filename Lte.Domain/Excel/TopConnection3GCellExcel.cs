using System;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Excel
{
    public class TopConnection3GCellExcel
    {
        [ExcelColumn("地市")]
        public string City { get; set; }

        [ExcelColumn("日期")]
        public DateTime StatDate { get; set; }

        [ExcelColumn("时")]
        public int StatHour { get; set; }

        public DateTime StatTime => StatDate.AddHours(StatHour);

        [ExcelColumn("站号")]
        public int BtsId { get; set; }

        [ExcelColumn("扇区")]
        public byte SectorId { get; set; }

        [ExcelColumn("载波")]
        public short Frequency { get; set; }

        [ExcelColumn("中文名")]
        public string CellName { get; set; }

        [ExcelColumn("无线掉线次数")]
        public int WirelessDrop { get; set; }

        [ExcelColumn("连接尝试次数")]
        public int ConnectionAttempts { get; set; }

        [ExcelColumn("连接失败次数")]
        public int ConnectionFails { get; set; }

        [ExcelColumn("反向链路繁忙率")]
        public double LinkBusyRate { get; set; }

        public static string SheetName { get; } = "连接TOP30小区";
    }
}