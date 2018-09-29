using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.LinqToCsv;
using Lte.Domain.LinqToCsv.Context;
using Lte.Domain.LinqToCsv.Description;
using Lte.Domain.Regular;

namespace Lte.Domain.Common
{
    public class FlowZteCsv : IStatTime, ILteCellQuery
    {
        [CsvColumn(Name = "开始时间")]
        public DateTime StatTime { get; set; }

        [CsvColumn(Name = "网元")]
        public int ENodebId { get; set; }

        [CsvColumn(Name = "小区")]
        public byte SectorId { get; set; }

        [CsvColumn(Name = "最大RRC连接用户数_1")]
        public int OldMaxRrcUsers { get; set; }

        [CsvColumn(Name = "最大RRC连接用户数-FDD")]
        public int NewMaxRrcUsers { get; set; }

        public int MaxRrcUsers => OldMaxRrcUsers + NewMaxRrcUsers;

        [CsvColumn(Name = "上行平均激活用户数_1")]
        public double OldUlAverageActiveUsers { get; set; }

        [CsvColumn(Name = "上行平均激活用户数-FDD_1484414261455-0-71")]
        public double NewUlAvgActUsers { get; set; }

        public double UplinkAverageActiveUsers => OldUlAverageActiveUsers + NewUlAvgActUsers;

        [CsvColumn(Name = "下行平均激活用户数_1")]
        public double OldDlAverageActiveUsers { get; set; }

        [CsvColumn(Name = "下行平均激活用户数-FDD_1484414261455-0-72")]
        public double NewDlAvgActUsers { get; set; }

        public double DownlinkAverageActiveUsers => OldDlAverageActiveUsers + NewDlAvgActUsers;

        [CsvColumn(Name = "平均RRC连接用户数_1")]
        public double AverageRrcUsers { get; set; }

        [CsvColumn(Name = "平均激活用户数_1")]
        public double OldAverageActiveUsers { get; set; }

        [CsvColumn(Name = "平均激活用户数")]
        public double NewAvgActUsers { get; set; }

        public double AverageActiveUsers => OldAverageActiveUsers + NewAvgActUsers;

        public int MaxActiveUsers
            =>
                OldMaxActiveUsers + UplinkQci8MaxActiveUsers + UplinkQci9MaxActiveUsers + DownlinkQci8MaxActiveUsers +
                DownlinkQci9MaxActiveUsers;

        [CsvColumn(Name = "最大激活用户数_1")]
        public int OldMaxActiveUsers { get; set; }

        [CsvColumn(Name = "[LTE]上行QCI8最大激活用户数")]
        public int UplinkQci8MaxActiveUsers { get; set; }

        [CsvColumn(Name = "[LTE]上行QCI9最大激活用户数")]
        public int UplinkQci9MaxActiveUsers { get; set; }

        [CsvColumn(Name = "[LTE]下行QCI8最大激活用户数")]
        public int DownlinkQci8MaxActiveUsers { get; set; }

        [CsvColumn(Name = "[LTE]下行QCI9最大激活用户数")]
        public int DownlinkQci9MaxActiveUsers { get; set; }

        [CsvColumn(Name = "小区PDCP接收上行数据的总时长(s)")]
        public int OldPdcpUplinkDuration { get; set; }

        [CsvColumn(Name = "小区PDCP接收上行数据的总时长(s)")]
        public int NewPdcpUlDuration { get; set; }

        public int PdcpUplinkDuration => OldPdcpUplinkDuration + NewPdcpUlDuration;

        [CsvColumn(Name = "小区PDCP发送下行数据的总时长(s)")]
        public int OldPdcpDownlinkDuration { get; set; }

        [CsvColumn(Name = "小区PDCP发送下行数据的总时长(s)")]
        public int NewPdcpDlDuration { get; set; }

        public int PdcpDownlinkDuration => OldPdcpDownlinkDuration + NewPdcpDlDuration;

        [CsvColumn(Name = "小区上行PDCP层流量（MB）_1440661576499")]
        public double OldUlPdcpFlowInMByte { get; set; }

        [CsvColumn(Name = "小区上行PDCP层流量（MB）")]
        public double NewUlPdcpFlowInMByte { get; set; }

