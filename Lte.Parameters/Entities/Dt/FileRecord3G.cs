using System.Data.Linq.Mapping;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Lte.Domain.Common.Geo;

namespace Lte.Parameters.Entities.Dt
{
    [AutoMapFrom(typeof(FileRecord3GCsv))]
    public class FileRecord3G : IGeoPoint<double?>, ICoverage, IRasterNum
    {
        [Column(Name = "rasterNum", DbType = "SmallInt")]
        public short RasterNum { get; set; }

        [Column(Name = "testTime", DbType = "Char(50)")]
        public string TestTimeString { get; set; }

        [Column(Name = "lon", DbType = "Float")]
        public double? Longtitute { get; set; }

        [Column(Name = "lat", DbType = "Float")]
        public double? Lattitute { get; set; }

        [Column(Name = "refPN", DbType = "SmallInt")]
        public short? Pn { get; set; }

        [Column(Name = "SINR", DbType = "Real")]
        public double? Sinr { get; set; }

        [Column(Name = "RxAGC0", DbType = "Real")]
        public double? RxAgc0 { get; set; }

        [Column(Name = "RxAGC1", DbType = "Real")]
        public double? RxAgc1 { get; set; }

        [Column(Name = "txAGC", DbType = "Real")]
        public double? TxAgc { get; set; }

        [Column(Name = "totalC2I", DbType = "Real")]
        public double? TotalCi { get; set; }

        [Column(Name = "DRCValue", DbType = "Int")]
        public int? DrcValue { get; set; }

        [Column(Name = "RLPThrDL", DbType = "Int")]
        public int? RlpThroughput { get; set; }

        public bool IsCoverage()
        {
            return (RxAgc0 == null ||RxAgc0 > -90)
                && (RxAgc1 == null || RxAgc1 > -90)
                && (TxAgc == null || TxAgc < 15)
                && (Sinr == null || Sinr > -5.5);
        }

        public bool IsValid()
        {
            return RxAgc0 != null || RxAgc1 != null || Sinr != null;
        }
    }
}