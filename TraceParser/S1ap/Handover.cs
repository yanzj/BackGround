using Lte.Domain.Common;
using System;
using System.Collections.Generic;
using Lte.Domain.Common.Types;
using TraceParser.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class HandoverCancel : TraceConfig
    {
        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder : DecoderBase<HandoverCancel>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(HandoverCancel config, BitArrayInputStream input)
            {
                InitDefaults();
                input.ReadBit();
                input.skipUnreadedBits();
                config.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                var num5 = input.ReadBits(nBits);
                for (var i = 0; i < num5; i++)
                {
                    var item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    config.protocolIEs.Add(item);
                }
            }
        }
    }

    [Serializable]
    public class HandoverCancelAcknowledge : TraceConfig
    {
        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder : DecoderBase<HandoverCancelAcknowledge>
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            protected override void ProcessConfig(HandoverCancelAcknowledge config, BitArrayInputStream input)
            {
                InitDefaults();
                input.ReadBit();
                input.skipUnreadedBits();
                config.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                var num5 = input.ReadBits(nBits);
                for (var i = 0; i < num5; i++)
                {
                    var item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    config.protocolIEs.Add(item);
                }
            }
        }
    }

    [Serializable]
    public class HandoverCommand
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public HandoverCommand Decode(BitArrayInputStream input)
            {
                var command = new HandoverCommand();
                command.InitDefaults();
                input.ReadBit();
                input.skipUnreadedBits();
                command.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                var num5 = input.ReadBits(nBits);
                for (var i = 0; i < num5; i++)
                {
                    var item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    command.protocolIEs.Add(item);
                }
                return command;
            }
        }
    }

    [Serializable]
    public class HandoverFailure
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public HandoverFailure Decode(BitArrayInputStream input)
            {
                var failure = new HandoverFailure();
                failure.InitDefaults();
                input.ReadBit();
                input.skipUnreadedBits();
                failure.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                var num5 = input.ReadBits(nBits);
                for (var i = 0; i < num5; i++)
                {
                    var item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    failure.protocolIEs.Add(item);
                }
                return failure;
            }
        }
    }

    [Serializable]
    public class HandoverNotify
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public HandoverNotify Decode(BitArrayInputStream input)
            {
                var notify = new HandoverNotify();
                notify.InitDefaults();
                input.ReadBit();
                input.skipUnreadedBits();
                notify.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                var num5 = input.ReadBits(nBits);
                for (var i = 0; i < num5; i++)
                {
                    var item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    notify.protocolIEs.Add(item);
                }
                return notify;
            }
        }
    }

    [Serializable]
    public class HandoverPreparationFailure
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public HandoverPreparationFailure Decode(BitArrayInputStream input)
            {
                var failure = new HandoverPreparationFailure();
                failure.InitDefaults();
                input.ReadBit();
                input.skipUnreadedBits();
                failure.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                var num5 = input.ReadBits(nBits);
                for (var i = 0; i < num5; i++)
                {
                    var item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    failure.protocolIEs.Add(item);
                }
                return failure;
            }
        }
    }

    [Serializable]
    public class HandoverRequest
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public HandoverRequest Decode(BitArrayInputStream input)
            {
                var request = new HandoverRequest();
                request.InitDefaults();
                input.ReadBit();
                input.skipUnreadedBits();
                request.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                var num5 = input.ReadBits(nBits);
                for (var i = 0; i < num5; i++)
                {
                    var item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    request.protocolIEs.Add(item);
                }
                return request;
            }
        }
    }

    [Serializable]
    public class HandoverRequestAcknowledge
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public HandoverRequestAcknowledge Decode(BitArrayInputStream input)
            {
                var acknowledge = new HandoverRequestAcknowledge();
                acknowledge.InitDefaults();
                input.ReadBit();
                input.skipUnreadedBits();
                acknowledge.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                var num5 = input.ReadBits(nBits);
                for (var i = 0; i < num5; i++)
                {
                    var item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    acknowledge.protocolIEs.Add(item);
                }
                return acknowledge;
            }
        }
    }

    [Serializable]
    public class HandoverRequired
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public HandoverRequired Decode(BitArrayInputStream input)
            {
                var required = new HandoverRequired();
                required.InitDefaults();
                input.ReadBit();
                input.skipUnreadedBits();
                required.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                var num5 = input.ReadBits(nBits);
                for (var i = 0; i < num5; i++)
                {
                    var item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    required.protocolIEs.Add(item);
                }
                return required;
            }
        }
    }

    [Serializable]
    public class HandoverRestrictionList
    {
        public void InitDefaults()
        {
        }

        public List<string> equivalentPLMNs { get; set; }

        public ForbiddenInterRATs? forbiddenInterRATs { get; set; }

        public List<ForbiddenLAs_Item> forbiddenLAs { get; set; }

        public List<ForbiddenTAs_Item> forbiddenTAs { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string servingPLMN { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public HandoverRestrictionList Decode(BitArrayInputStream input)
            {
                int num4;
                var list = new HandoverRestrictionList();
                list.InitDefaults();
                var stream = (input.ReadBit() != 0) ? new BitMaskStream(input, 5) : new BitMaskStream(input, 5);
                input.skipUnreadedBits();
                list.servingPLMN = input.readOctetString(3);
                if (stream.Read())
                {
                    list.equivalentPLMNs = new List<string>();
                    num4 = 4;
                    var num5 = input.ReadBits(num4) + 1;
                    for (var i = 0; i < num5; i++)
                    {
                        input.skipUnreadedBits();
                        var str = input.readOctetString(3);
                        list.equivalentPLMNs.Add(str);
                    }
                }
                if (stream.Read())
                {
                    list.forbiddenTAs = new List<ForbiddenTAs_Item>();
                    num4 = 4;
                    var num7 = input.ReadBits(num4) + 1;
                    for (var j = 0; j < num7; j++)
                    {
                        var item = ForbiddenTAs_Item.PerDecoder.Instance.Decode(input);
                        list.forbiddenTAs.Add(item);
                    }
                }
                if (stream.Read())
                {
                    list.forbiddenLAs = new List<ForbiddenLAs_Item>();
                    num4 = 4;
                    var num9 = input.ReadBits(num4) + 1;
                    for (var k = 0; k < num9; k++)
                    {
                        var item2 = ForbiddenLAs_Item.PerDecoder.Instance.Decode(input);
                        list.forbiddenLAs.Add(item2);
                    }
                }
                if (stream.Read())
                {
                    num4 = (input.ReadBit() == 0) ? 3 : 3;
                    list.forbiddenInterRATs = (ForbiddenInterRATs)input.ReadBits(num4);
                }
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    list.iE_Extensions = new List<ProtocolExtensionField>();
                    num4 = 0x10;
                    var num11 = input.ReadBits(num4) + 1;
                    for (var m = 0; m < num11; m++)
                    {
                        var field = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        list.iE_Extensions.Add(field);
                    }
                }
                return list;
            }
        }
    }

}
