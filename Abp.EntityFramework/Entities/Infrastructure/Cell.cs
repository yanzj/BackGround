using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Transform;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Antenna;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Excel;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Infrastructure
{
    [TypeDoc("定义LTE小区数据库中对应的ORM对象")]
    [AutoMapFrom(typeof(CellExcel), typeof(ConstructionExcel))]
    public class Cell : Entity, ILteCellParameters<short, int>, ILteCellQuery, IGeoPoint<double>, ICellAntenna<double>, IIsInUse
    {
        [MemberDoc("基站编号")]
        public int ENodebId { get; set; }

        [MemberDoc("扇区编号，目前2.1G取0-15，1.8G取48-63，800M取16-31")]
        public byte SectorId { get; set; }

        [MemberDoc("本地小区编号")]
        [AutoMapPropertyResolve("LocalCellId", typeof(ConstructionExcel))]
        public byte LocalSectorId { get; set; }

        [MemberDoc("频点号，目前2.1G主要用100频点，1.8G主要用1825频点，800M主要用2446频点")]
        public int Frequency { get; set; }

        [MemberDoc("频带编号，如1表示2.1G，3表示1.8G，5表示800M")]
        public byte BandClass { get; set; }

        [MemberDoc("物理小区编号，范围0-503")]
        public short Pci { get; set; }

        [MemberDoc("物理随机接入信道序列索引，范围0-839")]
        public short Prach { get; set; }

        [MemberDoc("参考信号(Reference Signal)功率，单位是dBm")]
        public double RsPower { get; set; } = 17.5;

        [AutoMapPropertyResolve("IsIndoor", typeof(CellExcel), typeof(IndoorDescriptionToOutdoorBoolTransform))]
        [MemberDoc("是否为室外小区")]
        public bool IsOutdoor { get; set; }

        [MemberDoc("跟踪区编号")]
        public int Tac { get; set; }

        [MemberDoc("小区经度")]
        public double Longtitute { get; set; }

        [MemberDoc("小区纬度")]
        public double Lattitute { get; set; }

        [MemberDoc("天线挂高，单位米")]
        public double Height { get; set; }

        [MemberDoc("方位角")]
        public double Azimuth { get; set; }

        [MemberDoc("机械下倾")]
        public double MTilt { get; set; }

        [MemberDoc("电子下倾")]
        public double ETilt { get; set; }

        [MemberDoc("天线增益，单位是dB")]
        public double AntennaGain { get; set; }

        [AutoMapPropertyResolve("TransmitReceive", typeof(CellExcel), typeof(AntennaPortsConfigureTransform))]
        [MemberDoc("天线收发配置")]
        public AntennaPortsConfigure AntennaPorts { get; set; }

        [MemberDoc("是否在用")]
        public bool IsInUse { get; set; } = true;
    }
}