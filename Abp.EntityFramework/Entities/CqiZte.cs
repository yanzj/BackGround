using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;

namespace Abp.EntityFramework.Entities
{
    [AutoMapFrom(typeof(FlowZteCsv))]
    public class CqiZte : Entity, ILteCellQuery, IStatTime
    {
        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public DateTime StatTime { get; set; }

        public int Cqi0Reports { get; set; }

        public int Cqi1Reports { get; set; }

        public int Cqi2Reports { get; set; }

        public int Cqi3Reports { get; set; }

        public int Cqi4Reports { get; set; }

        public int Cqi5Reports { get; set; }

        public int Cqi6Reports { get; set; }

        public int Cqi7Reports { get; set; }

        public int Cqi8Reports { get; set; }

        public int Cqi9Reports { get; set; }

        public int Cqi10Reports { get; set; }

        public int Cqi11Reports { get; set; }

        public int Cqi12Reports { get; set; }

        public int Cqi13Reports { get; set; }

        public int Cqi14Reports { get; set; }

        public int Cqi15Reports { get; set; }
    }
}