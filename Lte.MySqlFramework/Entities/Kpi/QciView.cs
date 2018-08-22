using System;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Kpi;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular;
using Lte.Domain.Regular.Attributes;

namespace Lte.MySqlFramework.Entities.Kpi
{
    [AutoMapFrom(typeof(QciHuawei), typeof(QciZte))]
    [IncreaseNumberKpi(KpiPrefix = "Cqi", KpiAffix = "Times", IndexRange = 16)]
    public class QciView : IStatTime, ILteCellQuery, IENodebName, ICityDistrictTown
    {
        public DateTime StatTime { get; set; }

        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public string ENodebName { get; set; }

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

        public Tuple<int, int> CqiCounts => this.GetDivsionIntTuple(7);

        public double CqiRate => (double)CqiCounts.Item2 * 100 / (CqiCounts.Item1 + CqiCounts.Item2);

        public string City { get; set; }

        public string District { get; set; }

        public string Town { get; set; }
    }
}
