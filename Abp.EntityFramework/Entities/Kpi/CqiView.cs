using System;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Kpi
{
    [AutoMapFrom(typeof(CqiHuawei), typeof(CqiZte))]
    [IncreaseNumberKpi(KpiPrefix = "Cqi", KpiAffix = "Reports", IndexRange = 16)]
    public class CqiView : IStatTime, ILteCellQuery, IENodebName, ICityDistrictTown
    {
        public DateTime StatTime { get; set; }

        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public string ENodebName { get; set; }

        public string District { get; set; }

        public string Town { get; set; }

        public string City { get; set; }

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

        public Tuple<int, int> CqiCounts => this.GetDivsionIntTuple(7);

        public double CqiRate => (double)CqiCounts.Item2 * 100 / (CqiCounts.Item1 + CqiCounts.Item2);
    }
}