        public double UplindPdcpFlowInMByte => OldUlPdcpFlowInMByte + NewUlPdcpFlowInMByte;

        [CsvColumn(Name = "小区下行PDCP层流量（MB）_1440661576499")]
        public double OldDlPdcpFlowInMByte { get; set; }

        [CsvColumn(Name = "小区下行PDCP层流量（MB）")]
        public double NewDlPdcpFlowInMByte { get; set; }

        public double DownlinkPdcpFlowInMByte => OldDlPdcpFlowInMByte + NewDlPdcpFlowInMByte;

        [CsvColumn(Name = "QCI8小区上行IP Throughput数据量高(兆比特)")]
        public string Qci8UplinkIpThroughputHigh { get; set; }

        [CsvColumn(Name = "QCI8小区上行IP Throughput数据量低(千比特)")]
        public string Qci8UplinkIpThroughputLow { get; set; }

        public double Qci8UplinkIpThroughput
            => (string.IsNullOrEmpty(Qci8UplinkIpThroughputHigh) ? 0 : Qci8UplinkIpThroughputHigh.ConvertToInt(0))
               +
               (string.IsNullOrEmpty(Qci8UplinkIpThroughputLow)
                   ? 0
                   : Qci8UplinkIpThroughputLow.Replace(",", "").ConvertToDouble(0) / 1024);

        [CsvColumn(Name = "QCI8小区上行IP Throughput数据传输时间(毫秒)")]
        public string Qci8UplinkIpThroughputDuration { get; set; }

        public double Qci8UplinkIpDuration => string.IsNullOrEmpty(Qci8UplinkIpThroughputDuration)
            ? 0
            : Qci8UplinkIpThroughputDuration.ConvertToDouble(0) / 1000;

        [CsvColumn(Name = "QCI9小区上行IP Throughput数据量高(兆比特)")]
        public string Qci9UplinkIpThroughputHigh { get; set; }

        [CsvColumn(Name = "QCI9小区上行IP Throughput数据量低(千比特)")]
        public string Qci9UplinkIpThroughputLow { get; set; }

        public double Qci9UplinkIpThroughput
            => (string.IsNullOrEmpty(Qci9UplinkIpThroughputHigh) ? 0 : Qci9UplinkIpThroughputHigh.ConvertToInt(0)) +
               (string.IsNullOrEmpty(Qci9UplinkIpThroughputLow)
                   ? 0
                   : Qci9UplinkIpThroughputLow.Replace(",", "").ConvertToDouble(0) / 1024);

        [CsvColumn(Name = "QCI9小区上行IP Throughput数据传输时间(毫秒)")]
        public string Qci9UplinkIpThroughputDuration { get; set; }

        public double Qci9UplinkIpDuration
            =>
                string.IsNullOrEmpty(Qci9UplinkIpThroughputDuration)
                    ? 0
                    : Qci9UplinkIpThroughputDuration.ConvertToDouble(0) / 1000;

        [CsvColumn(Name = "QCI8小区下行IP Throughput数据量高(兆比特)")]
        public string Qci8DownlinkIpThroughputHigh { get; set; }

        [CsvColumn(Name = "QCI8小区下行IP Throughput数据量低(千比特)")]
        public string Qci8DownlinkIpThroughputLow { get; set; }

        public double Qci8DownlinkIpThroughput
            => (string.IsNullOrEmpty(Qci8DownlinkIpThroughputHigh) ? 0 : Qci8DownlinkIpThroughputHigh.ConvertToInt(0)) +
               (string.IsNullOrEmpty(Qci8DownlinkIpThroughputLow)
                   ? 0
                   : Qci8DownlinkIpThroughputLow.Replace(",", "").ConvertToDouble(0) / 1024);

        [CsvColumn(Name = "QCI8小区下行IP Throughput数据传输时间(毫秒)")]
        public string Qci8DownlinkIpThroughputDuration { get; set; }

        public double Qci8DownlinkIpDuration
            =>
                string.IsNullOrEmpty(Qci8DownlinkIpThroughputDuration)
                    ? 0
                    : Qci8DownlinkIpThroughputDuration.ConvertToDouble(0) / 1000;

