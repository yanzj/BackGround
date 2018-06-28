using System.Data.Linq.Mapping;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Lte.Domain.Common.Geo;

namespace Lte.Parameters.Entities.Dt
{
    [AutoMapFrom(typeof(FileRecord2GCsv))]
    public class FileRecord2G : IGeoPoint<double?>, ICoverage, IRasterNum
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

        [Column(Name = "EcIo", DbType = "Real")]
        public double? Ecio { get; set; }

        [Column(Name = "RxAGC", DbType = "Real")]
        public double? RxAgc { get; set; }

        [Column(Name = "txAGC", DbType = "Real")]
        public double? TxAgc { get; set; }

        [Column(Name = "txPower", DbType = "Real")]
        public double? TxPower { get; set; }

        [Column(Name = "txGain", DbType = "Real")]
        public double? TxGain { get; set; }

        [Column(Name = "GridNo50m", DbType = "Int")]
        public int? GridNumber { get; set; }

        public bool IsCoverage()
        {
            return (RxAgc == null || RxAgc > -90)
                   && (TxAgc == null || TxAgc < 15)
                   && (Ecio == null || Ecio > -12);
        }

        public bool IsValid()
        {
            return RxAgc != null || Ecio != null;
        }
    }
}