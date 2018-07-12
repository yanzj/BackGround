using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Common.Geo
{
    [TypeDoc("CDMA小区编号和扇区编号定义 ")]
    public class CdmaCellIdPair
    {
        [MemberDoc("小区编号")]
        public int CellId { get; set; }

        [MemberDoc("扇区编号")]
        public byte SectorId { get; set; }

        [MemberDoc("小区类型")]
        public string CellType { get; set; }
    }
}