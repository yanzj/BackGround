using System;
using System.Collections.Generic;
using Lte.Domain.Regular.Attributes;

namespace Lte.MySqlFramework.Entities.Cdma
{
    [TypeDoc("����TOP���ӳɹ���С����ͼ")]
    public class TopConnection3GDateView
    {
        [MemberDoc("ͳ������")]
        public DateTime StatDate { get; set; }

        [MemberDoc("TOP���ӳɹ���С����ͼ�б�")]
        public IEnumerable<TopConnection3GCellView> StatViews { get; set; }
    }
}