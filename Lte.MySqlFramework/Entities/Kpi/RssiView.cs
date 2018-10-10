using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Kpi;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.MySqlFramework.Entities.Kpi
{
    [AutoMapFrom(typeof(RssiHuawei), typeof(RssiZte))]
    public class RssiView : IStatTime, ILteCellQuery, IENodebName, ICityDistrictTown
    {
        [MemberDoc("统计时间")]
        [ArraySumProtection]
        public DateTime StatTime { get; set; }

        [MemberDoc("基站编号")]
        [ArraySumProtection]
        public int ENodebId { get; set; }

        [MemberDoc("扇区编号")]
        [ArraySumProtection]
        public byte SectorId { get; set; }

        [ArraySumProtection]
        public string ENodebName { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string Town { get; set; }

        [MemberDoc("小区最大RSSI(分贝毫瓦)")]
        public double MaxRssi { get; set; }

        [MemberDoc("小区最小RSSI(分贝毫瓦)")]
        public double MinRssi { get; set; }

        [MemberDoc("小区平均RSSI(分贝毫瓦)")]
        public double AverageRssi { get; set; }

    }
}
