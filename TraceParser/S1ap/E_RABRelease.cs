using Lte.Domain.Common;
using System;
using System.Collections.Generic;
using Lte.Domain.Common.Types;

namespace TraceParser.S1ap
{
    [Serializable]
    public class E_RABReleaseCommand
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public E_RABReleaseCommand Decode(BitArrayInputStream input)
            {
                E_RABReleaseCommand command = new E_RABReleaseCommand();
                command.InitDefaults();
                input.ReadBit();
                input.skipUnreadedBits();
                command.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.ReadBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    command.protocolIEs.Add(item);
                }
                return command;
            }
        }
    }

    [Serializable]
    public class E_RABReleaseIndication
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public E_RABReleaseIndication Decode(BitArrayInputStream input)
            {
                E_RABReleaseIndication indication = new E_RABReleaseIndication();
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

    [Serializable]
    public class E_RABReleaseItemBearerRelComp
    {
        public void InitDefaults()
        {
        }

        public long e_RAB_ID { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public E_RABReleaseItemBearerRelComp Decode(BitArrayInputStream input)
            {
                E_RABReleaseItemBearerRelComp comp = new E_RABReleaseItemBearerRelComp();
                comp.InitDefaults();
                BitMaskStream stream = (input.ReadBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.ReadBit();
                comp.e_RAB_ID = input.ReadBits(4);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    comp.iE_Extensions = new List<ProtocolExtensionField>();
                    int nBits = 0x10;
                    int num5 = input.ReadBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        comp.iE_Extensions.Add(item);
                    }
                }
                return comp;
            }
        }
    }

    [Serializable]
    public class E_RABReleaseListBearerRelComp
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
    public class E_RABReleaseResponse
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public E_RABReleaseResponse Decode(BitArrayInputStream input)
            {
                E_RABReleaseResponse response = new E_RABReleaseResponse();
                response.InitDefaults();
                input.ReadBit();
                input.skipUnreadedBits();
                response.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.ReadBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    response.protocolIEs.Add(item);
                }
                return response;
            }
        }
    }

    [Serializable]
    public class E_RABToBeSwitchedDLItem
    {
        public void InitDefaults()
        {
        }

        public long e_RAB_ID { get; set; }

        public string gTP_TEID { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string transportLayerAddress { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public E_RABToBeSwitchedDLItem Decode(BitArrayInputStream input)
            {
                E_RABToBeSwitchedDLItem item = new E_RABToBeSwitchedDLItem();
                item.InitDefaults();
                BitMaskStream stream = (input.ReadBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.ReadBit();
                item.e_RAB_ID = input.ReadBits(4);
                input.ReadBit();
                int num = input.ReadBits(8);
                input.skipUnreadedBits();
                item.transportLayerAddress = input.ReadBitString(num + 1);
                input.skipUnreadedBits();
                item.gTP_TEID = input.readOctetString(4);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    item.iE_Extensions = new List<ProtocolExtensionField>();
                    int nBits = 0x10;
                    int num5 = input.ReadBits(nBits) + 1;
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
    public class E_RABToBeSwitchedULItem
    {
        public void InitDefaults()
        {
        }

        public long e_RAB_ID { get; set; }

        public string gTP_TEID { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string transportLayerAddress { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public E_RABToBeSwitchedULItem Decode(BitArrayInputStream input)
            {
                E_RABToBeSwitchedULItem item = new E_RABToBeSwitchedULItem();
                item.InitDefaults();
                BitMaskStream stream = (input.ReadBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.ReadBit();
                item.e_RAB_ID = input.ReadBits(4);
                input.ReadBit();
                int num = input.ReadBits(8);
                input.skipUnreadedBits();
                item.transportLayerAddress = input.ReadBitString(num + 1);
                input.skipUnreadedBits();
                item.gTP_TEID = input.readOctetString(4);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    item.iE_Extensions = new List<ProtocolExtensionField>();
                    int nBits = 0x10;
                    int num5 = input.ReadBits(nBits) + 1;
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

}
