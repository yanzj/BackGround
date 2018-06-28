using System;
using System.Globalization;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.LinqToCsv;
using Lte.Domain.Regular;

namespace Lte.Parameters.Entities.Dt
{
    public class FileRecord2GDingli : IGeoPoint<double?>, IPn, IStatTime
    {
        public string ComputerTime { get; set; }

        public DateTime StatTime
        {
            get { return ComputerTime.ConvertToDateTime(DateTime.Now); }
            set { ComputerTime = value.ToString(CultureInfo.InvariantCulture); }
        }

        public string TestTimeString => StatTime.ToString("yyyy-M-d HH:mm:ss.fff");

        [CsvColumn(Name = "Longitude")]
        public double? Longtitute { get; set; }

        [CsvColumn(Name = "Latitude")]
        public double? Lattitute { get; set; }
        
        [CsvColumn(Name = "CDMA RxAGC")]
        public double? RxAgc { get; set; }

        [CsvColumn(Name = "CDMA TxAGC")]
        public double? TxAgc { get; set; }

        [CsvColumn(Name = "CDMA Total Ec/Io")]
        public double? EcIo { get; set; }

        [CsvColumn(Name = "CDMA Reference PN")]
        public double? Pn { get; set; }

        [CsvColumn(Name = "CDMA TxPower")]
        public double? TxPower { get; set; }

        [CsvColumn(Name = "CDMA Tx Gain Adj")]
        public double? TxGain { get; set; }
    }
}