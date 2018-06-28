using Lte.Domain.Common;
using System;
using System.Collections.Generic;
using Lte.Domain.Common.Types;
using TraceParser.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class CellInfoGERAN_r9 : TraceConfig
    {
        public CarrierFreqGERAN carrierFreq_r9 { get; set; }

        public PhysCellIdGERAN physCellId_r9 { get; set; }

        public List<string> systemInformation_r9 { get; set; }

        public class PerDecoder : DecoderBase<CellInfoGERAN_r9>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CellInfoGERAN_r9 config, BitArrayInputStream input)
            {
                config.physCellId_r9 = PhysCellIdGERAN.PerDecoder.Instance.Decode(input);
                config.carrierFreq_r9 = CarrierFreqGERAN.PerDecoder.Instance.Decode(input);
                config.systemInformation_r9 = new List<string>();
                const int nBits = 4;
                int num3 = input.ReadBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    string item = input.readOctetString(input.ReadBits(5) + 1);
                    config.systemInformation_r9.Add(item);
                }
            }
        }
    }

    [Serializable]
    public class CellInfoUTRA_FDD_r9 : TraceConfig
    {
        public long physCellId_r9 { get; set; }

        public string utra_BCCH_Container_r9 { get; set; }

        public class PerDecoder : DecoderBase<CellInfoUTRA_FDD_r9>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CellInfoUTRA_FDD_r9 config, BitArrayInputStream input)
            {
                config.physCellId_r9 = input.ReadBits(9);
                int nBits = input.ReadBits(8);
                config.utra_BCCH_Container_r9 = input.readOctetString(nBits);
            }
        }
    }

    [Serializable]
    public class CellInfoUTRA_TDD_r10 : TraceConfig
    {
        public long carrierFreq_r10 { get; set; }

        public long physCellId_r10 { get; set; }

        public string utra_BCCH_Container_r10 { get; set; }

        public class PerDecoder : DecoderBase<CellInfoUTRA_TDD_r10>
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            protected override void ProcessConfig(CellInfoUTRA_TDD_r10 config, BitArrayInputStream input)
            {
                config.physCellId_r10 = input.ReadBits(7);
                config.carrierFreq_r10 = input.ReadBits(14);
                int nBits = input.ReadBits(8);
                config.utra_BCCH_Container_r10 = input.readOctetString(nBits);
            }
        }
    }

    [Serializable]
    public class CellInfoUTRA_TDD_r9 : TraceConfig
    {
        public long physCellId_r9 { get; set; }

        public string utra_BCCH_Container_r9 { get; set; }

        public class PerDecoder : DecoderBase<CellInfoUTRA_TDD_r9>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CellInfoUTRA_TDD_r9 config, BitArrayInputStream input)
            {
                config.physCellId_r9 = input.ReadBits(7);
                config.utra_BCCH_Container_r9 = input.readOctetString(input.ReadBits(8));
            }
        }
    }

    [Serializable]
    public class VisitedCellInfo_r12 : TraceConfig
    {
        public long timeSpent_r12 { get; set; }

        public visitedCellId_r12_Type visitedCellId_r12 { get; set; }

        public class PerDecoder : DecoderBase<VisitedCellInfo_r12>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(VisitedCellInfo_r12 config, BitArrayInputStream input)
            {
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    config.visitedCellId_r12 = visitedCellId_r12_Type.PerDecoder.Instance.Decode(input);
                }
                config.timeSpent_r12 = input.ReadBits(12);
            }
        }

        [Serializable]
        public class visitedCellId_r12_Type : TraceConfig
        {
            public CellGlobalIdEUTRA cellGlobalId_r12 { get; set; }

            public pci_arfcn_r12_Type pci_arfcn_r12 { get; set; }

            [Serializable]
            public class pci_arfcn_r12_Type : TraceConfig
            {
                public long carrierFreq_r12 { get; set; }

                public long physCellId_r12 { get; set; }

                public class PerDecoder : DecoderBase<pci_arfcn_r12_Type>
                {
                    public static readonly PerDecoder Instance = new PerDecoder();
                    
                    protected override void ProcessConfig(pci_arfcn_r12_Type config, BitArrayInputStream input)
                    {
                        config.physCellId_r12 = input.ReadBits(9);
                        config.carrierFreq_r12 = input.ReadBits(0x10);
                    }
                }
            }

            public class PerDecoder : DecoderBase<visitedCellId_r12_Type>
            {
                public static readonly PerDecoder Instance = new PerDecoder();
                
                protected override void ProcessConfig(visitedCellId_r12_Type config, BitArrayInputStream input)
                {
                    switch (input.ReadBits(1))
                    {
                        case 0:
                            config.cellGlobalId_r12 = CellGlobalIdEUTRA.PerDecoder.Instance.Decode(input);
                            return;
                        case 1:
                            config.pci_arfcn_r12 = pci_arfcn_r12_Type.PerDecoder.Instance.Decode(input);
                            return;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }
    }

}
