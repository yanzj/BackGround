using Lte.Domain.Common;
using System;
using Lte.Domain.Common.Types;
using TraceParser.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class CellSelectionInfo_v1130 : TraceConfig
    {
        public long q_QualMinWB_r11 { get; set; }

        public class PerDecoder : DecoderBase<CellSelectionInfo_v1130>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CellSelectionInfo_v1130 config, BitArrayInputStream input)
            {
                config.q_QualMinWB_r11 = input.ReadBits(5) + -34;
            }
        }
    }

    [Serializable]
    public class CellSelectionInfo_v920 : TraceConfig
    {
        public long q_QualMin_r9 { get; set; }

        public long? q_QualMinOffset_r9 { get; set; }

        public class PerDecoder : DecoderBase<CellSelectionInfo_v920>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CellSelectionInfo_v920 config, BitArrayInputStream input)
            {
                BitMaskStream stream = new BitMaskStream(input, 1);
                config.q_QualMin_r9 = input.ReadBits(5) + -34;
                if (stream.Read())
                {
                    config.q_QualMinOffset_r9 = input.ReadBits(3) + 1;
                }
            }
        }
    }

    [Serializable]
    public class CandidateCellInfo_r10 : TraceConfig
    {
        public long dl_CarrierFreq_r10 { get; set; }

        public long? dl_CarrierFreq_v1090 { get; set; }

        public long physCellId_r10 { get; set; }

        public long? rsrpResult_r10 { get; set; }

        public long? rsrqResult_r10 { get; set; }

        public class PerDecoder : DecoderBase<CandidateCellInfo_r10>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CandidateCellInfo_r10 config, BitArrayInputStream input)
            {
                bool flag = input.ReadBit() != 0;
                BitMaskStream stream = new BitMaskStream(input, 2);
                config.physCellId_r10 = input.ReadBits(9);
                config.dl_CarrierFreq_r10 = input.ReadBits(0x10);
                if (stream.Read())
                {
                    config.rsrpResult_r10 = input.ReadBits(7);
                }
                if (stream.Read())
                {
                    config.rsrqResult_r10 = input.ReadBits(6);
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
