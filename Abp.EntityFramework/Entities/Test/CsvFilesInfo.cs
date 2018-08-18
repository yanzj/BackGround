using System;
using Abp.Domain.Entities;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Test
{
    [TypeDoc("CSV测试文件信息")]
    public class CsvFilesInfo : Entity
    {
        [MemberDoc("测试日期")]
        public DateTime TestDate { get; set; }
        
        [MemberDoc("CSV测试文件名称")]
        public string CsvFileName { get; set; }
        
        [MemberDoc("测试距离（米）")]
        public double Distance { get; set; }

        [MemberDoc("数据点数")]
        public int Count { get; set; }

        [MemberDoc("有效覆盖的数据点数")]
        public int CoverageCount { get; set; }

        [MemberDoc("覆盖率")]
        public double CoverageRate => Count == 0 ? 0 : 100 * (double)CoverageCount / Count;

        public string FileType { get; set; }
    }
}