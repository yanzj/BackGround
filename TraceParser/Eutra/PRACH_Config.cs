using Lte.Domain.Common;
using System;
using Lte.Domain.Common.Types;

namespace TraceParser.Eutra
{
    [Serializable]
    public class PRACH_Config
    {
        public void InitDefaults()
        {
        }

        public PRACH_ConfigInfo prach_ConfigInfo { get; set; }

        public long rootSequenceIndex { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PRACH_Config Decode(BitArrayInputStream input)
            {
                PRACH_Config config = new PRACH_Config();
                config.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                config.rootSequenceIndex = input.ReadBits(10);
                if (stream.Read())
                {
                    config.prach_ConfigInfo = PRACH_ConfigInfo.PerDecoder.Instance.Decode(input);
                }
                return config;
            }
        }
    }

    [Serializable]
    public class PRACH_ConfigInfo
    {
        public void InitDefaults()
        {
        }

        public bool highSpeedFlag { get; set; }

        public long prach_ConfigIndex { get; set; }

        public long prach_FreqOffset { get; set; }

        public long zeroCorrelationZoneConfig { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PRACH_ConfigInfo Decode(BitArrayInputStream input)
            {
                PRACH_ConfigInfo info = new PRACH_ConfigInfo();
                info.InitDefaults();
                info.prach_ConfigIndex = input.ReadBits(6);
                info.highSpeedFlag = input.ReadBit() == 1;
                info.zeroCorrelationZoneConfig = input.ReadBits(4);
                info.prach_FreqOffset = input.ReadBits(7);
                return info;
            }
        }
    }

    [Serializable]
    public class PRACH_ConfigSCell_r10
    {
        public void InitDefaults()
        {
        }

        public long prach_ConfigIndex_r10 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PRACH_ConfigSCell_r10 Decode(BitArrayInputStream input)
            {
                PRACH_ConfigSCell_r10 _r = new PRACH_ConfigSCell_r10();
                _r.InitDefaults();
                _r.prach_ConfigIndex_r10 = input.ReadBits(6);
                return _r;
            }
        }
    }

    [Serializable]
    public class PRACH_ConfigSIB
    {
        public void InitDefaults()
        {
        }

        public PRACH_ConfigInfo prach_ConfigInfo { get; set; }

        public long rootSequenceIndex { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PRACH_ConfigSIB Decode(BitArrayInputStream input)
            {
                PRACH_ConfigSIB gsib = new PRACH_ConfigSIB();
                gsib.InitDefaults();
                gsib.rootSequenceIndex = input.ReadBits(10);
                gsib.prach_ConfigInfo = PRACH_ConfigInfo.PerDecoder.Instance.Decode(input);
                return gsib;
            }
        }
    }

}
