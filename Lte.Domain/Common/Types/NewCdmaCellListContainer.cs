using System.Collections.Generic;
using Lte.Domain.Excel;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Common.Types
{
    [TypeDoc("CDMA小区EXCEL信息容器，用于打包向服务器POST")]
    public class NewCdmaCellListContainer
    {
        public IEnumerable<CdmaCellExcel> Infos { get; set; }
    }
}