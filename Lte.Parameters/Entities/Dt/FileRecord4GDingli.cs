using System;
using System.Globalization;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.LinqToCsv;
using Lte.Domain.Regular;

namespace Lte.Parameters.Entities.Dt
{
    public class FileRecord4GDingli : IGeoPoint<double?>, IPn, IStatTime
    {
        public string ComputerTime { get; set; }

        public DateTime StatTime
        {
            get { return ComputerTime.ConvertToDateTime(DateTime.Now); }
            set { ComputerTime = value.ToString(CultureInfo.InvariantCulture); }
        }

        [CsvColumn(Name = "Longitude")]
        public double? Longtitute { get; set; }

        [CsvColumn(Name = "Latitude")]
        public double? Lattitute { get; set; }

        [CsvColumn(Name = "LTE eNodeB ID")]
        public int? ENodebId { get; set; }

        [CsvColumn(Name = "LTE SectorID")]
        public byte? SectorId { get; set; }

        [CsvColumn(Name = "LTE Frequency DL")]
        public double? Frequency { get; set; }

        [CsvColumn(Name = "LTE PCI")]
        public double? Pn { get; set; }

        [CsvColumn(Name = "LTE RSRP")]
        public double? SelfRsrp { get; set; }

        public double? Rsrp => SelfRsrp ?? N1Rsrp;

        [CsvColumn(Name = "LTE SINR")]
        public double? Sinr1 { get; set; }

        [CsvColumn(Name = "LTE CRS SINR")]
        public double? Sinr2 { get; set; }

        public double? Sinr => Sinr1 ?? Sinr2;

        [CsvColumn(Name = "LTE PDSCH BLER")]
        public double? DlBler1 { get; set; }

        [CsvColumn(Name = "LTE PDSCH Residual BLER")]
        public double? DlBler2 { get; set; }

        public double? DlBler => DlBler1 ?? DlBler2;

        [CsvColumn(Name = "LTE WideBand CQI")]
        public double? CqiAverage { get; set; }

        [CsvColumn(Name = "LTE MCS Average UL /s")]
        public byte? UlMcs { get; set; }

        [CsvColumn(Name = "LTE MCS Average DL /s")]
        public byte? DlMcs { get; set; }

        [CsvColumn(Name = "LTE PDCP Throughput UL")]
        public double? PdcpBpsUl { get; set; }

        public double? PdcpThroughputUl => PdcpBpsUl / 1024;

        [CsvColumn(Name = "LTE PDCP Throughput DL")]
        public double? PdcpBpsDl { get; set; }

        public double? PdcpThroughputDl => PdcpBpsDl / 1024;

        [CsvColumn(Name = "LTE PHY Throughput DL")]
        public double? PhyBpsDl { get; set; }

        public double? PhyThroughputDl => PhyBpsDl / 1024;

        [CsvColumn(Name = "LTE MAC Throughput DL")]
        public double? MacBpsDl { get; set; }

        public double? MacThroughputDl => MacBpsDl / 1024;

        [CsvColumn(Name = "LTE PUSCH RB Count /s")]
        public int? PuschRbNum { get; set; }

        [CsvColumn(Name = "LTE PDSCH RB Count /s")]
        public int? PdschRbNum { get; set; }

        [CsvColumn(Name = "UL LTE RB Count Per TB")]
        public double? PuschRbSizeAverage { get; set; }

        [CsvColumn(Name = "DL LTE RB Count Per TB")]
        public double? PdschRbSizeAverage { get; set; }

        [CsvColumn(Name = "LTE Cell 1st PCI")]
        public short? N1Pci { get; set; }

        [CsvColumn(Name = "LTE Cell 1st RSRP")]
        public double? N1Rsrp { get; set; }

        [CsvColumn(Name = "LTE Cell 2nd PCI")]
        public short? N2Pci { get; set; }

        [CsvColumn(Name = "LTE Cell 2nd RSRP")]
        public double? N2Rsrp { get; set; }

        [CsvColumn(Name = "LTE Cell 3rd PCI")]
        public short? N3Pci { get; set; }

        [CsvColumn(Name = "LTE Cell 3rd RSRP")]
        public double? N3Rsrp { get; set; }
    }
}