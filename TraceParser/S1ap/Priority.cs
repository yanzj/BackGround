using Lte.Domain.Common;
using System;
using System.Collections.Generic;
using Lte.Domain.Common.Types;

namespace TraceParser.S1ap
{
    [Serializable]
    public class PriorityLevel
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public long Decode(BitArrayInputStream input)
            {
                return input.ReadBits(4);
            }
        }
    }

    [Serializable]
    public class AllocationAndRetentionPriority
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public Pre_emptionCapability pre_emptionCapability { get; set; }

        public Pre_emptionVulnerability pre_emptionVulnerability { get; set; }

        public long priorityLevel { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public AllocationAndRetentionPriority Decode(BitArrayInputStream input)
            {
                AllocationAndRetentionPriority priority = new AllocationAndRetentionPriority();
                priority.InitDefaults();
                BitMaskStream stream = (input.ReadBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                priority.priorityLevel = input.ReadBits(4);
                int nBits = 1;
                priority.pre_emptionCapability = (Pre_emptionCapability)input.ReadBits(nBits);
                nBits = 1;
                priority.pre_emptionVulnerability = (Pre_emptionVulnerability)input.ReadBits(nBits);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    priority.iE_Extensions = new List<ProtocolExtensionField>();
                    nBits = 0x10;
                    int num5 = input.ReadBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        priority.iE_Extensions.Add(item);
                    }
                }
                return priority;
            }
        }
    }

}
