using System;
using System.Data.Linq.Mapping;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;

namespace Lte.Parameters.Entities.Dt
{
    [AutoMapFrom(typeof(FileRecord3GCsv))]
    public class FileRecord3G : Entity, IGeoPoint<double?>, ICoverage, IRasterNum, IFileId, IStatTime
    {
        public short RasterNum { get; set; }
        
        public DateTime StatTime { get; set; }
        
        public double? Longtitute { get; set; }
        
        public double? Lattitute { get; set; }
        
        public short? Pn { get; set; }
        
        public double? Sinr { get; set; }
        
        public double? RxAgc0 { get; set; }
        
        public double? RxAgc1 { get; set; }
        
        public double? TxAgc { get; set; }
        
        public double? TotalCi { get; set; }
        
        public int? DrcValue { get; set; }
        
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

        public int FileId { get; set; }
    }
}