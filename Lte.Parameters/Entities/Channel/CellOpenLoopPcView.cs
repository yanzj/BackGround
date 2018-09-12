using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Channel;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;

namespace Lte.Parameters.Entities.Channel
{
    [AutoMapFrom(typeof(CellUlpcComm), typeof(PowerControlULZte))]
    public class CellOpenLoopPcView : ILteCellQuery
    {
        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        [AutoMapPropertyResolve("p0NominalPUSCH", typeof(PowerControlULZte))]
        public int P0NominalPUSCH { get; set; }

        [AutoMapPropertyResolve("poNominalPUCCH", typeof(PowerControlULZte))]
        public int P0NominalPUCCH { get; set; }

        [AutoMapPropertyResolve("alpha", typeof(PowerControlULZte))]
        public int PassLossCoeff { get; set; }

        [AutoMapPropertyResolve("deltaMsg3", typeof(PowerControlULZte))]
        public int DeltaMsg2 { get; set; }

        [AutoMapPropertyResolve("deltaFPucchFormat2b", typeof(PowerControlULZte))]
        public int DeltaFPUCCHFormat2b { get; set; }

        [AutoMapPropertyResolve("deltaFPucchFormat2a", typeof(PowerControlULZte))]
        public int DeltaFPUCCHFormat2a { get; set; }

        [AutoMapPropertyResolve("deltaPreambleMsg3", typeof(PowerControlULZte))]
        public int DeltaPreambleMsg3 { get; set; }

        [AutoMapPropertyResolve("deltaFPucchFormat1b", typeof(PowerControlULZte))]
        public int DeltaFPUCCHFormat1b { get; set; }

        [AutoMapPropertyResolve("deltaFPucchFormat1", typeof(PowerControlULZte))]
        public int DeltaFPUCCHFormat1 { get; set; }

        [AutoMapPropertyResolve("deltaFPucchFormat2", typeof(PowerControlULZte))]
        public int DeltaFPUCCHFormat2 { get; set; }
    }
}