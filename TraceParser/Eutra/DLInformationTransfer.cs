﻿using Lte.Domain.Common;
using System;
using Lte.Domain.Common.Types;

namespace TraceParser.Eutra
{
    [Serializable]
    public class DLInformationTransfer
    {
        public void InitDefaults()
        {
        }

        public criticalExtensions_Type criticalExtensions { get; set; }

        public long rrc_TransactionIdentifier { get; set; }

        [Serializable]
        public class criticalExtensions_Type
        {
            public void InitDefaults()
            {
            }

            public c1_Type c1 { get; set; }

            public criticalExtensionsFuture_Type criticalExtensionsFuture { get; set; }

            [Serializable]
            public class c1_Type
            {
                public void InitDefaults()
                {
                }

                public DLInformationTransfer_r8_IEs dlInformationTransfer_r8 { get; set; }

                public object spare1 { get; set; }

                public object spare2 { get; set; }

                public object spare3 { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public c1_Type Decode(BitArrayInputStream input)
                    {
                        c1_Type type = new c1_Type();
                        type.InitDefaults();
                        switch (input.ReadBits(2))
                        {
                            case 0:
                                type.dlInformationTransfer_r8 = DLInformationTransfer_r8_IEs.PerDecoder.Instance.Decode(input);
                                return type;

                            case 1:
                                return type;

                            case 2:
                                return type;

                            case 3:
                                return type;
                        }
                        throw new Exception(GetType().Name + ":NoChoice had been choose");
                    }
                }
            }

            [Serializable]
            public class criticalExtensionsFuture_Type
            {
                public void InitDefaults()
                {
                }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public criticalExtensionsFuture_Type Decode(BitArrayInputStream input)
                    {
                        criticalExtensionsFuture_Type type = new criticalExtensionsFuture_Type();
                        type.InitDefaults();
                        return type;
                    }
                }
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public criticalExtensions_Type Decode(BitArrayInputStream input)
                {
                    criticalExtensions_Type type = new criticalExtensions_Type();
                    type.InitDefaults();
                    switch (input.ReadBits(1))
                    {
                        case 0:
                            type.c1 = c1_Type.PerDecoder.Instance.Decode(input);
                            return type;

                        case 1:
                            type.criticalExtensionsFuture = criticalExtensionsFuture_Type.PerDecoder.Instance.Decode(input);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public DLInformationTransfer Decode(BitArrayInputStream input)
            {
                DLInformationTransfer transfer = new DLInformationTransfer();
                transfer.InitDefaults();
                transfer.rrc_TransactionIdentifier = input.ReadBits(2);
                transfer.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
                return transfer;
            }
        }
    }

    [Serializable]
    public class DLInformationTransfer_r8_IEs
    {
        public void InitDefaults()
        {
        }

        public dedicatedInfoType_Type dedicatedInfoType { get; set; }

        public DLInformationTransfer_v8a0_IEs nonCriticalExtension { get; set; }

        [Serializable]
        public class dedicatedInfoType_Type
        {
            public void InitDefaults()
            {
            }

            public string dedicatedInfoCDMA2000_1XRTT { get; set; }

            public string dedicatedInfoCDMA2000_HRPD { get; set; }

            public string dedicatedInfoNAS { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public dedicatedInfoType_Type Decode(BitArrayInputStream input)
                {
                    int nBits;
                    dedicatedInfoType_Type type = new dedicatedInfoType_Type();
                    type.InitDefaults();
                    switch (input.ReadBits(2))
                    {
                        case 0:
                            nBits = input.ReadBits(8);
                            type.dedicatedInfoNAS = input.readOctetString(nBits);
                            return type;

                        case 1:
                            nBits = input.ReadBits(8);
                            type.dedicatedInfoCDMA2000_1XRTT = input.readOctetString(nBits);
                            return type;

                        case 2:
                            nBits = input.ReadBits(8);
                            type.dedicatedInfoCDMA2000_HRPD = input.readOctetString(nBits);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public DLInformationTransfer_r8_IEs Decode(BitArrayInputStream input)
            {
                DLInformationTransfer_r8_IEs es = new DLInformationTransfer_r8_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                es.dedicatedInfoType = dedicatedInfoType_Type.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    es.nonCriticalExtension = DLInformationTransfer_v8a0_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class DLInformationTransfer_v8a0_IEs
    {
        public void InitDefaults()
        {
        }

        public string lateNonCriticalExtension { get; set; }

        public nonCriticalExtension_Type nonCriticalExtension { get; set; }

        [Serializable]
        public class nonCriticalExtension_Type
        {
            public void InitDefaults()
            {
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public nonCriticalExtension_Type Decode(BitArrayInputStream input)
                {
                    nonCriticalExtension_Type type = new nonCriticalExtension_Type();
                    type.InitDefaults();
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public DLInformationTransfer_v8a0_IEs Decode(BitArrayInputStream input)
            {
                DLInformationTransfer_v8a0_IEs es = new DLInformationTransfer_v8a0_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    int nBits = input.ReadBits(8);
                    es.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = nonCriticalExtension_Type.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

}
