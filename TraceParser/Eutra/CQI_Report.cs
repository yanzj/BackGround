using Lte.Domain.Common;
using System;
using System.Collections.Generic;
using Lte.Domain.Common.Types;
using TraceParser.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class CQI_ReportConfig : TraceConfig
    {
        public CQI_ReportModeAperiodic? cqi_ReportModeAperiodic { get; set; }

        public CQI_ReportPeriodic cqi_ReportPeriodic { get; set; }

        public long nomPDSCH_RS_EPRE_Offset { get; set; }

        public class PerDecoder : DecoderBase<CQI_ReportConfig>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CQI_ReportConfig config, BitArrayInputStream input)
            {
                var stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    config.cqi_ReportModeAperiodic = (CQI_ReportModeAperiodic)input.ReadBits(3);
                }
                config.nomPDSCH_RS_EPRE_Offset = input.ReadBits(3) - 1;
                if (stream.Read())
                {
                    config.cqi_ReportPeriodic = CQI_ReportPeriodic.PerDecoder.Instance.Decode(input);
                }
            }
        }
    }

    [Serializable]
    public class CQI_ReportConfig_r10 : TraceConfig
    {
        public CQI_ReportAperiodic_r10 cqi_ReportAperiodic_r10 { get; set; }

        public CQI_ReportPeriodic_r10 cqi_ReportPeriodic_r10 { get; set; }

        public csi_SubframePatternConfig_r10_Type csi_SubframePatternConfig_r10 { get; set; }

        public long nomPDSCH_RS_EPRE_Offset { get; set; }

        public pmi_RI_Report_r9_Enum? pmi_RI_Report_r9 { get; set; }

        [Serializable]
        public class csi_SubframePatternConfig_r10_Type : TraceConfig
        {
            public setup_Type setup { get; set; }

            public class PerDecoder : DecoderBase<csi_SubframePatternConfig_r10_Type>
            {
                public static readonly PerDecoder Instance = new PerDecoder();
                
                protected override void ProcessConfig(csi_SubframePatternConfig_r10_Type config, BitArrayInputStream input)
                {
                    switch (input.ReadBits(1))
                    {
                        case 0:
                            return;

                        case 1:
                            config.setup = setup_Type.PerDecoder.Instance.Decode(input);
                            return;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }

            [Serializable]
            public class setup_Type : TraceConfig
            {
                public MeasSubframePattern_r10 csi_MeasSubframeSet1_r10 { get; set; }

                public MeasSubframePattern_r10 csi_MeasSubframeSet2_r10 { get; set; }

                public class PerDecoder : DecoderBase<setup_Type>
                {
                    public static readonly PerDecoder Instance = new PerDecoder();
                    
                    protected override void ProcessConfig(setup_Type config, BitArrayInputStream input)
                    {
                        config.csi_MeasSubframeSet1_r10 = MeasSubframePattern_r10.PerDecoder.Instance.Decode(input);
                        config.csi_MeasSubframeSet2_r10 = MeasSubframePattern_r10.PerDecoder.Instance.Decode(input);
                    }
                }
            }
        }

        public class PerDecoder : DecoderBase<CQI_ReportConfig_r10>
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            protected override void ProcessConfig(CQI_ReportConfig_r10 config, BitArrayInputStream input)
            {
                var stream = new BitMaskStream(input, 4);
                if (stream.Read())
                {
                    config.cqi_ReportAperiodic_r10 = CQI_ReportAperiodic_r10.PerDecoder.Instance.Decode(input);
                }
                config.nomPDSCH_RS_EPRE_Offset = input.ReadBits(3) + -1;
                if (stream.Read())
                {
                    config.cqi_ReportPeriodic_r10 = CQI_ReportPeriodic_r10.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    const int nBits = 1;
                    config.pmi_RI_Report_r9 = (pmi_RI_Report_r9_Enum)input.ReadBits(nBits);
                }
                if (stream.Read())
                {
                    config.csi_SubframePatternConfig_r10 = csi_SubframePatternConfig_r10_Type.PerDecoder.Instance.Decode(input);
                }
            }
        }

        public enum pmi_RI_Report_r9_Enum
        {
            setup
        }
    }

    [Serializable]
    public class CQI_ReportConfig_v1130 : TraceConfig
    {
        public CQI_ReportBoth_r11 cqi_ReportBoth_r11 { get; set; }

        public CQI_ReportPeriodic_v1130 cqi_ReportPeriodic_v1130 { get; set; }

        public class PerDecoder : DecoderBase<CQI_ReportConfig_v1130>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CQI_ReportConfig_v1130 config, BitArrayInputStream input)
            {
                config.cqi_ReportPeriodic_v1130 = CQI_ReportPeriodic_v1130.PerDecoder.Instance.Decode(input);
                config.cqi_ReportBoth_r11 = CQI_ReportBoth_r11.PerDecoder.Instance.Decode(input);
            }
        }
    }

    [Serializable]
    public class CQI_ReportConfig_v920 : TraceConfig
    {
        public cqi_Mask_r9_Enum? cqi_Mask_r9 { get; set; }

        public pmi_RI_Report_r9_Enum? pmi_RI_Report_r9 { get; set; }

        public enum cqi_Mask_r9_Enum
        {
            setup
        }

        public class PerDecoder : DecoderBase<CQI_ReportConfig_v920>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CQI_ReportConfig_v920 config, BitArrayInputStream input)
            {
                var stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    config.cqi_Mask_r9 = (cqi_Mask_r9_Enum)input.ReadBits(1);
                }
                if (stream.Read())
                {
                    config.pmi_RI_Report_r9 = (pmi_RI_Report_r9_Enum)input.ReadBits(1);
                }
            }
        }

        public enum pmi_RI_Report_r9_Enum
        {
            setup
        }
    }

    [Serializable]
    public class CQI_ReportConfigSCell_r10 : TraceConfig
    {
        public CQI_ReportModeAperiodic? cqi_ReportModeAperiodic_r10 { get; set; }

        public CQI_ReportPeriodic_r10 cqi_ReportPeriodicSCell_r10 { get; set; }

        public long nomPDSCH_RS_EPRE_Offset_r10 { get; set; }

        public pmi_RI_Report_r10_Enum? pmi_RI_Report_r10 { get; set; }

        public class PerDecoder : DecoderBase<CQI_ReportConfigSCell_r10>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CQI_ReportConfigSCell_r10 config, BitArrayInputStream input)
            {
                var stream = new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    config.cqi_ReportModeAperiodic_r10 = (CQI_ReportModeAperiodic)input.ReadBits(3);
                }
                config.nomPDSCH_RS_EPRE_Offset_r10 = input.ReadBits(3) + -1;
                if (stream.Read())
                {
                    config.cqi_ReportPeriodicSCell_r10 = CQI_ReportPeriodic_r10.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    config.pmi_RI_Report_r10 = (pmi_RI_Report_r10_Enum)input.ReadBits(1);
                }
            }
        }

        public enum pmi_RI_Report_r10_Enum
        {
            setup
        }
    }

    [Serializable]
    public class CQI_ReportAperiodic_r10 : TraceConfig
    {
        public object release { get; set; }

        public setup_Type setup { get; set; }

        public class PerDecoder : DecoderBase<CQI_ReportAperiodic_r10>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CQI_ReportAperiodic_r10 config, BitArrayInputStream input)
            {
                switch (input.ReadBits(1))
                {
                    case 0:
                        return;

                    case 1:
                        config.setup = setup_Type.PerDecoder.Instance.Decode(input);
                        return;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }

        [Serializable]
        public class setup_Type : TraceConfig
        {
            public aperiodicCSI_Trigger_r10_Type aperiodicCSI_Trigger_r10 { get; set; }

            public CQI_ReportModeAperiodic cqi_ReportModeAperiodic_r10 { get; set; }

            [Serializable]
            public class aperiodicCSI_Trigger_r10_Type : TraceConfig
            {
                public string trigger1_r10 { get; set; }

                public string trigger2_r10 { get; set; }

                public class PerDecoder : DecoderBase<aperiodicCSI_Trigger_r10_Type>
                {
                    public static readonly PerDecoder Instance = new PerDecoder();
                    
                    protected override void ProcessConfig(aperiodicCSI_Trigger_r10_Type config, BitArrayInputStream input)
                    {
                        config.trigger1_r10 = input.ReadBitString(8);
                        config.trigger2_r10 = input.ReadBitString(8);
                    }
                }
            }

            public class PerDecoder : DecoderBase<setup_Type>
            {
                public static readonly PerDecoder Instance = new PerDecoder();
                
                protected override void ProcessConfig(setup_Type config, BitArrayInputStream input)
                {
                    var stream = new BitMaskStream(input, 1);
                    const int nBits = 3;
                    config.cqi_ReportModeAperiodic_r10 = (CQI_ReportModeAperiodic)input.ReadBits(nBits);
                    if (stream.Read())
                    {
                        config.aperiodicCSI_Trigger_r10 = aperiodicCSI_Trigger_r10_Type.PerDecoder.Instance.Decode(input);
                    }
                }
            }
        }
    }

    [Serializable]
    public class CQI_ReportAperiodicProc_r11 : TraceConfig
    {
        public CQI_ReportModeAperiodic cqi_ReportModeAperiodic_r11 { get; set; }

        public bool trigger01_r11 { get; set; }

        public bool trigger10_r11 { get; set; }

        public bool trigger11_r11 { get; set; }

        public class PerDecoder : DecoderBase<CQI_ReportAperiodicProc_r11>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CQI_ReportAperiodicProc_r11 config, BitArrayInputStream input)
            {
                config.cqi_ReportModeAperiodic_r11 = (CQI_ReportModeAperiodic)input.ReadBits(3);
                config.trigger01_r11 = input.ReadBit() == 1;
                config.trigger10_r11 = input.ReadBit() == 1;
                config.trigger11_r11 = input.ReadBit() == 1;
            }
        }
    }

    [Serializable]
    public class CQI_ReportBoth_r11 : TraceConfig
    {
        public List<CSI_IM_Config_r11> csi_IM_ConfigToAddModList_r11 { get; set; }

        public List<long> csi_IM_ConfigToReleaseList_r11 { get; set; }

        public List<CSI_Process_r11> csi_ProcessToAddModList_r11 { get; set; }

        public List<long> csi_ProcessToReleaseList_r11 { get; set; }

        public class PerDecoder : DecoderBase<CQI_ReportBoth_r11>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CQI_ReportBoth_r11 config, BitArrayInputStream input)
            {
                var stream = new BitMaskStream(input, 4);
                if (stream.Read())
                {
                    config.csi_IM_ConfigToReleaseList_r11 = new List<long>();
                    for (var i = 0; i < input.ReadBits(2) + 1; i++)
                    {
                        long item = input.ReadBits(2) + 1;
                        config.csi_IM_ConfigToReleaseList_r11.Add(item);
                    }
                }
                if (stream.Read())
                {
                    config.csi_IM_ConfigToAddModList_r11 = new List<CSI_IM_Config_r11>();
                    for (var j = 0; j < input.ReadBits(1) + 1; j++)
                    {
                        config.csi_IM_ConfigToAddModList_r11.Add(CSI_IM_Config_r11.PerDecoder.Instance.Decode(input));
                    }
                }
                if (stream.Read())
                {
                    config.csi_ProcessToReleaseList_r11 = new List<long>();
                    for (var k = 0; k < input.ReadBits(2) + 1; k++)
                    {
                        config.csi_ProcessToReleaseList_r11.Add(input.ReadBits(2) + 1);
                    }
                }
                if (stream.Read())
                {
                    config.csi_ProcessToAddModList_r11 = new List<CSI_Process_r11>();
                    var num11 = input.ReadBits(2) + 1;
                    for (var m = 0; m < num11; m++)
                    {
                        config.csi_ProcessToAddModList_r11.Add(CSI_Process_r11.PerDecoder.Instance.Decode(input));
                    }
                }
            }
        }
    }

    [Serializable]
    public class CQI_ReportBothProc_r11 : TraceConfig
    {
        public pmi_RI_Report_r11_Enum? pmi_RI_Report_r11 { get; set; }

        public long? ri_Ref_CSI_ProcessId_r11 { get; set; }

        public class PerDecoder : DecoderBase<CQI_ReportBothProc_r11>
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            protected override void ProcessConfig(CQI_ReportBothProc_r11 config, BitArrayInputStream input)
            {
                var stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    config.ri_Ref_CSI_ProcessId_r11 = input.ReadBits(2) + 1;
                }
                if (stream.Read())
                {
                    var nBits = 1;
                    config.pmi_RI_Report_r11 = (pmi_RI_Report_r11_Enum)input.ReadBits(nBits);
                }
            }
        }

        public enum pmi_RI_Report_r11_Enum
        {
            setup
        }
    }

    [Serializable]
    public class CQI_ReportPeriodic : TraceConfig
    {
        public object release { get; set; }

        public setup_Type setup { get; set; }

        public class PerDecoder : DecoderBase<CQI_ReportPeriodic>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CQI_ReportPeriodic config, BitArrayInputStream input)
            {
                switch (input.ReadBits(1))
                {
                    case 0:
                        return;
                    case 1:
                        config.setup = setup_Type.PerDecoder.Instance.Decode(input);
                        return;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }

        [Serializable]
        public class setup_Type : TraceConfig
        {
            public cqi_FormatIndicatorPeriodic_Type cqi_FormatIndicatorPeriodic { get; set; }

            public long cqi_pmi_ConfigIndex { get; set; }

            public long cqi_PUCCH_ResourceIndex { get; set; }

            public long? ri_ConfigIndex { get; set; }

            public bool simultaneousAckNackAndCQI { get; set; }

            [Serializable]
            public class cqi_FormatIndicatorPeriodic_Type : TraceConfig
            {
                public subbandCQI_Type subbandCQI { get; set; }

                public object widebandCQI { get; set; }

                public class PerDecoder : DecoderBase<cqi_FormatIndicatorPeriodic_Type>
                {
                    public static readonly PerDecoder Instance = new PerDecoder();
                    
                    protected override void ProcessConfig(cqi_FormatIndicatorPeriodic_Type config, BitArrayInputStream input)
                    {
                        switch (input.ReadBits(1))
                        {
                            case 0:
                                return;
                            case 1:
                                config.subbandCQI = subbandCQI_Type.PerDecoder.Instance.Decode(input);
                                return;
                        }
                        throw new Exception(GetType().Name + ":NoChoice had been choose");
                    }
                }

                [Serializable]
                public class subbandCQI_Type : TraceConfig
                {
                    public long k { get; set; }

                    public class PerDecoder : DecoderBase<subbandCQI_Type>
                    {
                        public static readonly PerDecoder Instance = new PerDecoder();
                        
                        protected override void ProcessConfig(subbandCQI_Type config, BitArrayInputStream input)
                        {
                            config.k = input.ReadBits(2) + 1;
                        }
                    }
                }
            }

            public class PerDecoder : DecoderBase<setup_Type>
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                protected override void ProcessConfig(setup_Type config, BitArrayInputStream input)
                {
                    var stream = new BitMaskStream(input, 1);
                    config.cqi_PUCCH_ResourceIndex = input.ReadBits(11);
                    config.cqi_pmi_ConfigIndex = input.ReadBits(10);
                    config.cqi_FormatIndicatorPeriodic = cqi_FormatIndicatorPeriodic_Type.PerDecoder.Instance.Decode(input);
                    if (stream.Read())
                    {
                        config.ri_ConfigIndex = input.ReadBits(10);
                    }
                    config.simultaneousAckNackAndCQI = input.ReadBit() == 1;
                }
            }
        }
    }

    [Serializable]
    public class CQI_ReportPeriodic_r10 : TraceConfig
    {
        public object release { get; set; }

        public setup_Type setup { get; set; }

        public class PerDecoder : DecoderBase<CQI_ReportPeriodic_r10>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CQI_ReportPeriodic_r10 config, BitArrayInputStream input)
            {
                switch (input.ReadBits(1))
                {
                    case 0:
                        return;

                    case 1:
                        config.setup = setup_Type.PerDecoder.Instance.Decode(input);
                        return;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }

        [Serializable]
        public class setup_Type : TraceConfig
        {
            public cqi_FormatIndicatorPeriodic_r10_Type cqi_FormatIndicatorPeriodic_r10 { get; set; }

            public cqi_Mask_r9_Enum? cqi_Mask_r9 { get; set; }

            public long cqi_pmi_ConfigIndex { get; set; }

            public long cqi_PUCCH_ResourceIndex_r10 { get; set; }

            public long? cqi_PUCCH_ResourceIndexP1_r10 { get; set; }

            public csi_ConfigIndex_r10_Type csi_ConfigIndex_r10 { get; set; }

            public long? ri_ConfigIndex { get; set; }

            public bool simultaneousAckNackAndCQI { get; set; }

            [Serializable]
            public class cqi_FormatIndicatorPeriodic_r10_Type : TraceConfig
            {
                public subbandCQI_r10_Type subbandCQI_r10 { get; set; }

                public widebandCQI_r10_Type widebandCQI_r10 { get; set; }

                public class PerDecoder : DecoderBase<cqi_FormatIndicatorPeriodic_r10_Type>
                {
                    public static readonly PerDecoder Instance = new PerDecoder();
                    
                    protected override void ProcessConfig(cqi_FormatIndicatorPeriodic_r10_Type config, BitArrayInputStream input)
                    {
                        switch (input.ReadBits(1))
                        {
                            case 0:
                                config.widebandCQI_r10 = widebandCQI_r10_Type.PerDecoder.Instance.Decode(input);
                                return;

                            case 1:
                                config.subbandCQI_r10 = subbandCQI_r10_Type.PerDecoder.Instance.Decode(input);
                                return;
                        }
                        throw new Exception(GetType().Name + ":NoChoice had been choose");
                    }
                }

                [Serializable]
                public class subbandCQI_r10_Type : TraceConfig
                {
                    public long k { get; set; }

                    public periodicityFactor_r10_Enum periodicityFactor_r10 { get; set; }

                    public class PerDecoder : DecoderBase<subbandCQI_r10_Type>
                    {
                        public static readonly PerDecoder Instance = new PerDecoder();
                        
                        protected override void ProcessConfig(subbandCQI_r10_Type config, BitArrayInputStream input)
                        {
                            config.k = input.ReadBits(2) + 1;
                            var nBits = 1;
                            config.periodicityFactor_r10 = (periodicityFactor_r10_Enum)input.ReadBits(nBits);
                        }
                    }

                    public enum periodicityFactor_r10_Enum
                    {
                        n2,
                        n4
                    }
                }

                [Serializable]
                public class widebandCQI_r10_Type : TraceConfig
                {
                    public csi_ReportMode_r10_Enum? csi_ReportMode_r10 { get; set; }

                    public enum csi_ReportMode_r10_Enum
                    {
                        submode1,
                        submode2
                    }

                    public class PerDecoder : DecoderBase<widebandCQI_r10_Type>
                    {
                        public static readonly PerDecoder Instance = new PerDecoder();
                        
                        protected override void ProcessConfig(widebandCQI_r10_Type config, BitArrayInputStream input)
                        {
                            var stream = new BitMaskStream(input, 1);
                            if (!stream.Read()) return;
                            const int nBits = 1;
                            config.csi_ReportMode_r10 = (csi_ReportMode_r10_Enum)input.ReadBits(nBits);
                        }
                    }
                }
            }

            public enum cqi_Mask_r9_Enum
            {
                setup
            }

            [Serializable]
            public class csi_ConfigIndex_r10_Type : TraceConfig
            {
                public object release { get; set; }

                public setup_Type setup { get; set; }

                public class PerDecoder : DecoderBase<csi_ConfigIndex_r10_Type>
                {
                    public static readonly PerDecoder Instance = new PerDecoder();
                    
                    protected override void ProcessConfig(csi_ConfigIndex_r10_Type config, BitArrayInputStream input)
                    {
                        switch (input.ReadBits(1))
                        {
                            case 0:
                                return;
                            case 1:
                                config.setup = setup_Type.PerDecoder.Instance.Decode(input);
                                return;
                        }
                        throw new Exception(GetType().Name + ":NoChoice had been choose");
                    }
                }

                [Serializable]
                public class setup_Type : TraceConfig
                {
                    public long cqi_pmi_ConfigIndex2_r10 { get; set; }

                    public long? ri_ConfigIndex2_r10 { get; set; }

                    public class PerDecoder : DecoderBase<setup_Type>
                    {
                        public static readonly PerDecoder Instance = new PerDecoder();

                        protected override void ProcessConfig(setup_Type config, BitArrayInputStream input)
                        {
                            var stream = new BitMaskStream(input, 1);
                            config.cqi_pmi_ConfigIndex2_r10 = input.ReadBits(10);
                            if (stream.Read())
                            {
                                config.ri_ConfigIndex2_r10 = input.ReadBits(10);
                            }
                        }
                    }
                }
            }

            public class PerDecoder : DecoderBase<setup_Type>
            {
                public static readonly PerDecoder Instance = new PerDecoder();
                
                protected override void ProcessConfig(setup_Type config, BitArrayInputStream input)
                {
                    var stream = new BitMaskStream(input, 4);
                    config.cqi_PUCCH_ResourceIndex_r10 = input.ReadBits(11);
                    if (stream.Read())
                    {
                        config.cqi_PUCCH_ResourceIndexP1_r10 = input.ReadBits(11);
                    }
                    config.cqi_pmi_ConfigIndex = input.ReadBits(10);
                    config.cqi_FormatIndicatorPeriodic_r10 = cqi_FormatIndicatorPeriodic_r10_Type.PerDecoder.Instance.Decode(input);
                    if (stream.Read())
                    {
                        config.ri_ConfigIndex = input.ReadBits(10);
                    }
                    config.simultaneousAckNackAndCQI = input.ReadBit() == 1;
                    if (stream.Read())
                    {
                        var nBits = 1;
                        config.cqi_Mask_r9 = (cqi_Mask_r9_Enum)input.ReadBits(nBits);
                    }
                    if (stream.Read())
                    {
                        config.csi_ConfigIndex_r10 = csi_ConfigIndex_r10_Type.PerDecoder.Instance.Decode(input);
                    }
                }
            }
        }
    }

    [Serializable]
    public class CQI_ReportPeriodic_v1130 : TraceConfig
    {
        public List<CQI_ReportPeriodicProcExt_r11> cqi_ReportPeriodicProcExtToAddModList_r11 { get; set; }

        public List<long> cqi_ReportPeriodicProcExtToReleaseList_r11 { get; set; }

        public simultaneousAckNackAndCQI_Format3_r11_Enum? simultaneousAckNackAndCQI_Format3_r11 { get; set; }

        public class PerDecoder : DecoderBase<CQI_ReportPeriodic_v1130>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CQI_ReportPeriodic_v1130 config, BitArrayInputStream input)
            {
                var stream = new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    config.simultaneousAckNackAndCQI_Format3_r11 = (simultaneousAckNackAndCQI_Format3_r11_Enum)input.ReadBits(1);
                }
                if (stream.Read())
                {
                    config.cqi_ReportPeriodicProcExtToReleaseList_r11 = new List<long>();
                    var num3 = input.ReadBits(2) + 1;
                    for (var i = 0; i < num3; i++)
                    {
                        long item = input.ReadBits(2) + 1;
                        config.cqi_ReportPeriodicProcExtToReleaseList_r11.Add(item);
                    }
                }
                if (stream.Read())
                {
                    config.cqi_ReportPeriodicProcExtToAddModList_r11 = new List<CQI_ReportPeriodicProcExt_r11>();
                    var num6 = input.ReadBits(2) + 1;
                    for (var j = 0; j < num6; j++)
                    {
                        var r = CQI_ReportPeriodicProcExt_r11.PerDecoder.Instance.Decode(input);
                        config.cqi_ReportPeriodicProcExtToAddModList_r11.Add(r);
                    }
                }
            }
        }

        public enum simultaneousAckNackAndCQI_Format3_r11_Enum
        {
            setup
        }
    }

    [Serializable]
    public class CQI_ReportPeriodicProcExt_r11 : TraceConfig
    {
        public cqi_FormatIndicatorPeriodic_r11_Type cqi_FormatIndicatorPeriodic_r11 { get; set; }

        public long cqi_pmi_ConfigIndex_r11 { get; set; }

        public long cqi_ReportPeriodicProcExtId_r11 { get; set; }

        public csi_ConfigIndex_r11_Type csi_ConfigIndex_r11 { get; set; }

        public long? ri_ConfigIndex_r11 { get; set; }

        [Serializable]
        public class cqi_FormatIndicatorPeriodic_r11_Type : TraceConfig
        {
            public subbandCQI_r11_Type subbandCQI_r11 { get; set; }

            public widebandCQI_r11_Type widebandCQI_r11 { get; set; }

            public class PerDecoder : DecoderBase<cqi_FormatIndicatorPeriodic_r11_Type>
            {
                public static readonly PerDecoder Instance = new PerDecoder();
                
                protected override void ProcessConfig(cqi_FormatIndicatorPeriodic_r11_Type config, BitArrayInputStream input)
                {
                    switch (input.ReadBits(1))
                    {
                        case 0:
                            config.widebandCQI_r11 = widebandCQI_r11_Type.PerDecoder.Instance.Decode(input);
                            return;

                        case 1:
                            config.subbandCQI_r11 = subbandCQI_r11_Type.PerDecoder.Instance.Decode(input);
                            return;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }

            [Serializable]
            public class subbandCQI_r11_Type : TraceConfig
            {
                public long k { get; set; }

                public periodicityFactor_r11_Enum periodicityFactor_r11 { get; set; }

                public class PerDecoder : DecoderBase<subbandCQI_r11_Type>
                {
                    public static readonly PerDecoder Instance = new PerDecoder();
                    
                    protected override void ProcessConfig(subbandCQI_r11_Type config, BitArrayInputStream input)
                    {
                        config.k = input.ReadBits(2) + 1;
                        var nBits = 1;
                        config.periodicityFactor_r11 = (periodicityFactor_r11_Enum)input.ReadBits(nBits);
                    }
                }

                public enum periodicityFactor_r11_Enum
                {
                    n2,
                    n4
                }
            }

            [Serializable]
            public class widebandCQI_r11_Type : TraceConfig
            {
                public csi_ReportMode_r11_Enum? csi_ReportMode_r11 { get; set; }

                public enum csi_ReportMode_r11_Enum
                {
                    submode1,
                    submode2
                }

                public class PerDecoder : DecoderBase<widebandCQI_r11_Type>
                {
                    public static readonly PerDecoder Instance = new PerDecoder();
                    
                    protected override void ProcessConfig(widebandCQI_r11_Type config, BitArrayInputStream input)
                    {
                        var stream = new BitMaskStream(input, 1);
                        if (stream.Read())
                        {
                            const int nBits = 1;
                            config.csi_ReportMode_r11 = (csi_ReportMode_r11_Enum)input.ReadBits(nBits);
                        }
                    }
                }
            }
        }

        [Serializable]
        public class csi_ConfigIndex_r11_Type : TraceConfig
        {
            public object release { get; set; }

            public setup_Type setup { get; set; }

            public class PerDecoder : DecoderBase<csi_ConfigIndex_r11_Type>
            {
                public static readonly PerDecoder Instance = new PerDecoder();
                
                protected override void ProcessConfig(csi_ConfigIndex_r11_Type config, BitArrayInputStream input)
                {
                    switch (input.ReadBits(1))
                    {
                        case 0:
                            return;
                        case 1:
                            config.setup = setup_Type.PerDecoder.Instance.Decode(input);
                            return;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }

            [Serializable]
            public class setup_Type : TraceConfig
            {
                public long cqi_pmi_ConfigIndex2_r11 { get; set; }

                public long? ri_ConfigIndex2_r11 { get; set; }

                public class PerDecoder : DecoderBase<setup_Type>
                {
                    public static readonly PerDecoder Instance = new PerDecoder();
                    
                    protected override void ProcessConfig(setup_Type config, BitArrayInputStream input)
                    {
                        var stream = new BitMaskStream(input, 1);
                        config.cqi_pmi_ConfigIndex2_r11 = input.ReadBits(10);
                        if (stream.Read())
                        {
                            config.ri_ConfigIndex2_r11 = input.ReadBits(10);
                        }
                    }
                }
            }
        }

        public class PerDecoder : DecoderBase<CQI_ReportPeriodicProcExt_r11>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CQI_ReportPeriodicProcExt_r11 config, BitArrayInputStream input)
            {
                input.ReadBit();
                var stream = new BitMaskStream(input, 2);
                config.cqi_ReportPeriodicProcExtId_r11 = input.ReadBits(2) + 1;
                config.cqi_pmi_ConfigIndex_r11 = input.ReadBits(10);
                config.cqi_FormatIndicatorPeriodic_r11 = cqi_FormatIndicatorPeriodic_r11_Type.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    config.ri_ConfigIndex_r11 = input.ReadBits(10);
                }
                if (stream.Read())
                {
                    config.csi_ConfigIndex_r11 = csi_ConfigIndex_r11_Type.PerDecoder.Instance.Decode(input);
                }
            }
        }
    }

}
