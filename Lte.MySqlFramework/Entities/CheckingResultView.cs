using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.Regular.Attributes;

namespace Lte.MySqlFramework.Entities
{
    [TypeDoc("巡检结果信息")]
    [AutoMapFrom(typeof(CheckingBasic))]
    public class CheckingResultView : IGeoPoint<double>, IBeginDate
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

        [MemberDoc("巡检结果详细信息")]
        public IEnumerable<CheckingDetailsView> CheckingDetailsViews { get; set; }
    }
}
