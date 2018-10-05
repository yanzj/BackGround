using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;

namespace Lte.Parameters.Entities.Dt
{
    [AutoMapFrom(typeof(FileRecordVolteCsv))]
    public class FileRecordVolte : Entity, IGeoPoint<double?>, ICoverage, IRasterNum, IFileId, IStatTime
    {
        public double? Longtitute { get; set; }
        
        public double? Lattitute { get; set; }
        
        public DateTime StatTime { get; set; }
        
        public int? ENodebId { get; set; }
        
        public byte? SectorId { get; set; }
        
        public double? Rsrp { get; set; }
        
        public double? Sinr { get; set; }
        
        public double? PdcchPathloss { get; set; }
        
        public double? PdschPathloss { get; set; }
        
        public double? PucchTxPower { get; set; }
        
        public double? PucchPathloss { get; set; }
        
        public double? PuschPathloss { get; set; }
        
        public int? FirstEarfcn { get; set; }
        
        public short? FirstPci { get; set; }
        
        public double? FirstRsrp { get; set; }
        
        public int? SecondEarfcn { get; set; }
        
        public short? SecondPci { get; set; }
        
        public double? SecondRsrp { get; set; }
        
        public int? ThirdEarfcn { get; set; }
        
        public short? ThirdPci { get; set; }
        
        public double? ThirdRsrp { get; set; }
        
        public int? FourthEarfcn { get; set; }
        
        public short? FourthPci { get; set; }
        
        public double? FourthRsrp { get; set; }
        
        public int? FifthEarfcn { get; set; }
        
        public short? FifthPci { get; set; }
        
        public double? FifthRsrp { get; set; }
        
        public int? SixthEarfcn { get; set; }
        
        public short? SixthPci { get; set; }
        
        public double? SixthRsrp { get; set; }
        
        public byte? RtpFrameType { get; set; }
        
        public double? RtpLoggedPayloadSize { get; set; }
        
        public double? RtpPayloadSize { get; set; }
        
        public double? PolqaMos { get; set; }
        
        public double? PacketLossCount { get; set; }
        
        public double? RxRtpPacketNum { get; set; }
        
        public double? VoicePacketDelay { get; set; }
        
        public double? VoicePacketLossRate { get; set; }
        
        public double? VoiceJitter { get; set; }
        
        public double? Rank2Cqi { get; set; }
        
        public double? Rank1Cqi { get; set; }

        public bool IsCoverage()
        {
            return (Rsrp == null || Rsrp > -105)
                   && (Sinr == null || Sinr > -3);
        }

        public bool IsValid()
        {
            return Rsrp != null || Sinr != null;
        }
        
        public short RasterNum { get; set; }

        public int FileId { get; set; }
    }
}
