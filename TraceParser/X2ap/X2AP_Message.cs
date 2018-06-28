using Lte.Domain.Common;
using System;
using System.Collections.Generic;
using Lte.Domain.Common.Types;
using TraceParser.Common;

namespace TraceParser.X2ap
{
    [Serializable]
    public class X2AP_Message
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                int nBits = 0;
                while (true)
                {
                    switch (input.ReadBit())
                    {
                        case 0:
                            nBits += input.ReadBits(7);
                            goto Label_0096;

                        case 1:
                            switch (input.ReadBit())
                            {
                                case 0:
                                    nBits += input.ReadBits(14);
                                    goto Label_0096;

                                case 1:
                                    input.ReadBits(2);
                                    nBits += input.ReadBits(4) * 0x400;
                                    break;
                            }
                            break;
                    }
                }
            Label_0096:
                return input.readOctetString(nBits);
            }
        }
    }

    [Serializable]
    public class X2AP_PDU : TraceConfig
    {
        public InitiatingMessage initiatingMessage { get; set; }

        public SuccessfulOutcome successfulOutcome { get; set; }

        public UnsuccessfulOutcome unsuccessfulOutcome { get; set; }

        public class PerDecoder : DecoderBase<X2AP_PDU>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(X2AP_PDU config, BitArrayInputStream input)
            {
                InitDefaults();
                input.ReadBit();
                switch (input.ReadBits(2))
                {
                    case 0:
                        config.initiatingMessage = InitiatingMessage.PerDecoder.Instance.Decode(input);
                        return;
                    case 1:
                        config.successfulOutcome = SuccessfulOutcome.PerDecoder.Instance.Decode(input);
                        return;
                    case 2:
                        config.unsuccessfulOutcome = UnsuccessfulOutcome.PerDecoder.Instance.Decode(input);
                        return;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }

    [Serializable]
    public class X2AP_PRIVATE_IES
    {
        public static object Switcher(object unique, string membername, BitArrayInputStream input)
        {
            return null;
        }

        public Criticality criticality { get; set; }

        public PrivateIE_ID id { get; set; }

        public Presence presence { get; set; }

        public object Value { get; set; }
    }

    [Serializable]
    public class X2MessageTransfer
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public X2MessageTransfer Decode(BitArrayInputStream input)
            {
                X2MessageTransfer transfer = new X2MessageTransfer();
                transfer.InitDefaults();
                input.ReadBit();
                input.skipUnreadedBits();
                transfer.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.ReadBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    transfer.protocolIEs.Add(item);
                }
                return transfer;
            }
        }
    }

    [Serializable]
    public class X2Release
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public X2Release Decode(BitArrayInputStream input)
            {
                X2Release release = new X2Release();
                release.InitDefaults();
                input.ReadBit();
                input.skipUnreadedBits();
                release.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.ReadBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    release.protocolIEs.Add(item);
                }
                return release;
            }
        }
    }

    [Serializable]
    public class X2SetupFailure
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public X2SetupFailure Decode(BitArrayInputStream input)
            {
                X2SetupFailure failure = new X2SetupFailure();
                failure.InitDefaults();
                input.ReadBit();
                input.skipUnreadedBits();
                failure.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.ReadBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    failure.protocolIEs.Add(item);
                }
                return failure;
            }
        }
    }

    [Serializable]
    public class X2SetupRequest
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public X2SetupRequest Decode(BitArrayInputStream input)
            {
                X2SetupRequest request = new X2SetupRequest();
                request.InitDefaults();
                input.ReadBit();
                input.skipUnreadedBits();
                request.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.ReadBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    request.protocolIEs.Add(item);
                }
                return request;
            }
        }
    }

    [Serializable]
    public class X2SetupResponse
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public X2SetupResponse Decode(BitArrayInputStream input)
            {
                X2SetupResponse response = new X2SetupResponse();
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

}
