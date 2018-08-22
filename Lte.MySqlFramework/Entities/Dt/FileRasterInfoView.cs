using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Lte.Domain.Regular.Attributes;

namespace Lte.MySqlFramework.Entities.Dt
{
    [TypeDoc("测试数据文件网格视图，一个测试数据文件包含的若干个网格")]
    public class FileRasterInfoView
    {
        [MemberDoc("测试数据文件名")]
        [Required]
        public string CsvFileName { get; set; }

        [MemberDoc("包含的网格编号列表")]
        public IEnumerable<int> RasterNums { get; set; }
    }
}