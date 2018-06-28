using Lte.Domain.Common;
using System;
using Lte.Domain.Common.Types;
using TraceParser.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class DL_CCCH_Message : TraceConfig
    {
        public DL_CCCH_MessageType message { get; set; }

        public class PerDecoder : DecoderBase<DL_CCCH_Message>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(DL_CCCH_Message config, BitArrayInputStream input)
            {
                InitDefaults();
                config.message = DL_CCCH_MessageType.PerDecoder.Instance.Decode(input);
            }
        }
    }

    [Serializable]
    public class DL_CCCH_MessageType : TraceConfig
    {
        public c1_Type c1 { get; set; }

        public messageClassExtension_Type messageClassExtension { get; set; }

        [Serializable]
        public class c1_Type : TraceConfig
        {
            public RRCConnectionReestablishment rrcConnectionReestablishment { get; set; }

            public RRCConnectionReestablishmentReject rrcConnectionReestablishmentReject { get; set; }

            public RRCConnectionReject rrcConnectionReject { get; set; }

            public RRCConnectionSetup rrcConnectionSetup { get; set; }

            public class PerDecoder : DecoderBase<c1_Type>
            {
                public static readonly PerDecoder Instance = new PerDecoder();
                
                protected override void ProcessConfig(c1_Type config, BitArrayInputStream input)
                {
                    InitDefaults();
                    switch (input.ReadBits(2))
                    {
                        case 0:
                            config.rrcConnectionReestablishment
                                = RRCConnectionReestablishment.PerDecoder.Instance.Decode(input);
                            return;
                        case 1:
                            config.rrcConnectionReestablishmentReject
                                = RRCConnectionReestablishmentReject.PerDecoder.Instance.Decode(input);
                            return;
                        case 2:
                            config.rrcConnectionReject = RRCConnectionReject.PerDecoder.Instance.Decode(input);
                            return;
                        case 3:
                            config.rrcConnectionSetup = RRCConnectionSetup.PerDecoder.Instance.Decode(input);
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
                    InitDefaults();
                }
            }
        }

        public class PerDecoder : DecoderBase<DL_CCCH_MessageType>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(DL_CCCH_MessageType config, BitArrayInputStream input)
            {
                InitDefaults();
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