        [CsvColumn(Name = "QCI9小区下行IP Throughput数据量高(兆比特)")]
        public string Qci9DownlinkIpThroughputHigh { get; set; }

        [CsvColumn(Name = "QCI9小区下行IP Throughput数据量低(千比特)")]
        public string Qci9DownlinkIpThroughputLow { get; set; }

        public double Qci9DownlinkIpThroughput
            => (string.IsNullOrEmpty(Qci9DownlinkIpThroughputHigh) ? 0 : Qci9DownlinkIpThroughputHigh.ConvertToInt(0)) +
               (string.IsNullOrEmpty(Qci9DownlinkIpThroughputLow)
                   ? 0
                   : Qci9DownlinkIpThroughputLow.Replace(",", "").ConvertToDouble(0) / 1024);

        [CsvColumn(Name = "QCI9小区下行IP Throughput数据传输时间(毫秒)")]
        public string Qci9DownlinkIpThroughputDuration { get; set; }

        public double Qci9DownlinkIpDuration
            =>
                string.IsNullOrEmpty(Qci9DownlinkIpThroughputDuration)
                    ? 0
                    : Qci9DownlinkIpThroughputDuration.ConvertToDouble(0) / 1000;

        [CsvColumn(Name = "下行PDSCH信道上模式3调度次数")]
        public int SchedulingTm3Old { get; set; }

        [CsvColumn(Name = "下行PDSCH信道上模式4调度次数")]
        public int SchedulingTm4 { get; set; }

        [CsvColumn(Name = "下行PDSCH信道上模式2调度次数")]
        public int SchedulingTm2 { get; set; }

        public int SchedulingTm3 => SchedulingTm3Old + SchedulingTm4 + SchedulingTm2;

        [CsvColumn(Name = "下行PDSCH信道上模式3 RI=2调度次数")]
        public int SchedulingTm3Rank2Old { get; set; }

        [CsvColumn(Name = "下行PDSCH信道上模式4 RI=2调度次数")]
        public int SchedulingTm4Rank2 { get; set; }

        public int SchedulingTm3Rank2 => SchedulingTm3Rank2Old + SchedulingTm4Rank2;

        [CsvColumn(Name = "LTE-CDMA2000系统间重定向请求次数(A2事件触发原因)")]
        public int RedirectA2 { get; set; }

        [CsvColumn(Name = "LTE-CDMA2000系统间重定向请求次数(B2事件触发原因)")]
        public int RedirectB2 { get; set; }

        [CsvColumn(Name = "mt-Access类型RRC连接建立成功次数")]
        public int MtAccessRrcSuccess { get; set; }

        [CsvColumn(Name = "mt-Access类型RRC连接失败次数，定时器超时")]
        public int MtAccessRrcFailTimer { get; set; }

        [CsvColumn(Name = "mt-Access类型RRC连接失败次数，eNB接纳失败")]
        public int MtAccessRrcFailAllow { get; set; }

        [CsvColumn(Name = "mt-Access类型RRC连接失败次数，其他原因")]
        public int MtAccessRrcFailOther { get; set; }

        [CsvColumn(Name = "mo-Signalling类型RRC连接建立成功次数")]
        public int MoSignallingRrcSuccess { get; set; }

        [CsvColumn(Name = "mo-Signalling类型RRC连接失败次数，定时器超时")]
        public int MoSignallingRrcFailTimer { get; set; }

        [CsvColumn(Name = "mo-Signalling类型RRC连接失败次数，eNB接纳失败")]
        public int MoSignallingRrcFailAllow { get; set; }

        [CsvColumn(Name = "mo-Signalling类型RRC连接失败次数，其他原因")]
        public int MoSignallingRrcFailOther { get; set; }

        [CsvColumn(Name = "mo-Data类型RRC连接建立成功次数")]
        public int MoDataRrcSuccess { get; set; }

        [CsvColumn(Name = "mo-Data类型RRC连接失败次数，定时器超时")]
        public int MoDataRrcFailTimer { get; set; }

        [CsvColumn(Name = "mo-Data类型RRC连接失败次数，eNB接纳失败")]
        public int MoDataRrcFailAllow { get; set; }

