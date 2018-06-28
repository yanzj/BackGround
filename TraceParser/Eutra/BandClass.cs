using Lte.Domain.Common;
using System;
using Lte.Domain.Common.Types;
using TraceParser.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class BandClassInfoCDMA2000 : TraceConfig
    {
        public BandclassCDMA2000 bandClass { get; set; }

        public long? cellReselectionPriority { get; set; }

        public long threshX_High { get; set; }

        public long threshX_Low { get; set; }

        public class PerDecoder : DecoderBase<BandClassInfoCDMA2000>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(BandClassInfoCDMA2000 config, BitArrayInputStream input)
            {
                input.ReadBit();
                BitMaskStream stream = new BitMaskStream(input, 1);
                input.ReadBit();
                config.bandClass = (BandclassCDMA2000)input.ReadBits(5);
                if (stream.Read())
                {
                    config.cellReselectionPriority = input.ReadBits(3);
                }
                config.threshX_High = input.ReadBits(6);
                config.threshX_Low = input.ReadBits(6);
            }
        }
    }

    [Serializable]
    public class BandClassPriority1XRTT : TraceConfig
    {
        public BandclassCDMA2000 bandClass { get; set; }

        public long cellReselectionPriority { get; set; }

        public class PerDecoder : DecoderBase<BandClassPriority1XRTT>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(BandClassPriority1XRTT config, BitArrayInputStream input)
            {
                input.ReadBit();
                config.bandClass = (BandclassCDMA2000)input.ReadBits(5);
                config.cellReselectionPriority = input.ReadBits(3);
            }
        }
    }

    [Serializable]
    public class BandClassPriorityHRPD : TraceConfig
    {
        public BandclassCDMA2000 bandClass { get; set; }

        public long cellReselectionPriority { get; set; }

        public class PerDecoder : DecoderBase<BandClassPriorityHRPD>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(BandClassPriorityHRPD config, BitArrayInputStream input)
            {
                input.ReadBit();
                config.bandClass = (BandclassCDMA2000)input.ReadBits(5);
                config.cellReselectionPriority = input.ReadBits(3);
            }
        }
    }

    [Serializable]
    public class SupportedBandEUTRA : TraceConfig
    {
        public long bandEUTRA { get; set; }

        public bool halfDuplex { get; set; }

        public class PerDecoder : DecoderBase<SupportedBandEUTRA>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(SupportedBandEUTRA config, BitArrayInputStream input)
            {
                config.bandEUTRA = input.ReadBits(6) + 1;
                config.halfDuplex = input.ReadBit() == 1;
            }
        }
    }

    [Serializable]
    public class SupportedBandEUTRA_v9e0 : TraceConfig
    {
        public long? bandEUTRA_v9e0 { get; set; }

        public class PerDecoder : DecoderBase<SupportedBandEUTRA_v9e0>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(SupportedBandEUTRA_v9e0 config, BitArrayInputStream input)
            {
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    config.bandEUTRA_v9e0 = input.ReadBits(8) + 0x41;
                }
            }
        }
    }

}
