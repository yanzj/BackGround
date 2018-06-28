using Lte.Domain.Common;
using System;
using System.Collections.Generic;
using Lte.Domain.Common.Types;
using TraceParser.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class CarrierFreqCDMA2000 : TraceConfig
    {
        public long arfcn { get; set; }

        public BandclassCDMA2000 bandClass { get; set; }

        public class PerDecoder : DecoderBase<CarrierFreqCDMA2000>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CarrierFreqCDMA2000 config, BitArrayInputStream input)
            {
                input.ReadBit();
                config.bandClass = (BandclassCDMA2000)input.ReadBits(5);
                config.arfcn = input.ReadBits(11);
            }
        }
    }

    [Serializable]
    public class CarrierFreqEUTRA : TraceConfig
    {
        public long dl_CarrierFreq { get; set; }

        public long? ul_CarrierFreq { get; set; }

        public class PerDecoder : DecoderBase<CarrierFreqEUTRA>
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            protected override void ProcessConfig(CarrierFreqEUTRA config, BitArrayInputStream input)
            {
                BitMaskStream stream = new BitMaskStream(input, 1);
                config.dl_CarrierFreq = input.ReadBits(0x10);
                if (stream.Read())
                {
                    config.ul_CarrierFreq = input.ReadBits(0x10);
                }
            }
        }
    }

    [Serializable]
    public class CarrierFreqEUTRA_v9e0 : TraceConfig
    {
        public long dl_CarrierFreq_v9e0 { get; set; }

        public long? ul_CarrierFreq_v9e0 { get; set; }

        public class PerDecoder : DecoderBase<CarrierFreqEUTRA_v9e0>
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            protected override void ProcessConfig(CarrierFreqEUTRA_v9e0 config, BitArrayInputStream input)
            {
                BitMaskStream stream = new BitMaskStream(input, 1);
                config.dl_CarrierFreq_v9e0 = input.ReadBits(0x12);
                if (stream.Read())
                {
                    config.ul_CarrierFreq_v9e0 = input.ReadBits(0x12);
                }
            }
        }
    }

    [Serializable]
    public class CarrierFreqGERAN : TraceConfig
    {
        public long arfcn { get; set; }

        public BandIndicatorGERAN bandIndicator { get; set; }

        public class PerDecoder : DecoderBase<CarrierFreqGERAN>
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            protected override void ProcessConfig(CarrierFreqGERAN config, BitArrayInputStream input)
            {
                config.arfcn = input.ReadBits(10);
                config.bandIndicator = (BandIndicatorGERAN)input.ReadBits(1);
            }
        }
    }

    [Serializable]
    public class CarrierFreqInfoUTRA_FDD_v8h0 : TraceConfig
    {
        public List<long> multiBandInfoList { get; set; }

        public class PerDecoder : DecoderBase<CarrierFreqInfoUTRA_FDD_v8h0>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CarrierFreqInfoUTRA_FDD_v8h0 config, BitArrayInputStream input)
            {
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    config.multiBandInfoList = new List<long>();
                    int num3 = input.ReadBits(3) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        long item = input.ReadBits(7) + 1;
                        config.multiBandInfoList.Add(item);
                    }
                }
            }
        }
    }

    [Serializable]
    public class CarrierFreqsGERAN : TraceConfig
    {
        public BandIndicatorGERAN bandIndicator { get; set; }

        public followingARFCNs_Type followingARFCNs { get; set; }

        public long startingARFCN { get; set; }

        [Serializable]
        public class followingARFCNs_Type : TraceConfig
        {
            public equallySpacedARFCNs_Type equallySpacedARFCNs { get; set; }

            public List<long> explicitListOfARFCNs { get; set; }

            public string variableBitMapOfARFCNs { get; set; }

            [Serializable]
            public class equallySpacedARFCNs_Type : TraceConfig
            {
                public long arfcn_Spacing { get; set; }

                public long numberOfFollowingARFCNs { get; set; }

                public class PerDecoder : DecoderBase<equallySpacedARFCNs_Type>
                {
                    public static readonly PerDecoder Instance = new PerDecoder();
                    
                    protected override void ProcessConfig(equallySpacedARFCNs_Type config, BitArrayInputStream input)
                    {
                        config.arfcn_Spacing = input.ReadBits(3) + 1;
                        config.numberOfFollowingARFCNs = input.ReadBits(5);
                    }
                }
            }

            public class PerDecoder : DecoderBase<followingARFCNs_Type>
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                protected override void ProcessConfig(followingARFCNs_Type config, BitArrayInputStream input)
                {
                    switch (input.ReadBits(2))
                    {
                        case 0:
                            {
                                config.explicitListOfARFCNs = new List<long>();
                                int num4 = input.ReadBits(5);
                                for (int i = 0; i < num4; i++)
                                {
                                    long item = input.ReadBits(10);
                                    config.explicitListOfARFCNs.Add(item);
                                }
                                return;
                            }
                        case 1:
                            config.equallySpacedARFCNs = equallySpacedARFCNs_Type.PerDecoder.Instance.Decode(input);
                            return;

                        case 2:
                            int num = input.ReadBits(4);
                            config.variableBitMapOfARFCNs = input.readOctetString(num + 1);
                            return;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        public class PerDecoder : DecoderBase<CarrierFreqsGERAN>
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            protected override void ProcessConfig(CarrierFreqsGERAN config, BitArrayInputStream input)
            {
                config.startingARFCN = input.ReadBits(10);
                int nBits = 1;
                config.bandIndicator = (BandIndicatorGERAN)input.ReadBits(nBits);
                config.followingARFCNs = followingARFCNs_Type.PerDecoder.Instance.Decode(input);
            }
        }
    }

    [Serializable]
    public class CarrierFreqsInfoGERAN : TraceConfig
    {
        public CarrierFreqsGERAN carrierFreqs { get; set; }

        public commonInfo_Type commonInfo { get; set; }

        [Serializable]
        public class commonInfo_Type : TraceConfig
        {
            public long? cellReselectionPriority { get; set; }

            public string ncc_Permitted { get; set; }

            public long? p_MaxGERAN { get; set; }

            public long q_RxLevMin { get; set; }

            public long threshX_High { get; set; }

            public long threshX_Low { get; set; }

            public class PerDecoder : DecoderBase<commonInfo_Type>
            {
                public static readonly PerDecoder Instance = new PerDecoder();
                
                protected override void ProcessConfig(commonInfo_Type config, BitArrayInputStream input)
                {
                    BitMaskStream stream = new BitMaskStream(input, 2);
                    if (stream.Read())
                    {
                        config.cellReselectionPriority = input.ReadBits(3);
                    }
                    config.ncc_Permitted = input.ReadBitString(8);
                    config.q_RxLevMin = input.ReadBits(6);
                    if (stream.Read())
                    {
                        config.p_MaxGERAN = input.ReadBits(6);
                    }
                    config.threshX_High = input.ReadBits(5);
                    config.threshX_Low = input.ReadBits(5);
                }
            }
        }

        public class PerDecoder : DecoderBase<CarrierFreqsInfoGERAN>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CarrierFreqsInfoGERAN config, BitArrayInputStream input)
            {
                input.ReadBit();
                config.carrierFreqs = CarrierFreqsGERAN.PerDecoder.Instance.Decode(input);
                config.commonInfo = commonInfo_Type.PerDecoder.Instance.Decode(input);
            }
        }
    }

    [Serializable]
    public class CarrierFreqUTRA_FDD : TraceConfig
    {
        public long carrierFreq { get; set; }

        public long? cellReselectionPriority { get; set; }

        public long p_MaxUTRA { get; set; }

        public long q_QualMin { get; set; }

        public long q_RxLevMin { get; set; }

        public long threshX_High { get; set; }

        public long threshX_Low { get; set; }

        public threshX_Q_r9_Type threshX_Q_r9 { get; set; }

        public class PerDecoder : DecoderBase<CarrierFreqUTRA_FDD>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CarrierFreqUTRA_FDD config, BitArrayInputStream input)
            {
                bool flag = input.ReadBit() != 0;
                BitMaskStream stream = new BitMaskStream(input, 1);
                config.carrierFreq = input.ReadBits(14);
                if (stream.Read())
                {
                    config.cellReselectionPriority = input.ReadBits(3);
                }
                config.threshX_High = input.ReadBits(5);
                config.threshX_Low = input.ReadBits(5);
                config.q_RxLevMin = input.ReadBits(6) + -60;
                config.p_MaxUTRA = input.ReadBits(7) + -50;
                config.q_QualMin = input.ReadBits(5) + -24;
                if (flag)
                {
                    BitMaskStream stream2 = new BitMaskStream(input, 1);
                    if (stream2.Read())
                    {
                        config.threshX_Q_r9 = threshX_Q_r9_Type.PerDecoder.Instance.Decode(input);
                    }
                }
            }
        }

        [Serializable]
        public class threshX_Q_r9_Type : TraceConfig
        {
            public long threshX_HighQ_r9 { get; set; }

            public long threshX_LowQ_r9 { get; set; }

            public class PerDecoder : DecoderBase<threshX_Q_r9_Type>
            {
                public static readonly PerDecoder Instance = new PerDecoder();
                
                protected override void ProcessConfig(threshX_Q_r9_Type config, BitArrayInputStream input)
                {
                    config.threshX_HighQ_r9 = input.ReadBits(5);
                    config.threshX_LowQ_r9 = input.ReadBits(5);
                }
            }
        }
    }

    [Serializable]
    public class CarrierFreqUTRA_TDD : TraceConfig
    {
        public long carrierFreq { get; set; }

        public long? cellReselectionPriority { get; set; }

        public long p_MaxUTRA { get; set; }

        public long q_RxLevMin { get; set; }

        public long threshX_High { get; set; }

        public long threshX_Low { get; set; }

        public class PerDecoder : DecoderBase<CarrierFreqUTRA_TDD>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            

            protected override void ProcessConfig(CarrierFreqUTRA_TDD config, BitArrayInputStream input)
            {
                BitMaskStream stream = (input.ReadBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                config.carrierFreq = input.ReadBits(14);
                if (stream.Read())
                {
                    config.cellReselectionPriority = input.ReadBits(3);
                }
                config.threshX_High = input.ReadBits(5);
                config.threshX_Low = input.ReadBits(5);
                config.q_RxLevMin = input.ReadBits(6) + -60;
                config.p_MaxUTRA = input.ReadBits(7) + -50;
            }
        }
    }

    [Serializable]
    public class AffectedCarrierFreq_r11 : TraceConfig
    {
        public long carrierFreq_r11 { get; set; }

        public interferenceDirection_r11_Enum interferenceDirection_r11 { get; set; }

        public enum interferenceDirection_r11_Enum
        {
            eutra,
            other,
            both,
            spare
        }

        public class PerDecoder : DecoderBase<AffectedCarrierFreq_r11>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(AffectedCarrierFreq_r11 config, BitArrayInputStream input)
            {
                config.carrierFreq_r11 = input.ReadBits(5) + 1;
                config.interferenceDirection_r11 = (interferenceDirection_r11_Enum)input.ReadBits(2);
            }
        }
    }

    [Serializable]
    public class CarrierBandwidthEUTRA : TraceConfig
    {
        public dl_Bandwidth_Enum dl_Bandwidth { get; set; }

        public ul_Bandwidth_Enum? ul_Bandwidth { get; set; }

        public enum dl_Bandwidth_Enum
        {
            n6,
            n15,
            n25,
            n50,
            n75,
            n100,
            spare10,
            spare9,
            spare8,
            spare7,
            spare6,
            spare5,
            spare4,
            spare3,
            spare2,
            spare1
        }

        public class PerDecoder : DecoderBase<CarrierBandwidthEUTRA>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CarrierBandwidthEUTRA config, BitArrayInputStream input)
            {
                BitMaskStream stream = new BitMaskStream(input, 1);
                config.dl_Bandwidth = (dl_Bandwidth_Enum)input.ReadBits(4);
                if (stream.Read())
                {
                    config.ul_Bandwidth = (ul_Bandwidth_Enum)input.ReadBits(4);
                }
            }
        }

        public enum ul_Bandwidth_Enum
        {
            n6,
            n15,
            n25,
            n50,
            n75,
            n100,
            spare10,
            spare9,
            spare8,
            spare7,
            spare6,
            spare5,
            spare4,
            spare3,
            spare2,
            spare1
        }
    }

}
