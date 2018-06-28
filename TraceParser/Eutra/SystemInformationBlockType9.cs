using Lte.Domain.Common;
using System;
using Lte.Domain.Common.Types;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SystemInformationBlockType9
    {
        public void InitDefaults()
        {
        }

        public string hnb_Name { get; set; }

        public string lateNonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType9 Decode(BitArrayInputStream input)
            {
                int nBits;
                var type = new SystemInformationBlockType9();
                type.InitDefaults();
                var flag = input.ReadBit() != 0;
                var stream = flag ? new BitMaskStream(input, 2) : new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    nBits = input.ReadBits(6);
                    type.hnb_Name = input.readOctetString(nBits + 1);
                }
                if (flag && stream.Read())
                {
                    nBits = input.ReadBits(8);
                    type.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                return type;
            }
        }
    }
}