using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;

namespace Abp.EntityFramework.Entities
{
    [AutoMapFrom(typeof(FlowZteCsv))]
    public class DoubleFlowZte : Entity, ILteCellQuery, IStatTime
    {
        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public DateTime StatTime { get; set; }

        public long SchedulingTm3Old { get; set; }

        public long SchedulingTm4 { get; set; }

        public long SchedulingTm2 { get; set; }

        public long SchedulingTm3Rank2Old { get; set; }

        public long SchedulingTm4Rank2 { get; set; }

        public int PrbPerSubFrame => SectorId < 16 ? 100 : (SectorId < 32 ? 25 : 75);

        public long CloseLoopRank1Prbs => (SchedulingTm4 - SchedulingTm4Rank2) * PrbPerSubFrame;

        public long CloseLoopRank2Prbs => SchedulingTm4Rank2 * PrbPerSubFrame;

        public long OpenLoopRank1Prbs => (SchedulingTm2 + SchedulingTm3Old - SchedulingTm3Rank2Old) * PrbPerSubFrame;

        public long OpenLoopRank2Prbs => SchedulingTm3Rank2Old * PrbPerSubFrame;
    }
}
