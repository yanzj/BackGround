using System.Collections.Generic;
using Lte.Domain.Excel;

namespace Lte.Domain.Common.Types
{
    public class NewBtsListContainer
    {
        public IEnumerable<BtsExcel> Infos { get; set; }
    }
}