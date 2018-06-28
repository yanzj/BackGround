using Lte.Domain.Common;
using System;
using System.Collections.Generic;
using Lte.Domain.Common.Types;

namespace TraceParser.S1ap
{
    [Serializable]
    public class InitiatingMessage
    {
        public void InitDefaults()
        {
        }

        public Criticality criticality { get; set; }

        public long procedureCode { get; set; }

        public object value { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public InitiatingMessage Decode(BitArrayInputStream input)
            {
                InitiatingMessage message = new InitiatingMessage();
                message.InitDefaults();
                input.skipUnreadedBits();
                message.procedureCode = input.ReadBits(8);
                const int num4 = 2;
                message.criticality = (Criticality)input.ReadBits(num4);
                input.skipUnreadedBits();
                int nBits = 0;
                while (true)
                {
                    switch (input.ReadBit())
                    {
                        case 0:
                            nBits += input.ReadBits(7);
                            goto Label_00CF;

                        case 1:
                            switch (input.ReadBit())
                            {
                                case 0:
                                    nBits += input.ReadBits(14);
                                    goto Label_00CF;

                                case 1:
                                    input.ReadBits(2);
                                    nBits += input.ReadBits(4) * 0x400;
                                    break;
                            }
                            break;
                    }
                }
            Label_00CF:
                long num3 = input.Position;
                try
                {
                    message.value = S1AP_ELEMENTARY_PROCEDURE.Switcher(message.procedureCode, "InitiatingMessage", input);
                    input.skipUnreadedBits();
                }
                catch (Exception)
                {
                    input.skipUnreadedBits();
                    input.Position = num3;
                    message.value = input.readOctetString(nBits);
                }
                if (input.Position != (num3 + nBits))
                {
                    input.Position = num3 + nBits;
                }
                return message;
            }
        }
    }

    [Serializable]
    public class InitialUEMessage
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public InitialUEMessage Decode(BitArrayInputStream input)
            {
                InitialUEMessage message = new InitialUEMessage();
                message.InitDefaults();
                input.ReadBit();
                input.skipUnreadedBits();
                message.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.ReadBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    message.protocolIEs.Add(item);
                }
                return message;
            }
        }
    }

}
