using Lte.Domain.Common;
using System;
using Lte.Domain.Common.Types;
using TraceParser.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class UL_CCCH_Message : TraceConfig
    {
        public UL_CCCH_MessageType message { get; set; }

        public class PerDecoder : DecoderBase<UL_CCCH_Message>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(UL_CCCH_Message config, BitArrayInputStream input)
            {
                config.message = UL_CCCH_MessageType.PerDecoder.Instance.Decode(input);
            }
        }
    }

    [Serializable]
    public class UL_CCCH_MessageType : TraceConfig
    {
        public c1_Type c1 { get; set; }

        public messageClassExtension_Type messageClassExtension { get; set; }

        [Serializable]
        public class c1_Type : TraceConfig
        {
            public RRCConnectionReestablishmentRequest rrcConnectionReestablishmentRequest { get; set; }

            public RRCConnectionRequest rrcConnectionRequest { get; set; }

            public class PerDecoder : DecoderBase<c1_Type>
            {
                public static readonly PerDecoder Instance = new PerDecoder();
                
                protected override void ProcessConfig(c1_Type config, BitArrayInputStream input)
                {
                    switch (input.ReadBits(1))
                    {
                        case 0:
                            config.rrcConnectionReestablishmentRequest
                                = RRCConnectionReestablishmentRequest.PerDecoder.Instance.Decode(input);
                            return;
                        case 1:
                            config.rrcConnectionRequest = RRCConnectionRequest.PerDecoder.Instance.Decode(input);
                            return;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        [Serializable]
        public class messageClassExtension_Type : TraceConfig
        {
            public class PerDecoder : DecoderBase<messageClassExtension_Type>
            {
                public static readonly PerDecoder Instance = new PerDecoder();
                
                protected override void ProcessConfig(messageClassExtension_Type config, BitArrayInputStream input)
                {
                }
            }
        }

        public class PerDecoder : DecoderBase<UL_CCCH_MessageType>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(UL_CCCH_MessageType config, BitArrayInputStream input)
            {
                switch (input.ReadBits(1))
                {
                    case 0:
                        config.c1 = c1_Type.PerDecoder.Instance.Decode(input);
                        return;

                    case 1:
                        config.messageClassExtension = messageClassExtension_Type.PerDecoder.Instance.Decode(input);
                        return;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }

}
