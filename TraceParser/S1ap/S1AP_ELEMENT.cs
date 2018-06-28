using Lte.Domain.Common;
using System;
using Lte.Domain.Common.Types;
using TraceParser.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class S1AP_PDU : TraceConfig
    {
        public InitiatingMessage initiatingMessage { get; set; }

        public SuccessfulOutcome successfulOutcome { get; set; }

        public UnsuccessfulOutcome unsuccessfulOutcome { get; set; }

        public class PerDecoder : DecoderBase<S1AP_PDU>
        {
            public static readonly PerDecoder Instance = new PerDecoder();
            
            protected override void ProcessConfig(S1AP_PDU config, BitArrayInputStream input)
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
    public class S1AP_PRIVATE_IES
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
    public class S1AP_PROTOCOL_EXTENSION
    {
        public static object Switcher(long unique, string membername, BitArrayInputStream input)
        {
            switch (unique)
            {
                case 0x8fL:
                    return id_Data_Forwarding_Not_PossibleClass.GetMemberObj(membername, input);

                case 0x95L:
                    return id_Time_Synchronization_InfoClass.GetMemberObj(membername, input);
            }
            return null;
        }

        public Criticality criticality { get; set; }

        public object Extension { get; set; }

        public long id { get; set; }

        public Presence presence { get; set; }

        public class id_Data_Forwarding_Not_PossibleClass
        {
            public static Data_Forwarding_Not_Possible Extension(BitArrayInputStream input)
            {
                int nBits = 1;
                return (Data_Forwarding_Not_Possible)input.ReadBits(nBits);
            }

            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Extension"))
                {
                    return Extension(input);
                }
                return null;
            }
        }

        public class id_Time_Synchronization_InfoClass
        {
            public static TimeSynchronizationInfo Extension(BitArrayInputStream input)
            {
                return TimeSynchronizationInfo.PerDecoder.Instance.Decode(input);
            }

            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Extension"))
                {
                    return Extension(input);
                }
                return null;
            }
        }
    }

    [Serializable]
    public class S1AP_PROTOCOL_IES_PAIR
    {
        public static object Switcher(object unique, string membername, BitArrayInputStream input)
        {
            return null;
        }

        public Criticality firstCriticality { get; set; }

        public object FirstValue { get; set; }

        public long id { get; set; }

        public Presence presence { get; set; }

        public Criticality secondCriticality { get; set; }

        public object SecondValue { get; set; }
    }

}
