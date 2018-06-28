using Lte.Domain.Common;
using System;
using Lte.Domain.Common.Types;
using TraceParser.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class CellsToAddMod : TraceConfig
    {
        public long cellIndex { get; set; }

        public Q_OffsetRange cellIndividualOffset { get; set; }

        public long physCellId { get; set; }

        public class PerDecoder : DecoderBase<CellsToAddMod>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CellsToAddMod config, BitArrayInputStream input)
            {
                config.cellIndex = input.ReadBits(5) + 1;
                config.physCellId = input.ReadBits(9);
                const int nBits = 5;
                config.cellIndividualOffset = (Q_OffsetRange)input.ReadBits(nBits);
            }
        }
    }

    [Serializable]
    public class AltTTT_CellsToAddMod_r12 : TraceConfig
    {
        public long cellIndex { get; set; }

        public PhysCellIdRange physCellIdRange { get; set; }

        public class PerDecoder : DecoderBase<AltTTT_CellsToAddMod_r12>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(AltTTT_CellsToAddMod_r12 config, BitArrayInputStream input)
            {
                config.cellIndex = input.ReadBits(5) + 1;
                config.physCellIdRange = PhysCellIdRange.PerDecoder.Instance.Decode(input);
            }
        }
    }

    [Serializable]
    public class CellsToAddModCDMA2000 : TraceConfig
    {
        public long cellIndex { get; set; }

        public long physCellId { get; set; }

        public class PerDecoder : DecoderBase<CellsToAddModCDMA2000>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CellsToAddModCDMA2000 config, BitArrayInputStream input)
            {
                config.cellIndex = input.ReadBits(5) + 1;
                config.physCellId = input.ReadBits(9);
            }
        }
    }

    [Serializable]
    public class CellsToAddModUTRA_FDD : TraceConfig
    {
        public long cellIndex { get; set; }

        public long physCellId { get; set; }

        public class PerDecoder : DecoderBase<CellsToAddModUTRA_FDD>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CellsToAddModUTRA_FDD config, BitArrayInputStream input)
            {
                config.cellIndex = input.ReadBits(5) + 1;
                config.physCellId = input.ReadBits(9);
            }
        }
    }

    [Serializable]
    public class CellsToAddModUTRA_TDD : TraceConfig
    {
        public long cellIndex { get; set; }

        public long physCellId { get; set; }

        public class PerDecoder : DecoderBase<CellsToAddModUTRA_TDD>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CellsToAddModUTRA_TDD config, BitArrayInputStream input)
            {
                config.cellIndex = input.ReadBits(5) + 1;
                config.physCellId = input.ReadBits(7);
            }
        }
    }

    [Serializable]
    public class BlackCellsToAddMod : TraceConfig
    {
        public long cellIndex { get; set; }

        public PhysCellIdRange physCellIdRange { get; set; }

        public class PerDecoder : DecoderBase<BlackCellsToAddMod>
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            protected override void ProcessConfig(BlackCellsToAddMod config, BitArrayInputStream input)
            {
                config.cellIndex = input.ReadBits(5) + 1;
                config.physCellIdRange = PhysCellIdRange.PerDecoder.Instance.Decode(input);
            }
        }
    }

    [Serializable]
    public class SCellToAddMod_r10 : TraceConfig
    {
        public cellIdentification_r10_Type cellIdentification_r10 { get; set; }

        public long? dl_CarrierFreq_v1090 { get; set; }

        public RadioResourceConfigCommonSCell_r10 radioResourceConfigCommonSCell_r10 { get; set; }

        public RadioResourceConfigDedicatedSCell_r10 radioResourceConfigDedicatedSCell_r10 { get; set; }

        public long sCellIndex_r10 { get; set; }

        [Serializable]
        public class cellIdentification_r10_Type : TraceConfig
        {
            public long dl_CarrierFreq_r10 { get; set; }

            public long physCellId_r10 { get; set; }

            public class PerDecoder : DecoderBase<cellIdentification_r10_Type>
            {
                public static readonly PerDecoder Instance = new PerDecoder();
                
                protected override void ProcessConfig(cellIdentification_r10_Type config, BitArrayInputStream input)
                {
                    config.physCellId_r10 = input.ReadBits(9);
                    config.dl_CarrierFreq_r10 = input.ReadBits(0x10);
                }
            }
        }

        public class PerDecoder : DecoderBase<SCellToAddMod_r10>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(SCellToAddMod_r10 config, BitArrayInputStream input)
            {
                bool flag = input.ReadBit() != 0;
                BitMaskStream stream = new BitMaskStream(input, 3);
                config.sCellIndex_r10 = input.ReadBits(3) + 1;
                if (stream.Read())
                {
                    config.cellIdentification_r10 = cellIdentification_r10_Type.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    config.radioResourceConfigCommonSCell_r10 = RadioResourceConfigCommonSCell_r10.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    config.radioResourceConfigDedicatedSCell_r10 = RadioResourceConfigDedicatedSCell_r10.PerDecoder.Instance.Decode(input);
                }
                if (flag)
                {
                    BitMaskStream stream2 = new BitMaskStream(input, 1);
                    if (stream2.Read())
                    {
                        config.dl_CarrierFreq_v1090 = input.ReadBits(0x12) + 0x10000;
                    }
                }
            }
        }
    }

}
