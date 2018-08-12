using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;

namespace Abp.EntityFramework.Entities.Kpi
{
    [AutoMapFrom(typeof(FlowHuaweiCsv))]
    public class QciHuawei : Entity, ILocalCellQuery, IStatTime
    {
        public DateTime StatTime { get; set; }

        public int ENodebId { get; set; }

        public byte LocalCellId { get; set; }

        public int Cqi0Times { get; set; }

        public int Cqi1Times { get; set; }

        public int Cqi2Times { get; set; }

        public int Cqi3Times { get; set; }

        public int Cqi4Times { get; set; }

        public int Cqi5Times { get; set; }

        public int Cqi6Times { get; set; }

        public int Cqi7Times { get; set; }

        public int Cqi8Times { get; set; }

        public int Cqi9Times { get; set; }

        public int Cqi10Times { get; set; }

        public int Cqi11Times { get; set; }

        public int Cqi12Times { get; set; }

        public int Cqi13Times { get; set; }

        public int Cqi14Times { get; set; }

        public int Cqi15Times { get; set; }
    }
}