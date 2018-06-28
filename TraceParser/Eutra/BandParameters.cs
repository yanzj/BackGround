using Lte.Domain.Common;
using System;
using System.Collections.Generic;
using Lte.Domain.Common.Types;
using TraceParser.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class BandCombinationParameters_v1130 : TraceConfig
    {
        public List<BandParameters_v1130> bandParameterList_r11 { get; set; }

        public multipleTimingAdvance_r11_Enum? multipleTimingAdvance_r11 { get; set; }

        public simultaneousRx_Tx_r11_Enum? simultaneousRx_Tx_r11 { get; set; }

        public enum multipleTimingAdvance_r11_Enum
        {
            supported
        }

        public class PerDecoder : DecoderBase<BandCombinationParameters_v1130>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(BandCombinationParameters_v1130 config, BitArrayInputStream input)
            {
                BitMaskStream stream = (input.ReadBit() != 0) ? new BitMaskStream(input, 3) : new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    config.multipleTimingAdvance_r11 = (multipleTimingAdvance_r11_Enum)input.ReadBits(1);
                }
                if (stream.Read())
                {
                    config.simultaneousRx_Tx_r11 = (simultaneousRx_Tx_r11_Enum)input.ReadBits(1);
                }
                if (stream.Read())
                {
                    config.bandParameterList_r11 = new List<BandParameters_v1130>();
                    int num3 = input.ReadBits(6) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        BandParameters_v1130 item = BandParameters_v1130.PerDecoder.Instance.Decode(input);
                        config.bandParameterList_r11.Add(item);
                    }
                }
            }
        }

        public enum simultaneousRx_Tx_r11_Enum
        {
            supported
        }
    }

    [Serializable]
    public class BandCombinationParametersExt_r10 : TraceConfig
    {
        public string supportedBandwidthCombinationSet_r10 { get; set; }

        public class PerDecoder : DecoderBase<BandCombinationParametersExt_r10>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(BandCombinationParametersExt_r10 config, BitArrayInputStream input)
            {
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    int num = input.ReadBits(5);
                    config.supportedBandwidthCombinationSet_r10 = input.ReadBitString(num + 1);
                }
            }
        }
    }

    [Serializable]
    public class BandInfoEUTRA : TraceConfig
    {
        public List<InterFreqBandInfo> interFreqBandList { get; set; }

        public List<InterRAT_BandInfo> interRAT_BandList { get; set; }

        public class PerDecoder : DecoderBase<BandInfoEUTRA>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(BandInfoEUTRA config, BitArrayInputStream input)
            {
                BitMaskStream stream = new BitMaskStream(input, 1);
                config.interFreqBandList = new List<InterFreqBandInfo>();
                int num3 = input.ReadBits(6) + 1;
                for (int i = 0; i < num3; i++)
                {
                    InterFreqBandInfo item = InterFreqBandInfo.PerDecoder.Instance.Decode(input);
                    config.interFreqBandList.Add(item);
                }
                if (stream.Read())
                {
                    config.interRAT_BandList = new List<InterRAT_BandInfo>();
                    int num5 = input.ReadBits(6) + 1;
                    for (int j = 0; j < num5; j++)
                    {
                        InterRAT_BandInfo info2 = InterRAT_BandInfo.PerDecoder.Instance.Decode(input);
                        config.interRAT_BandList.Add(info2);
                    }
                }
            }
        }
    }

    [Serializable]
    public class BandParameters_r10 : TraceConfig
    {
        public long bandEUTRA_r10 { get; set; }

        public List<CA_MIMO_ParametersDL_r10> bandParametersDL_r10 { get; set; }

        public List<CA_MIMO_ParametersUL_r10> bandParametersUL_r10 { get; set; }

        public class PerDecoder : DecoderBase<BandParameters_r10>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(BandParameters_r10 config, BitArrayInputStream input)
            {
                BitMaskStream stream = new BitMaskStream(input, 2);
                config.bandEUTRA_r10 = input.ReadBits(6) + 1;
                if (stream.Read())
                {
                    config.bandParametersUL_r10 = new List<CA_MIMO_ParametersUL_r10>();
                    int num3 = input.ReadBits(4) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        CA_MIMO_ParametersUL_r10 item = CA_MIMO_ParametersUL_r10.PerDecoder.Instance.Decode(input);
                        config.bandParametersUL_r10.Add(item);
                    }
                }
                if (stream.Read())
                {
                    config.bandParametersDL_r10 = new List<CA_MIMO_ParametersDL_r10>();
                    int num5 = input.ReadBits(4) + 1;
                    for (int j = 0; j < num5; j++)
                    {
                        CA_MIMO_ParametersDL_r10 _r3 = CA_MIMO_ParametersDL_r10.PerDecoder.Instance.Decode(input);
                        config.bandParametersDL_r10.Add(_r3);
                    }
                }
            }
        }
    }

    [Serializable]
    public class BandParameters_v1090 : TraceConfig
    {
        public long? bandEUTRA_v1090 { get; set; }

        public class PerDecoder : DecoderBase<BandParameters_v1090>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(BandParameters_v1090 config, BitArrayInputStream input)
            {
                input.ReadBit();
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    config.bandEUTRA_v1090 = input.ReadBits(8) + 0x41;
                }
            }
        }
    }

    [Serializable]
    public class BandParameters_v1130 : TraceConfig
    {
        public supportedCSI_Proc_r11_Enum supportedCSI_Proc_r11 { get; set; }

        public class PerDecoder : DecoderBase<BandParameters_v1130>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(BandParameters_v1130 config, BitArrayInputStream input)
            {
                config.supportedCSI_Proc_r11 = (supportedCSI_Proc_r11_Enum)input.ReadBits(2);
            }
        }

        public enum supportedCSI_Proc_r11_Enum
        {
            n1,
            n3,
            n4
        }
    }

    [Serializable]
    public class InterRAT_BandInfo : TraceConfig
    {
        public bool interRAT_NeedForGaps { get; set; }

        public class PerDecoder : DecoderBase<InterRAT_BandInfo>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(InterRAT_BandInfo config, BitArrayInputStream input)
            {
                config.interRAT_NeedForGaps = input.ReadBit() == 1;
            }
        }
    }

}
