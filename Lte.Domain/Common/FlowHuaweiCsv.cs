using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lte.Domain.Common.Types;
using Lte.Domain.LinqToCsv;
using Lte.Domain.LinqToCsv.Context;
using Lte.Domain.LinqToCsv.Description;
using Lte.Domain.Regular;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Common
{
    public class FlowHuaweiCsv : IStatTime
    {
        [CsvColumn(Name = "开始时间")]
        [ArraySumProtection]
        public DateTime StatTime { get; set; }

        [CsvColumn(Name = "小区")]
        [ArraySumProtection]
        public string CellInfo { get; set; }
        
        public int ENodebId
        {
            get
            {
                if (string.IsNullOrEmpty(CellInfo)) return 0;
                var fields = CellInfo.GetSplittedFields(", ");
                return fields.Length < 4 ? 0 : fields[3].GetSplittedFields('=')[1].ConvertToInt(0);
            }
        }
        
        public byte LocalCellId
        {
            get
            {
                if (string.IsNullOrEmpty(CellInfo)) return 0;
                var fields = CellInfo.GetSplittedFields(", ");
                return fields.Length < 4 ? (byte)0 : fields[1].GetSplittedFields('=')[1].ConvertToByte(0);
            }
        }

        public byte SectorId
        {
            get
            {
                if (string.IsNullOrEmpty(CellInfo)) return 0;
                var fields = CellInfo.GetSplittedFields(", ");
                return fields.Length < 6 ? (byte)0 : fields[5].GetSplittedFields('=')[1].ConvertToByte(0);
            }
        }

        [CsvColumn(Name = "小区PDCP层所发送的下行数据的总吞吐量 (比特)")]
        public long PdcpDownlinkFlowInByte { get; set; }

        [CsvColumn(Name = "小区PDCP层所接收到的上行数据的总吞吐量 (比特)")]
        public long PdcpUplinkFlowInByte { get; set; }

        [CsvColumn(Name = "小区内的平均用户数 (无)")]
        [ArrayAverage]
        public double AverageUsers { get; set; }

        [CsvColumn(Name = "小区内的最大用户数 (无)")]
        [ArrayMax]
        public int MaxUsers { get; set; }

        [CsvColumn(Name = "平均激活用户数 (无)")]
        [ArrayAverage]
        public double AverageActiveUsers { get; set; }

        [CsvColumn(Name = "最大激活用户数 (无)")]
        [ArrayMax]
        public int MaxActiveUsers { get; set; }

        [CsvColumn(Name = "上行平均激活用户数 (无)")]
        [ArrayAverage]
        public double UplinkAverageUsers { get; set; }

        [CsvColumn(Name = "上行最大激活用户数 (无)")]
        [ArrayMax]
        public int UplinkMaxUsers { get; set; }

        [CsvColumn(Name = "下行平均激活用户数 (无)")]
        [ArrayAverage]
        public double DownlinkAverageUsers { get; set; }

        [CsvColumn(Name = "下行最大激活用户数 (无)")]
        [ArrayMax]
        public int DownlinkMaxUsers { get; set; }

        [CsvColumn(Name = "小区下行有数据传输总时长(1ms精度) (毫秒)")]
        [ArraySumProtection]
        public string DownlinkDurationInMsString { get; set; }

        public int DownlinkDurationInMs
        {
            get { return DownlinkDurationInMsString.ConvertToInt(0); }
            set { DownlinkDurationInMsString = value.ToString(); }
        }

        [CsvColumn(Name = "小区上行有数据传输总时长(1ms精度) (毫秒)")]
        [ArraySumProtection]
        public string UplinkDurationInMsString { get; set; }

        public int UplinkDurationInMs
        {
            get { return UplinkDurationInMsString.ConvertToInt(0); }
            set { UplinkDurationInMsString = value.ToString(); }
        }

        [CsvColumn(Name = "小区Uu接口寻呼用户个数 (无)")]
        public string PagingUsersString { get; set; }

        [CsvColumn(Name = "小区接收到属于Group A的Preamble消息次数 (无)")]
        public int GroupAPreambles { get; set; }

        [CsvColumn(Name = "小区接收到属于Group B的Preamble消息的次数 (无)")]
        public int GroupBPreambles { get; set; }

        [CsvColumn(Name = "小区接收到专用前导消息的次数 (无)")]
        public int DedicatedPreambles { get; set; }

        [CsvColumn(Name = "统计周期内上行DCI所使用的PDCCH CCE个数 (无)")]
        public long UplinkDciCces { get; set; }

        public double UplinkDciCceRate => TotalCces == 0 ? 0 : (double)UplinkDciCces / TotalCces;

        [CsvColumn(Name = "统计周期内下行DCI所使用的PDCCH CCE个数 (无)")]
        public long DownlinkDciCces { get; set; }

        public double DownlinkDciCceRate => TotalCces == 0 ? 0 : (double)DownlinkDciCces / TotalCces;

        [CsvColumn(Name = "统计周期内可用的PDCCH CCE的个数 (无)")]
        public long TotalCces { get; set; }

        [CsvColumn(Name = "PUCCH的PRB资源分配的平均值 (无)")]
        public string PucchPrbsString { get; set; }

        [CsvColumn(Name = "使UE缓存为空的最后一个TTI所传的上行PDCP吞吐量 (比特)")]
        [ArraySumProtection]
        public string LastTtiUplinkFlowInByteString { get; set; }

        public long LastTtiUplinkFlowInByte
        {
            get { return LastTtiUplinkFlowInByteString.ConvertToLong(0); }
            set { LastTtiUplinkFlowInByteString = value.ToString(); }
        }
        

        [CsvColumn(Name = "扣除使UE缓存为空的最后一个TTI之后的上行数传时长 (毫秒)")]
        [ArraySumProtection]
        public string ButLastUplinkDurationInMsString { get; set; }

        public int ButLastUplinkDurationInMs
        {
            get { return ButLastUplinkDurationInMsString.ConvertToInt(0); }
            set { ButLastUplinkDurationInMsString = value.ToString(); }
        } 

        [CsvColumn(Name = "使缓存为空的最后一个TTI所传的下行PDCP吞吐量 (比特)")]
        [ArraySumProtection]
        public string LastTtiDownlinkFlowInByteString { get; set; }

        public long LastTtiDownlinkFlowInByte {
            get { return LastTtiDownlinkFlowInByteString.ConvertToLong(0); }
            set { LastTtiDownlinkFlowInByteString = value.ToString(); }
        }

        [CsvColumn(Name = "扣除使下行缓存为空的最后一个TTI之后的数传时长 (毫秒)")]
        [ArraySumProtection]
        public string ButLastDownlinkDurationInMsString { get; set; }

        public int ButLastDownlinkDurationInMs
        {
            get { return ButLastDownlinkDurationInMsString.ConvertToInt(0); }
            set { ButLastDownlinkDurationInMsString = value.ToString(); }
        }

        [CsvColumn(Name = "RANK1的上报次数 (无)")]
        public string SchedulingRank1String { get; set; }

        [CsvColumn(Name = "RANK2的上报次数 (无)")]
        public string SchedulingRank2String { get; set; }

        [CsvColumn(Name = "重定向到CDMA2000 HRPD的总次数 (无)")]
        public int RedirectCdma2000 { get; set; }

        [CsvColumn(Name = "emergency原因的RRC连接建立尝试次数 (无)")]
        public int EmergencyRrcRequest { get; set; }

        [CsvColumn(Name = "emergency原因的RRC连接建立尝试次数(包括重发) (无)")]
        public int EmergencyRrcRequestAll { get; set; }

        [CsvColumn(Name = "emergency原因的RRC连接建立成功次数 (无)")]
        public int EmergencyRrcSuccess { get; set; }

        [CsvColumn(Name = "highPriorityAccess原因的RRC连接建立尝试次数 (无)")]
        public int HighPriorityRrcRequest { get; set; }

        [CsvColumn(Name = "highPriorityAccess原因的RRC连接建立尝试次数(包括重发) (无)")]
        public int HighPriorityRrcRequestAll { get; set; }

        [CsvColumn(Name = "highPriorityAccess原因的RRC连接建立成功次数 (无)")]
        public int HighPriorityRrcSuccess { get; set; }

        [CsvColumn(Name = "mo-Data原因的RRC连接建立尝试次数 (无)")]
        public int MoDataRrcRequest { get; set; }

        [CsvColumn(Name = "mo-Data原因的RRC连接建立尝试次数(包括重发) (无)")]
        public int MoDataRrcRequestAll { get; set; }

        [CsvColumn(Name = "mo-Data原因的RRC连接建立成功次数 (无)")]
        public int MoDataRrcSuccess { get; set; }

        [CsvColumn(Name = "mo-Signalling原因的RRC连接建立尝试次数 (无)")]
        public int MoSignallingRrcRequest { get; set; }

        [CsvColumn(Name = "mo-Signalling原因的RRC连接建立尝试次数(包括重发) (无)")]
        public int MoSignallingRrcRequestAll { get; set; }

        [CsvColumn(Name = "mo-Signalling原因的RRC连接建立成功次数 (无)")]
        public int MoSignallingRrcSuccess { get; set; }

        [CsvColumn(Name = "mt-Access原因的RRC连接建立尝试次数 (无)")]
        public int MtAccessRrcRequest { get; set; }

        [CsvColumn(Name = "mt-Access原因的RRC连接建立尝试次数(包括重发) (无)")]
        public int MtAccessRrcRequestAll { get; set; }

        [CsvColumn(Name = "mt-Access原因的RRC连接建立成功次数 (无)")]
        public int MtAccessRrcSuccess { get; set; }

        [CsvColumn(Name = "其它资源分配失败而导致RRC连接建立失败的次数 (无)")]
        public int RrcFailOtherResource { get; set; }

        [CsvColumn(Name = "用户数规格受限导致的RRC连接建立失败次数 (无)")]
        public int RrcFailUserLimit { get; set; }

        [CsvColumn(Name = "其它原因导致的RRC连接建立被拒绝的次数 (无)")]
        public int RrcRejectFail { get; set; }

        [CsvColumn(Name = "MME过载导致的发送RRC Connection Reject消息次数 (无)")]
        public int RrcRejectOverload { get; set; }

        [CsvColumn(Name = "流控导致的RRC重建请求消息丢弃次数 (无)")]
        public int RrcReconstructionLostFlowControl { get; set; }

        [CsvColumn(Name = "流控导致的RRC Connection Request 消息丢弃次数 (无)")]
        public int RrcRequestLostFlowControl { get; set; }

        [CsvColumn(Name = "资源分配失败而导致RRC连接建立失败次数 (无)")]
        public int RrcFailResourceAssignment { get; set; }

        [CsvColumn(Name = "UE无应答而导致RRC连接建立失败次数 (无)")]
        public int RrcFailUeNoAnswer { get; set; }

        [CsvColumn(Name = "因为SRS资源分配失败而导致RRC连接建立失败的次数 (无)")]
        public int RrcFailSrsAssignment { get; set; }

        [CsvColumn(Name = "因为PUCCH资源分配失败而导致RRC连接建立失败的次数 (无)")]
        public int RrcFailPucchAssignment { get; set; }

        [CsvColumn(Name = "流控导致的发送RRC Connection Reject消息次数 (无)")]
        public int RrcRejectFlowControl { get; set; }

        [CsvColumn(Name = "空口上报全带宽CQI为0的次数 (无)")]
        public int Cqi0Times { get; set; }

        [CsvColumn(Name = "空口上报全带宽CQI为1的次数 (无)")]
        public int Cqi1Times { get; set; }

        [CsvColumn(Name = "空口上报全带宽CQI为2的次数 (无)")]
        public int Cqi2Times { get; set; }

        [CsvColumn(Name = "空口上报全带宽CQI为3的次数 (无)")]
        public int Cqi3Times { get; set; }

        [CsvColumn(Name = "空口上报全带宽CQI为4的次数 (无)")]
        public int Cqi4Times { get; set; }

        [CsvColumn(Name = "空口上报全带宽CQI为5的次数 (无)")]
        public int Cqi5Times { get; set; }

        [CsvColumn(Name = "空口上报全带宽CQI为6的次数 (无)")]
        public int Cqi6Times { get; set; }

        [CsvColumn(Name = "空口上报全带宽CQI为7的次数 (无)")]
        public int Cqi7Times { get; set; }

        [CsvColumn(Name = "空口上报全带宽CQI为8的次数 (无)")]
        public int Cqi8Times { get; set; }

        [CsvColumn(Name = "空口上报全带宽CQI为9的次数 (无)")]
        public int Cqi9Times { get; set; }

        [CsvColumn(Name = "空口上报全带宽CQI为10的次数 (无)")]
        public int Cqi10Times { get; set; }

        [CsvColumn(Name = "空口上报全带宽CQI为11的次数 (无)")]
        public int Cqi11Times { get; set; }

        [CsvColumn(Name = "空口上报全带宽CQI为12的次数 (无)")]
        public int Cqi12Times { get; set; }

        [CsvColumn(Name = "空口上报全带宽CQI为13的次数 (无)")]
        public int Cqi13Times { get; set; }

        [CsvColumn(Name = "空口上报全带宽CQI为14的次数 (无)")]
        public int Cqi14Times { get; set; }

        [CsvColumn(Name = "空口上报全带宽CQI为15的次数 (无)")]
        public int Cqi15Times { get; set; }

        [CsvColumn(Name = "下行Physical Resource Block被使用的平均个数 (无)")]
        public double PdschPrbs { get; set; }

        [CsvColumn(Name = "下行PDSCH DRB的Physical Resource Block被使用的平均个数 (无)")]
        public double DownlinkDtchPrbNumber { get; set; }

        [CsvColumn(Name = "下行可用的PRB个数 (无)")]
        public int DownlinkPrbSubframe { get; set; }

        [CsvColumn(Name = "上行Physical Resource Block被使用的平均个数 (无)")]
        public double PuschPrbs { get; set; }

        [CsvColumn(Name = "上行PUSCH DRB的Physical Resource Block被使用的平均个数 (无)")]
        public double UplinkDtchPrbNumber { get; set; }

        [CsvColumn(Name = "上行可用的PRB个数 (无)")]
        public int UplinkPrbSubframe { get; set; }

        [CsvColumn(Name = "PDSCH的PRB资源利用率在区间0内的样本数 (无)")]
        public double PdschUsageInterval0Seconds { get; set; }

        [CsvColumn(Name = "PDSCH的PRB资源利用率在区间1内的样本数 (无)")]
        public double PdschUsageInterval10Seconds { get; set; }

        [CsvColumn(Name = "PDSCH的PRB资源利用率在区间2内的样本数 (无)")]
        public double PdschUsageInterval20Seconds { get; set; }

        [CsvColumn(Name = "PDSCH的PRB资源利用率在区间3内的样本数 (无)")]
        public double PdschUsageInterval30Seconds { get; set; }

        [CsvColumn(Name = "PDSCH的PRB资源利用率在区间4内的样本数 (无)")]
        public double PdschUsageInterval40Seconds { get; set; }

        [CsvColumn(Name = "PDSCH的PRB资源利用率在区间5内的样本数 (无)")]
        public double PdschUsageInterval50Seconds { get; set; }

        [CsvColumn(Name = "PDSCH的PRB资源利用率在区间6内的样本数 (无)")]
        public double PdschUsageInterval60Seconds { get; set; }

        [CsvColumn(Name = "PDSCH的PRB资源利用率在区间7内的样本数 (无)")]
        public double PdschUsageInterval70Seconds { get; set; }

        [CsvColumn(Name = "PDSCH的PRB资源利用率在区间8内的样本数 (无)")]
        public double PdschUsageInterval80Seconds { get; set; }

        [CsvColumn(Name = "PDSCH的PRB资源利用率在区间9内的样本数 (无)")]
        public double PdschUsageInterval90Seconds { get; set; }

        [CsvColumn(Name = "PUSCH的PRB资源利用率在区间0内的样本数 (无)")]
        public double PuschUsageInterval0Seconds { get; set; }

        [CsvColumn(Name = "PUSCH的PRB资源利用率在区间1内的样本数 (无)")]
        public double PuschUsageInterval10Seconds { get; set; }

        [CsvColumn(Name = "PUSCH的PRB资源利用率在区间2内的样本数 (无)")]
        public double PuschUsageInterval20Seconds { get; set; }

        [CsvColumn(Name = "PUSCH的PRB资源利用率在区间3内的样本数 (无)")]
        public double PuschUsageInterval30Seconds { get; set; }

        [CsvColumn(Name = "PUSCH的PRB资源利用率在区间4内的样本数 (无)")]
        public double PuschUsageInterval40Seconds { get; set; }

        [CsvColumn(Name = "PUSCH的PRB资源利用率在区间5内的样本数 (无)")]
        public double PuschUsageInterval50Seconds { get; set; }

        [CsvColumn(Name = "PUSCH的PRB资源利用率在区间6内的样本数 (无)")]
        public double PuschUsageInterval60Seconds { get; set; }

        [CsvColumn(Name = "PUSCH的PRB资源利用率在区间7内的样本数 (无)")]
        public double PuschUsageInterval70Seconds { get; set; }

        [CsvColumn(Name = "PUSCH的PRB资源利用率在区间8内的样本数 (无)")]
        public double PuschUsageInterval80Seconds { get; set; }

        [CsvColumn(Name = "PUSCH的PRB资源利用率在区间9内的样本数 (无)")]
        public double PuschUsageInterval90Seconds { get; set; }

        public static List<FlowHuaweiCsv> ReadFlowHuaweiCsvs(StreamReader reader)
        {
            return CsvContext.Read<FlowHuaweiCsv>(reader, CsvFileDescription.CommaDescription).ToList();
        }
    }
}