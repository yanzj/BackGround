using Lte.Domain.Common;
using System;
using Lte.Domain.Common.Types;
using TraceParser.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class AC_BarringConfig : TraceConfig
    {
        public ac_BarringFactor_Enum ac_BarringFactor { get; set; }

        public string ac_BarringForSpecialAC { get; set; }

        public ac_BarringTime_Enum ac_BarringTime { get; set; }

        public class PerDecoder : DecoderBase<AC_BarringConfig>
        {
            public static IDecoder<AC_BarringConfig> Instance => new PerDecoder();
            
            protected override void ProcessConfig(AC_BarringConfig config, BitArrayInputStream input)
            {
                config.ac_BarringFactor = (ac_BarringFactor_Enum)input.ReadBits(4);
                config.ac_BarringTime = (ac_BarringTime_Enum)input.ReadBits(3);
                config.ac_BarringForSpecialAC = input.ReadBitString(5);
            }
        }
    }

    [Serializable]
    public class AC_BarringConfig1XRTT_r9 : TraceConfig
    {
        public long ac_Barring0to9_r9 { get; set; }

        public long ac_Barring10_r9 { get; set; }

        public long ac_Barring11_r9 { get; set; }

        public long ac_Barring12_r9 { get; set; }

        public long ac_Barring13_r9 { get; set; }

        public long ac_Barring14_r9 { get; set; }

        public long ac_Barring15_r9 { get; set; }

        public long ac_BarringEmg_r9 { get; set; }

        public long ac_BarringMsg_r9 { get; set; }

        public long ac_BarringReg_r9 { get; set; }

        public class PerDecoder : DecoderBase<AC_BarringConfig1XRTT_r9>
        {
            public static IDecoder<AC_BarringConfig1XRTT_r9> Instance => new PerDecoder();

            protected override void ProcessConfig(AC_BarringConfig1XRTT_r9 config, BitArrayInputStream input)
            {
                config.ac_Barring0to9_r9 = input.ReadBits(6);
                config.ac_Barring10_r9 = input.ReadBits(3);
                config.ac_Barring11_r9 = input.ReadBits(3);
                config.ac_Barring12_r9 = input.ReadBits(3);
                config.ac_Barring13_r9 = input.ReadBits(3);
                config.ac_Barring14_r9 = input.ReadBits(3);
                config.ac_Barring15_r9 = input.ReadBits(3);
                config.ac_BarringMsg_r9 = input.ReadBits(3);
                config.ac_BarringReg_r9 = input.ReadBits(3);
                config.ac_BarringEmg_r9 = input.ReadBits(3);
            }
        }
    }
}
