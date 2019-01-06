using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Entities.RegionKpi;
using AutoMapper;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular.Attributes;
using Lte.MySqlFramework.Abstract.Infrastructure;

namespace Lte.Evaluations.ViewModels.Precise
{
    [AutoMapFrom(typeof(PreciseCoverage4G))]
    [AutoMapTo(typeof(Precise4GSector), typeof(TownPreciseStat))]
    [TypeDoc("4G精确覆盖率视图")]
    public class Precise4GView : IENodebName
    {
        [MemberDoc("小区编号")]
        public int CellId { get; set; }

        [MemberDoc("扇区编号")]
        public byte SectorId { get; set; }

        [MemberDoc("总测量报告数")]
        public int TotalMrs { get; set; }

        [MemberDoc("第一邻区数")]
        public int FirstNeighbors { get; set; }

        [MemberDoc("第二邻区数")]
        public int SecondNeighbors { get; set; }

        [MemberDoc("第三邻区数")]
        public int ThirdNeighbors { get; set; }

        [MemberDoc("第一邻区重叠覆盖率")]
        public double FirstRate { get; set; }

        [MemberDoc("第一邻区精确覆盖率")]
        public double FirstPreciseRate { get; set; }

        [MemberDoc("第二邻区重叠覆盖率")]
        public double SecondRate { get; set; }

        [MemberDoc("第二邻区精确覆盖率")]
        public double SecondPreciseRate { get; set; }

        [MemberDoc("第三邻区重叠覆盖率")]
        public double ThirdRate { get; set; }

        [MemberDoc("第三邻区精确覆盖率")]
        public double ThirdPreciseRate { get; set; }

        [MemberDoc("基站名称")]
        public string ENodebName { get; set; } = "未导入基站";

        [MemberDoc("TOP天数")]
        public int TopDates { get; set; }

        public static Precise4GView ConstructView(PreciseCoverage4G stat, IENodebRepository repository)
        {
            var view = Mapper.Map<PreciseCoverage4G, Precise4GView>(stat);
            var eNodeb = repository.FirstOrDefault(x => x.ENodebId == stat.CellId);
            view.ENodebName = eNodeb?.Name;
            return view;
        }
    }
}
