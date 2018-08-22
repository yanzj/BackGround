using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Lte.Domain.Regular.Attributes;

namespace Lte.MySqlFramework.Entities.Dt
{
    [TypeDoc("���������ļ�������ͼ��һ�����������ļ����������ɸ�����")]
    public class FileRasterInfoView
    {
        [MemberDoc("���������ļ���")]
        [Required]
        public string CsvFileName { get; set; }

        [MemberDoc("�������������б�")]
        public IEnumerable<int> RasterNums { get; set; }
    }
}