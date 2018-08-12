using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Excel;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Maintainence
{
    [TypeDoc("巡检结果基本信息")]
    [AutoMapFrom(typeof(CheckingBasicExcel))]
    public class CheckingBasic : Entity
    {
        [MemberDoc("巡检流水号")]
        public string CheckingFlowNumber { get; set; }

        [MemberDoc("开始时间")]
        public DateTime BeginDate { get; set; }

        [MemberDoc("结束时间")]
        public DateTime? EndDate { get; set; }

        [MemberDoc("巡检经度")]
        public double Longtitute { get; set; }

        [MemberDoc("巡检纬度")]
        public double Lattitute { get; set; }

        [MemberDoc("巡检距离")]
        public double Distance { get; set; }
    }
}
