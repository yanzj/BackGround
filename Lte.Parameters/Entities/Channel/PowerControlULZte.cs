using Abp.Domain.Entities;
using Abp.EntityFramework.Dependency;
using MongoDB.Bson;

namespace Lte.Parameters.Entities.Channel
{
    public class PowerControlULZte : IEntity<ObjectId>, IZteMongo
    {
        public bool IsTransient()
        {
            return false;
        }

        public ObjectId Id { get; set; }

        public int eNodeB_Id { get; set; }

        public string eNodeB_Name { get; set; }

        public string lastModifedTime { get; set; }

        public string iDate { get; set; }

        public string parentLDN { get; set; }

        public string description { get; set; }

        public int pucchTgtPsLwLimt { get; set; }

        public int swithForPHR { get; set; }

        public int p0NominalPUSCH { get; set; }

        public int poUpMax { get; set; }

        public int p0UePucchPub { get; set; }

        public int switchForDCI3A3Pucch { get; set; }

        public int puschCLPCSwitch { get; set; }

        public int ueTransMcsTarget { get; set; }

        public int powerOffsetOfSRS { get; set; }

        public int deltaFPucchFormat1 { get; set; }

        public int deltaFPucchFormat2a { get; set; }

        public int deltaFPucchFormat1b { get; set; }

        public int deltaPreambleMsg3 { get; set; }

        public int lenofNIWindow { get; set; }

        public int deltaFPucchFormat2b { get; set; }

        public int pucchTgtPsHiLimt { get; set; }

        public int rsrpPeriodMeasSwitchDl { get; set; }

        public int alpha { get; set; }

        public int oiSwitchOfClosePc { get; set; }

        public int ks { get; set; }

        public int deltaMsg3 { get; set; }

        public int p0UePusch1Pub { get; set; }

        public int poNominalPUCCH { get; set; }

        public int dCI3A3SelPusch { get; set; }

        public int puschPCAdjType { get; set; }

        public int dCI3A3SelPucch { get; set; }

        public int poDownMax { get; set; }

        public int rsrpEventMeasSwitchDl { get; set; }

        public int poNominalPUSCH1 { get; set; }

        public int filterCoeffRSRP { get; set; }

        public int switchForCLPCofPUCCH { get; set; }

        public int targetIOT { get; set; }

        public int deltaFPucchFormat2 { get; set; }

        public int PowerControlUL { get; set; }

        public int switchForDCI3A3Pusch { get; set; }

        public int switchForCLPCofPUSCH { get; set; }

        public int ueTransPowerCeiling { get; set; }

        public int? deltaFPucchFormat1bCS { get; set; }

        public int? deltaFPucchFormat3 { get; set; }

        public int? tarSinrPucchUl { get; set; }

        public int? fiMarginforMU { get; set; }

        public int? deltaFiMarginforVoLTE { get; set; }

        public int? fiMarginfor64Qam { get; set; }

        public int? puschPwrFiUpMargin { get; set; }

        public int? puschPwrNiThr { get; set; }

        public int? pucchFmt2InPowCtrlSwch { get; set; }

        public double? targetBler4A0 { get; set; }

        public double? targetBler4A1 { get; set; }

        public double? targetBler4A2 { get; set; }

        public int? deltaPL { get; set; }

        public int? phrThpThrInAtmosduct { get; set; }

        public int? difofPucchPsandNi { get; set; }

        public int? pmaxforPuschPowCtrl { get; set; }

        public int? pucchPsTargetAdapSwch { get; set; }
    }
}