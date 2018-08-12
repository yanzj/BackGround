using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Infrastructure;
using Lte.Domain.Regular.Attributes;

namespace Lte.Evaluations.ViewModels.Precise
{
    [AutoMapFrom(typeof(Cell))]
    [TypeDoc("4G精确覆盖率视图，用于扇区信息显示")]
    public class Precise4GSector
    {
        [MemberDoc("小区编号")]
        public int CellId { get; set; }

        [MemberDoc("扇区编号")]
        public byte SectorId { get; set; }

        [MemberDoc("总测量报告数")]
        public int TotalMrs { get; set; }

        [MemberDoc("第二邻区数")]
        public int SecondNeighbors { get; set; }

        [MemberDoc("第一邻区精确覆盖率")]
        public double FirstRate { get; set; }

        [MemberDoc("第二邻区精确覆盖率")]
        public double SecondRate { get; set; }

        [MemberDoc("第三邻区精确覆盖率")]
        public double ThirdRate { get; set; }

        [MemberDoc("基站名称")]
        public string ENodebName { get; set; } = "未导入基站";

        [MemberDoc("TOP天数")]
        public int TopDates { get; set; }

        [MemberDoc("天线挂高")]
        public double Height { get; set; }

        [MemberDoc("方位角")]
        public double Azimuth { get; set; }

        [MemberDoc("总下倾角")]
        public double DownTilt { get; set; }

        [MemberDoc("小区经度")]
        public double Longtitute { get; set; }

        [MemberDoc("小区纬度")]
        public double Lattitute { get; set; }

        [MemberDoc("物理小区编号，范围0-503")]
        public short Pci { get; set; }

        [MemberDoc("参考信号(Reference Signal)功率，单位是dBm")]
        public double RsPower { get; set; }
    }
}