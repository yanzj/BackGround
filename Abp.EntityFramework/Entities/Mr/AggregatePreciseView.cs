using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.Domain.Common.Types;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Mr
{
    [AutoMapFrom(typeof(TownPreciseStat))]
    [TypeDoc("聚合MRS覆盖统计视图")]
    public class AggregatePreciseView : IStatTime
    {
        [MemberDoc("小区个数")]
        public int CellCount { get; set; }

        public string Name { get; set; }
        
        [ArraySumProtection]
        public DateTime StatTime { get; set; }
        
        public long TotalMrs { get; set; }

        public long ThirdNeighbors { get; set; }

        public long SecondNeighbors { get; set; }

        public int FirstNeighbors { get; set; }

        public double PreciseRate => 100 - (double)SecondNeighbors * 100 / TotalMrs;

        public double FirstRate => 100 - (double)FirstNeighbors * 100 / TotalMrs;

        public double ThirdRate => 100 - (double)ThirdNeighbors * 100 / TotalMrs;

        public long NeighborsMore { get; set; }

        public long InterFirstNeighbors { get; set; }

        public long InterSecondNeighbors { get; set; }

        public long InterThirdNeighbors { get; set; }
    }
}
