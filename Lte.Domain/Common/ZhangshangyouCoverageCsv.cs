using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.LinqToCsv;

namespace Lte.Domain.Common
{
    public class ZhangshangyouCoverageCsv : IStatTime, IGeoPoint<double>, IENodebName
    {
        [CsvColumn(Name = "时间")]
        public DateTime StatTime { get; set; }

        [CsvColumn(Name = "楼宇名称")]
        public string BuildingName { get; set; }

        [CsvColumn(Name = "用户")]
        public string UserName { get; set; }

        [CsvColumn(Name = "任务ID")]
        public string SerialNumber { get; set; }

        [CsvColumn(Name = "IMEI")]
        public string Imei { get; set; }

        [CsvColumn(Name = "IMSI")]
        public string Imsi { get; set; }

        [CsvColumn(Name = "终端型号")]
        public string Terminal { get; set; }

        [CsvColumn(Name = "数据回传网络")]
        public string BackhaulNetworkDescription { get; set; }

        [CsvColumn(Name = "百度经度")]
        public double Longtitute { get; set; }

        [CsvColumn(Name = "百度纬度")]
        public double Lattitute { get; set; }

        [CsvColumn(Name = "行政区")]
        public string District { get; set; }

        [CsvColumn(Name = "街道")]
        public string Road { get; set; }

        [CsvColumn(Name = "门牌号")]
        public string Address { get; set; }

        [CsvColumn(Name = "LTE地市")]
        public string LteCity { get; set; }

        [CsvColumn(Name = "LTE基站编号")]
        public string ENodebIdString { get; set; }

        [CsvColumn(Name = "LTE扇区号")]
        public string SectorIdString { get; set; }

        [CsvColumn(Name = "LTE基站名称")]
        public string ENodebName { get; set; }

        [CsvColumn(Name = "CI")]
        public string Ci { get; set; }

        [CsvColumn(Name = "PCI")]
        public string PciString { get; set; }

        [CsvColumn(Name = "TAC")]
        public string TacString { get; set; }

        [CsvColumn(Name = "RSRP")]
        public double Rsrp { get; set; }

        [CsvColumn(Name = "RSRQ")]
        public double Rsrq { get; set; }

        [CsvColumn(Name = "RSSI")]
        public double Rssi { get; set; }

        [CsvColumn(Name = "RSSNR")]
        public double Sinr { get; set; }

        [CsvColumn(Name = "CDMA地市")]
        public string CdmaCity { get; set; }

        [CsvColumn(Name = "BSC")]
        public string CdmaBscString { get; set; }

        [CsvColumn(Name = "CDMA基站编号")]
        public string BtsIdString { get; set; }

        [CsvColumn(Name = "CDMA基站名称")]
        public string BtsName { get; set; }

        [CsvColumn(Name = "CDMA扇区号")]
        public string CdmaSectorIdString { get; set; }

        [CsvColumn(Name = "CID")]
        public string CidString { get; set; }

        [CsvColumn(Name = "3GRX")]
        public double Rx3G { get; set; }

        [CsvColumn(Name = "3GEc/Io")]
        public double EcIo3G { get; set; }

        [CsvColumn(Name = "3GSNR")]
        public double Sinr3G { get; set; }

        [CsvColumn(Name = "2GRX")]
        public double Rx2G { get; set; }

        [CsvColumn(Name = "2GEc/Io")]
        public double EcIo { get; set; }

        [CsvColumn(Name = "GSMCELLID")]
        public string GsmCellId { get; set; }

        [CsvColumn(Name = "GSMLAC")]
        public string GsmLac { get; set; }

        [CsvColumn(Name = "GSMRX")]
        public double GsmRx { get; set; }

    }
}
