using Lte.Domain.Common;
using System;
using System.Collections.Generic;
using Lte.Domain.Common.Types;
using TraceParser.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class AS_Config : TraceConfig
    {
        public AntennaInfoCommon antennaInfoCommon { get; set; }

        public long sourceDl_CarrierFreq { get; set; }

        public MasterInformationBlock sourceMasterInformationBlock { get; set; }

        public MeasConfig sourceMeasConfig { get; set; }

        public OtherConfig_r9 sourceOtherConfig_r9 { get; set; }

        public RadioResourceConfigDedicated sourceRadioResourceConfig { get; set; }

        public List<SCellToAddMod_r10> sourceSCellConfigList_r10 { get; set; }

        public SecurityAlgorithmConfig sourceSecurityAlgorithmConfig { get; set; }

        public SystemInformationBlockType1 sourceSystemInformationBlockType1 { get; set; }

        public string sourceSystemInformationBlockType1Ext { get; set; }

        public SystemInformationBlockType2 sourceSystemInformationBlockType2 { get; set; }

        public string sourceUE_Identity { get; set; }

        public class PerDecoder : DecoderBase<AS_Config>
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            protected override void ProcessConfig(AS_Config config, BitArrayInputStream input)
            {
                var flag = input.ReadBit() != 0;
                config.sourceMeasConfig = MeasConfig.PerDecoder.Instance.Decode(input);
                config.sourceRadioResourceConfig = RadioResourceConfigDedicated.PerDecoder.Instance.Decode(input);
                config.sourceSecurityAlgorithmConfig = SecurityAlgorithmConfig.PerDecoder.Instance.Decode(input);
                config.sourceUE_Identity = input.ReadBitString(0x10);
                config.sourceMasterInformationBlock = MasterInformationBlock.PerDecoder.Instance.Decode(input);
                config.sourceSystemInformationBlockType1 = SystemInformationBlockType1.PerDecoder.Instance.Decode(input);
                config.sourceSystemInformationBlockType2 = SystemInformationBlockType2.PerDecoder.Instance.Decode(input);
                config.antennaInfoCommon = AntennaInfoCommon.PerDecoder.Instance.Decode(input);
                config.sourceDl_CarrierFreq = input.ReadBits(0x10);
                if (!flag) return;
                var stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    var nBits = input.ReadBits(8);
                    config.sourceSystemInformationBlockType1Ext = input.readOctetString(nBits);
                }
                config.sourceOtherConfig_r9 = OtherConfig_r9.PerDecoder.Instance.Decode(input);

                stream = new BitMaskStream(input, 1);
                if (!stream.Read()) return;
                config.sourceSCellConfigList_r10 = new List<SCellToAddMod_r10>();
                var num3 = input.ReadBits(2) + 1;
                for (var i = 0; i < num3; i++)
                {
                    var item = SCellToAddMod_r10.PerDecoder.Instance.Decode(input);
                    config.sourceSCellConfigList_r10.Add(item);
                }
            }
        }
    }

    [Serializable]
    public class AS_Config_v9e0 : TraceConfig
    {
        public long sourceDl_CarrierFreq_v9e0 { get; set; }

        public class PerDecoder : DecoderBase<AS_Config_v9e0>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(AS_Config_v9e0 config, BitArrayInputStream input)
            {
                config.sourceDl_CarrierFreq_v9e0 = input.ReadBits(0x12) + 0x10000;
            }
        }
    }

    [Serializable]
    public class AS_Context : TraceConfig
    {
        public ReestablishmentInfo reestablishmentInfo { get; set; }

        public class PerDecoder : DecoderBase<AS_Context>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(AS_Context config, BitArrayInputStream input)
            {
                var stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    config.reestablishmentInfo = ReestablishmentInfo.PerDecoder.Instance.Decode(input);
                }
            }
        }
    }

    [Serializable]
    public class AS_Context_v1130 : TraceConfig
    {
        public string idc_Indication_r11 { get; set; }

        public string mbmsInterestIndication_r11 { get; set; }

        public string powerPrefIndication_r11 { get; set; }

        public class PerDecoder : DecoderBase<AS_Context_v1130>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(AS_Context_v1130 config, BitArrayInputStream input)
            {
                input.ReadBit();
                var stream = new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    config.idc_Indication_r11 = input.readOctetString(input.ReadBits(8));
                }
                if (stream.Read())
                {
                    config.mbmsInterestIndication_r11 = input.readOctetString(input.ReadBits(8));
                }
                if (stream.Read())
                {
                    config.powerPrefIndication_r11 = input.readOctetString(input.ReadBits(8));
                }
            }
        }
    }

}
