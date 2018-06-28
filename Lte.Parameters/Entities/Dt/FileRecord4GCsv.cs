using System;
using System.Globalization;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.LinqToCsv;
using Lte.Domain.Regular;

namespace Lte.Parameters.Entities.Dt
{
    [AutoMapFrom(typeof(FileRecord4GZte), typeof(FileRecord4GHuawei))]
    public class FileRecord4GCsv : IGeoPoint<double?>, IPn, IStatTime
    {
        [CsvColumn(Name = "Index")]
        public int? Ind { get; set; }

        public DateTime? LogTime { get; set; }

        [CsvColumn(Name = "Time")]
        public string ComputerTime { get; set; }

        public DateTime StatTime
        {
            get { return ComputerTime.ConvertToDateTime(DateTime.Now); }
            set { ComputerTime = value.ToString(CultureInfo.InvariantCulture); }
        }

        [CsvColumn(Name = "Lon")]
        public double? Longtitute { get; set; }

        [CsvColumn(Name = "Lat")]
        public double? Lattitute { get; set; }

        [CsvColumn(Name = "eNodeB ID")]
        public int? ENodebId { get; set; }

        [CsvColumn(Name = "Cell ID")]
        public byte? SectorId { get; set; }

        [CsvColumn(Name = "Frequency DL(MHz)")]
        public double? Frequency { get; set; }

        [CsvColumn(Name = "PCI")]
        public double? Pn { get; set; }

        [CsvColumn(Name = "CRS RSRP")]
        public double? Rsrp { get; set; }

        [CsvColumn(Name = "CRS SINR")]
        public double? Sinr { get; set; }

        [CsvColumn(Name = "DL BLER(%)")]
        public byte? DlBler { get; set; }

        [CsvColumn(Name = "CQI Average")]
        public double? CqiAverage { get; set; }

        [CsvColumn(Name = "UL MCS Value # Average")]
        public byte? UlMcs { get; set; }

        [CsvColumn(Name = "DL MCS Value # Average")]
        public byte? DlMcs { get; set; }

        [CsvColumn(Name = "PDCP Thr'put UL(kb/s)")]
        public double? PdcpThroughputUl { get; set; }

        [CsvColumn(Name = "PDCP Thr'put DL(kb/s)")]
        public double? PdcpThroughputDl { get; set; }

        [CsvColumn(Name = "PHY Thr'put DL(kb/s)")]
        public double? PhyThroughputDl { get; set; }

        [CsvColumn(Name = "MAC Thr'put DL(kb/s)")]
        public double? MacThroughputDl { get; set; }

        [CsvColumn(Name = "PUSCH Rb Num/s")]
        public int? PuschRbNum { get; set; }

        [CsvColumn(Name = "PDSCH Rb Num/s")]
        public int? PdschRbNum { get; set; }

        [CsvColumn(Name = "PUSCH TB Size Ave(bits)")]
        public int? PuschRbSizeAverage { get; set; }

        [CsvColumn(Name = "PDSCH TB Size Ave(bits)")]
        public int? PdschRbSizeAverage { get; set; }

        [CsvColumn(Name = "NCell PCI #1")]
        public short? N1Pci { get; set; }

        [CsvColumn(Name = "NCell RSRP #1")]
        public double? N1Rsrp { get; set; }

        [CsvColumn(Name = "NCell PCI #2")]
        public short? N2Pci { get; set; }

        [CsvColumn(Name = "NCell RSRP #2")]
        public double? N2Rsrp { get; set; }

        [CsvColumn(Name = "NCell PCI #3")]
        public short? N3Pci { get; set; }

        [CsvColumn(Name = "NCell RSRP #3")]
        public double? N3Rsrp { get; set; }
    }
}