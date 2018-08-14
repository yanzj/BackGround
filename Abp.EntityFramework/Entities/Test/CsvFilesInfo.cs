using System;
using Abp.Domain.Entities;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Test
{
    [TypeDoc("CSV�����ļ���Ϣ")]
    public class CsvFilesInfo : Entity
    {
        [MemberDoc("��������")]
        public DateTime TestDate { get; set; }
        
        [MemberDoc("CSV�����ļ�����")]
        public string CsvFileName { get; set; }
        
        [MemberDoc("���Ծ��루�ף�")]
        public double Distance { get; set; }

        [MemberDoc("���ݵ���")]
        public int Count { get; set; }

        [MemberDoc("��Ч���ǵ����ݵ���")]
        public int CoverageCount { get; set; }

        [MemberDoc("������")]
        public double CoverageRate => Count == 0 ? 0 : 100 * (double)CoverageCount / Count;
    }
}