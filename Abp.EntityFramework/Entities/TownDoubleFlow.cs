using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;

namespace Abp.EntityFramework.Entities
{
    [AutoMapFrom(typeof(DoubleFlowHuawei), typeof(DoubleFlowZte))]
    public class TownDoubleFlow : Entity, ITownId, IStatTime
    {
        public int TownId { get; set; }

        public DateTime StatTime { get; set; }

        public long CloseLoopRank1Prbs { get; set; }

        public long CloseLoopRank2Prbs { get; set; }

        public long OpenLoopRank1Prbs { get; set; }

        public long OpenLoopRank2Prbs { get; set; }

    }
}
