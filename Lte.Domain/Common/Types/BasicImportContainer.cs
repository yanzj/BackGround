using System.Collections.Generic;
using Lte.Domain.Excel;

namespace Lte.Domain.Common.Types
{
    public static class BasicImportContainer
    {
        public static List<ENodebExcel> ENodebExcels { get; set; } = new List<ENodebExcel>();

        public static List<CellExcel> CellExcels { get; set; } = new List<CellExcel>();

        public static int LteRruIndex { get; set; }

        public static int LteCellIndex { get; set; }

        public static List<CdmaCellExcel> CdmaCellExcels { get; set; } = new List<CdmaCellExcel>();

        public static int CdmaRruIndex { get; set; }
    }
}
