using Lte.Domain.Common;
using System;
using System.Collections.Generic;
using Lte.Domain.Common.Types;
using TraceParser.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class CellReselectionParametersCDMA2000 : TraceConfig
    {
        public List<BandClassInfoCDMA2000> bandClassList { get; set; }

        public List<NeighCellCDMA2000> neighCellList { get; set; }

        public long t_ReselectionCDMA2000 { get; set; }

        public SpeedStateScaleFactors t_ReselectionCDMA2000_SF { get; set; }

        public class PerDecoder : DecoderBase<CellReselectionParametersCDMA2000>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CellReselectionParametersCDMA2000 config, BitArrayInputStream input)
            {
                BitMaskStream stream = new BitMaskStream(input, 1);
                config.bandClassList = new List<BandClassInfoCDMA2000>();
                int nBits = 5;
                int num3 = input.ReadBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    BandClassInfoCDMA2000 item = BandClassInfoCDMA2000.PerDecoder.Instance.Decode(input);
                    config.bandClassList.Add(item);
                }
                config.neighCellList = new List<NeighCellCDMA2000>();
                nBits = 4;
                int num5 = input.ReadBits(nBits) + 1;
                for (int j = 0; j < num5; j++)
                {
                    NeighCellCDMA2000 lcdma = NeighCellCDMA2000.PerDecoder.Instance.Decode(input);
                    config.neighCellList.Add(lcdma);
                }
                config.t_ReselectionCDMA2000 = input.ReadBits(3);
                if (stream.Read())
                {
                    config.t_ReselectionCDMA2000_SF = SpeedStateScaleFactors.PerDecoder.Instance.Decode(input);
                }
            }
        }
    }

    [Serializable]
    public class CellReselectionParametersCDMA2000_r11 : TraceConfig
    {
        public List<BandClassInfoCDMA2000> bandClassList { get; set; }

        public List<NeighCellCDMA2000_r11> neighCellList_r11 { get; set; }

        public long t_ReselectionCDMA2000 { get; set; }

        public SpeedStateScaleFactors t_ReselectionCDMA2000_SF { get; set; }

        public class PerDecoder : DecoderBase<CellReselectionParametersCDMA2000_r11>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CellReselectionParametersCDMA2000_r11 config, BitArrayInputStream input)
            {
                BitMaskStream stream = new BitMaskStream(input, 1);
                config.bandClassList = new List<BandClassInfoCDMA2000>();
                int nBits = 5;
                int num3 = input.ReadBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    BandClassInfoCDMA2000 item = BandClassInfoCDMA2000.PerDecoder.Instance.Decode(input);
                    config.bandClassList.Add(item);
                }
                config.neighCellList_r11 = new List<NeighCellCDMA2000_r11>();
                nBits = 4;
                int num5 = input.ReadBits(nBits) + 1;
                for (int j = 0; j < num5; j++)
                {
                    NeighCellCDMA2000_r11 _r2 = NeighCellCDMA2000_r11.PerDecoder.Instance.Decode(input);
                    config.neighCellList_r11.Add(_r2);
                }
                config.t_ReselectionCDMA2000 = input.ReadBits(3);
                if (stream.Read())
                {
                    config.t_ReselectionCDMA2000_SF = SpeedStateScaleFactors.PerDecoder.Instance.Decode(input);
                }
            }
        }
    }

    [Serializable]
    public class CellReselectionParametersCDMA2000_v920 : TraceConfig
    {
        public List<NeighCellCDMA2000_v920> neighCellList_v920 { get; set; }

        public class PerDecoder : DecoderBase<CellReselectionParametersCDMA2000_v920>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CellReselectionParametersCDMA2000_v920 config, BitArrayInputStream input)
            {
                config.neighCellList_v920 = new List<NeighCellCDMA2000_v920>();
                int num3 = input.ReadBits(4) + 1;
                for (int i = 0; i < num3; i++)
                {
                    NeighCellCDMA2000_v920 item = NeighCellCDMA2000_v920.PerDecoder.Instance.Decode(input);
                    config.neighCellList_v920.Add(item);
                }
            }
        }
    }

    [Serializable]
    public class CellChangeOrder : TraceConfig
    {
        public t304_Enum t304 { get; set; }

        public targetRAT_Type_Type targetRAT_Type { get; set; }

        public class PerDecoder : DecoderBase<CellChangeOrder>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CellChangeOrder config, BitArrayInputStream input)
            {
                int nBits = 3;
                config.t304 = (t304_Enum)input.ReadBits(nBits);
                config.targetRAT_Type = targetRAT_Type_Type.PerDecoder.Instance.Decode(input);
            }
        }

        public enum t304_Enum
        {
            ms100,
            ms200,
            ms500,
            ms1000,
            ms2000,
            ms4000,
            ms8000,
            spare1
        }

        [Serializable]
        public class targetRAT_Type_Type : TraceConfig
        {
            public geran_Type geran { get; set; }

            [Serializable]
            public class geran_Type : TraceConfig
            {
                public CarrierFreqGERAN carrierFreq { get; set; }

                public string networkControlOrder { get; set; }

                public PhysCellIdGERAN physCellId { get; set; }

                public SI_OrPSI_GERAN systemInformation { get; set; }

                public class PerDecoder : DecoderBase<geran_Type>
                {
                    public static readonly PerDecoder Instance = new PerDecoder();
                    
                    protected override void ProcessConfig(geran_Type config, BitArrayInputStream input)
                    {
                        BitMaskStream stream = new BitMaskStream(input, 2);
                        config.physCellId = PhysCellIdGERAN.PerDecoder.Instance.Decode(input);
                        config.carrierFreq = CarrierFreqGERAN.PerDecoder.Instance.Decode(input);
                        if (stream.Read())
                        {
                            config.networkControlOrder = input.ReadBitString(2);
                        }
                        if (stream.Read())
                        {
                            config.systemInformation = SI_OrPSI_GERAN.PerDecoder.Instance.Decode(input);
                        }
                    }
                }
            }

            public class PerDecoder : DecoderBase<targetRAT_Type_Type>
            {
                public static readonly PerDecoder Instance = new PerDecoder();
                
                protected override void ProcessConfig(targetRAT_Type_Type config, BitArrayInputStream input)
                {
                    input.ReadBit();
                    if (input.ReadBits(1) != 0)
                    {
                        throw new Exception(GetType().Name + ":NoChoice had been choose");
                    }
                    config.geran = geran_Type.PerDecoder.Instance.Decode(input);
                }
            }
        }
    }

}
