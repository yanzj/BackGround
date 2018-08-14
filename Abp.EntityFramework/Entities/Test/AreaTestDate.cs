using System;
using Abp.Domain.Entities;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Test
{
    [TypeDoc("����������ڼ�¼")]
    public class AreaTestDate : Entity, IArea
    {
        [MemberDoc("��������")]
        public string Area { get; set; }

        [MemberDoc("���2G��������")]
        public DateTime LatestDate2G { get; set; }

        [MemberDoc("���3G��������")]
        public DateTime LatestDate3G { get; set; }

        [MemberDoc("���4G��������")]
        public DateTime LatestDate4G { get; set; }

        [MemberDoc("���VoLTE��������")]
        public DateTime LatestDateVolte { get; set; }
    }
}