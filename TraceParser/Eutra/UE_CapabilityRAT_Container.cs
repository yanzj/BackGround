using Lte.Domain.Common;
using System;
using System.Collections.Generic;
using System.IO;
using Lte.Domain.Common.Types;

namespace TraceParser.Eutra
{
    [Serializable]
    public class UE_CapabilityRAT_Container
    {
        private static byte[] GetBufferByString(string HexWithOutSp)
        {
            int result;
            Math.DivRem(HexWithOutSp.Length, 2, out result);
            List<byte> list = new List<byte>();
            if (result == 0)
            {
                for (int i = 0; i < HexWithOutSp.Length; i++)
                {
                    int num3 = Convert.ToInt16(HexWithOutSp.Substring(i, 2), 0x10);
                    list.Add((byte) num3);
                    i++;
                }
            }
            return list.ToArray();
        }

        private static BitArrayInputStream GetInputStream(string HexWithOutSp)
        {
            Stream byteStream = new MemoryStream();
            byte[] bufferByString = GetBufferByString(HexWithOutSp);
            byteStream.Write(bufferByString, 0, bufferByString.Length);
            byteStream.Position = 0L;
            return new BitArrayInputStream(byteStream) { Position = 0L };
        }

        public void InitDefaults()
        {
        }

        public RAT_Type rat_Type { get; set; }


        public dynamic ueCapabilityRAT_Container { get; set; }

        private class A21MobileSubscriptionInformation
        {
            private string _A21ElementIdentifier = "0BH";

            public string A21ElementIdentifier
            {
                get
                {
                    return _A21ElementIdentifier;
                }
                set
                {
                    _A21ElementIdentifier = value;
                }
            }

            public int AllBandClassesIncluded { get; set; }

            public List<BandClass> BandClassList { get; set; }

            public int CurrentBandSubclass { get; set; }

            public int Length { get; set; }

            public string RecordIdentifier { get; set; }

            public int RecordLength { get; set; }

            public class BandClass
            {
                public int AllBandClassesIncluded { get; set; }

                public int BandClassValue { get; set; }

                public int BandSubclassLength { get; set; }

                public List<SubClasses> SubClassesList { get; set; }
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public A21MobileSubscriptionInformation Decode(BitArrayInputStream input)
                {
                    A21MobileSubscriptionInformation information = new A21MobileSubscriptionInformation {
                        A21ElementIdentifier = input.readOctetString(1),
                        Length = input.ReadBits(8),
                        RecordIdentifier = input.readOctetString(1),
                        RecordLength = input.ReadBits(8),
                        AllBandClassesIncluded = input.ReadBits(1),
                        CurrentBandSubclass = input.ReadBits(7),
                        BandClassList = new List<BandClass>()
                    };
                    for (int i = 1; i < information.RecordLength; i++)
                    {
                        BandClass item = new BandClass {
                            BandClassValue = input.ReadBits(8),
                            AllBandClassesIncluded = input.ReadBits(1)
                        };
                        input.ReadBits(3);
                        item.BandSubclassLength = input.ReadBits(4);
                        i += 2;
                        item.SubClassesList = new List<SubClasses>();
                        for (int j = 0; j < item.BandSubclassLength; j++)
                        {
                            SubClasses classes = new SubClasses {
                                SC7 = input.ReadBit(),
                                SC6 = input.ReadBit(),
                                SC5 = input.ReadBit(),
                                SC4 = input.ReadBit(),
                                SC3 = input.ReadBit(),
                                SC2 = input.ReadBit(),
                                SC1 = input.ReadBit(),
                                SC0 = input.ReadBit()
                            };
                            item.SubClassesList.Add(classes);
                            i++;
                        }
                        information.BandClassList.Add(item);
                    }
                    return information;
                }
            }

            public class SubClasses
            {
                public int SC0 { get; set; }

                public int SC1 { get; set; }

                public int SC2 { get; set; }

                public int SC3 { get; set; }

                public int SC4 { get; set; }

                public int SC5 { get; set; }

                public int SC6 { get; set; }

                public int SC7 { get; set; }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UE_CapabilityRAT_Container Decode(BitArrayInputStream input)
            {
                string str;
                int nBits = 0;
                UE_CapabilityRAT_Container container = new UE_CapabilityRAT_Container();
                container.InitDefaults();
                int num2 = (input.ReadBit() == 0) ? 3 : 3;
                container.rat_Type = (RAT_Type) input.ReadBits(num2);
                nBits = input.ReadBits(8);
                if ((container.rat_Type == RAT_Type.eutra) && (nBits > 0))
                {
                    str = input.readOctetString(nBits);
                    BitArrayInputStream inputStream = GetInputStream(str.Remove(str.Length - 2) + "00000000");
                    container.ueCapabilityRAT_Container = UE_EUTRA_Capability.PerDecoder.Instance.Decode(inputStream);
                    return container;
                }
                if (container.rat_Type == RAT_Type.cdma2000_1XRTT)
                {
                    container.ueCapabilityRAT_Container = A21MobileSubscriptionInformation.PerDecoder.Instance.Decode(input);
                    return container;
                }
                str = input.readOctetString(nBits);
                container.ueCapabilityRAT_Container = str;
                return container;
            }
        }
    }
}
