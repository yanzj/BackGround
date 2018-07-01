using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular.Attributes;

namespace Lte.MySqlFramework.Entities
{
    [TypeDoc("掌上优测试详单")]
    [AutoMapFrom(typeof(ZhangshangyouQuality))]
    public class ZhangshangyouQualityView : IStatTime, ILteCellQuery, IENodebName
    {
        [MemberDoc("任务编号")]
        public string SerialNumber { get; set; }

        [MemberDoc("时间")]
        public DateTime StatTime { get; set; }

        [MemberDoc("回传网络")]
        [AutoMapPropertyResolve("BackhaulNetwork", typeof(ZhangshangyouQuality), typeof(NetworkTypeDescriptionTransform))]
        public NetworkType BackhaulNetworkDescription { get; set; }

        [MemberDoc("运营商")]
        [AutoMapPropertyResolve("Operator", typeof(ZhangshangyouQuality), typeof(OperatorDescriptionTransform))]
        public Operator OperatorDescription { get; set; }

        [MemberDoc("任务类型")]
        [AutoMapPropertyResolve("TestType", typeof(ZhangshangyouQuality), typeof(TestTypeDescriptionTransform))]
        public TestType TestTypeDescription { get; set; }

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
        
        [MemberDoc("网页首包延时(ms)")]
        public double FirstPacketDelay { get; set; }
        
        [MemberDoc("网页打开延时(s)")]
        public double PageOpenDelay { get; set; }
        
        [MemberDoc("网页Dns解析时延(ms)")]
        public int PageDnsDelay { get; set; }
        
        [MemberDoc("网页建立连接时延(ms)")]
        public int PageConnectionSetupDelay { get; set; }
        
        [MemberDoc("网页发送请求时延(ms)")]
        public int PageRequestDelay { get; set; }
        
        [MemberDoc("网页接受响应时延(ms)")]
        public int PageResponseDelayString { get; set; }
        
        [MemberDoc("网页速率(Mbps)")]
        public double PageRate { get; set; }
        
        [MemberDoc("网页总次数")]
        public int PageTestTimes { get; set; }
        
        [MemberDoc("测速下载峰值速率(Kbps)")]
        public double DownloadPeakRate { get; set; }
        
        [MemberDoc("测速下载平均速率(Kbps)")]
        public double DownloadAverageRate { get; set; }
        
        [MemberDoc("测速上传峰值速率(Kbps)")]
        public double UploadPeakRate { get; set; }
        
        [MemberDoc("测速上传平均速率(Kbps)")]
        public double UploadAverageRate { get; set; }
        
        [MemberDoc("Ping时延(ms)")]
        public int PingDelay { get; set; }
        
        [MemberDoc("Ping总次数")]
        public int PingTimes { get; set; }
        
        [MemberDoc("Ping丢包次数")]
        public int PingLossPackets { get; set; }

        public double PingLossRate => PingTimes == 0 ? 0 : ((double) PingLossPackets / PingTimes * 100);
        
        [MemberDoc("视频连接时延(ms)")]
        public int StreamConnectionDelay { get; set; }
        
        [MemberDoc("视频播放时延(ms)")]
        public int StreamPlayDelay { get; set; }
        
        [MemberDoc("视频平均速率(Kbps)")]
        public double StreamAverageRate { get; set; }
        
        [MemberDoc("视频最大速率(Kbps)")]
        public double StreamPeakRate { get; set; }
        
        [MemberDoc("视频成功次数")]
        public int StreamSuccessTimes { get; set; }
        
        [MemberDoc("视频总次数")]
        public int StreamTestTimes { get; set; }

        public double StreamSuccessRate => StreamTestTimes == 0 ? 0 : ((double)StreamSuccessTimes / StreamTestTimes * 100);

        [MemberDoc("LTE地市")]
        public string LteCity { get; set; }

        [MemberDoc("LTE基站编号")]
        public int ENodebId { get; set; }

        [MemberDoc("LTE基站小区")]
        public byte SectorId { get; set; }

        [MemberDoc("LTE基站名")]
        public string ENodebName { get; set; }

        [MemberDoc("CI")]
        public string Ci { get; set; }

        [MemberDoc("PCI")]
        public int Pci { get; set; }

        [MemberDoc("CDMA基站地市")]
        public string CdmaCity { get; set; }

        [MemberDoc("CDMA基站BSC")]
        public byte CdmaBsc { get; set; }

        [MemberDoc("CDMA基站编号")]
        public int BtsId { get; set; }

        [MemberDoc("CDMA基站名称")]
        public string BtsName { get; set; }

        [MemberDoc("CDMA基站扇区")]
        public byte CdmaSectorId { get; set; }

        [MemberDoc("CID")]
        public int Cid { get; set; }

    }
}
