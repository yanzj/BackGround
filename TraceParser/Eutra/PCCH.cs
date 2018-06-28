using Lte.Domain.Common;
using System;
using Lte.Domain.Common.Types;
using TraceParser.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class PCCH_Config
    {
        public void InitDefaults()
        {
        }

        public defaultPagingCycle_Enum defaultPagingCycle { get; set; }

        public nB_Enum nB { get; set; }

        public enum defaultPagingCycle_Enum
        {
            rf32,
            rf64,
            rf128,
            rf256
        }

        public enum nB_Enum
        {
            fourT,
            twoT,
            oneT,
            halfT,
            quarterT,
            oneEighthT,
            oneSixteenthT,
            oneThirtySecondT
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PCCH_Config Decode(BitArrayInputStream input)
            {
                var config = new PCCH_Config();
                config.InitDefaults();
                var nBits = 2;
                config.defaultPagingCycle = (defaultPagingCycle_Enum)input.ReadBits(nBits);
                nBits = 3;
                config.nB = (nB_Enum)input.ReadBits(nBits);
                return config;
            }
        }
    }

    [Serializable]
    public class PCCH_Message : TraceConfig
    {
        public PCCH_MessageType message { get; set; }

        public class PerDecoder : DecoderBase<PCCH_Message>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(PCCH_Message config, BitArrayInputStream input)
            {
                InitDefaults();
                config.message = PCCH_MessageType.PerDecoder.Instance.Decode(input);
            }
        }
    }

    [Serializable]
    public class PCCH_MessageType
    {
        public void InitDefaults()
        {
        }

        public c1_Type c1 { get; set; }

        public messageClassExtension_Type messageClassExtension { get; set; }

        [Serializable]
        public class c1_Type
        {
            public void InitDefaults()
            {
            }

            public Paging paging { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public c1_Type Decode(BitArrayInputStream input)
                {
                    var type = new c1_Type();
                    type.InitDefaults();
                    if (input.ReadBits(1) != 0)
                    {
                        throw new Exception(GetType().Name + ":NoChoice had been choose");
                    }
                    type.paging = Paging.PerDecoder.Instance.Decode(input);
                    return type;
                }
            }
        }

        [Serializable]
        public class messageClassExtension_Type
        {
            public void InitDefaults()
            {
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public messageClassExtension_Type Decode(BitArrayInputStream input)
                {
                    var type = new messageClassExtension_Type();
                    type.InitDefaults();
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PCCH_MessageType Decode(BitArrayInputStream input)
            {
                var type = new PCCH_MessageType();
                type.InitDefaults();
                switch (input.ReadBits(1))
                {
                    case 0:
                        type.c1 = c1_Type.PerDecoder.Instance.Decode(input);
                        return type;

                    case 1:
                        type.messageClassExtension = messageClassExtension_Type.PerDecoder.Instance.Decode(input);
                        return type;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }

}
