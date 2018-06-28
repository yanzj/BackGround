using Abp.EntityFramework.AutoMapper;
using Lte.Parameters.Entities.Channel;

namespace Lte.Parameters.Entities.Basic
{
    [AutoMapFrom(typeof(PDSCHCfg), typeof(PowerControlDLZte))]
    public class CellPower
    {
        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public double RsPower { get; set; }

        public int Pa { get; set; }

        public int Pb { get; set; }

        public int? TxPowerOffsetAnt0 { get; set; }

        public int? TxPowerOffsetAnt1 { get; set; }

        public int? TxPowerOffsetAnt2 { get; set; }

        public int? TxPowerOffsetAnt3 { get; set; }

        public int[] Dci0PowerOffsets { get; set; }

        public int[] Dci1PowerOffsets { get; set; }

        public int[] Dci1APowerOffsets { get; set; }

        public int[] Dci1BPowerOffsets { get; set; }

        public int[] Dci1CPowerOffsets { get; set; }

        public int[] Dci1DPowerOffsets { get; set; }

        public int[] Dci2PowerOffsets { get; set; }

        public int[] Dci2APowerOffsets { get; set; }

        public int[] Dci3PowerOffsets { get; set; }

        public int[] Dci3APowerOffsets { get; set; }

        public CellPower(EUtranCellFDDZte cellFdd, PowerControlDLZte pcDl)
        {
            ENodebId = cellFdd.eNodeB_Id;
            RsPower = cellFdd.cellReferenceSignalPower;
            Pb = cellFdd.pb;
            pcDl.MapTo(this);
            Pa = pcDl.paForDTCH;
        }

        public CellPower(PDSCHCfg cfg, CellDlpcPdschPa paCfg)
        {
            cfg.MapTo(this);
            ENodebId = cfg.eNodeB_Id;
            Pa = paCfg.PaPcOff;
        }

        public CellPower() { }
    }
}