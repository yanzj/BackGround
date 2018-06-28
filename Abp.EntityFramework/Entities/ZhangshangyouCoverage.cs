using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities
{
    [TypeDoc("掌上优信号详单")]
    [AutoMapFrom(typeof(ZhangshangyouCoverageCsv))]
    public class ZhangshangyouCoverage : Entity, IStatTime, IGeoPoint<double>, ILteCellQuery, IENodebName
    {
        [MemberDoc("时间")]
        public DateTime StatTime { get; set; }

        [MemberDoc("楼宇名称")]
        public string BuildingName { get; set; }

        [MemberDoc("用户")]
        public string UserName { get; set; }

        [MemberDoc("任务ID")]
        public string SerialNumber { get; set; }

        [MemberDoc("IMEI")]
        public string Imei { get; set; }

        [MemberDoc("IMSI")]
        public string Imsi { get; set; }

        [MemberDoc("终端型号")]
        public string Terminal { get; set; }

        [MemberDoc("数据回传网络")]
        [AutoMapPropertyResolve("BackhaulNetworkDescription", typeof(ZhangshangyouQualityCsv), typeof(NetworkTypeTransform))]
        public NetworkType BackhaulNetwork { get; set; }

        [MemberDoc("百度经度")]
        public double Longtitute { get; set; }

        [MemberDoc("百度纬度")]
        public double Lattitute { get; set; }

        [MemberDoc("行政区")]
        public string District { get; set; }

        [MemberDoc("街道")]
        public string Road { get; set; }

        [MemberDoc("门牌号")]
        public string Address { get; set; }

        [MemberDoc("LTE地市")]
        public string LteCity { get; set; }

        [MemberDoc("LTE基站编号")]
        [AutoMapPropertyResolve("ENodebIdString", typeof(ZhangshangyouQualityCsv), typeof(StringToIntTransform))]
        public int ENodebId { get; set; }

        [MemberDoc("LTE扇区号")]
        [AutoMapPropertyResolve("SectorIdString", typeof(ZhangshangyouQualityCsv), typeof(StringToByteTransform))]
        public byte SectorId { get; set; }

        [MemberDoc("LTE基站名称")]
        public string ENodebName { get; set; }

        [MemberDoc("CI")]
        public string Ci { get; set; }

        [MemberDoc("PCI")]
        [AutoMapPropertyResolve("PciString", typeof(ZhangshangyouQualityCsv), typeof(StringToIntTransform))]
        public int Pci { get; set; }

        [MemberDoc("TAC")]
        [AutoMapPropertyResolve("TacString", typeof(ZhangshangyouQualityCsv), typeof(StringToIntTransform))]
        public int Tac { get; set; }

        [MemberDoc("RSRP")]
        public double Rsrp { get; set; }

        [MemberDoc("RSRQ")]
        public double Rsrq { get; set; }

        [MemberDoc("RSSI")]
        public double Rssi { get; set; }

        [MemberDoc("RSSNR")]
        public double Sinr { get; set; }

        [MemberDoc("CDMA地市")]
        public string CdmaCity { get; set; }

        [MemberDoc("BSC")]
        [AutoMapPropertyResolve("CdmaBscString", typeof(ZhangshangyouQualityCsv), typeof(StringToByteTransform))]
        public byte CdmaBsc { get; set; }

        [MemberDoc("CDMA基站编号")]
        [AutoMapPropertyResolve("BtsIdString", typeof(ZhangshangyouQualityCsv), typeof(StringToIntTransform))]
        public int BtsId { get; set; }

        [MemberDoc("CDMA基站名称")]
        public string BtsName { get; set; }

        [MemberDoc("CDMA扇区号")]
        [AutoMapPropertyResolve("CdmaSectorIdString", typeof(ZhangshangyouQualityCsv), typeof(StringToByteTransform))]
        public byte CdmaSectorId { get; set; }

        [MemberDoc("CID")]
        [AutoMapPropertyResolve("CidString", typeof(ZhangshangyouQualityCsv), typeof(StringToIntTransform))]
        public int Cid { get; set; }

        [MemberDoc("3GRX")]
        public double Rx3G { get; set; }

        [MemberDoc("3GEc/Io")]
        public double EcIo3G { get; set; }

        [MemberDoc("3GSNR")]
        public double Sinr3G { get; set; }

        [MemberDoc("2GRX")]
        public double Rx2G { get; set; }

        [MemberDoc("2GEc/Io")]
        public double EcIo { get; set; }

        [MemberDoc("GSMCELLID")]
        public string GsmCellId { get; set; }

        [MemberDoc("GSMLAC")]
        public string GsmLac { get; set; }

        [MemberDoc("GSMRX")]
        public double GsmRx { get; set; }

    }
}
