using Lte.Domain.Common;
using System;
using Lte.Domain.Common.Types;
using TraceParser.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class CellGlobalIdCDMA2000 : TraceConfig
    {
        public string cellGlobalId1XRTT { get; set; }

        public string cellGlobalIdHRPD { get; set; }

        public class PerDecoder : DecoderBase<CellGlobalIdCDMA2000>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CellGlobalIdCDMA2000 config, BitArrayInputStream input)
            {
                switch (input.ReadBits(1))
                {
                    case 0:
                        config.cellGlobalId1XRTT = input.ReadBitString(0x2f);
                        return;

                    case 1:
                        config.cellGlobalIdHRPD = input.ReadBitString(0x80);
                        return;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }

    [Serializable]
    public class CellGlobalIdEUTRA : TraceConfig
    {
        public string cellIdentity { get; set; }

        public PLMN_Identity plmn_Identity { get; set; }

        public class PerDecoder : DecoderBase<CellGlobalIdEUTRA>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CellGlobalIdEUTRA config, BitArrayInputStream input)
            {
                config.plmn_Identity = PLMN_Identity.PerDecoder.Instance.Decode(input);
                config.cellIdentity = input.ReadBitString(0x1c);
            }
        }
    }

    [Serializable]
    public class CellGlobalIdGERAN : TraceConfig
    {
        public string cellIdentity { get; set; }

        public string locationAreaCode { get; set; }

        public PLMN_Identity plmn_Identity { get; set; }

        public class PerDecoder : DecoderBase<CellGlobalIdGERAN>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CellGlobalIdGERAN config, BitArrayInputStream input)
            {
                config.plmn_Identity = PLMN_Identity.PerDecoder.Instance.Decode(input);
                config.locationAreaCode = input.ReadBitString(0x10);
                config.cellIdentity = input.ReadBitString(0x10);
            }
        }
    }

    [Serializable]
    public class CellGlobalIdUTRA : TraceConfig
    {
        public string cellIdentity { get; set; }

        public PLMN_Identity plmn_Identity { get; set; }

        public class PerDecoder : DecoderBase<CellGlobalIdUTRA>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CellGlobalIdUTRA config, BitArrayInputStream input)
            {
                config.plmn_Identity = PLMN_Identity.PerDecoder.Instance.Decode(input);
                config.cellIdentity = input.ReadBitString(0x1c);
            }
        }
    }

}
