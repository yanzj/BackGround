using System;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common;
using Lte.Domain.Common.Types;

namespace Lte.Parameters.Entities.Kpi
{
    [AutoMapFrom(typeof(InterferenceMatrixMongo))]
    public class NeighborRsrpView
    {
        public DateTime StatDate { get; set; }

        public int? NeighborEarfcn { get; set; }

        [AutoMapPropertyResolve("NeighborRsrpBelow120", typeof(InterferenceMatrixMongo), typeof(NullableZeroIntTransform))]
        public int NeighborRsrpBelow120 { get; set; }

        [AutoMapPropertyResolve("NeighborRsrpBetween120110", typeof(InterferenceMatrixMongo), typeof(NullableZeroIntTransform))]
        public int NeighborRsrpBetween120110 { get; set; }

        [AutoMapPropertyResolve("NeighborRsrpBetween110105", typeof(InterferenceMatrixMongo), typeof(NullableZeroIntTransform))]
        public int NeighborRsrpBetween110105 { get; set; }

        [AutoMapPropertyResolve("NeighborRsrpBetween105100", typeof(InterferenceMatrixMongo), typeof(NullableZeroIntTransform))]
        public int NeighborRsrpBetween105100 { get; set; }

        [AutoMapPropertyResolve("NeighborRsrpBetween10090", typeof(InterferenceMatrixMongo), typeof(NullableZeroIntTransform))]
        public int NeighborRsrpBetween10090 { get; set; }

        [AutoMapPropertyResolve("NeighborRsrpAbove90", typeof(InterferenceMatrixMongo), typeof(NullableZeroIntTransform))]
        public int NeighborRsrpAbove90 { get; set; }

        public double AverageNeighborRsrp
            =>
                (NeighborRsrpBelow120 * (-130) + NeighborRsrpBetween120110 * (-114.5) + NeighborRsrpBetween110105 * (-107.5) +
                 NeighborRsrpBetween105100 * (-102.5) + NeighborRsrpBetween10090 * (-94.5) + NeighborRsrpAbove90 * (-82.5)) /
                (NeighborRsrpBelow120 + NeighborRsrpBetween120110 + NeighborRsrpBetween110105 +
                 NeighborRsrpBetween105100 + NeighborRsrpBetween10090 + NeighborRsrpAbove90);
    }
}