using System;
using Abp.Domain.Entities;
using Lte.Domain.Common.Wireless;

namespace Abp.EntityFramework.Entities
{
    public class PreciseWorkItemCell : Entity, IWorkItemCell
    {
        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public string WorkItemNumber { get; set; }

        public double Db6Share { get; set; }

        public double Db10Share { get; set; }

        public double BackwardDb6Share { get; set; }

        public double BackwardDb10Share { get; set; }

        public double Mod3Share { get; set; }

        public double Mod6Share { get; set; }

        public double BackwardMod3Share { get; set; }

        public double BackwardMod6Share { get; set; }

        public double WeakCoverageRate { get; set; }

        public double OverCoverageRate { get; set; }

        public double OriginalDownTilt { get; set; }

        public double OriginalRsPower { get; set; }

        public double AdjustDownTilt { get; set; }

        public double AdjustRsPower { get; set; }

        public DateTime? BeginDate { get; set; }

        public DateTime? FininshDate { get; set; }
    }
}