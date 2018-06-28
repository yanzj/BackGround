using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;

namespace Abp.EntityFramework.Entities
{
    [AutoMapFrom(typeof(QciHuawei), typeof(QciZte))]
    public class TownQciStat : Entity, ITownId, IStatTime
    {
        public int TownId { get; set; }

        public DateTime StatTime { get; set; }

        public long Cqi0Times { get; set; }

        public long Cqi1Times { get; set; }

        public long Cqi2Times { get; set; }

        public long Cqi3Times { get; set; }

        public long Cqi4Times { get; set; }

        public long Cqi5Times { get; set; }

        public long Cqi6Times { get; set; }

        public long Cqi7Times { get; set; }

        public long Cqi8Times { get; set; }

        public long Cqi9Times { get; set; }

        public long Cqi10Times { get; set; }

        public long Cqi11Times { get; set; }

        public long Cqi12Times { get; set; }

        public long Cqi13Times { get; set; }

        public long Cqi14Times { get; set; }

        public long Cqi15Times { get; set; }
    }
}