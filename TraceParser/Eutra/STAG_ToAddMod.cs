using Lte.Domain.Common;
using System;
using Lte.Domain.Common.Types;

namespace TraceParser.Eutra
{
    [Serializable]
    public class STAG_ToAddMod_r11
    {
        public void InitDefaults()
        {
        }

        public long stag_Id_r11 { get; set; }

        public TimeAlignmentTimer timeAlignmentTimerSTAG_r11 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public STAG_ToAddMod_r11 Decode(BitArrayInputStream input)
            {
                STAG_ToAddMod_r11 _r = new STAG_ToAddMod_r11();
                _r.InitDefaults();
                input.ReadBit();
                _r.stag_Id_r11 = input.ReadBits(2) + 1;
                const int nBits = 3;
                _r.timeAlignmentTimerSTAG_r11 = (TimeAlignmentTimer)input.ReadBits(nBits);
                return _r;
            }
        }
    }
}
