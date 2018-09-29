using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.EntityFramework.Entities.Kpi
{
    [AutoMapFrom(typeof(FlowZteCsv))]
    [TypeDoc("中兴RSSI统计")]
    public class RssiZte : Entity, ILteCellQuery, IStatTime
    {
        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public DateTime StatTime { get; set; }

        [MemberDoc("小区最大RSSI(分贝毫瓦)")]
        public double MaxRssi { get; set; }

        [MemberDoc("小区最小RSSI(分贝毫瓦)")]
        public double MinRssi { get; set; }

        [MemberDoc("小区平均RSSI(分贝毫瓦)")]
        public double AverageRssi { get; set; }

        [MemberDoc("Pusch信道RSSI小于等于-120dBm的上报次数")]
        public int PuschRssiBelow120 { get; set; }

        [MemberDoc("Pusch信道RSSI在范围(-120dBm,-116dBm]的上报次数")]
        public int PuschRssi120To116 { get; set; }

        [MemberDoc("Pusch信道RSSI在范围(-116dBm,-112dBm]的上报次数")]
        public int PuschRssi116To112 { get; set; }

        [MemberDoc("Pusch信道RSSI在范围(-112dBm,-108dBm]的上报次数")]
        public int PuschRssi112To108 { get; set; }

        [MemberDoc("Pusch信道RSSI在范围(-108dBm,-104dBm]的上报次数")]
        public int PuschRssi108To104 { get; set; }

        [MemberDoc("Pusch信道RSSI在范围(-104dBm,-100dBm]的上报次数")]
        public int PuschRssi104To100 { get; set; }

        [MemberDoc("Pusch信道RSSI在范围(-100dBm,-96dBm]的上报次数")]
        public int PuschRssi100To96 { get; set; }

        [MemberDoc("Pusch信道RSSI在范围(-96dBm,-92dBm]的上报次数")]
        public int PuschRssi96To92 { get; set; }

        [MemberDoc("Pusch信道RSSI大于-92dBm的上报次数")]
        public int PuschRssiAbove92 { get; set; }

        [MemberDoc("Pucch信道RSSI小于等于-120dBm的上报次数")]
        public int PucchRssiBelow120 { get; set; }

        [MemberDoc("Pucch信道RSSI在范围(-120dBm,-116dBm]的上报次数")]
        public int PucchRssi120To116 { get; set; }

        [MemberDoc("Pucch信道RSSI在范围(-116dBm,-112dBm]的上报次数")]
        public int PucchRssi116To112 { get; set; }

        [MemberDoc("Pucch信道RSSI在范围(-112dBm,-108dBm]的上报次数")]
        public int PucchRssi112To108 { get; set; }

        [MemberDoc("Pucch信道RSSI在范围(-108dBm,-104dBm]的上报次数")]
        public int PucchRssi108To104 { get; set; }

        [MemberDoc("Pucch信道RSSI在范围(-104dBm,-100dBm]的上报次数")]
        public int PucchRssi104To100 { get; set; }

        [MemberDoc("Pucch信道RSSI在范围(-100dBm,-96dBm]的上报次数")]
        public int PucchRssi100To96 { get; set; }

        [MemberDoc("Pucch信道RSSI在范围(-96dBm,-92dBm]的上报次数")]
        public int PucchRssi96To92 { get; set; }

        [MemberDoc("Pucch信道RSSI大于-92dBm的上报次数")]
        public int PucchRssiAbove92 { get; set; }
    }
}