        [CsvColumn(Name = "mo-Data类型RRC连接失败次数，其他原因")]
        public int MoDataRrcFailOther { get; set; }

        [CsvColumn(Name = "RRC连接释放次数，UserInactive触发释放")]
        public int RrcReleaseUserInactive { get; set; }

        [CsvColumn(Name = "RRC连接释放次数，MME通过上下文释放消息触发释放")]
        public int RrcReleaseMmeContext { get; set; }

        [CsvColumn(Name = "RRC连接释放次数，ENB资源不足引发的释放")]
        public int RrcReleaseResourceLack { get; set; }

        [CsvColumn(Name = "RRC连接释放次数，小区关断或复位引发释放")]
        public int RrcReleaseCellReset { get; set; }

        [CsvColumn(Name = "RRC连接释放次数，ENB触发的其他原因")]
        public int RrcReleaseOther { get; set; }

        [CsvColumn(Name = "highPriorityAccess类型RRC连接建立成功次数")]
        public int HighPriorityAccessRrcSuccess { get; set; }

        [CsvColumn(Name = "highPriorityAccess类型RRC连接失败次数，定时器超时")]
        public int HighPriorityAccessRrcFailTimer { get; set; }

        [CsvColumn(Name = "highPriorityAccess类型RRC连接失败次数，eNB接纳失败")]
        public int HighPriorityAccessRrcFailAllow { get; set; }

        [CsvColumn(Name = "highPriorityAccess类型RRC连接失败次数，其他原因")]
        public int HighPriorityAccessRrcFailOther { get; set; }

        [CsvColumn(Name = "emergency类型RRC连接建立成功次数")]
        public int EmergencyRrcSuccess { get; set; }

        [CsvColumn(Name = "emergency类型RRC连接失败次数，定时器超时")]
        public int EmergencyRrcFailTimer { get; set; }

        [CsvColumn(Name = "emergency类型RRC连接失败次数，eNB接纳失败")]
        public int EmergencyRrcFailAllow { get; set; }

        [CsvColumn(Name = "emergency类型RRC连接失败次数，其他原因")]
        public int EmergencyRrcFailOther { get; set; }

        [CsvColumn(Name = "RRC连接建立总时长(毫秒)")]
        public int RrcTotalDurationInMs { get; set; }

        [CsvColumn(Name = "RRC连接建立最大时间(毫秒)")]
        public int RrcMaxDurationInMs { get; set; }

        [CsvColumn(Name = "RRC连接释放次数，空口定时器超时")]
        public int RrcReleaseTimer { get; set; }

        [CsvColumn(Name = "RRC连接释放次数，ENB等待UE上下文建立定时器超时导致释放")]
        public int RrcReleaseUeContextTimer { get; set; }

        [CsvColumn(Name = "RRC连接释放次数，空口质量差触发RLF")]
        public int RrcReleaseBadRsrp { get; set; }

        [CsvColumn(Name = "RRC连接释放次数，RLC达到最大重传次数")]
        public int RrcReleaseRlcMaxRetransmit { get; set; }

        [CsvColumn(Name = "RRC连接释放次数，PDCP完整性保护失败")]
        public int RrcReleasePdcpIntegrationFail { get; set; }

        [CsvColumn(Name = "RRC连接释放次数，Gtpu ErrInd触发释放")]
        public int RrcReleaseGptu { get; set; }

        [CsvColumn(Name = "RRC连接释放次数，Path故障触发释放")]
        public int RrcReleasePathMalfunction { get; set; }

        [CsvColumn(Name = "RRC连接释放次数，光口故障触发释放")]
        public int RrcReleaseFiber { get; set; }

        [CsvColumn(Name = "RRC连接释放次数，ENB检测到UE被异常拔掉")]
        public int RrcReleaseUeExit { get; set; }

        [CsvColumn(Name = "RRC连接释放次数，跨站重建立失败导致的释放")]
        public int RrcReleaseInterSiteReconstruction { get; set; }

        [CsvColumn(Name = "RRC连接释放次数，ENB重定向导致释放")]
        public int RrcReleaseRedirect { get; set; }

