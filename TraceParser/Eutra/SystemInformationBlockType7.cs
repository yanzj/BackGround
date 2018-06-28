using Lte.Domain.Common;
using System;
using System.Collections.Generic;
using Lte.Domain.Common.Types;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SystemInformationBlockType7
    {
        public void InitDefaults()
        {
        }

        public List<CarrierFreqsInfoGERAN> carrierFreqsInfoList { get; set; }

        public string lateNonCriticalExtension { get; set; }

        public long t_ReselectionGERAN { get; set; }

        public SpeedStateScaleFactors t_ReselectionGERAN_SF { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType7 Decode(BitArrayInputStream input)
            {
                var type = new SystemInformationBlockType7();
                type.InitDefaults();
                var flag = false;
                flag = input.ReadBit() != 0;
                var stream = flag ? new BitMaskStream(input, 3) : new BitMaskStream(input, 2);
                type.t_ReselectionGERAN = input.ReadBits(3);
                if (stream.Read())
                {
                    type.t_ReselectionGERAN_SF = SpeedStateScaleFactors.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    type.carrierFreqsInfoList = new List<CarrierFreqsInfoGERAN>();
                    var num2 = 4;
                    var num3 = input.ReadBits(num2) + 1;
                    for (var i = 0; i < num3; i++)
                    {
                        var item = CarrierFreqsInfoGERAN.PerDecoder.Instance.Decode(input);
                        type.carrierFreqsInfoList.Add(item);
                    }
                }
                if (flag && stream.Read())
                {
                    var nBits = input.ReadBits(8);
                    type.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                return type;
            }
        }
    }
}