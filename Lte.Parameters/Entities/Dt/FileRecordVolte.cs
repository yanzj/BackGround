using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Lte.Domain.Common.Geo;

namespace Lte.Parameters.Entities.Dt
{
    [AutoMapFrom(typeof(FileRecordVolteCsv))]
    public class FileRecordVolte : IGeoPoint<double?>, ICoverage, IRasterNum
    {
        [Column(Name = "lon", DbType = "Float")]
        public double? Longtitute { get; set; }

        [Column(Name = "lat", DbType = "Float")]
        public double? Lattitute { get; set; }

        [Column(Name = "testTime", DbType = "Char(50)")]
        public string TestTimeString { get; set; }

        [Column(Name = "eNodeBID", DbType = "Int")]
        public int? ENodebId { get; set; }

        [Column(Name = "cellID", DbType = "TinyInt")]
        public byte? SectorId { get; set; }

        [Column(Name = "RSRP", DbType = "Real")]
        public double? Rsrp { get; set; }

        [Column(Name = "SINR", DbType = "Real")]
        public double? Sinr { get; set; }

        [Column(Name = "PDCCHPathloss", DbType = "Real")]
        public double? PdcchPathloss { get; set; }

        [Column(Name = "PDSCHPathloss", DbType = "Real")]
        public double? PdschPathloss { get; set; }

        [Column(Name = "PUCCHTxPower", DbType = "Real")]
        public double? PucchTxPower { get; set; }

        [Column(Name = "PUCCHPathloss", DbType = "Real")]
        public double? PucchPathloss { get; set; }

        [Column(Name = "PUSCHPathloss", DbType = "Real")]
        public double? PuschPathloss { get; set; }

        [Column(Name = "Cell1stEARFCN", DbType = "Int")]
        public int? FirstEarfcn { get; set; }

        [Column(Name = "Cell1stPCI", DbType = "SmallInt")]
        public short? FirstPci { get; set; }

        [Column(Name = "Cell1stRSRP", DbType = "Real")]
        public double? FirstRsrp { get; set; }

        [Column(Name = "Cell2ndEARFCN", DbType = "Int")]
        public int? SecondEarfcn { get; set; }

        [Column(Name = "Cell2ndPCI", DbType = "SmallInt")]
        public short? SecondPci { get; set; }

        [Column(Name = "Cell2ndRSRP", DbType = "Real")]
        public double? SecondRsrp { get; set; }

        [Column(Name = "Cell3rdEARFCN", DbType = "Int")]
        public int? ThirdEarfcn { get; set; }

        [Column(Name = "Cell3rdPCI", DbType = "SmallInt")]
        public short? ThirdPci { get; set; }

        [Column(Name = "Cell3rdRSRP", DbType = "Real")]
        public double? ThirdRsrp { get; set; }

        [Column(Name = "Cell4thEARFCN", DbType = "Int")]
        public int? FourthEarfcn { get; set; }

        [Column(Name = "Cell4thPCI", DbType = "SmallInt")]
        public short? FourthPci { get; set; }

        [Column(Name = "Cell4thRSRP", DbType = "Real")]
        public double? FourthRsrp { get; set; }

        [Column(Name = "Cell5thEARFCN", DbType = "Int")]
        public int? FifthEarfcn { get; set; }

        [Column(Name = "Cell5thPCI", DbType = "SmallInt")]
        public short? FifthPci { get; set; }

        [Column(Name = "Cell5thRSRP", DbType = "Real")]
        public double? FifthRsrp { get; set; }

        [Column(Name = "Cell6thEARFCN", DbType = "Int")]
        public int? SixthEarfcn { get; set; }

        [Column(Name = "Cell6thPCI", DbType = "SmallInt")]
        public short? SixthPci { get; set; }

        [Column(Name = "Cell6thRSRP", DbType = "Real")]
        public double? SixthRsrp { get; set; }

        [Column(Name = "RTPFrameType", DbType = "TinyInt")]
        public byte? RtpFrameType { get; set; }

        [Column(Name = "RTPLoggedPayloadSize", DbType = "Real")]
        public double? RtpLoggedPayloadSize { get; set; }

        [Column(Name = "RTPPayloadSize", DbType = "Real")]
        public double? RtpPayloadSize { get; set; }

        [Column(Name = "PacketLossCount", DbType = "Real")]
        public double? PacketLossCount { get; set; }

        [Column(Name = "RxRTPPacketNum", DbType = "Real")]
        public double? RxRtpPacketNum { get; set; }

        [Column(Name = "VoicePacketDelay", DbType = "Real")]
        public double? VoicePacketDelay { get; set; }

        [Column(Name = "VoicePacketLossRate", DbType = "Real")]
        public double? VoicePacketLossRate { get; set; }

        [Column(Name = "VoiceRFC1889Jitter", DbType = "Real")]
        public double? VoiceJitter { get; set; }

        [Column(Name = "Rank2CQICode0", DbType = "Real")]
        public double? Rank2Cqi { get; set; }

        [Column(Name = "Rank1CQI", DbType = "Real")]
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

        [Column(Name = "rasterNum", DbType = "SmallInt")]
        public short RasterNum { get; set; }
    }
}
