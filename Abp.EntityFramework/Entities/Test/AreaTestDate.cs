using System;
using Abp.Domain.Entities;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Test
{
    [TypeDoc("区域测试日期记录")]
    public class AreaTestDate : Entity, IArea
    {
        [MemberDoc("区域名称")]
        public string Area { get; set; }

        [MemberDoc("最近2G测试日期")]
        public DateTime LatestDate2G { get; set; }

        [MemberDoc("最近3G测试日期")]
        public DateTime LatestDate3G { get; set; }

        [MemberDoc("最近4G测试日期")]
        public DateTime LatestDate4G { get; set; }

        [MemberDoc("最近VoLTE测试日期")]
        public DateTime LatestDateVolte { get; set; }
    }
}