        [CsvColumn(Name = "RRC连接释放次数，ENB无线链路失败导致释放")]
        public int RrcReleaseRadioLink { get; set; }

        [CsvColumn(Name = "RRC连接释放次数，重建立失败引发的释放")]
        public int RrcReleaseReconstructionFail { get; set; }

        [CsvColumn(Name = "RRC连接释放次数，S1链路故障引发释放")]
        public int RrcReleaseS1 { get; set; }

        [CsvColumn(Name = "RRC连接释放次数，MME其它原因导致释放")]
        public int RrcReleaseMmeOther { get; set; }

        [CsvColumn(Name = "RRC连接释放次数，切换失败原因导致释放")]
        public int RrcReleaseSwitchFail { get; set; }

        [CsvColumn(Name = "mt-Access类型RRC连接建立请求次数")]
        public int MtAccessRrcRequest { get; set; }

        [CsvColumn(Name = "mo-Signalling类型RRC连接建立请求次数")]
        public int MoSignallingRrcRequest { get; set; }

        [CsvColumn(Name = "mo-Data类型RRC连接建立请求次数")]
        public int MoDataRrcRequest { get; set; }

        [CsvColumn(Name = "highPriorityAccess类型RRC连接建立请求次数")]
        public int HighPriorityAccessRrcRequest { get; set; }

        [CsvColumn(Name = "emergency类型RRC连接建立请求次数")]
        public int EmergencyRrcRequest { get; set; }

        [CsvColumn(Name = "CQI0下行调度次数")]
        public int Cqi0Times { get; set; }

        [CsvColumn(Name = "CQI1下行调度次数")]
        public int Cqi1Times { get; set; }

        [CsvColumn(Name = "CQI2下行调度次数")]
        public int Cqi2Times { get; set; }

        [CsvColumn(Name = "CQI3下行调度次数")]
        public int Cqi3Times { get; set; }

        [CsvColumn(Name = "CQI4下行调度次数")]
        public int Cqi4Times { get; set; }

        [CsvColumn(Name = "CQI5下行调度次数")]
        public int Cqi5Times { get; set; }

        [CsvColumn(Name = "CQI6下行调度次数")]
        public int Cqi6Times { get; set; }

        [CsvColumn(Name = "CQI7下行调度次数")]
        public int Cqi7Times { get; set; }

        [CsvColumn(Name = "CQI8下行调度次数")]
        public int Cqi8Times { get; set; }

        [CsvColumn(Name = "CQI9下行调度次数")]
        public int Cqi9Times { get; set; }

        [CsvColumn(Name = "CQI10下行调度次数")]
        public int Cqi10Times { get; set; }

        [CsvColumn(Name = "CQI11下行调度次数")]
        public int Cqi11Times { get; set; }

        [CsvColumn(Name = "CQI12下行调度次数")]
        public int Cqi12Times { get; set; }

        [CsvColumn(Name = "CQI13下行调度次数")]
        public int Cqi13Times { get; set; }

        [CsvColumn(Name = "CQI14下行调度次数")]
        public int Cqi14Times { get; set; }

        [CsvColumn(Name = "CQI15下行调度次数")]
        public int Cqi15Times { get; set; }

        [CsvColumn(Name = "小区CQI0使用数量")]
        public int Cqi0Reports { get; set; }

        [CsvColumn(Name = "小区CQI1使用数量")]
        public int Cqi1Reports { get; set; }

        [CsvColumn(Name = "小区CQI2使用数量")]
        public int Cqi2Reports { get; set; }

        [CsvColumn(Name = "小区CQI3使用数量")]
        public int Cqi3Reports { get; set; }

        [CsvColumn(Name = "小区CQI4使用数量")]
        public int Cqi4Reports { get; set; }

        [CsvColumn(Name = "小区CQI5使用数量")]
        public int Cqi5Reports { get; set; }

        [CsvColumn(Name = "小区CQI6使用数量")]
        public int Cqi6Reports { get; set; }

        [CsvColumn(Name = "小区CQI7使用数量")]
        public int Cqi7Reports { get; set; }

        [CsvColumn(Name = "小区CQI8使用数量")]
        public int Cqi8Reports { get; set; }

