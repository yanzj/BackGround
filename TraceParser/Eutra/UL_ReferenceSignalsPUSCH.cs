using Lte.Domain.Common;
using System;
using Lte.Domain.Common.Types;

namespace TraceParser.Eutra
{
    [Serializable]
    public class UL_ReferenceSignalsPUSCH
    {
        public void InitDefaults()
        {
        }

        public long cyclicShift { get; set; }

        public long groupAssignmentPUSCH { get; set; }

        public bool groupHoppingEnabled { get; set; }

        public bool sequenceHoppingEnabled { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UL_ReferenceSignalsPUSCH Decode(BitArrayInputStream input)
            {
                UL_ReferenceSignalsPUSCH spusch = new UL_ReferenceSignalsPUSCH();
                spusch.InitDefaults();
                spusch.groupHoppingEnabled = input.ReadBit() == 1;
                spusch.groupAssignmentPUSCH = input.ReadBits(5);
                spusch.sequenceHoppingEnabled = input.ReadBit() == 1;
                spusch.cyclicShift = input.ReadBits(3);
                return spusch;
            }
        }
    }
}
