using System.Linq;
using Abp.Domain.Entities;
using Abp.EntityFramework.Dependency;
using Lte.Domain.Common.Types;
using Lte.Domain.Regular;
using Lte.Domain.Regular.Attributes;
using MongoDB.Bson;

namespace Lte.Parameters.Entities.Channel
{
    [TypeDoc("中兴下行功率控制数据结构")]
    public class PowerControlDLZte : IEntity<ObjectId>, IZteMongo
    {
        public ObjectId Id { get; set; }

        public bool IsTransient()
        {
            return false;
        }

        public int eNodeB_Id { get; set; }

        public string eNodeB_Name { get; set; }

        public string lastModifedTime { get; set; }

        public string iDate { get; set; }

        public string parentLDN { get; set; }

        public string description { get; set; }

        public int paForPCCH { get; set; }

        public int paForBCCH { get; set; }

        public int paForDTCH { get; set; }

        [MemberDoc("PDCCH DCI0的功率相对于DTCH_PA的偏差(dB)")]
        public string pdcchF0PwrOfst { get; set; }

        public int[] Dci0PowerOffsets => pdcchF0PwrOfst.GetSplittedFields(',').Select(x => x.ConvertToInt(0)).ToArray();

        [MemberDoc("PDCCH DCI1的功率相对于DTCH_PA的偏差(dB)")]
        public string pdcchF1PwrOfst { get; set; }

        public int[] Dci1PowerOffsets => pdcchF1PwrOfst.GetSplittedFields(',').Select(x => x.ConvertToInt(0)).ToArray();

        [MemberDoc("PDCCH DCI1A功率相对于DTCH_PA的偏差(dB)")]
        public string pdcchF1APwrOfst { get; set; }

        public int[] Dci1APowerOffsets => pdcchF1APwrOfst.GetSplittedFields(',').Select(x => x.ConvertToInt(0)).ToArray();

        [MemberDoc("PDCCH DCI1B功率相对于DTCH_PA的偏差(dB)")]
        public string pdcchF1BPwrOfst { get; set; }

        public int[] Dci1BPowerOffsets => pdcchF1BPwrOfst.GetSplittedFields(',').Select(x => x.ConvertToInt(0)).ToArray();

        [MemberDoc("PDCCH DCI1C功率相对于DTCH_PA的偏差(dB)")]
        public string pdcchF1CPwrOfst { get; set; }

        public int[] Dci1CPowerOffsets => pdcchF1CPwrOfst.GetSplittedFields(',').Select(x => x.ConvertToInt(0)).ToArray();

        [MemberDoc("PDCCH DCI1D功率相对于DTCH_PA的偏差(dB)")]
        public string pdcchF1DPwrOfst { get; set; }

        public int[] Dci1DPowerOffsets => pdcchF1DPwrOfst.GetSplittedFields(',').Select(x => x.ConvertToInt(0)).ToArray();

        [MemberDoc("PDCCH DCI2功率相对于DTCH_PA的偏差(dB)")]
        public string pdcchF2PwrOfst { get; set; }

        public int[] Dci2PowerOffsets => pdcchF2PwrOfst.GetSplittedFields(',').Select(x => x.ConvertToInt(0)).ToArray();

        [MemberDoc("PDCCH DCI2A功率相对于DTCH_PA的偏差(dB)")]
        public string pdcchF2APwrOfst { get; set; }

        public int[] Dci2APowerOffsets => pdcchF2APwrOfst.GetSplittedFields(',').Select(x => x.ConvertToInt(0)).ToArray();

        [MemberDoc("PDCCH DCI3功率相对于DTCH_PA的偏差(dB)")]
        public string pdcchF3PwrOfst { get; set; }

        public int[] Dci3PowerOffsets => pdcchF3PwrOfst.GetSplittedFields(',').Select(x => x.ConvertToInt(0)).ToArray();

        [MemberDoc("PDCCH DCI3A功率相对于DTCH_PA的偏差(dB)")]
        public string pdcchF3APwrOfst { get; set; }

        public int[] Dci3APowerOffsets => pdcchF3APwrOfst.GetSplittedFields(',').Select(x => x.ConvertToInt(0)).ToArray();

        public int pdschCLPCSwchDl { get; set; }

        public int paForMSG2 { get; set; }

        public int pcfichPwrOfst { get; set; }

        public int paForCCCH { get; set; }

        public int PowerControlDL { get; set; }

        public int phichPwrOfst { get; set; }

        public int paForDCCH { get; set; }

        public int csiRSPwrOfst { get; set; }

        public int? adjustPwr4CrsCtrl1 { get; set; }

        public int? adjustPwr4CrsCtrl2 { get; set; }

        public int? adjustPwr4PwrCtrl { get; set; }

        public int? adjustPwr4TM3PwrCtrl { get; set; }

        public string mcsThr4PwrCtrl { get; set; }

        public string mcsThres4PwrCtrl { get; set; }

        public double? rbRatioThr4PwrCtrl { get; set; }

        public double? rbRatioThr4CrsCtrl { get; set; }

        public string qamMcsThr4PwrCtrl { get; set; }

        public string qam256MCSThr4PwrCtrl { get; set; }

        public double? cellRBRatioThr4PwrCtrl { get; set; }

        public int? pwrCtlSwchDl { get; set; }

        public int? crsPwrCtlSwch { get; set; }

        public int? uenumThr4CrsCtrl { get; set; }

        public int? magicRadioCssPwrCtrl { get; set; }

        public int? pwrWaterFillSwchDL { get; set; }
    }
}
