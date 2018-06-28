using Lte.Domain.Common;
using System;
using System.Collections.Generic;
using Lte.Domain.Common.Types;
using TraceParser.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class ConnEstFailReport_r11 : TraceConfig
    {
        public bool contentionDetected_r11 { get; set; }

        public CellGlobalIdEUTRA failedCellId_r11 { get; set; }

        public LocationInfo_r10 locationInfo_r11 { get; set; }

        public bool maxTxPowerReached_r11 { get; set; }

        public measResultFailedCell_r11_Type measResultFailedCell_r11 { get; set; }

        public List<MeasResult2EUTRA_v9e0> measResultListEUTRA_v1130 { get; set; }

        public measResultNeighCells_r11_Type measResultNeighCells_r11 { get; set; }

        public long numberOfPreamblesSent_r11 { get; set; }

        public long timeSinceFailure_r11 { get; set; }

        [Serializable]
        public class measResultFailedCell_r11_Type : TraceConfig
        {
            public long rsrpResult_r11 { get; set; }

            public long? rsrqResult_r11 { get; set; }

            public class PerDecoder : DecoderBase<measResultFailedCell_r11_Type>
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                protected override void ProcessConfig(measResultFailedCell_r11_Type config, BitArrayInputStream input)
                {
                    BitMaskStream stream = new BitMaskStream(input, 1);
                    config.rsrpResult_r11 = input.ReadBits(7);
                    if (stream.Read())
                    {
                        config.rsrqResult_r11 = input.ReadBits(6);
                    }
                }
            }
        }

        [Serializable]
        public class measResultNeighCells_r11_Type : TraceConfig
        {
            public List<MeasResult2EUTRA_r9> measResultListEUTRA_r11 { get; set; }

            public List<MeasResultGERAN> measResultListGERAN_r11 { get; set; }

            public List<MeasResult2UTRA_r9> measResultListUTRA_r11 { get; set; }

            public List<MeasResult2CDMA2000_r9> measResultsCDMA2000_r11 { get; set; }

            public class PerDecoder : DecoderBase<measResultNeighCells_r11_Type>
            {
                public static readonly PerDecoder Instance = new PerDecoder();
                
                protected override void ProcessConfig(measResultNeighCells_r11_Type config, BitArrayInputStream input)
                {
                    BitMaskStream stream = new BitMaskStream(input, 4);
                    if (stream.Read())
                    {
                        config.measResultListEUTRA_r11 = new List<MeasResult2EUTRA_r9>();
                        int num3 = input.ReadBits(3) + 1;
                        for (int i = 0; i < num3; i++)
                        {
                            MeasResult2EUTRA_r9 item = MeasResult2EUTRA_r9.PerDecoder.Instance.Decode(input);
                            config.measResultListEUTRA_r11.Add(item);
                        }
                    }
                    if (stream.Read())
                    {
                        config.measResultListUTRA_r11 = new List<MeasResult2UTRA_r9>();
                        int num5 = input.ReadBits(3) + 1;
                        for (int j = 0; j < num5; j++)
                        {
                            MeasResult2UTRA_r9 _r2 = MeasResult2UTRA_r9.PerDecoder.Instance.Decode(input);
                            config.measResultListUTRA_r11.Add(_r2);
                        }
                    }
                    if (stream.Read())
                    {
                        config.measResultListGERAN_r11 = new List<MeasResultGERAN>();
                        int num7 = input.ReadBits(3) + 1;
                        for (int k = 0; k < num7; k++)
                        {
                            MeasResultGERAN tgeran = MeasResultGERAN.PerDecoder.Instance.Decode(input);
                            config.measResultListGERAN_r11.Add(tgeran);
                        }
                    }
                    if (stream.Read())
                    {
                        config.measResultsCDMA2000_r11 = new List<MeasResult2CDMA2000_r9>();
                        int num9 = input.ReadBits(3) + 1;
                        for (int m = 0; m < num9; m++)
                        {
                            MeasResult2CDMA2000_r9 _r3 = MeasResult2CDMA2000_r9.PerDecoder.Instance.Decode(input);
                            config.measResultsCDMA2000_r11.Add(_r3);
                        }
                    }
                }
            }
        }

        public class PerDecoder : DecoderBase<ConnEstFailReport_r11>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(ConnEstFailReport_r11 config, BitArrayInputStream input)
            {
                BitMaskStream stream = (input.ReadBit() != 0) ? new BitMaskStream(input, 3) : new BitMaskStream(input, 3);
                config.failedCellId_r11 = CellGlobalIdEUTRA.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    config.locationInfo_r11 = LocationInfo_r10.PerDecoder.Instance.Decode(input);
                }
                config.measResultFailedCell_r11 = measResultFailedCell_r11_Type.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    config.measResultNeighCells_r11 = measResultNeighCells_r11_Type.PerDecoder.Instance.Decode(input);
                }
                config.numberOfPreamblesSent_r11 = input.ReadBits(8) + 1;
                config.contentionDetected_r11 = input.ReadBit() == 1;
                config.maxTxPowerReached_r11 = input.ReadBit() == 1;
                config.timeSinceFailure_r11 = input.ReadBits(0x12);
                if (stream.Read())
                {
                    config.measResultListEUTRA_v1130 = new List<MeasResult2EUTRA_v9e0>();
                    int num3 = input.ReadBits(3) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        MeasResult2EUTRA_v9e0 item = MeasResult2EUTRA_v9e0.PerDecoder.Instance.Decode(input);
                        config.measResultListEUTRA_v1130.Add(item);
                    }
                }
            }
        }
    }
}
