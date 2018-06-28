﻿using Lte.Domain.Common;
using System;
using Lte.Domain.Common.Types;

namespace TraceParser.Eutra
{
    [Serializable]
    public class RRCConnectionSetupComplete
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

                public RRCConnectionSetupComplete_r8_IEs rrcConnectionSetupComplete_r8 { get; set; }

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
                                type.rrcConnectionSetupComplete_r8 = RRCConnectionSetupComplete_r8_IEs.PerDecoder.Instance.Decode(input);
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
                            type.criticalExtensionsFuture 
                                = criticalExtensionsFuture_Type.PerDecoder.Instance.Decode(input);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RRCConnectionSetupComplete Decode(BitArrayInputStream input)
            {
                RRCConnectionSetupComplete complete = new RRCConnectionSetupComplete();
                complete.InitDefaults();
                complete.rrc_TransactionIdentifier = input.ReadBits(2);
                complete.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
                return complete;
            }
        }
    }

    [Serializable]
    public class RRCConnectionSetupComplete_r8_IEs
    {
        public void InitDefaults()
        {
        }

        public string dedicatedInfoNAS { get; set; }

        public RRCConnectionSetupComplete_v8a0_IEs nonCriticalExtension { get; set; }

        public RegisteredMME registeredMME { get; set; }

        public long selectedPLMN_Identity { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RRCConnectionSetupComplete_r8_IEs Decode(BitArrayInputStream input)
            {
                RRCConnectionSetupComplete_r8_IEs es = new RRCConnectionSetupComplete_r8_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                es.selectedPLMN_Identity = input.ReadBits(3) + 1;
                if (stream.Read())
                {
                    es.registeredMME = RegisteredMME.PerDecoder.Instance.Decode(input);
                }
                int nBits = input.ReadBits(8);
                es.dedicatedInfoNAS = input.readOctetString(nBits);
                if (stream.Read())
                {
                    es.nonCriticalExtension = RRCConnectionSetupComplete_v8a0_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class RRCConnectionSetupComplete_v1020_IEs
    {
        public void InitDefaults()
        {
        }

        public gummei_Type_r10_Enum? gummei_Type_r10 { get; set; }

        public logMeasAvailable_r10_Enum? logMeasAvailable_r10 { get; set; }

        public RRCConnectionSetupComplete_v1130_IEs nonCriticalExtension { get; set; }

        public rlf_InfoAvailable_r10_Enum? rlf_InfoAvailable_r10 { get; set; }

        public rn_SubframeConfigReq_r10_Enum? rn_SubframeConfigReq_r10 { get; set; }

        public enum gummei_Type_r10_Enum
        {
            native,
            mapped
        }

        public enum logMeasAvailable_r10_Enum
        {
            _true
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RRCConnectionSetupComplete_v1020_IEs Decode(BitArrayInputStream input)
            {
                int num2;
                RRCConnectionSetupComplete_v1020_IEs es = new RRCConnectionSetupComplete_v1020_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 5);
                if (stream.Read())
                {
                    num2 = 1;
                    es.gummei_Type_r10 = (gummei_Type_r10_Enum)input.ReadBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    es.rlf_InfoAvailable_r10 = (rlf_InfoAvailable_r10_Enum)input.ReadBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    es.logMeasAvailable_r10 = (logMeasAvailable_r10_Enum)input.ReadBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    es.rn_SubframeConfigReq_r10 = (rn_SubframeConfigReq_r10_Enum)input.ReadBits(num2);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = RRCConnectionSetupComplete_v1130_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }

        public enum rlf_InfoAvailable_r10_Enum
        {
            _true
        }

        public enum rn_SubframeConfigReq_r10_Enum
        {
            required,
            notRequired
        }
    }

    [Serializable]
    public class RRCConnectionSetupComplete_v1130_IEs
    {
        public void InitDefaults()
        {
        }

        public connEstFailInfoAvailable_r11_Enum? connEstFailInfoAvailable_r11 { get; set; }

        public RRCConnectionSetupComplete_v12xy_IEs nonCriticalExtension { get; set; }

        public enum connEstFailInfoAvailable_r11_Enum
        {
            _true
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RRCConnectionSetupComplete_v1130_IEs Decode(BitArrayInputStream input)
            {
                RRCConnectionSetupComplete_v1130_IEs es = new RRCConnectionSetupComplete_v1130_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    const int nBits = 1;
                    es.connEstFailInfoAvailable_r11 = (connEstFailInfoAvailable_r11_Enum)input.ReadBits(nBits);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = RRCConnectionSetupComplete_v12xy_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class RRCConnectionSetupComplete_v12xy_IEs
    {
        public void InitDefaults()
        {
        }

        public mobilityHistoryAvail_r12_Enum? mobilityHistoryAvail_r12 { get; set; }

        public mobilityState_r12_Enum? mobilityState_r12 { get; set; }

        public nonCriticalExtension_Type nonCriticalExtension { get; set; }

        public enum mobilityHistoryAvail_r12_Enum
        {
            _true
        }

        public enum mobilityState_r12_Enum
        {
            normal,
            medium,
            high,
            spare
        }

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

            public RRCConnectionSetupComplete_v12xy_IEs Decode(BitArrayInputStream input)
            {
                int num2;
                RRCConnectionSetupComplete_v12xy_IEs es = new RRCConnectionSetupComplete_v12xy_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    num2 = 2;
                    es.mobilityState_r12 = (mobilityState_r12_Enum)input.ReadBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    es.mobilityHistoryAvail_r12 = (mobilityHistoryAvail_r12_Enum)input.ReadBits(num2);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = nonCriticalExtension_Type.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class RRCConnectionSetupComplete_v8a0_IEs
    {
        public void InitDefaults()
        {
        }

        public string lateNonCriticalExtension { get; set; }

        public RRCConnectionSetupComplete_v1020_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RRCConnectionSetupComplete_v8a0_IEs Decode(BitArrayInputStream input)
            {
                RRCConnectionSetupComplete_v8a0_IEs es = new RRCConnectionSetupComplete_v8a0_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    int nBits = input.ReadBits(8);
                    es.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = RRCConnectionSetupComplete_v1020_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

}
