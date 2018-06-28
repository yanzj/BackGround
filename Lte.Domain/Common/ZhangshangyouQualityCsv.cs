using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.LinqToCsv;

namespace Lte.Domain.Common
{
    public class ZhangshangyouQualityCsv : IStatTime, IENodebName
    {
        [CsvColumn(Name = "任务编号")]
        public string SerialNumber { get; set; }

        [CsvColumn(Name = "时间")]
        public DateTime StatTime { get; set; }

        [CsvColumn(Name = "回传网络")]
        public string BackhaulNetworkDescription { get; set; }

        [CsvColumn(Name = "运营商")]
        public string OperatorDescription { get; set; }

        [CsvColumn(Name = "任务类型")]
        public string TestTypeDescription { get; set; }

        [CsvColumn(Name = "楼宇名称")]
        public string BuildingName { get; set; }

        [CsvColumn(Name = "楼层")]
        public string Floor { get; set; }

        [CsvColumn(Name = "用户")]
        public string UserName { get; set; }

        [CsvColumn(Name = "终端")]
        public string Terminal { get; set; }

        [CsvColumn(Name = "IMEI")]
        public string Imei { get; set; }

        [CsvColumn(Name = "IMSI")]
        public string Imsi { get; set; }

        [CsvColumn(Name = "网页首包延时(ms)")]
        public string FirstPacketDelayString { get; set; }

        [CsvColumn(Name = "网页打开延时(s)")]
        public string PageOpenDelayString { get; set; }

        [CsvColumn(Name = "网页Dns解析时延(ms)")]
        public string PageDnsDelayString { get; set; }

        [CsvColumn(Name = "网页建立连接时延(ms)")]
        public string PageConnectionSetupDelayString { get; set; }

        [CsvColumn(Name = "网页发送请求时延(ms)")]
        public string PageRequestDelayString { get; set; }

        [CsvColumn(Name = "网页接受响应时延(ms)")]
        public string PageResponseDelayString { get; set; }

        [CsvColumn(Name = "网页速率(Mbps)")]
        public string PageRateString { get; set; }

        [CsvColumn(Name = "网页总次数")]
        public string PageTestTimesString { get; set; }

        [CsvColumn(Name = "网页成功次数")]
        public string PageSuccessTimesString { get; set; }

        [CsvColumn(Name = "测速下载峰值速率(Kbps)")]
        public string DownloadPeakRateString { get; set; }

        [CsvColumn(Name = "测速下载平均速率(Kbps)")]
        public string DownloadAverageRateString { get; set; }

        [CsvColumn(Name = "测速上传峰值速率(Kbps)")]
        public string UploadPeakRateString { get; set; }

        [CsvColumn(Name = "测速上传平均速率(Kbps)")]
        public string UploadAverageRateString { get; set; }

        [CsvColumn(Name = "Ping时延(ms)")]
        public string PingDelayString { get; set; }

        [CsvColumn(Name = "Ping总次数")]
        public string PingTimesString { get; set; }

        [CsvColumn(Name = "Ping丢包次数")]
        public string PingLossPacketsString { get; set; }

        [CsvColumn(Name = "视频连接时延(ms)")]
        public string StreamConnectionDelayString { get; set; }

        [CsvColumn(Name = "视频播放时延(ms)")]
        public string StreamPlayDelayString { get; set; }

        [CsvColumn(Name = "视频平均速率(Kbps)")]
        public string StreamAverageRateString { get; set; }

        [CsvColumn(Name = "视频最大速率(Kbps)")]
        public string StreamPeakRateString { get; set; }

        [CsvColumn(Name = "视频成功次数")]
        public string StreamSuccessTimesString { get; set; }

        [CsvColumn(Name = "视频总次数")]
        public string StreamTestTimesString { get; set; }

        [CsvColumn(Name = "LTE地市")]
        public string LteCity { get; set; }

        [CsvColumn(Name = "LTE基站编号")]
        public string ENodebIdString { get; set; }

        [CsvColumn(Name = "LTE基站小区")]
        public string SectorIdString { get; set; }

        [CsvColumn(Name = "LTE基站名")]
        public string ENodebName { get; set; }

        [CsvColumn(Name = "CI")]
        public string Ci { get; set; }

        [CsvColumn(Name = "PCI")]
        public string PciString { get; set; }

        [CsvColumn(Name = "CDMA基站地市")]
        public string CdmaCity { get; set; }

        [CsvColumn(Name = "CDMA基站BSC")]
        public string CdmaBscString { get; set; }

        [CsvColumn(Name = "CDMA基站编号")]
        public string BtsIdString { get; set; }

        [CsvColumn(Name = "CDMA基站名称")]
        public string BtsName { get; set; }

        [CsvColumn(Name = "CDMA基站扇区")]
        public string CdmaSectorIdString { get; set; }

        [CsvColumn(Name = "CID")]
        public string CidString { get; set; }

    }
}
