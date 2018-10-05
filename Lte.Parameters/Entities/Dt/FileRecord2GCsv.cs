using System;
using System.Globalization;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.LinqToCsv;
using Lte.Domain.Regular;

namespace Lte.Parameters.Entities.Dt
{
    [AutoMapFrom(typeof(FileRecord2GDingli))]
    public class FileRecord2GCsv : IGeoPoint<double?>, IPn, IStatTime
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

        public string Event { get; set; }

        [CsvColumn(Name = "Direction ")]
        public string Direction { get; set; }

        [CsvColumn(Name = "RxAGC")]
        public double? RxAgc { get; set; }

        [CsvColumn(Name = "TxAGC")]
        public double? TxAgc { get; set; }

        [CsvColumn(Name = "Total Ec/Io")]
        public double? EcIo { get; set; }

        [CsvColumn(Name = "Reference PN")]
        public double? Pn { get; set; }

        [CsvColumn(Name = "TxPower")]
        public double? TxPower { get; set; }

        [CsvColumn(Name = "Tx Gain Adj")]
        public double? TxGain { get; set; }
    }
}