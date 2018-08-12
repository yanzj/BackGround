using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Infrastructure
{
    [TypeDoc("包含PCI的LTE邻区关系视图")]
    [AutoMapFrom(typeof(NearestPciCell))]
    public class NearestPciCellView
    {
        [MemberDoc("小区编号（对于LTE来说就是基站编号）")]
        public int CellId { get; set; }

        [MemberDoc("扇区编号")]
        public byte SectorId { get; set; }

        [MemberDoc("邻区小区编号")]
        public int NearestCellId { get; set; }

        [MemberDoc("邻区扇区编号")]
        public byte NearestSectorId { get; set; }

        [MemberDoc("PCI，便于查询邻区")]
        public short Pci { get; set; }

        [MemberDoc("切换次数，仅供参考")]
        public int TotalTimes { get; set; }

        [MemberDoc("邻区基站名称")]
        public string NearestENodebName { get; set; }
    }
}