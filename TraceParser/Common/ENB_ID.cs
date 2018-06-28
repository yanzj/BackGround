using Lte.Domain.Common;
using System;
using System.Collections.Generic;
using Lte.Domain.Common.Types;
using TraceParser.S1ap;

namespace TraceParser.Common
{
    [Serializable]
    public class ENB_ID : TraceConfig
    {
        public string home_eNB_ID { get; set; }

        public string macro_eNB_ID { get; set; }

        public class PerDecoder : DecoderBase<ENB_ID>
        {
            public static PerDecoder Instance => new PerDecoder();

            protected override void ProcessConfig(ENB_ID config, BitArrayInputStream input)
            {
                input.ReadBit();
                switch (input.ReadBits(1))
                {
                    case 0:
                        config.macro_eNB_ID = input.ReadBitString(20);
                        break;
                    case 1:
                        config.home_eNB_ID = input.ReadBitString(0x1c);
                        break;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }

    [Serializable]
    public class Global_ENB_ID : TraceConfig
    {
        public ENB_ID eNB_ID { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string pLMNidentity { get; set; }

        public class PerDecoder : DecoderBase<Global_ENB_ID>
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            protected override void ProcessConfig(Global_ENB_ID config, BitArrayInputStream input)
            {
                input.ReadBit();
                var stream = new BitMaskStream(input, 1);
                input.skipUnreadedBits();
                config.pLMNidentity = input.readOctetString(3);
                config.eNB_ID = ENB_ID.PerDecoder.Instance.Decode(input);
                if (!stream.Read()) return;
                input.skipUnreadedBits();
                config.iE_Extensions = new List<ProtocolExtensionField>();
                var num5 = input.ReadBits(0x10) + 1;
                for (var i = 0; i < num5; i++)
                {
                    var item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                    config.iE_Extensions.Add(item);
                }
            }
        }
    }

}
