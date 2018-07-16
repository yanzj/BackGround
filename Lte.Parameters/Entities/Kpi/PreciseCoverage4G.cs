using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using System;
using Lte.Domain.Common;
using Lte.Domain.Excel;
using Lte.Domain.Regular.Attributes;

namespace Lte.Parameters.Entities.Kpi
{
    public class PreciseCoverage4G : Entity
    {
        [ArraySumProtection]
        public DateTime StatTime { get; set; }

        public string DateString => StatTime.ToShortDateString();

        [ArraySumProtection]
        public int CellId { get; set; }

        [ArraySumProtection]
        public byte SectorId { get; set; }

        public int Neighbors0 { get; set; }

        public int Neighbors1 { get; set; }

        public int Neighbors2 { get; set; }

        public int Neighbors3 { get; set; }

        public int NeighborsMore { get; set; }

        public int InterFirstNeighbors => Neighbors1 + Neighbors2 + Neighbors3 + NeighborsMore;

        public int InterSecondNeighbors => Neighbors2 + Neighbors3 + NeighborsMore;

        public int InterThirdNeighbors => Neighbors3 + NeighborsMore;

        public int TotalMrs { get; set; }

        public int ThirdNeighbors { get; set; }

        public int SecondNeighbors { get; set; }

        public int FirstNeighbors { get; set; }

        public double FirstRate => 100 * (double)FirstNeighbors / TotalMrs;

        public double FirstPreciseRate => 100 - FirstRate;

        public double SecondRate => 100 * (double)SecondNeighbors / TotalMrs;

        public double SecondPreciseRate => 100 - SecondRate;

        public double ThirdRate => 100 * (double)ThirdNeighbors / TotalMrs;

        public double ThirdPreciseRate => 100 - ThirdRate;
    }
}
