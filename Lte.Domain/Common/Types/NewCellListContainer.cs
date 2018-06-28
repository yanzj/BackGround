using System.Collections.Generic;
using Lte.Domain.Excel;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Common.Types
{
    [TypeDoc("新的小区信息列表容器")]
    public class NewCellListContainer
    {
        [MemberDoc("Excel小区信息列表")]
        public IEnumerable<CellExcel> Infos { get; set; }
    }
}