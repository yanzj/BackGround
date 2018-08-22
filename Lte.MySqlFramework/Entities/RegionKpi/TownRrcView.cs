using System;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;

namespace Lte.MySqlFramework.Entities.RegionKpi
{
    [AutoMapFrom(typeof(TownRrcStat))]
    public class TownRrcView : ICityDistrictTown, IStatTime
    {
        public string District { get; set; }

        public string Town { get; set; }

        public string City { get; set; }

        public DateTime StatTime { get; set; }

        public int MtAccessRrcRequest { get; set; }

        public int MtAccessRrcSuccess { get; set; }

        public double MtAccessRrcRate => MtAccessRrcRequest == 0 ? 0 : (100 * (double)MtAccessRrcSuccess / MtAccessRrcRequest);

        public int MtAccessRrcFail { get; set; }

        public int MoSignallingRrcRequest { get; set; }

        public int MoSignallingRrcSuccess { get; set; }

        public double MoSiganllingRrcRate
            => MoSignallingRrcRequest == 0 ? 0 : (100 * (double)MoSignallingRrcSuccess / MoSignallingRrcRequest);

        public int MoSignallingRrcFail { get; set; }

        public int MoDataRrcRequest { get; set; }

        public int MoDataRrcSuccess { get; set; }

        public double MoDataRrcRate => MoDataRrcRequest == 0 ? 0 : (100 * (double)MoDataRrcSuccess / MoDataRrcRequest);

        public int MoDataRrcFail { get; set; }

        public int HighPriorityRrcRequest { get; set; }

        public int HighPriorityRrcSuccess { get; set; }

        public double HighPriorityRrcRate
            => HighPriorityRrcRequest == 0 ? 0 : (100 * (double)HighPriorityRrcSuccess / HighPriorityRrcRequest);

        public int HighPriorityRrcFail { get; set; }

        public int EmergencyRrcRequest { get; set; }

        public int EmergencyRrcSuccess { get; set; }

        public double EmergencyRrcRate
            => EmergencyRrcRequest == 0 ? 0 : (100 * (double)EmergencyRrcSuccess / EmergencyRrcRequest);

        public int EmergencyRrcFail { get; set; }

        public int TotalRrcRequest { get; set; }

        public int TotalRrcSuccess { get; set; }

        public double RrcSuccessRate => TotalRrcRequest == 0 ? 0 : (100 * (double)TotalRrcSuccess / TotalRrcRequest);

        public int TotalRrcFail { get; set; }
    }
}