using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Kpi;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;

namespace Abp.EntityFramework.Entities.RegionKpi
{
    [AutoMapFrom(typeof(RrcHuawei), typeof(RrcZte))]
    public class TownRrcStat : Entity, ITownId, IStatTime
    {
        public int TownId { get; set; }

        public DateTime StatTime { get; set; }

        public int MtAccessRrcRequest { get; set; }

        public int MtAccessRrcSuccess { get; set; }

        public int MtAccessRrcFail { get; set; }

        public int MoSignallingRrcRequest { get; set; }

        public int MoSignallingRrcSuccess { get; set; }

        public int MoSignallingRrcFail { get; set; }

        public int MoDataRrcRequest { get; set; }

        public int MoDataRrcSuccess { get; set; }

        public int MoDataRrcFail { get; set; }

        public int HighPriorityRrcRequest { get; set; }

        public int HighPriorityRrcSuccess { get; set; }

        public int HighPriorityRrcFail { get; set; }

        public int EmergencyRrcRequest { get; set; }

        public int EmergencyRrcSuccess { get; set; }

        public int EmergencyRrcFail { get; set; }

        public int TotalRrcRequest { get; set; }

        public int TotalRrcSuccess { get; set; }

        public int TotalRrcFail { get; set; }
    }
}