using Lte.Domain.Common;
using System;
using System.Collections.Generic;
using Lte.Domain.Common.Types;

namespace TraceParser.S1ap
{
    [Serializable]
    public class UE_associatedLogicalS1_ConnectionItem
    {
        public void InitDefaults()
        {
        }

        public long? eNB_UE_S1AP_ID { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public long? mME_UE_S1AP_ID { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UE_associatedLogicalS1_ConnectionItem Decode(BitArrayInputStream input)
            {
                int num4;
                UE_associatedLogicalS1_ConnectionItem item = new UE_associatedLogicalS1_ConnectionItem();
                item.InitDefaults();
                BitMaskStream stream = (input.ReadBit() != 0) ? new BitMaskStream(input, 3) : new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    num4 = input.ReadBits(2) + 1;
                    input.skipUnreadedBits();
                    item.mME_UE_S1AP_ID = input.ReadBits(num4 * 8);
                }
                if (stream.Read())
                {
                    num4 = input.ReadBits(2) + 1;
                    input.skipUnreadedBits();
                    item.eNB_UE_S1AP_ID = input.ReadBits(num4 * 8);
                }
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    item.iE_Extensions = new List<ProtocolExtensionField>();
                    num4 = 0x10;
                    int num5 = input.ReadBits(num4) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField field = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        item.iE_Extensions.Add(field);
                    }
                }
                return item;
            }
        }
    }

    [Serializable]
    public class UE_associatedLogicalS1_ConnectionListRes
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<ProtocolIE_Field> Decode(BitArrayInputStream input)
            {
                return new List<ProtocolIE_Field>();
            }
        }
    }

    [Serializable]
    public class UE_associatedLogicalS1_ConnectionListResAck
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<ProtocolIE_Field> Decode(BitArrayInputStream input)
            {
                return new List<ProtocolIE_Field>();
            }
        }
    }

}
