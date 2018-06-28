using Lte.Domain.Common;
using System;
using Lte.Domain.Common.Types;
using TraceParser.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class CrossCarrierSchedulingConfig_r10 : TraceConfig
    {
        public schedulingCellInfo_r10_Type schedulingCellInfo_r10 { get; set; }

        public class PerDecoder : DecoderBase<CrossCarrierSchedulingConfig_r10>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CrossCarrierSchedulingConfig_r10 config, BitArrayInputStream input)
            {
                config.schedulingCellInfo_r10 = schedulingCellInfo_r10_Type.PerDecoder.Instance.Decode(input);
            }
        }

        [Serializable]
        public class schedulingCellInfo_r10_Type : TraceConfig
        {
            public other_r10_Type other_r10 { get; set; }

            public own_r10_Type own_r10 { get; set; }

            [Serializable]
            public class other_r10_Type : TraceConfig
            {
                public long pdsch_Start_r10 { get; set; }

                public long schedulingCellId_r10 { get; set; }

                public class PerDecoder : DecoderBase<other_r10_Type>
                {
                    public static readonly PerDecoder Instance = new PerDecoder();
                    
                    protected override void ProcessConfig(other_r10_Type config, BitArrayInputStream input)
                    {
                        config.schedulingCellId_r10 = input.ReadBits(3);
                        config.pdsch_Start_r10 = input.ReadBits(2) + 1;
                    }
                }
            }

            [Serializable]
            public class own_r10_Type : TraceConfig
            {
                public bool cif_Presence_r10 { get; set; }

                public class PerDecoder : DecoderBase<own_r10_Type>
                {
                    public static readonly PerDecoder Instance = new PerDecoder();
                    
                    protected override void ProcessConfig(own_r10_Type config, BitArrayInputStream input)
                    {
                        config.cif_Presence_r10 = input.ReadBit() == 1;
                    }
                }
            }

            public class PerDecoder : DecoderBase<schedulingCellInfo_r10_Type>
            {
                public static readonly PerDecoder Instance = new PerDecoder();
                
                protected override void ProcessConfig(schedulingCellInfo_r10_Type config, BitArrayInputStream input)
                {
                    switch (input.ReadBits(1))
                    {
                        case 0:
                            config.own_r10 = own_r10_Type.PerDecoder.Instance.Decode(input);
                            return;
                        case 1:
                            config.other_r10 = other_r10_Type.PerDecoder.Instance.Decode(input);
                            return;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }
    }
}