        [CsvColumn(Name = "小区CQI9使用数量")]
        public int Cqi9Reports { get; set; }

        [CsvColumn(Name = "小区CQI10使用数量")]
        public int Cqi10Reports { get; set; }

        [CsvColumn(Name = "小区CQI11使用数量")]
        public int Cqi11Reports { get; set; }

        [CsvColumn(Name = "小区CQI12使用数量")]
        public int Cqi12Reports { get; set; }

        [CsvColumn(Name = "小区CQI13使用数量")]
        public int Cqi13Reports { get; set; }

        [CsvColumn(Name = "小区CQI14使用数量")]
        public int Cqi14Reports { get; set; }

        [CsvColumn(Name = "小区CQI15使用数量")]
        public int Cqi15Reports { get; set; }

        [CsvColumn(Name = "小区载频PUSCH实际使用PRB个数")]
        public long PuschPrbNumber { get; set; }

        public double PuschPrbs => (double)PuschPrbNumber / 3600000;

        [CsvColumn(Name = "小区载频PUSCH可用的PRB个数")]
        public long PuschPrbCapacity { get; set; }

        public int UplinkPrbSubframe => (int) (PuschPrbCapacity / 3600000);

        [CsvColumn(Name = "小区载频PDSCH实际使用PRB个数")]
        public long PdschPrbNumber { get; set; }

        public double PdschPrbs => (double)PdschPrbNumber / 3600000;

        [CsvColumn(Name = "小区载频PDSCH可用的PRB个数")]
        public long PdschPrbCapacity { get; set; }

        public int DownlinkPrbSubframe => (int)(PdschPrbCapacity / 3600000);

        [CsvColumn(Name = "上行DTCH占用RB数")]
        public double UplinkDtchPrbNumber { get; set; }

        public double UplinkDtchPrbs => UplinkDtchPrbNumber / 3600000;

        [CsvColumn(Name = "下行DTCH占用RB数")]
        public double DownlinkDtchPrbNumber { get; set; }

        public double DownlinkDtchPrbs => DownlinkDtchPrbNumber / 3600000;

        [CsvColumn(Name = "小区PUSCH PRB利用率在范围[0,20%]的次数")]
        public long PuschUsageInterval0Number { get; set; }

        public double PuschUsageInterval0Seconds => (double) PuschUsageInterval0Number / 1000;

        [CsvColumn(Name = "小区PUSCH PRB利用率在范围[21%,40%]的次数")]
        public long PuschUsageInterval20Number { get; set; }

        public double PuschUsageInterval20Seconds => (double)PuschUsageInterval20Number / 1000;

        [CsvColumn(Name = "小区PUSCH PRB利用率在范围[41%,60%]的次数")]
        public long PuschUsageInterval40Number { get; set; }

        public double PuschUsageInterval40Seconds => (double)PuschUsageInterval40Number / 1000;

        [CsvColumn(Name = "小区PUSCH PRB利用率在范围[61%,80%]的次数")]
        public long PuschUsageInterval60Number { get; set; }

        public double PuschUsageInterval60Seconds => (double)PuschUsageInterval60Number / 1000;

        [CsvColumn(Name = "小区PUSCH PRB利用率在范围[81%,100%]的次数")]
        public long PuschUsageInterval80Number { get; set; }

        public double PuschUsageInterval80Seconds => (double)PuschUsageInterval80Number / 1000;

        [CsvColumn(Name = "小区PDSCH PRB利用率在范围[0,20%]的次数")]
        public long PdschUsageInterval0Number { get; set; }

        public double PdschUsageInterval0Seconds => (double)PdschUsageInterval0Number / 1000;

        [CsvColumn(Name = "小区PDSCH PRB利用率在范围[21%,40%]的次数")]
        public long PdschUsageInterval20Number { get; set; }

        public double PdschUsageInterval20Seconds => (double)PdschUsageInterval20Number / 1000;

        [CsvColumn(Name = "小区PDSCH PRB利用率在范围[41%,60%]的次数")]
        public long PdschUsageInterval40Number { get; set; }

        public double PdschUsageInterval40Seconds => (double)PdschUsageInterval40Number / 1000;

