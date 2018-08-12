using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Cdma
{
    [TypeDoc("扇区视图，用于地理化显示")]
    [AutoMapFrom(typeof(CdmaCellView))]
    public class CdmaSectorView
    {
        [MemberDoc("小区名称，用于辨析小区")]
        public string CellName { get; set; }

        [MemberDoc("基站编号")]
        public int BtsId { get; set; }

        [MemberDoc("扇区编号")]
        public byte SectorId { get; set; }

        [MemberDoc("是否为室内小区")]
        public string Indoor { get; set; }

        [MemberDoc("方位角")]
        public double Azimuth { get; set; }

        [MemberDoc("经度")]
        public double Longtitute { get; set; }

        [MemberDoc("纬度")]
        public double Lattitute { get; set; }

        [MemberDoc("天线挂高")]
        public double Height { get; set; }

        [MemberDoc("下倾角")]
        public double DownTilt { get; set; }

        [MemberDoc("天线增益")]
        public double AntennaGain { get; set; }

        [MemberDoc("频点")]
        public int Frequency { get; set; }

        public short Pn { get; set; }

        public string CellType { get; set; }

        [MemberDoc("其他信息")]
        public string OtherInfos { get; set; }
    }
}