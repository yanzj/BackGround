using System.Collections.Generic;
using Lte.Domain.Excel;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Common.Types
{
    [TypeDoc("新基站信息容器")]
    public class NewENodebListContainer
    {
        [MemberDoc("基站Excel信息列表")]
        public IEnumerable<ENodebExcel> Infos { get; set; }
    }
}