        [CsvColumn(Name = "小区PDSCH PRB利用率在范围[61%,80%]的次数")]
        public long PdschUsageInterval60Number { get; set; }

        public double PdschUsageInterval60Seconds => (double)PdschUsageInterval60Number / 1000;

        [CsvColumn(Name = "小区PDSCH PRB利用率在范围[81%,100%]的次数")]
        public long PdschUsageInterval80Number { get; set; }

        public double PdschUsageInterval80Seconds => (double)PdschUsageInterval80Number / 1000;

        public static List<FlowZteCsv> ReadFlowZteCsvs(StreamReader reader)
        {
            return
                CsvContext.Read<FlowZteCsv>(reader, CsvFileDescription.CommaDescription)
                    .ToList()
                    .Where(x => !string.IsNullOrEmpty(x.Qci8DownlinkIpThroughputDuration)).ToList();
        }

        [CsvColumn(Name = "小区最大RSSI(分贝毫瓦)")]
        public double MaxRssi { get; set; }

        [CsvColumn(Name = "小区最小RSSI(分贝毫瓦)")]
        public double MinRssi { get; set; }

        [CsvColumn(Name = "小区平均RSSI(分贝毫瓦)")]
        public double AverageRssi { get; set; }

        [CsvColumn(Name = "Pusch信道RSSI小于等于-120dBm的上报次数")]
        public int PuschRssiBelow120 { get; set; }

        [CsvColumn(Name = "Pusch信道RSSI在范围(-120dBm,-116dBm]的上报次数")]
        public int PuschRssi120To116 { get; set; }

        [CsvColumn(Name = "Pusch信道RSSI在范围(-116dBm,-112dBm]的上报次数")]
        public int PuschRssi116To112 { get; set; }

        [CsvColumn(Name = "Pusch信道RSSI在范围(-112dBm,-108dBm]的上报次数")]
        public int PuschRssi112To108 { get; set; }

        [CsvColumn(Name = "Pusch信道RSSI在范围(-108dBm,-104dBm]的上报次数")]
        public int PuschRssi108To104 { get; set; }

        [CsvColumn(Name = "Pusch信道RSSI在范围(-104dBm,-100dBm]的上报次数")]
        public int PuschRssi104To100 { get; set; }

        [CsvColumn(Name = "Pusch信道RSSI在范围(-100dBm,-96dBm]的上报次数")]
        public int PuschRssi100To96 { get; set; }

        [CsvColumn(Name = "Pusch信道RSSI在范围(-96dBm,-92dBm]的上报次数")]
        public int PuschRssi96To92 { get; set; }

        [CsvColumn(Name = "Pusch信道RSSI大于-92dBm的上报次数")]
        public int PuschRssiAbove92 { get; set; }

        [CsvColumn(Name = "Pucch信道RSSI小于等于-120dBm的上报次数")]
        public int PucchRssiBelow120 { get; set; }

        [CsvColumn(Name = "Pucch信道RSSI在范围(-120dBm,-116dBm]的上报次数")]
        public int PucchRssi120To116 { get; set; }

        [CsvColumn(Name = "Pucch信道RSSI在范围(-116dBm,-112dBm]的上报次数")]
        public int PucchRssi116To112 { get; set; }

        [CsvColumn(Name = "Pucch信道RSSI在范围(-112dBm,-108dBm]的上报次数")]
        public int PucchRssi112To108 { get; set; }

        [CsvColumn(Name = "Pucch信道RSSI在范围(-108dBm,-104dBm]的上报次数")]
        public int PucchRssi108To104 { get; set; }

        [CsvColumn(Name = "Pucch信道RSSI在范围(-104dBm,-100dBm]的上报次数")]
        public int PucchRssi104To100 { get; set; }

        [CsvColumn(Name = "Pucch信道RSSI在范围(-100dBm,-96dBm]的上报次数")]
        public int PucchRssi100To96 { get; set; }

        [CsvColumn(Name = "Pucch信道RSSI在范围(-96dBm,-92dBm]的上报次数")]
        public int PucchRssi96To92 { get; set; }

        [CsvColumn(Name = "Pucch信道RSSI大于-92dBm的上报次数")]
        public int PucchRssiAbove92 { get; set; }
    }

}
