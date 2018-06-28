using Lte.Domain.Common;
using System;
using System.Collections.Generic;
using Lte.Domain.Common.Types;

namespace TraceParser.S1ap
{
    [Serializable]
    public class ErrorIndication
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ErrorIndication Decode(BitArrayInputStream input)
            {
                ErrorIndication indication = new ErrorIndication();
                indication.InitDefaults();
                input.ReadBit();
                input.skipUnreadedBits();
                indication.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.ReadBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    indication.protocolIEs.Add(item);
                }
                return indication;
            }
        }
    }
}
