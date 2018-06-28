using Lte.Domain.Common;
using System;
using System.Collections.Generic;
using Lte.Domain.Common.Types;
using TraceParser.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class AdditionalSI_Info_r9 : TraceConfig
    {
        public string csg_Identity_r9 { get; set; }

        public csg_MemberStatus_r9_Enum? csg_MemberStatus_r9 { get; set; }

        public enum csg_MemberStatus_r9_Enum
        {
            member
        }

        public class PerDecoder : DecoderBase<AdditionalSI_Info_r9>
        {
            public static PerDecoder Instance => new PerDecoder();

            protected override void ProcessConfig(AdditionalSI_Info_r9 config, BitArrayInputStream input)
            {
                var stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    config.csg_MemberStatus_r9 = (csg_MemberStatus_r9_Enum)input.ReadBits(1);
                }
                if (stream.Read())
                {
                    config.csg_Identity_r9 = input.ReadBitString(0x1b);
                }
            }
        }
    }

    [Serializable]
    public class SI_OrPSI_GERAN : TraceConfig
    {
        public List<string> psi { get; set; }

        public List<string> si { get; set; }

        public class PerDecoder : DecoderBase<SI_OrPSI_GERAN>
        {
            public static PerDecoder Instance => new PerDecoder();

            protected override void ProcessConfig(SI_OrPSI_GERAN config, BitArrayInputStream input)
            {
                var choice = input.ReadBits(1);
                if (choice != 0 && choice != 1)
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                config.si = new List<string>();
                var num4 = input.ReadBits(4) + 1;
                for (var num5 = 0; num5 < num4; num5++)
                {
                    var num = input.ReadBits(5);
                    var str = input.readOctetString(num + 1);
                    config.si.Add(str);
                }
            }
        }
    }

}
