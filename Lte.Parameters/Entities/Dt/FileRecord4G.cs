using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;

namespace Lte.Parameters.Entities.Dt
{
    [AutoMapFrom(typeof(FileRecord4GCsv), typeof(FileRecord4GDingli))]
    public class FileRecord4G : Entity, IGeoPoint<double?>, ICoverage, IRasterNum, IFileId, IStatTime
    {
        public int? Ind { get; set; }
        
        public short RasterNum { get; set; }
        
        public DateTime StatTime { get; set; }
        
        public double? Longtitute { get; set; }
        
        public double? Lattitute { get; set; }
        
        public int? ENodebId { get; set; }
        
        public byte? SectorId { get; set; }
        
        public double? Frequency { get; set; }
        
        [AutoMapPropertyResolve("Pn", typeof(FileRecord4GCsv))]
        public short? Pci { get; set; }
        
        public double? Rsrp { get; set; }
        
        public double? Sinr { get; set; }
        
        public byte? DlBler { get; set; }
        
        public double? CqiAverage { get; set; }
        
        public byte? UlMcs { get; set; }
        
        public byte? DlMcs { get; set; }
        
        public double? PdcpThroughputUl { get; set; }
        
        public double? PdcpThroughputDl { get; set; }
        
        public double? PhyThroughputDl { get; set; }
        
        public double? MacThroughputDl { get; set; }
        
        public int? PuschRbNum { get; set; }
        
        public int? PdschRbNum { get; set; }
        
        public int? PuschRbSizeAverage { get; set; }
        
        public int? PdschRbSizeAverage { get; set; }
        
        public short? N1Pci { get; set; }
        
        public double? N1Rsrp { get; set; }
        
        public short? N2Pci { get; set; }
        
        public double? N2Rsrp { get; set; }
        
        public short? N3Pci { get; set; }
        
        public double? N3Rsrp { get; set; }

        public bool IsCoverage()
        {
            return (Rsrp == null || Rsrp > -105)
                   && (Sinr == null || Sinr > -3);
        }

        public bool IsValid()
        {
            return Rsrp != null || Sinr != null;
        }

        public int FileId { get; set; }
    }
}