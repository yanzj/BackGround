using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Region;
using Lte.Domain.Common.Wireless.Station;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Test
{
    [TypeDoc("掌上优测试详单")]
    [AutoMapFrom(typeof(ZhangshangyouQualityCsv))]
    public class ZhangshangyouQuality : Entity, IStatTime, ILteCellQuery, IENodebName
    {
        [MemberDoc("任务编号")]
        public string SerialNumber { get; set; }

        [MemberDoc("时间")]
        public DateTime StatTime { get; set; }

        [MemberDoc("回传网络")]
        [AutoMapPropertyResolve("BackhaulNetworkDescription", typeof(ZhangshangyouQualityCsv), typeof(NetworkTypeTransform))]
        public NetworkType BackhaulNetwork { get; set; }

        [MemberDoc("运营商")]
        [AutoMapPropertyResolve("OperatorDescription", typeof(ZhangshangyouQualityCsv), typeof(OperatorTransform))]
        public Operator Operator { get; set; }

        [MemberDoc("任务类型")]
        [AutoMapPropertyResolve("TestTypeDescription", typeof(ZhangshangyouQualityCsv), typeof(TestTypeTransform))]
        public TestType TestType { get; set; }

        [MemberDoc("楼宇名称")]
        public string BuildingName { get; set; }

        [MemberDoc("楼层")]
        public string Floor { get; set; }

        [MemberDoc("用户")]
        public string UserName { get; set; }

        [MemberDoc("终端")]
        public string Terminal { get; set; }

        [MemberDoc("IMEI")]
        public string Imei { get; set; }

        [MemberDoc("IMSI")]
        public string Imsi { get; set; }

        [AutoMapPropertyResolve("FirstPacketDelayString", typeof(ZhangshangyouQualityCsv), typeof(StringToDoubleTransform))]
        [MemberDoc("网页首包延时(ms)")]
        public double FirstPacketDelay { get; set; }

        [AutoMapPropertyResolve("PageOpenDelayString", typeof(ZhangshangyouQualityCsv), typeof(StringToDoubleTransform))]
        [MemberDoc("网页打开延时(s)")]
        public double PageOpenDelay { get; set; }

        [AutoMapPropertyResolve("PageDnsDelayString", typeof(ZhangshangyouQualityCsv), typeof(StringToIntTransform))]
        [MemberDoc("网页Dns解析时延(ms)")]
        public int PageDnsDelay { get; set; }

        [AutoMapPropertyResolve("PageConnectionSetupDelayString", typeof(ZhangshangyouQualityCsv), typeof(StringToIntTransform))]
        [MemberDoc("网页建立连接时延(ms)")]
        public int PageConnectionSetupDelay { get; set; }

        [AutoMapPropertyResolve("PageRequestDelayString", typeof(ZhangshangyouQualityCsv), typeof(StringToIntTransform))]
        [MemberDoc("网页发送请求时延(ms)")]
        public int PageRequestDelay { get; set; }

        [AutoMapPropertyResolve("PageResponseDelayString", typeof(ZhangshangyouQualityCsv), typeof(StringToIntTransform))]
        [MemberDoc("网页接受响应时延(ms)")]
        public int PageResponseDelayString { get; set; }

        [AutoMapPropertyResolve("PageRateString", typeof(ZhangshangyouQualityCsv), typeof(StringToDoubleTransform))]
        [MemberDoc("网页速率(Mbps)")]
        public double PageRate { get; set; }

        [AutoMapPropertyResolve("PageTestTimesString", typeof(ZhangshangyouQualityCsv), typeof(StringToIntTransform))]
        [MemberDoc("网页总次数")]
        public int PageTestTimes { get; set; }

        [AutoMapPropertyResolve("DownloadPeakRateString", typeof(ZhangshangyouQualityCsv), typeof(StringToDoubleTransform))]
        [MemberDoc("测速下载峰值速率(Kbps)")]
        public double DownloadPeakRate { get; set; }

        [AutoMapPropertyResolve("DownloadAverageRateString", typeof(ZhangshangyouQualityCsv), typeof(StringToDoubleTransform))]
        [MemberDoc("测速下载平均速率(Kbps)")]
        public double DownloadAverageRate { get; set; }

        [AutoMapPropertyResolve("UploadPeakRateString", typeof(ZhangshangyouQualityCsv), typeof(StringToDoubleTransform))]
        [MemberDoc("测速上传峰值速率(Kbps)")]
        public double UploadPeakRate { get; set; }

        [AutoMapPropertyResolve("DownloadAverageRateString", typeof(ZhangshangyouQualityCsv), typeof(StringToDoubleTransform))]
        [MemberDoc("测速上传平均速率(Kbps)")]
        public double UploadAverageRate { get; set; }

        [AutoMapPropertyResolve("PingDelayString", typeof(ZhangshangyouQualityCsv), typeof(StringToIntTransform))]
        [MemberDoc("Ping时延(ms)")]
        public int PingDelay { get; set; }

        [AutoMapPropertyResolve("PingTimesString", typeof(ZhangshangyouQualityCsv), typeof(StringToIntTransform))]
        [MemberDoc("Ping总次数")]
        public int PingTimes { get; set; }

        [AutoMapPropertyResolve("PingLossPacketsString", typeof(ZhangshangyouQualityCsv), typeof(StringToIntTransform))]
        [MemberDoc("Ping丢包次数")]
        public int PingLossPackets { get; set; }

        [AutoMapPropertyResolve("StreamConnectionDelayString", typeof(ZhangshangyouQualityCsv), typeof(StringToIntTransform))]
        [MemberDoc("视频连接时延(ms)")]
        public int StreamConnectionDelay { get; set; }

        [AutoMapPropertyResolve("StreamPlayDelayString", typeof(ZhangshangyouQualityCsv), typeof(StringToIntTransform))]
        [MemberDoc("视频播放时延(ms)")]
        public int StreamPlayDelay { get; set; }

        [AutoMapPropertyResolve("StreamAverageRateString", typeof(ZhangshangyouQualityCsv), typeof(StringToDoubleTransform))]
        [MemberDoc("视频平均速率(Kbps)")]
        public double StreamAverageRate { get; set; }

        [AutoMapPropertyResolve("StreamPeakRateString", typeof(ZhangshangyouQualityCsv), typeof(StringToDoubleTransform))]
        [MemberDoc("视频最大速率(Kbps)")]
        public double StreamPeakRate { get; set; }

        [AutoMapPropertyResolve("StreamSuccessTimesString", typeof(ZhangshangyouQualityCsv), typeof(StringToIntTransform))]
        [MemberDoc("视频成功次数")]
        public int StreamSuccessTimes { get; set; }

        [AutoMapPropertyResolve("StreamTestTimesString", typeof(ZhangshangyouQualityCsv), typeof(StringToIntTransform))]
        [MemberDoc("视频总次数")]
        public int StreamTestTimes { get; set; }

        [MemberDoc("LTE地市")]
        public string LteCity { get; set; }

        [MemberDoc("LTE基站编号")]
        [AutoMapPropertyResolve("ENodebIdString", typeof(ZhangshangyouQualityCsv), typeof(StringToIntTransform))]
        public int ENodebId { get; set; }

        [MemberDoc("LTE基站小区")]
        [AutoMapPropertyResolve("SectorIdString", typeof(ZhangshangyouQualityCsv), typeof(StringToByteTransform))]
        public byte SectorId { get; set; }

        [MemberDoc("LTE基站名")]
        public string ENodebName { get; set; }

        [MemberDoc("CI")]
        public string Ci { get; set; }

        [MemberDoc("PCI")]
        [AutoMapPropertyResolve("PciString", typeof(ZhangshangyouQualityCsv), typeof(StringToIntTransform))]
        public int Pci { get; set; }

        [MemberDoc("CDMA基站地市")]
        public string CdmaCity { get; set; }

        [MemberDoc("CDMA基站BSC")]
        [AutoMapPropertyResolve("CdmaBscString", typeof(ZhangshangyouQualityCsv), typeof(StringToByteTransform))]
        public byte CdmaBsc { get; set; }

        [MemberDoc("CDMA基站编号")]
        [AutoMapPropertyResolve("BtsIdString", typeof(ZhangshangyouQualityCsv), typeof(StringToIntTransform))]
        public int BtsId { get; set; }

        [MemberDoc("CDMA基站名称")]
        public string BtsName { get; set; }

        [MemberDoc("CDMA基站扇区")]
        [AutoMapPropertyResolve("CdmaSectorIdString", typeof(ZhangshangyouQualityCsv), typeof(StringToByteTransform))]
        public byte CdmaSectorId { get; set; }

        [MemberDoc("CID")]
        [AutoMapPropertyResolve("CidString", typeof(ZhangshangyouQualityCsv), typeof(StringToIntTransform))]
        public int Cid { get; set; }

    }
}
