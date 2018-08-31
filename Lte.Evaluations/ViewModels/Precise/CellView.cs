using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Infrastructure;
using AutoMapper;
using Lte.Domain.Common.Transform;
using Lte.Domain.Common.Types;
using Lte.Domain.Regular.Attributes;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Entities;
using Lte.MySqlFramework.Entities.Infrastructure;

namespace Lte.Evaluations.ViewModels.Precise
{
    [AutoMapFrom(typeof(Cell))]
    [AutoMapTo(typeof(SectorView))]
    [TypeDoc("LTE小区视图，显示小区详细信息")]
    public class CellView
    {
        [MemberDoc("基站名称")]
        public string ENodebName { get; private set; }

        [MemberDoc("基站编号")]
        public int ENodebId { get; set; }

        [MemberDoc("扇区编号，目前2.1G取0-15，1.8G取48-63，800M取16-31")]
        public byte SectorId { get; set; }

        [MemberDoc("小区名称")]
        public string CellName => ENodebName + "-" + SectorId;

        [MemberDoc("频点号，目前2.1G主要用100频点，1.8G主要用1825频点，800M主要用2446频点")]
        public int Frequency { get; set; }

        [MemberDoc("频带编号，如1表示2.1G，3表示1.8G，5表示800M")]
        public byte BandClass { get; set; }

        [MemberDoc("物理小区编号，范围0-503")]
        public short Pci { get; set; }

        [MemberDoc("物理随机接入信道序列索引，范围0-839")]
        public short Prach { get; set; }

        [MemberDoc("参考信号(Reference Signal)功率，单位是dBm")]
        public double RsPower { get; set; }

        [MemberDoc("跟踪区编号")]
        public int Tac { get; set; }

        [MemberDoc("天线挂高，单位米")]
        public double Height { get; set; }

        [MemberDoc("方位角")]
        public double Azimuth { get; set; }

        [AutoMapPropertyResolve("IsOutdoor", typeof(Cell), typeof(OutdoorDescriptionTransform))]
        [MemberDoc("室内外小区")]
        public string Indoor { get; set; }

        [MemberDoc("机械下倾")]
        public double MTilt { get; set; }

        [MemberDoc("电子下倾")]
        public double ETilt { get; set; }

        [MemberDoc("总下倾角")]
        public double DownTilt => MTilt + ETilt;

        [MemberDoc("天线增益，单位是dB")]
        public double AntennaGain { get; set; }

        [MemberDoc("小区经度")]
        public double Longtitute { get; set; }

        [MemberDoc("小区纬度")]
        public double Lattitute { get; set; }

        [MemberDoc("其他信息")]
        public string OtherInfos
            => "PCI: " + Pci + "; PRACH: " + Prach + "; RS Power(dBm): " + RsPower + "; TAC: " +
               Tac + "; ENodebId: " + ENodebId;

        [MemberDoc("是否在用")]
        public bool IsInUse { get; set; }

        public static CellView ConstructView(Cell cell, IENodebRepository repository)
        {
            var view = Mapper.Map<Cell, CellView>(cell);
            var eNodeb = repository.FirstOrDefault(x => x.ENodebId == cell.ENodebId);
            view.ENodebName = eNodeb?.Name;
            return view;
        }
    }
}