using System;
using System.Collections.Generic;

namespace Lte.Domain.LinqToExcel.Entities
{
    public class ExcelQueryConstructorArgs
    {
        internal string FileName { get; set; }

        internal ExcelDatabaseEngine DatabaseEngine { get; set; }

        internal Dictionary<string, string> ColumnMappings { get; set; }

        internal Dictionary<string, Func<string, object>> Transformations { get; set; }

        internal StrictMappingType? StrictMapping { get; set; }

        internal bool UsePersistentConnection { get; set; }

        internal TrimSpacesType TrimSpaces { get; set; }

        internal bool ReadOnly { get; set; }
    }
}