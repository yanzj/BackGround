using Lte.Domain.Common;
using System;
using System.Collections.Generic;
using Lte.Domain.Common.Types;

namespace TraceParser.X2ap
{
    [Serializable]
    public class PrivateIE_Container
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<PrivateIE_Field> Decode(BitArrayInputStream input)
            {
                return new List<PrivateIE_Field>();
            }
        }
    }

    [Serializable]
    public class PrivateIE_Field
    {
        public void InitDefaults()
        {
        }

        public Criticality criticality { get; set; }

        public PrivateIE_ID id { get; set; }

        public object value { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PrivateIE_Field Decode(BitArrayInputStream input)
            {
                PrivateIE_Field field = new PrivateIE_Field();
                field.InitDefaults();
                input.skipUnreadedBits();
                int nBits = 0;
                while (true)
                {
                    switch (input.ReadBit())
                    {
                        case 0:
                            nBits += input.ReadBits(7);
                            goto Label_00A5;

                        case 1:
                            switch (input.ReadBit())
                            {
                                case 0:
                                    nBits += input.ReadBits(14);
                                    goto Label_00A5;

                                case 1:
                                    input.ReadBits(2);
                                    nBits += input.ReadBits(4) * 0x400;
                                    break;
                            }
                            break;
                    }
                }
            Label_00A5:
                long num3 = input.Position;
                try
                {
                    field.id = PrivateIE_ID.PerDecoder.Instance.Decode(input);
                    input.skipUnreadedBits();
                }
                catch (Exception)
                {
                    input.skipUnreadedBits();
                    input.Position = num3;
                    field.id.global = input.readOctetString(nBits);
                }
                if (input.Position != (num3 + nBits))
                {
                    input.Position = num3 + nBits;
                }
                int num4 = 2;
                field.criticality = (Criticality)input.ReadBits(num4);
                input.skipUnreadedBits();
                nBits = 0;
                while (true)
                {
                    switch (input.ReadBit())
                    {
                        case 0:
                            nBits += input.ReadBits(7);
                            goto Label_01AE;

                        case 1:
                            switch (input.ReadBit())
                            {
                                case 0:
                                    nBits += input.ReadBits(14);
                                    goto Label_01AE;

                                case 1:
                                    input.ReadBits(2);
                                    nBits += input.ReadBits(4) * 0x400;
                                    break;
                            }
                            break;
                    }
                }
            Label_01AE:
                num3 = input.Position;
                try
                {
                    field.value = X2AP_PRIVATE_IES.Switcher(field.id, "Value", input);
                    input.skipUnreadedBits();
                }
                catch (Exception)
                {
                    input.skipUnreadedBits();
                    input.Position = num3;
                    field.value = input.readOctetString(nBits);
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
    public class PrivateIE_ID
    {
        public void InitDefaults()
        {
        }

        public string global { get; set; }

        public long local { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PrivateIE_ID Decode(BitArrayInputStream input)
            {
                PrivateIE_ID eie_id = new PrivateIE_ID();
                eie_id.InitDefaults();
                switch (input.ReadBits(1))
                {
                    case 0:
                        {
                            int num4 = input.ReadBits(1) + 1;
                            input.skipUnreadedBits();
                            eie_id.local = input.ReadBits(num4 * 8);
                            return eie_id;
                        }
                    case 1:
                        return eie_id;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }

    [Serializable]
    public class EUTRA_Mode_Info
    {
        public void InitDefaults()
        {
        }

        public FDD_Info fDD { get; set; }

        public TDD_Info tDD { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public EUTRA_Mode_Info Decode(BitArrayInputStream input)
            {
                EUTRA_Mode_Info info = new EUTRA_Mode_Info();
                info.InitDefaults();
                input.ReadBit();
                switch (input.ReadBits(1))
                {
                    case 0:
                        info.fDD = FDD_Info.PerDecoder.Instance.Decode(input);
                        return info;

                    case 1:
                        info.tDD = TDD_Info.PerDecoder.Instance.Decode(input);
                        return info;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }

    [Serializable]
    public class EUTRANCellIdentifier
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                return input.ReadBitString(0x1c);
            }
        }
    }

    [Serializable]
    public class EUTRANTraceID
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                return input.readOctetString(8);
            }
        }
    }

    [Serializable]
    public class FDD_Info
    {
        public void InitDefaults()
        {
        }

        public long dL_EARFCN { get; set; }

        public Transmission_Bandwidth dL_Transmission_Bandwidth { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public long uL_EARFCN { get; set; }

        public Transmission_Bandwidth uL_Transmission_Bandwidth { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public FDD_Info Decode(BitArrayInputStream input)
            {
                FDD_Info info = new FDD_Info();
                info.InitDefaults();
                BitMaskStream stream = (input.ReadBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                int nBits = input.ReadBits(1) + 1;
                input.skipUnreadedBits();
                info.uL_EARFCN = input.ReadBits(nBits * 8);
                nBits = input.ReadBits(1) + 1;
                input.skipUnreadedBits();
                info.dL_EARFCN = input.ReadBits(nBits * 8);
                nBits = (input.ReadBit() == 0) ? 3 : 3;
                info.uL_Transmission_Bandwidth = (Transmission_Bandwidth)input.ReadBits(nBits);
                nBits = (input.ReadBit() == 0) ? 3 : 3;
                info.dL_Transmission_Bandwidth = (Transmission_Bandwidth)input.ReadBits(nBits);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    info.iE_Extensions = new List<ProtocolExtensionField>();
                    nBits = 0x10;
                    int num5 = input.ReadBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        info.iE_Extensions.Add(item);
                    }
                }
                return info;
            }
        }
    }

    [Serializable]
    public class TDD_Info
    {
        public void InitDefaults()
        {
        }

        public long eARFCN { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public SpecialSubframe_Info specialSubframe_Info { get; set; }

        public SubframeAssignment subframeAssignment { get; set; }

        public Transmission_Bandwidth transmission_Bandwidth { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public TDD_Info Decode(BitArrayInputStream input)
            {
                TDD_Info info = new TDD_Info();
                info.InitDefaults();
                BitMaskStream stream = (input.ReadBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                int nBits = input.ReadBits(1) + 1;
                input.skipUnreadedBits();
                info.eARFCN = input.ReadBits(nBits * 8);
                nBits = (input.ReadBit() == 0) ? 3 : 3;
                info.transmission_Bandwidth = (Transmission_Bandwidth)input.ReadBits(nBits);
                nBits = (input.ReadBit() == 0) ? 3 : 3;
                info.subframeAssignment = (SubframeAssignment)input.ReadBits(nBits);
                info.specialSubframe_Info = SpecialSubframe_Info.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    info.iE_Extensions = new List<ProtocolExtensionField>();
                    nBits = 0x10;
                    int num5 = input.ReadBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        info.iE_Extensions.Add(item);
                    }
                }
                return info;
            }
        }
    }

    [Serializable]
    public class SpecialSubframe_Info
    {
        public void InitDefaults()
        {
        }

        public CyclicPrefixDL cyclicPrefixDL { get; set; }

        public CyclicPrefixUL cyclicPrefixUL { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public SpecialSubframePatterns specialSubframePatterns { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SpecialSubframe_Info Decode(BitArrayInputStream input)
            {
                SpecialSubframe_Info info = new SpecialSubframe_Info();
                info.InitDefaults();
                BitMaskStream stream = (input.ReadBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                int nBits = (input.ReadBit() == 0) ? 4 : 4;
                info.specialSubframePatterns = (SpecialSubframePatterns)input.ReadBits(nBits);
                nBits = 1;
                info.cyclicPrefixDL = (CyclicPrefixDL)input.ReadBits(nBits);
                nBits = 1;
                info.cyclicPrefixUL = (CyclicPrefixUL)input.ReadBits(nBits);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    info.iE_Extensions = new List<ProtocolExtensionField>();
                    nBits = 0x10;
                    int num5 = input.ReadBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        info.iE_Extensions.Add(item);
                    }
                }
                return info;
            }
        }
    }

    [Serializable]
    public class PrivateMessage
    {
        public void InitDefaults()
        {
        }

        public List<PrivateIE_Field> privateIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PrivateMessage Decode(BitArrayInputStream input)
            {
                PrivateMessage message = new PrivateMessage();
                message.InitDefaults();
                input.ReadBit();
                input.skipUnreadedBits();
                message.privateIEs = new List<PrivateIE_Field>();
                const int nBits = 0x10;
                int num5 = input.ReadBits(nBits) + 1;
                for (int i = 0; i < num5; i++)
                {
                    PrivateIE_Field item = PrivateIE_Field.PerDecoder.Instance.Decode(input);
                    message.privateIEs.Add(item);
                }
                return message;
            }
        }
    }

}
