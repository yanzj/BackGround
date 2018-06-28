using Lte.Domain.Common;
using System;
using Lte.Domain.Common.Types;
using TraceParser.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class AntennaInfoCommon : TraceConfig
    {
        public antennaPortsCount_Enum antennaPortsCount { get; set; }

        public enum antennaPortsCount_Enum
        {
            an1,
            an2,
            an4,
            spare1
        }

        public class PerDecoder : DecoderBase<AntennaInfoCommon>
        {
            public static PerDecoder Instance => new PerDecoder();

            protected override void ProcessConfig(AntennaInfoCommon config, BitArrayInputStream input)
            {
                config.antennaPortsCount = (antennaPortsCount_Enum)input.ReadBits(2);
            }
        }
    }

    [Serializable]
    public class AntennaInfoDedicated : TraceConfig
    {
        public codebookSubsetRestriction_Type codebookSubsetRestriction { get; set; }

        public transmissionMode_Enum transmissionMode { get; set; }

        public ue_TransmitAntennaSelection_Type ue_TransmitAntennaSelection { get; set; }

        [Serializable]
        public class codebookSubsetRestriction_Type : TraceConfig
        {
            [CodeBit(Position = 0, BitToBeRead = 2)]
            public string n2TxAntenna_tm3 { get; set; }

            [CodeBit(Position = 1, BitToBeRead = 4)]
            public string n2TxAntenna_tm4 { get; set; }

            [CodeBit(Position = 2, BitToBeRead = 6)]
            public string n2TxAntenna_tm5 { get; set; }

            [CodeBit(Position = 3, BitToBeRead = 0x40)]
            public string n2TxAntenna_tm6 { get; set; }

            [CodeBit(Position = 4, BitToBeRead = 4)]
            public string n4TxAntenna_tm3 { get; set; }

            [CodeBit(Position = 5, BitToBeRead = 0x10)]
            public string n4TxAntenna_tm4 { get; set; }

            [CodeBit(Position = 6, BitToBeRead = 4)]
            public string n4TxAntenna_tm5 { get; set; }

            [CodeBit(Position = 7, BitToBeRead = 0x10)]
            public string n4TxAntenna_tm6 { get; set; }

            public class PerDecoder : DecoderBase<codebookSubsetRestriction_Type>
            {
                public static PerDecoder Instance => new PerDecoder();
                
                protected override void ProcessConfig(codebookSubsetRestriction_Type config, BitArrayInputStream input)
                {
                    config.ReadCodeBits(input, input.ReadBits(3));
                }
            }
        }

        public class PerDecoder : DecoderBase<AntennaInfoDedicated>
        {
            public static PerDecoder Instance => new PerDecoder();
            
            protected override void ProcessConfig(AntennaInfoDedicated config, BitArrayInputStream input)
            {
                var stream = new BitMaskStream(input, 1);
                config.transmissionMode = (transmissionMode_Enum)input.ReadBits(3);
                if (stream.Read())
                {
                    config.codebookSubsetRestriction = codebookSubsetRestriction_Type.PerDecoder.Instance.Decode(input);
                }
                config.ue_TransmitAntennaSelection = ue_TransmitAntennaSelection_Type.PerDecoder.Instance.Decode(input);
            }
        }

        public enum transmissionMode_Enum
        {
            tm1,
            tm2,
            tm3,
            tm4,
            tm5,
            tm6,
            tm7,
            tm8_v920
        }

        [Serializable]
        public class ue_TransmitAntennaSelection_Type : TraceConfig
        {
            public object release { get; set; }

            public setup_Enum setup { get; set; }

            public class PerDecoder : DecoderBase<ue_TransmitAntennaSelection_Type>
            {
                public static PerDecoder Instance => new PerDecoder();
                
                protected override void ProcessConfig(ue_TransmitAntennaSelection_Type config, BitArrayInputStream input)
                {
                    switch (input.ReadBits(1))
                    {
                        case 0:
                            config.release = new object();
                            return;
                        case 1:
                            config.setup = (setup_Enum)input.ReadBits(1);
                            return;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }

            public enum setup_Enum
            {
                closedLoop,
                openLoop
            }
        }
    }

    [Serializable]
    public class AntennaInfoDedicated_r10 : TraceConfig
    {
        public string codebookSubsetRestriction_r10 { get; set; }

        public transmissionMode_r10_Enum transmissionMode_r10 { get; set; }

        public ue_TransmitAntennaSelection_Type ue_TransmitAntennaSelection { get; set; }

        public class PerDecoder : DecoderBase<AntennaInfoDedicated_r10>
        {
            public static PerDecoder Instance => new PerDecoder();
            
            protected override void ProcessConfig(AntennaInfoDedicated_r10 config, BitArrayInputStream input)
            {
                var stream = new BitMaskStream(input, 1);
                config.transmissionMode_r10 = (transmissionMode_r10_Enum)input.ReadBits(4);
                if (stream.Read())
                {
                    var nBits = input.ReadBits(8);
                    config.codebookSubsetRestriction_r10 = input.ReadBitString(nBits);
                }
                config.ue_TransmitAntennaSelection = ue_TransmitAntennaSelection_Type.PerDecoder.Instance.Decode(input);
            }
        }

        public enum transmissionMode_r10_Enum
        {
            tm1,
            tm2,
            tm3,
            tm4,
            tm5,
            tm6,
            tm7,
            tm8_v920,
            tm9_v1020,
            tm10_v1130,
            spare6,
            spare5,
            spare4,
            spare3,
            spare2,
            spare1
        }

        [Serializable]
        public class ue_TransmitAntennaSelection_Type : TraceConfig
        {
            public object release { get; set; }

            public setup_Enum setup { get; set; }

            public class PerDecoder : DecoderBase<ue_TransmitAntennaSelection_Type>
            {
                public static PerDecoder Instance => new PerDecoder();
                
                protected override void ProcessConfig(ue_TransmitAntennaSelection_Type config, BitArrayInputStream input)
                {
                    switch (input.ReadBits(1))
                    {
                        case 0:
                            return;
                        case 1:
                            config.setup = (setup_Enum)input.ReadBits(1);
                            return;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }

            public enum setup_Enum
            {
                closedLoop,
                openLoop
            }
        }
    }

    [Serializable]
    public class AntennaInfoDedicated_v12xx : TraceConfig
    {
        public alternativeCodebookEnabledFor4TX_r12_Enum? alternativeCodebookEnabledFor4TX_r12 { get; set; }

        public enum alternativeCodebookEnabledFor4TX_r12_Enum
        {
            _true
        }

        public class PerDecoder : DecoderBase<AntennaInfoDedicated_v12xx>
        {
            public static PerDecoder Instance => new PerDecoder();
            
            protected override void ProcessConfig(AntennaInfoDedicated_v12xx config, BitArrayInputStream input)
            {
                var stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    config.alternativeCodebookEnabledFor4TX_r12 
                        = (alternativeCodebookEnabledFor4TX_r12_Enum)input.ReadBits(1);
                }
            }
        }
    }

    [Serializable]
    public class AntennaInfoDedicated_v920 : TraceConfig
    {
        public codebookSubsetRestriction_v920_Type codebookSubsetRestriction_v920 { get; set; }

        [Serializable]
        public class codebookSubsetRestriction_v920_Type : TraceConfig
        {
            [CodeBit(Position = 0, BitToBeRead = 6)]
            public string n2TxAntenna_tm8_r9 { get; set; }

            [CodeBit(Position = 1, BitToBeRead = 0x20)]
            public string n4TxAntenna_tm8_r9 { get; set; }

            public class PerDecoder : DecoderBase<codebookSubsetRestriction_v920_Type>
            {
                public static PerDecoder Instance => new PerDecoder();
                
                protected override void ProcessConfig(codebookSubsetRestriction_v920_Type config, BitArrayInputStream input)
                {
                    config.ReadCodeBits(input, input.ReadBits(1));
                }
            }
        }

        public class PerDecoder : DecoderBase<AntennaInfoDedicated_v920>
        {
            public static PerDecoder Instance => new PerDecoder();
            
            protected override void ProcessConfig(AntennaInfoDedicated_v920 config, BitArrayInputStream input)
            {
                var stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    config.codebookSubsetRestriction_v920 
                        = codebookSubsetRestriction_v920_Type.PerDecoder.Instance.Decode(input);
                }
            }
        }
    }

    [Serializable]
    public class AntennaInfoUL_r10 : TraceConfig
    {
        public fourAntennaPortActivated_r10_Enum? fourAntennaPortActivated_r10 { get; set; }

        public transmissionModeUL_r10_Enum? transmissionModeUL_r10 { get; set; }

        public enum fourAntennaPortActivated_r10_Enum
        {
            setup
        }

        public class PerDecoder : DecoderBase<AntennaInfoUL_r10>
        {
            public static PerDecoder Instance => new PerDecoder();
            
            protected override void ProcessConfig(AntennaInfoUL_r10 config, BitArrayInputStream input)
            {
                var stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    config.transmissionModeUL_r10 = (transmissionModeUL_r10_Enum)input.ReadBits(3);
                }
                if (stream.Read())
                {
                    config.fourAntennaPortActivated_r10 = (fourAntennaPortActivated_r10_Enum)input.ReadBits(1);
                }
            }
        }

        public enum transmissionModeUL_r10_Enum
        {
            tm1,
            tm2,
            spare6,
            spare5,
            spare4,
            spare3,
            spare2,
            spare1
        }
    }

}
