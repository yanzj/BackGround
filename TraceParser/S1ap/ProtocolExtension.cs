using Lte.Domain.Common;
using System;
using System.Collections.Generic;
using Lte.Domain.Common.Types;

namespace TraceParser.S1ap
{
    [Serializable]
    public class ProtocolExtensionContainer
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<ProtocolExtensionField> Decode(BitArrayInputStream input)
            {
                return new List<ProtocolExtensionField>();
            }
        }
    }

    [Serializable]
    public class ProtocolExtensionField
    {
        public void InitDefaults()
        {
        }

        public Criticality criticality { get; set; }

        public object extensionValue { get; set; }

        public long id { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ProtocolExtensionField Decode(BitArrayInputStream input)
            {
                int nBits = 0;
                long num3 = 0L;
                ProtocolExtensionField field = new ProtocolExtensionField();
                field.InitDefaults();
                int num4 = input.ReadBits(1) + 1;
                input.skipUnreadedBits();
                field.id = input.ReadBits(num4 * 8);
                num4 = 2;
                field.criticality = (Criticality)input.ReadBits(num4);
                input.skipUnreadedBits();
                nBits = 0;
                while (true)
                {
                    switch (input.ReadBit())
                    {
                        case 0:
                            nBits += input.ReadBits(7);
                            goto Label_00DD;

                        case 1:
                            switch (input.ReadBit())
                            {
                                case 0:
                                    nBits += input.ReadBits(14);
                                    goto Label_00DD;

                                case 1:
                                    input.ReadBits(2);
                                    nBits += input.ReadBits(4) * 0x400;
                                    break;
                            }
                            break;
                    }
                }
            Label_00DD:
                num3 = input.Position;
                try
                {
                    field.extensionValue = S1AP_PROTOCOL_EXTENSION.Switcher(field.id, "Extension", input);
                    input.skipUnreadedBits();
                }
                catch (Exception)
                {
                    input.skipUnreadedBits();
                    input.Position = num3;
                    field.extensionValue = input.readOctetString(nBits);
                }
                if (input.Position != (num3 + nBits))
                {
                    input.Position = num3 + nBits;
                }
                return field;
            }
        }
    }

    [Serializable]
    public class ProtocolExtensionID
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public long Decode(BitArrayInputStream input)
            {
                int num2 = input.ReadBits(1) + 1;
                input.skipUnreadedBits();
                return input.ReadBits(num2 * 8);
            }
        }
    }

}
