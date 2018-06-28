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
    public class FileRecord4GZte : IGeoPoint<double?>, IPn, IStatTime
    {
        [CsvColumn(Name = "Offset")]
        public int? Ind { get; set; }

        [CsvColumn(Name = "LogTime")]
        public string ComputerTime { get; set; }

        public DateTime StatTime
        {
            get { return ComputerTime.ConvertToDateTime(DateTime.Now); }
            set { ComputerTime = value.ToString(CultureInfo.InvariantCulture); }
        }

        [CsvColumn(Name = "GPS_Lon")]
        public double? Longtitute { get; set; }

        [CsvColumn(Name = "GPS_Lat")]
        public double? Lattitute { get; set; }

        [CsvColumn(Name = "MS1_Server Cell CellID")]
        public string CellString { get; set; }

        public string[] CellFields => CellString?.GetSplittedFields('_');

        public int? ENodebId
            => (CellFields != null && CellFields.Length > 0) ? CellFields[0].ConvertToInt(0) : (int?) null;

        public byte? SectorId
            => (CellFields != null && CellFields.Length > 1) ? CellFields[1].ConvertToByte(0) : (byte?) null;

        [CsvColumn(Name = "MS1_Downlink Frequency")]
        public double? Frequency { get; set; }

        [CsvColumn(Name = "MS1_Server Cell PCI(PCell)")]
        public double? Pn { get; set; }

        [CsvColumn(Name = "MS1_Server Cell RSRP(PCell)")]
        public double? Rsrp { get; set; }

        [CsvColumn(Name = "MS1_Server Cell SINR(PCell)")]
        public double? Sinr { get; set; }

        [CsvColumn(Name = "MS1_PDSCH Total BLER(PCell)")]
        public double? DlBlerRate { get; set; }

        public byte? DlBler => (byte?) (DlBlerRate * 100);

        [CsvColumn(Name = "MS1_SCell1 WideBand CQI")]
        public double? CqiAverage { get; set; }

        [CsvColumn(Name = "MS1_Uplink MCS")]
        public byte? UlMcs { get; set; }

        [CsvColumn(Name = "MS1_Downlink TB0 Average MCS")]
        public byte? DlMcs { get; set; }

        [CsvColumn(Name = "MS1_PDCP UL PDU Throughput(Mbit/s)")]
        public double? PdcpThroughputUlMbps { get; set; }

        public double? PdcpThroughputUl => PdcpThroughputUlMbps * 1024;

        [CsvColumn(Name = "MS1_PDCP DL PDU Throughput(Mbit/s)")]
        public double? PdcpThroughputDlMbps { get; set; }

        public double? PdcpThroughputDl => PdcpThroughputDlMbps * 1024;

        [CsvColumn(Name = "MS1_Layer1 DL Throughput(Mbit/s)(PCell)")]
        public double? PhyThroughputDlMbps { get; set; }

        public double? PhyThroughputDl => PhyThroughputDlMbps * 1024;

        [CsvColumn(Name = "MS1_MAC DL Throughput(Mbit/s)(PCell)")]
        public double? MacThroughputDlMbps { get; set; }

        public double? MacThroughputDl => MacThroughputDlMbps * 1024;

        [CsvColumn(Name = "PUSCH Rb Num/s")]
        public int? PuschRbNum { get; set; }

        [CsvColumn(Name = "PDSCH Rb Num/s")]
        public int? PdschRbNum { get; set; }

        [CsvColumn(Name = "MS1_PUSCH Total RB Number")]
        public int? PuschRbSizeAverage { get; set; }

        [CsvColumn(Name = "MS1_PDSCH Total RBNumber")]
        public int? PdschRbSizeAverage { get; set; }

        [CsvColumn(Name = "MS1_Neighbor Cell PCI[1]")]
        public short? N1Pci { get; set; }

        [CsvColumn(Name = "MS1_Neighbor Cell RSRP[1]")]
        public double? N1Rsrp { get; set; }

        [CsvColumn(Name = "MS1_Neighbor Cell PCI[2]")]
        public short? N2Pci { get; set; }

        [CsvColumn(Name = "MS1_Neighbor Cell RSRP[2]")]
        public double? N2Rsrp { get; set; }

        [CsvColumn(Name = "MS1_Neighbor Cell PCI[3]")]
        public short? N3Pci { get; set; }

        [CsvColumn(Name = "MS1_Neighbor Cell RSRP[3]")]
        public double? N3Rsrp { get; set; }
    }
}