using System;
using System.Data.Linq.Mapping;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;

namespace Lte.Parameters.Entities.Dt
{
    [AutoMapFrom(typeof(FileRecord2GCsv))]
    public class FileRecord2G : Entity, IGeoPoint<double?>, ICoverage, IRasterNum, IFileId, IStatTime
    {
        public short RasterNum { get; set; }
        
        public DateTime StatTime { get; set; }
        
        public double? Longtitute { get; set; }
        
        public double? Lattitute { get; set; }
        
        public short? Pn { get; set; }
        
        public double? Ecio { get; set; }
        
        public double? RxAgc { get; set; }
        
        public double? TxAgc { get; set; }

        public double? TxPower { get; set; }
        
        public double? TxGain { get; set; }
        
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

        public int FileId { get; set; }
    }
}