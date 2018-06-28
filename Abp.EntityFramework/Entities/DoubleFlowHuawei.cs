using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;

namespace Abp.EntityFramework.Entities
{
    [AutoMapFrom(typeof(CqiHuaweiCsv))]
    public class DoubleFlowHuawei : Entity, ILteCellQuery, IStatTime
    {
        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public DateTime StatTime { get; set; }

        public long CloseLoopRank1Prbs { get; set; }

        public long CloseLoopRank2Prbs { get; set; }

        public long OpenLoopRank1Prbs { get; set; }

        public long OpenLoopRank2Prbs { get; set; }
    }
}
