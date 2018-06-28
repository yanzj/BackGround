using Lte.Domain.Common;
using System;
using Lte.Domain.Common.Types;
using TraceParser.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class BCCH_Config : TraceConfig
    {
        public modificationPeriodCoeff_Enum modificationPeriodCoeff { get; set; }

        public enum modificationPeriodCoeff_Enum
        {
            n2,
            n4,
            n8,
            n16
        }

        public class PerDecoder : DecoderBase<BCCH_Config>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(BCCH_Config config, BitArrayInputStream input)
            {
                InitDefaults();
                config.modificationPeriodCoeff = (modificationPeriodCoeff_Enum)input.ReadBits(2);
            }
        }
    }

    [Serializable]
    public class BCCH_BCH_Message : TraceConfig
    {
        public MasterInformationBlock message { get; set; }

        public class PerDecoder : DecoderBase<BCCH_BCH_Message>
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            protected override void ProcessConfig(BCCH_BCH_Message config, BitArrayInputStream input)
            {
                InitDefaults();
                config.message = MasterInformationBlock.PerDecoder.Instance.Decode(input);
            }
        }
    }

    [Serializable]
    public class BCCH_DL_SCH_Message : TraceConfig
    {
        public BCCH_DL_SCH_MessageType message { get; set; }

        public class PerDecoder : DecoderBase<BCCH_DL_SCH_Message>
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            protected override void ProcessConfig(BCCH_DL_SCH_Message config, BitArrayInputStream input)
            {
                config.message = BCCH_DL_SCH_MessageType.PerDecoder.Instance.Decode(input);
            }
        }
    }

    [Serializable]
    public class BCCH_DL_SCH_MessageType : TraceConfig
    {
        public c1_Type c1 { get; set; }

        public messageClassExtension_Type messageClassExtension { get; set; }

        [Serializable]
        public class c1_Type : TraceConfig
        {
            public SystemInformation systemInformation { get; set; }

            public SystemInformationBlockType1 systemInformationBlockType1 { get; set; }

            public class PerDecoder : DecoderBase<c1_Type>
            {
                public static readonly PerDecoder Instance = new PerDecoder();
                
                protected override void ProcessConfig(c1_Type config, BitArrayInputStream input)
                {
                    switch (input.ReadBits(1))
                    {
                        case 0:
                            config.systemInformation = SystemInformation.PerDecoder.Decode(input);
                            return;
                        case 1:
                            config.systemInformationBlockType1 = SystemInformationBlockType1.PerDecoder.Instance.Decode(input);
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

        public class PerDecoder : DecoderBase<BCCH_DL_SCH_MessageType>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(BCCH_DL_SCH_MessageType config, BitArrayInputStream input)
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