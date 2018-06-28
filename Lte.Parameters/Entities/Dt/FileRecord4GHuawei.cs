using System;
using System.Globalization;
using Abp.EntityFramework.Dependency;
using Lte.Domain.Common;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.LinqToCsv;
using Lte.Domain.Regular;

namespace Lte.Parameters.Entities.Dt
{
    public class FileRecord4GHuawei : IGeoPoint<double?>, IPn, IStatTime
    {
        [CsvColumn(Name = "No.")]
        public int? Ind { get; set; }

        [CsvColumn(Name = "Longitude")]
        public double? Longtitute { get; set; }

        [CsvColumn(Name = "Latitude")]
        public double? Lattitute { get; set; }

        [CsvColumn(Name = "DateTime")]
        public string ComputerTime { get; set; }

        public DateTime StatTime
        {
            get { return ComputerTime.ConvertToDateTime(DateTime.Now); }
            set { ComputerTime = value.ToString(CultureInfo.InvariantCulture); }
        }

        [CsvColumn(Name = "ECI(eNodeBID/CellID)_All Logs")]
        public string CellString { get; set; }

        public string[] CellFields => CellString?.GetSplittedFields(" / ");

        public int? ENodebId
            => (CellFields != null && CellFields.Length > 0) ? CellFields[0].ConvertToInt(0) : (int?)null;

        public byte? SectorId
            => (CellFields != null && CellFields.Length > 1) ? CellFields[1].ConvertToByte(0) : (byte?)null;

        [CsvColumn(Name = "DL Frequency_All Logs")]
        public double? Frequency { get; set; }

        [CsvColumn(Name = "Serving PCI_All Logs")]
        public double? Pn { get; set; }

        [CsvColumn(Name = "Serving RSRP_All Logs")]
        public double? Rsrp { get; set; }

        [CsvColumn(Name = "Serving PCC SINR_All Logs")]
        public double? Sinr { get; set; }

        [CsvColumn(Name = "PCC PDCCH BLER(%)_All Logs")]
        public double? DlBler { get; set; }

        [CsvColumn(Name = "PCC CQIPeriodicity_All Logs")]
        public double? CqiAverage { get; set; }

        [CsvColumn(Name = "MS1_Uplink MCS")]
        public byte? UlMcs { get; set; }

        [CsvColumn(Name = "DL MCSTotal_All Logs")]
        public int? TotalDlMcs { get; set; }

        public byte? DlMcs => (byte?) (TotalDlMcs / 30);

        [CsvColumn(Name = "PDCP Throughput UL_All Logs")]
        public double? PdcpThroughputUl { get; set; }

        [CsvColumn(Name = "PDCP Throughput DL_All Logs")]
        public double? PdcpThroughputDl { get; set; }

        [CsvColumn(Name = "PCC PHY Throughput DL_All Logs")]
        public double? PhyThroughputDl { get; set; }

        [CsvColumn(Name = "PCC MAC Throughput DL_All Logs")]
        public double? MacThroughputDl { get; set; }

        [CsvColumn(Name = "PUSCH RB Number/s_All Logs")]
        public int? PuschRbNum { get; set; }

        [CsvColumn(Name = "PDSCH RB Number/s_All Logs")]
        public int? PdschRbNum { get; set; }

        [CsvColumn(Name = "MS1_PUSCH Total RB Number")]
        public int? PuschRbSizeAverage { get; set; }

        [CsvColumn(Name = "MS1_PDSCH Total RBNumber")]
        public int? PdschRbSizeAverage { get; set; }

        [CsvColumn(Name = "1st PCI in Detected Cells_All Logs")]
        public short? N1Pci { get; set; }

        [CsvColumn(Name = "1st RSRP in Detected Cells_All Logs")]
        public double? N1Rsrp { get; set; }

        [CsvColumn(Name = "2nd PCI in Detected Cells_All Logs")]
        public short? N2Pci { get; set; }

        [CsvColumn(Name = "2nd RSRP in Detected Cells_All Logs")]
        public double? N2Rsrp { get; set; }

        [CsvColumn(Name = "3rd PCI in Detected Cells_All Logs")]
        public short? N3Pci { get; set; }

        [CsvColumn(Name = "3rd RSRP in Detected Cells_All Logs")]
        public double? N3Rsrp { get; set; }
    }
}