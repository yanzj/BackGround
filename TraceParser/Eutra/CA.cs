using Lte.Domain.Common;
using System;
using Lte.Domain.Common.Types;
using TraceParser.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class CA_MIMO_ParametersDL_r10 : TraceConfig
    {
        public CA_BandwidthClass_r10 ca_BandwidthClassDL_r10 { get; set; }

        public MIMO_CapabilityDL_r10? supportedMIMO_CapabilityDL_r10 { get; set; }

        public class PerDecoder : DecoderBase<CA_MIMO_ParametersDL_r10>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CA_MIMO_ParametersDL_r10 config, BitArrayInputStream input)
            {
                BitMaskStream stream = new BitMaskStream(input, 1);
                input.ReadBit();
                config.ca_BandwidthClassDL_r10 = (CA_BandwidthClass_r10)input.ReadBits(3);
                if (stream.Read())
                {
                    config.supportedMIMO_CapabilityDL_r10 = (MIMO_CapabilityDL_r10)input.ReadBits(2);
                }
            }
        }
    }

    [Serializable]
    public class CA_MIMO_ParametersUL_r10 : TraceConfig
    {
        public CA_BandwidthClass_r10 ca_BandwidthClassUL_r10 { get; set; }

        public MIMO_CapabilityUL_r10? supportedMIMO_CapabilityUL_r10 { get; set; }

        public class PerDecoder : DecoderBase<CA_MIMO_ParametersUL_r10>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CA_MIMO_ParametersUL_r10 config, BitArrayInputStream input)
            {
                BitMaskStream stream = new BitMaskStream(input, 1);
                input.ReadBit();
                config.ca_BandwidthClassUL_r10 = (CA_BandwidthClass_r10)input.ReadBits(3);
                if (stream.Read())
                {
                    config.supportedMIMO_CapabilityUL_r10 = (MIMO_CapabilityUL_r10)input.ReadBits(1);
                }
            }
        }
    }

}
