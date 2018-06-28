using System.Collections.Generic;

namespace Lte.Domain.LinqToExcel.Entities
{
    public class ExcelRowNoHeader : List<ExcelCell>
    {
        public ExcelRowNoHeader(IEnumerable<ExcelCell> cells)
        {
            AddRange(cells);
        }
    }
}
