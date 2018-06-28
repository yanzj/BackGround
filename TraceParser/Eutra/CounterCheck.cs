using Lte.Domain.Common;
using System;
using System.Collections.Generic;
using Lte.Domain.Common.Types;
using TraceParser.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class CounterCheck : TraceConfig
    {
        public criticalExtensions_Type criticalExtensions { get; set; }

        public long rrc_TransactionIdentifier { get; set; }

        [Serializable]
        public class criticalExtensions_Type : TraceConfig
        {
            public c1_Type c1 { get; set; }

            public criticalExtensionsFuture_Type criticalExtensionsFuture { get; set; }

            [Serializable]
            public class c1_Type : TraceConfig
            {
                public CounterCheck_r8_IEs counterCheck_r8 { get; set; }

                public object spare1 { get; set; }

                public object spare2 { get; set; }

                public object spare3 { get; set; }

                public class PerDecoder : DecoderBase<c1_Type>
                {
                    public static readonly PerDecoder Instance = new PerDecoder();
                    
                    protected override void ProcessConfig(c1_Type config, BitArrayInputStream input)
                    {
                        switch (input.ReadBits(2))
                        {
                            case 0:
                                config.counterCheck_r8 = CounterCheck_r8_IEs.PerDecoder.Instance.Decode(input);
                                return;

                            case 1:
                            case 2:
                            case 3:
                                return;
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
                        var type = new criticalExtensionsFuture_Type();
                        type.InitDefaults();
                        return type;
                    }
                }
            }

            public class PerDecoder : DecoderBase<criticalExtensions_Type>
            {
                public static readonly PerDecoder Instance = new PerDecoder();
                
                protected override void ProcessConfig(criticalExtensions_Type config, BitArrayInputStream input)
                {
                    switch (input.ReadBits(1))
                    {
                        case 0:
                            config.c1 = c1_Type.PerDecoder.Instance.Decode(input);
                            return;

                        case 1:
                            config.criticalExtensionsFuture = criticalExtensionsFuture_Type.PerDecoder.Instance.Decode(input);
                            return;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        public class PerDecoder : DecoderBase<CounterCheck>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CounterCheck config, BitArrayInputStream input)
            {
                config.rrc_TransactionIdentifier = input.ReadBits(2);
                config.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
            }
        }
    }

    [Serializable]
    public class CounterCheck_r8_IEs : TraceConfig
    {
        public List<DRB_CountMSB_Info> drb_CountMSB_InfoList { get; set; }

        public CounterCheck_v8a0_IEs nonCriticalExtension { get; set; }

        public class PerDecoder : DecoderBase<CounterCheck_r8_IEs>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CounterCheck_r8_IEs config, BitArrayInputStream input)
            {
                var stream = new BitMaskStream(input, 1);
                config.drb_CountMSB_InfoList = new List<DRB_CountMSB_Info>();
                var num3 = input.ReadBits(4) + 1;
                for (var i = 0; i < num3; i++)
                {
                    var item = DRB_CountMSB_Info.PerDecoder.Instance.Decode(input);
                    config.drb_CountMSB_InfoList.Add(item);
                }
                if (stream.Read())
                {
                    config.nonCriticalExtension = CounterCheck_v8a0_IEs.PerDecoder.Instance.Decode(input);
                }
            }
        }
    }

    [Serializable]
    public class CounterCheck_v8a0_IEs : TraceConfig
    {
        public string lateNonCriticalExtension { get; set; }

        public nonCriticalExtension_Type nonCriticalExtension { get; set; }

        [Serializable]
        public class nonCriticalExtension_Type : TraceConfig
        {
            public class PerDecoder : DecoderBase<nonCriticalExtension_Type>
            {
                public static readonly PerDecoder Instance = new PerDecoder();
                
                protected override void ProcessConfig(nonCriticalExtension_Type config, BitArrayInputStream input)
                {
                    
                }
            }
        }

        public class PerDecoder : DecoderBase<CounterCheck_v8a0_IEs>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CounterCheck_v8a0_IEs config, BitArrayInputStream input)
            {
                var stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    var nBits = input.ReadBits(8);
                    config.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                if (stream.Read())
                {
                    config.nonCriticalExtension = nonCriticalExtension_Type.PerDecoder.Instance.Decode(input);
                }
            }
        }
    }

    [Serializable]
    public class CounterCheckResponse : TraceConfig
    {
        public criticalExtensions_Type criticalExtensions { get; set; }

        public long rrc_TransactionIdentifier { get; set; }

        [Serializable]
        public class criticalExtensions_Type : TraceConfig
        {
            public CounterCheckResponse_r8_IEs counterCheckResponse_r8 { get; set; }

            public criticalExtensionsFuture_Type criticalExtensionsFuture { get; set; }

            [Serializable]
            public class criticalExtensionsFuture_Type : TraceConfig
            {
                public class PerDecoder : DecoderBase<criticalExtensionsFuture_Type>
                {
                    public static readonly PerDecoder Instance = new PerDecoder();
                    
                    protected override void ProcessConfig(criticalExtensionsFuture_Type config, BitArrayInputStream input)
                    {
                        
                    }
                }
            }

            public class PerDecoder : DecoderBase<criticalExtensions_Type>
            {
                public static readonly PerDecoder Instance = new PerDecoder();
                
                protected override void ProcessConfig(criticalExtensions_Type config, BitArrayInputStream input)
                {
                    switch (input.ReadBits(1))
                    {
                        case 0:
                            config.counterCheckResponse_r8 = CounterCheckResponse_r8_IEs.PerDecoder.Instance.Decode(input);
                            return;

                        case 1:
                            config.criticalExtensionsFuture = criticalExtensionsFuture_Type.PerDecoder.Instance.Decode(input);
                            return;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        public class PerDecoder : DecoderBase<CounterCheckResponse>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CounterCheckResponse config, BitArrayInputStream input)
            {
                config.rrc_TransactionIdentifier = input.ReadBits(2);
                config.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
            }
        }
    }

    [Serializable]
    public class CounterCheckResponse_r8_IEs : TraceConfig
    {
        public List<DRB_CountInfo> drb_CountInfoList { get; set; }

        public CounterCheckResponse_v8a0_IEs nonCriticalExtension { get; set; }

        public class PerDecoder : DecoderBase<CounterCheckResponse_r8_IEs>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CounterCheckResponse_r8_IEs config, BitArrayInputStream input)
            {
                var stream = new BitMaskStream(input, 1);
                config.drb_CountInfoList = new List<DRB_CountInfo>();
                var num3 = input.ReadBits(4);
                for (var i = 0; i < num3; i++)
                {
                    var item = DRB_CountInfo.PerDecoder.Instance.Decode(input);
                    config.drb_CountInfoList.Add(item);
                }
                if (stream.Read())
                {
                    config.nonCriticalExtension = CounterCheckResponse_v8a0_IEs.PerDecoder.Instance.Decode(input);
                }
            }
        }
    }

    [Serializable]
    public class CounterCheckResponse_v8a0_IEs : TraceConfig
    {
        public string lateNonCriticalExtension { get; set; }

        public nonCriticalExtension_Type nonCriticalExtension { get; set; }

        [Serializable]
        public class nonCriticalExtension_Type : TraceConfig
        {
            public class PerDecoder : DecoderBase<nonCriticalExtension_Type>
            {
                public static readonly PerDecoder Instance = new PerDecoder();
                
                protected override void ProcessConfig(nonCriticalExtension_Type config, BitArrayInputStream input)
                {
                    
                }
            }
        }

        public class PerDecoder : DecoderBase<CounterCheckResponse_v8a0_IEs>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(CounterCheckResponse_v8a0_IEs config, BitArrayInputStream input)
            {
                var stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    var nBits = input.ReadBits(8);
                    config.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                if (stream.Read())
                {
                    config.nonCriticalExtension = nonCriticalExtension_Type.PerDecoder.Instance.Decode(input);
                }
            }
        }

    }

    [Serializable]
    public class CountingRequestInfo_r10 : TraceConfig
    {
        public TMGI_r9 tmgi_r10 { get; set; }

        public class PerDecoder : DecoderBase<CountingRequestInfo_r10>
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            protected override void ProcessConfig(CountingRequestInfo_r10 config, BitArrayInputStream input)
            {
                input.ReadBit();
                config.tmgi_r10 = TMGI_r9.PerDecoder.Instance.Decode(input);
            }
        }
    }

    [Serializable]
    public class CountingResponseInfo_r10 : TraceConfig
    {
        public long countingResponseService_r10 { get; set; }

        public class PerDecoder : DecoderBase<CountingResponseInfo_r10>
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            protected override void ProcessConfig(CountingResponseInfo_r10 config, BitArrayInputStream input)
            {
                input.ReadBit();
                config.countingResponseService_r10 = input.ReadBits(4);
            }
        }
    }
}
