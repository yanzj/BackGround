using System;
using System.Globalization;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.LinqToCsv;
using Lte.Domain.Regular;

namespace Lte.Parameters.Entities.Dt
{
    public class FileRecordVolteCsv : IGeoPoint<double?>, IStatTime, IPn
    {
        [CsvColumn(Name = "Longitude")]
        public double? Longtitute { get; set; }

        [CsvColumn(Name = "Latitude")]
        public double? Lattitute { get; set; }
        
        public string ComputerTime { get; set; }

        public DateTime StatTime
        {
            get { return ComputerTime.ConvertToDateTime(DateTime.Now); }
            set { ComputerTime = value.ToString(CultureInfo.InvariantCulture); }
        }

        [CsvColumn(Name = "eNodeB ID")]
        public int? ENodebId { get; set; }

        [CsvColumn(Name = "SectorID")]
        public byte? SectorId { get; set; }

        [CsvColumn(Name = "PDCCH Pathloss")]
        public double? PdcchPathloss { get; set; }

        [CsvColumn(Name = "PDSCH Pathloss")]
        public double? PdschPathloss { get; set; }

        [CsvColumn(Name = "PUCCH TxPower")]
        public double? PucchTxPower { get; set; }

        [CsvColumn(Name = "PUCCH Pathloss")]
        public double? PucchPathloss { get; set; }

        [CsvColumn(Name = "PUSCH Pathloss")]
        public double? PuschPathloss { get; set; }

        [CsvColumn(Name = "RSRP")]
        public double? Rsrp { get; set; }

        [CsvColumn(Name = "SINR")]
        public double? Sinr { get; set; }

        [CsvColumn(Name = "Cell 1st EARFCN")]
        public int? FirstEarfcn { get; set; }

        [CsvColumn(Name = "Cell 1st PCI")]
        public short? FirstPci { get; set; }

        [CsvColumn(Name = "Cell 1st RSRP")]
        public double? FirstRsrp { get; set; }

        [CsvColumn(Name = "Cell 2nd EARFCN")]
        public int? SecondEarfcn { get; set; }

        [CsvColumn(Name = "Cell 2nd PCI")]
        public short? SecondPci { get; set; }

        [CsvColumn(Name = "Cell 2nd RSRP")]
        public double? SecondRsrp { get; set; }

        [CsvColumn(Name = "Cell 3rd EARFCN")]
        public int? ThirdEarfcn { get; set; }

        [CsvColumn(Name = "Cell 3rd PCI")]
        public short? ThirdPci { get; set; }

        [CsvColumn(Name = "Cell 3rd RSRP")]
        public double? ThirdRsrp { get; set; }

        [CsvColumn(Name = "Cell 4th EARFCN")]
        public int? FourthEarfcn { get; set; }

        [CsvColumn(Name = "Cell 4th PCI")]
        public short? FourthPci { get; set; }

        [CsvColumn(Name = "Cell 4th RSRP")]
        public double? FourthRsrp { get; set; }

        [CsvColumn(Name = "Cell 5th EARFCN")]
        public int? FifthEarfcn { get; set; }

        [CsvColumn(Name = "Cell 5th PCI")]
        public short? FifthPci { get; set; }

        [CsvColumn(Name = "Cell 5th RSRP")]
        public double? FifthRsrp { get; set; }

        [CsvColumn(Name = "Cell 6th EARFCN")]
        public int? SixthEarfcn { get; set; }

        [CsvColumn(Name = "Cell 6th PCI")]
        public short? SixthPci { get; set; }

        [CsvColumn(Name = "Cell 6th RSRP")]
        public double? SixthRsrp { get; set; }

        [CsvColumn(Name = "RTP Frame Type")]
        public byte? RtpFrameType { get; set; }

        [CsvColumn(Name = "RTP Logged Payload Size")]
        public int? RtpLoggedPayloadSize { get; set; }

        [CsvColumn(Name = "RTP Payload Size")]
        public int? RtpPayloadSize { get; set; }

        [CsvColumn(Name = "Packet LossCount")]
        public int? PacketLossCount { get; set; }

        [CsvColumn(Name = "Rx RTP Packet Num")]
        public int? RxRtpPacketNum { get; set; }

        [CsvColumn(Name = "Voice Packet Delay")]
        public int? VoicePacketDelay { get; set; }

        [CsvColumn(Name = "Voice Packet Loss Rate")]
        public double? VoicePacketLossRate { get; set; }

        [CsvColumn(Name = "Voice RFC1889 Jitter")]
        public int? VoiceJitter { get; set; }

        [CsvColumn(Name = "Rank2 CQI Code0")]
        public byte? Rank2Cqi { get; set; }

        [CsvColumn(Name = "Rank1 CQI")]
        public byte? Rank1Cqi { get; set; }

        public double? Pn
        {
            get { return FirstPci; }
            set
            {
                if (value != null) FirstPci = (short)value;
            }
        }
    }
}
