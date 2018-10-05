using System;
using System.Globalization;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.LinqToCsv;
using Lte.Domain.Regular;

namespace Lte.Parameters.Entities.Dt
{
    public class FileRecord3GCsv : IGeoPoint<double?>, IPn, IStatTime
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

        public string CellName { get; set; }

        [CsvColumn(Name = "EV RxAGC0")]
        public double? RxAgc0 { get; set; }

        [CsvColumn(Name = "EV RxAGC1")]
        public double? RxAgc1 { get; set; }

        [CsvColumn(Name = "EV TxAGC")]
        public double? TxAgc { get; set; }

        [CsvColumn(Name = "EV Total C/I")]
        public double? TotalCi { get; set; }

        [CsvColumn(Name = "EV Total SINR")]
        public double? Sinr { get; set; }

        [CsvColumn(Name = "EV DRC Value")]
        public double? DrcValue { get; set; }

        [CsvColumn(Name = "EV Serving Sector PN")]
        public double? Pn { get; set; }

        [CsvColumn(Name = "EV RLP Instant Throughput DL")]
        public double? RlpThroughput { get; set; }
    }
}