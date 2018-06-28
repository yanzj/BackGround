using System;
using System.Collections.Generic;
using System.Linq;

namespace Lte.Domain.LinqToExcel.Entities
{
    public class ExcelRow : List<ExcelCell>
    {
        private readonly IDictionary<string, int> _columnIndexMapping;

        public ExcelRow() :
            this(new List<ExcelCell>(), new Dictionary<string, int>())
        { }

        public ExcelRow(IList<ExcelCell> cells, IDictionary<string, int> columnIndexMapping)
        {
            for (int i = 0; i < cells.Count; i++) Insert(i, cells[i]);
            _columnIndexMapping = columnIndexMapping;
        }

        public ExcelCell this[string columnName]
        {
            get
            {
                if (!_columnIndexMapping.ContainsKey(columnName))
                    throw new ArgumentException(
                        $"'{columnName}' column name does not exist. Valid column names are '{string.Join("', '", _columnIndexMapping.Keys.ToArray())}'");
                return base[_columnIndexMapping[columnName]];
            }
        }

        public IEnumerable<string> ColumnNames => _columnIndexMapping.Keys;
    }
}
