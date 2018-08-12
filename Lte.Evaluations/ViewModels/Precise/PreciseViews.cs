using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Kpi;
using AutoMapper;
using Lte.Domain.Regular.Attributes;
using Lte.MySqlFramework.Abstract;

namespace Lte.Evaluations.ViewModels.Precise
{
    [AutoMapFrom(typeof(PreciseCoverage4G))]
    [AutoMapTo(typeof(Precise4GSector))]
    [TypeDoc("4G精确覆盖率视图")]
    public class Precise4GView
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

    public static class PreciseViewsQuery
    {
        public static Precise4GSector ConstructSector(this Precise4GView view, ICellRepository repository)
        {
            var sector = Mapper.Map<Precise4GView, Precise4GSector>(view);
            var cell = repository.GetBySectorId(view.CellId, view.SectorId);
            if (cell == null)
            {
                sector.Height = -1;
            }
            else
            {
                Mapper.Map(cell, sector);
                sector.DownTilt = cell.MTilt + cell.ETilt;
            }
            return sector;
        }
    }
}
