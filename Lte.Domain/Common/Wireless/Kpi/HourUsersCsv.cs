﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;
using Lte.Domain.LinqToCsv;
using Lte.Domain.LinqToCsv.Context;
using Lte.Domain.LinqToCsv.Description;
using Lte.Domain.Regular;

namespace Lte.Domain.Common.Wireless.Kpi
{
    public class HourUsersCsv : IStatTime, ILteCellQuery
    {
        [CsvColumn(Name = "时间")]
        public DateTime StatTime { get; set; }

        [CsvColumn(Name = "ENBID")]
        public int ENodebId { get; set; }

        [CsvColumn(Name = "小区ID")]
        public byte SectorId { get; set; }

        [CsvColumn(Name = "小区标识")]
        public string CellSerialNumber { get; set; }

        [CsvColumn(Name = "4.14 最大RRC连接用户数(个)")]
        public int MaxRrcUsers { get; set; }

        [CsvColumn(Name = "4.16 平均RRC连接用户数(个)")]
        public double AverageRrcUsers { get; set; }

        [CsvColumn(Name = "4.17 上行平均激活用户数(个)")]
        public double UplinkAverageActiveUsers { get; set; }

        [CsvColumn(Name = "4.17 上行平均激活用户数(QCI1)(个)")]
        public double UplinkAverageActiveUsersQci1 { get; set; }

        [CsvColumn(Name = "4.17 上行平均激活用户数(QCI2)(个)")]
        public double UplinkAverageActiveUsersQci2 { get; set; }

        [CsvColumn(Name = "4.17 上行平均激活用户数(QCI3)(个)")]
        public double UplinkAverageActiveUsersQci3 { get; set; }

        [CsvColumn(Name = "4.17 上行平均激活用户数(QCI4)(个)")]
        public double UplinkAverageActiveUsersQci4 { get; set; }

        [CsvColumn(Name = "4.17 上行平均激活用户数(QCI5)(个)")]
        public double UplinkAverageActiveUsersQci5 { get; set; }

        [CsvColumn(Name = "4.17 上行平均激活用户数(QCI6)(个)")]
        public double UplinkAverageActiveUsersQci6 { get; set; }

        [CsvColumn(Name = "4.17 上行平均激活用户数(QCI7)(个)")]
        public double UplinkAverageActiveUsersQci7 { get; set; }

        [CsvColumn(Name = "4.17 上行平均激活用户数(QCI8)(个)")]
        public double UplinkAverageActiveUsersQci8 { get; set; }

        [CsvColumn(Name = "4.17 上行平均激活用户数(QCI9)(个)")]
        public double UplinkAverageActiveUsersQci9 { get; set; }

        [CsvColumn(Name = "4.18 下行平均激活用户数(个)")]
        public double DownlinkAverageActiveUsers { get; set; }

        [CsvColumn(Name = "4.18 下行平均激活用户数(QCI1)(个)")]
        public double DownlinkAverageActiveUsersQci1 { get; set; }

        [CsvColumn(Name = "4.18 下行平均激活用户数(QCI2)(个)")]
        public double DownlinkAverageActiveUsersQci2 { get; set; }

        [CsvColumn(Name = "4.18 下行平均激活用户数(QCI3)(个)")]
        public double DownlinkAverageActiveUsersQci3 { get; set; }

        [CsvColumn(Name = "4.18 下行平均激活用户数(QCI4)(个)")]
        public double DownlinkAverageActiveUsersQci4 { get; set; }

        [CsvColumn(Name = "4.18 下行平均激活用户数(QCI5)(个)")]
        public double DownlinkAverageActiveUsersQci5 { get; set; }

        [CsvColumn(Name = "4.18 下行平均激活用户数(QCI6)(个)")]
        public double DownlinkAverageActiveUsersQci6 { get; set; }

        [CsvColumn(Name = "4.18 下行平均激活用户数(QCI7)(个)")]
        public double DownlinkAverageActiveUsersQci7 { get; set; }

        [CsvColumn(Name = "4.18 下行平均激活用户数(QCI8)(个)")]
        public double DownlinkAverageActiveUsersQci8 { get; set; }

        [CsvColumn(Name = "4.18 下行平均激活用户数(QCI9)(个)")]
        public double DownlinkAverageActiveUsersQci9 { get; set; }

        [CsvColumn(Name = "4.20 最大激活用户数(个)")]
        public int MaxActiveUsers { get; set; }

        [CsvColumn(Name = "4.21 小区平均CA能力用户数(个)")]
        public string AverageCaUserString { get; set; }

        public double? AverageCaUsers =>
            string.IsNullOrEmpty(AverageCaUserString) ? (double?)null : AverageCaUserString.ConvertToDouble(0);

        [CsvColumn(Name = "4.22 小区最大CA能力用户数(个)")]
        public int MaxCaUsers { get; set; }

        [CsvColumn(Name = "4.23 PCell小区下行CA平均激活UE数(个)")]
        public double PCellDownlinkAverageCaUes { get; set; }

        [CsvColumn(Name = "4.24 PCell小区下行CA最大激活UE数(个)")]
        public int? PCellDownlinkMaxCaUes { get; set; }

        [CsvColumn(Name = "4.25 Pcell分配给CA用户的下行PDSCH PRB总数(个)")]
        public int PCellPdschCaPrbs { get; set; }

        [CsvColumn(Name = "4.26 Scell分配给CA用户的下行PDSCH PRB总数(个)")]
        public int SCellPdschCaPrbs { get; set; }

        [CsvColumn(Name = "4.27 小区上行CoMP状态的平均用户数(个)")]
        public double UplinkCompAverageUsers { get; set; }

        [CsvColumn(Name = "4.28 小区上行CoMP状态的最大用户数(个)")]
        public int? UplinkCompMaxUsers { get; set; }

        public static List<HourUsersCsv> ReadCsvs(StreamReader reader)
        {
            return
                CsvContext.Read<HourUsersCsv>(reader, CsvFileDescription.CommaDescription)
                    .ToList();
        }
    }
}
