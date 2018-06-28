using Lte.Domain.Common;
using System;
using Lte.Domain.Common.Types;
using TraceParser.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class DL_DCCH_Message : TraceConfig
    {
        public DL_DCCH_MessageType message { get; set; }

        public class PerDecoder : DecoderBase<DL_DCCH_Message>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(DL_DCCH_Message config, BitArrayInputStream input)
            {
                InitDefaults();
                config.message = DL_DCCH_MessageType.PerDecoder.Instance.Decode(input);
            }
        }
    }

    [Serializable]
    public class DL_DCCH_MessageType : TraceConfig
    {
        public c1_Type c1 { get; set; }

        public messageClassExtension_Type messageClassExtension { get; set; }

        [Serializable]
        public class c1_Type : TraceConfig
        {
            public CounterCheck counterCheck { get; set; }

            public CSFBParametersResponseCDMA2000 csfbParametersResponseCDMA2000 { get; set; }

            public DLInformationTransfer dlInformationTransfer { get; set; }

            public HandoverFromEUTRAPreparationRequest handoverFromEUTRAPreparationRequest { get; set; }

            public LoggedMeasurementConfiguration_r10 loggedMeasurementConfiguration_r10 { get; set; }

            public MobilityFromEUTRACommand mobilityFromEUTRACommand { get; set; }

            public RNReconfiguration_r10 rnReconfiguration_r10 { get; set; }

            public RRCConnectionReconfiguration rrcConnectionReconfiguration { get; set; }

            public RRCConnectionRelease rrcConnectionRelease { get; set; }

            public SecurityModeCommand securityModeCommand { get; set; }

            public object spare1 { get; set; }

            public object spare2 { get; set; }

            public object spare3 { get; set; }

            public object spare4 { get; set; }

            public UECapabilityEnquiry ueCapabilityEnquiry { get; set; }

            public UEInformationRequest_r9 ueInformationRequest_r9 { get; set; }

            public class PerDecoder : DecoderBase<c1_Type>
            {
                public static readonly PerDecoder Instance = new PerDecoder();
                
                protected override void ProcessConfig(c1_Type config, BitArrayInputStream input)
                {
                    InitDefaults();
                    switch (input.ReadBits(4))
                    {
                        case 0:
                            config.csfbParametersResponseCDMA2000
                                = CSFBParametersResponseCDMA2000.PerDecoder.Instance.Decode(input);
                            return;
                        case 1:
                            config.dlInformationTransfer = DLInformationTransfer.PerDecoder.Instance.Decode(input);
                            return;
                        case 2:
                            config.handoverFromEUTRAPreparationRequest
                                = HandoverFromEUTRAPreparationRequest.PerDecoder.Instance.Decode(input);
                            return;
                        case 3:
                            config.mobilityFromEUTRACommand = MobilityFromEUTRACommand.PerDecoder.Instance.Decode(input);
                            return;
                        case 4:
                            config.rrcConnectionReconfiguration
                                = RRCConnectionReconfiguration.PerDecoder.Instance.Decode(input);
                            return;
                        case 5:
                            config.rrcConnectionRelease = RRCConnectionRelease.PerDecoder.Instance.Decode(input);
                            return;
                        case 6:
                            config.securityModeCommand = SecurityModeCommand.PerDecoder.Instance.Decode(input);
                            return;
                        case 7:
                            config.ueCapabilityEnquiry = UECapabilityEnquiry.PerDecoder.Instance.Decode(input);
                            return;
                        case 8:
                            config.counterCheck = CounterCheck.PerDecoder.Instance.Decode(input);
                            return;
                        case 9:
                            config.ueInformationRequest_r9 = UEInformationRequest_r9.PerDecoder.Instance.Decode(input);
                            return;
                        case 10:
                            config.loggedMeasurementConfiguration_r10 = LoggedMeasurementConfiguration_r10.PerDecoder.Instance.Decode(input);
                            return;
                        case 11:
                            config.rnReconfiguration_r10 = RNReconfiguration_r10.PerDecoder.Instance.Decode(input);
                            return;
                        case 12:
                        case 13:
                        case 14:
                        case 15:
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

        public class PerDecoder : DecoderBase<DL_DCCH_MessageType>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(DL_DCCH_MessageType config, BitArrayInputStream input)
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

