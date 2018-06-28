using Lte.Domain.Common;
using System;
using Lte.Domain.Common.Types;

namespace TraceParser.S1ap
{
    [Serializable]
    public class SuccessfulOutcome
    {
        public void InitDefaults()
        {
        }

        public Criticality criticality { get; set; }

        public long procedureCode { get; set; }

        public object value { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SuccessfulOutcome Decode(BitArrayInputStream input)
            {
                SuccessfulOutcome outcome = new SuccessfulOutcome();
                outcome.InitDefaults();
                input.skipUnreadedBits();
                outcome.procedureCode = input.ReadBits(8);
                const int num4 = 2;
                outcome.criticality = (Criticality)input.ReadBits(num4);
                input.skipUnreadedBits();
                int nBits = 0;
                while (true)
                {
                    switch (input.ReadBit())
                    {
                        case 0:
                            nBits += input.ReadBits(7);
                            goto Label_00CF;

                        case 1:
                            switch (input.ReadBit())
                            {
                                case 0:
                                    nBits += input.ReadBits(14);
                                    goto Label_00CF;

                                case 1:
                                    input.ReadBits(2);
                                    nBits += input.ReadBits(4) * 0x400;
                                    break;
                            }
                            break;
                    }
                }
            Label_00CF:
                long num3 = input.Position;
                try
                {
                    outcome.value = S1AP_ELEMENTARY_PROCEDURE.Switcher(outcome.procedureCode, "SuccessfulOutcome", input);
                    input.skipUnreadedBits();
                }
                catch (Exception)
                {
                    input.skipUnreadedBits();
                    input.Position = num3;
                    outcome.value = input.readOctetString(nBits);
                }
                if (input.Position != (num3 + nBits))
                {
                    input.Position = num3 + nBits;
                }
                return outcome;
            }
        }
    }

    [Serializable]
    public class UnsuccessfulOutcome
    {
        public void InitDefaults()
        {
        }

        public Criticality criticality { get; set; }

        public long procedureCode { get; set; }

        public object value { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UnsuccessfulOutcome Decode(BitArrayInputStream input)
            {
                UnsuccessfulOutcome outcome = new UnsuccessfulOutcome();
                outcome.InitDefaults();
                input.skipUnreadedBits();
                outcome.procedureCode = input.ReadBits(8);
                int num4 = 2;
                outcome.criticality = (Criticality)input.ReadBits(num4);
                input.skipUnreadedBits();
                int nBits = 0;
                while (true)
                {
                    switch (input.ReadBit())
                    {
                        case 0:
                            nBits += input.ReadBits(7);
                            goto Label_00CF;

                        case 1:
                            switch (input.ReadBit())
                            {
                                case 0:
                                    nBits += input.ReadBits(14);
                                    goto Label_00CF;

                                case 1:
                                    input.ReadBits(2);
                                    nBits += input.ReadBits(4) * 0x400;
                                    break;
                            }
                            break;
                    }
                }
            Label_00CF:
                long num3 = input.Position;
                try
                {
                    outcome.value = S1AP_ELEMENTARY_PROCEDURE.Switcher(outcome.procedureCode, "UnsuccessfulOutcome", input);
                    input.skipUnreadedBits();
                }
                catch (Exception)
                {
                    input.skipUnreadedBits();
                    input.Position = num3;
                    outcome.value = input.readOctetString(nBits);
                }
                if (input.Position != (num3 + nBits))
                {
                    input.Position = num3 + nBits;
                }
                return outcome;
            }
        }
    }

}
