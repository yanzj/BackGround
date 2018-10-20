using Abp.Domain.Entities;
using Abp.EntityFramework.Dependency;
using MongoDB.Bson;

namespace Lte.Parameters.Entities.Basic
{
    public class EUtranCellFDDZte : IEntity<ObjectId>, IZteMongo
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

        public int flagSwiMode { get; set; }

        public double latitude { get; set; }

        public int cpMerger { get; set; }

        public int ratTarCarriFre_ratTarCarriFreTDD { get; set; }

        public int siWindowLength { get; set; }

        public int minMCSDl { get; set; }

        public int pullCardJudgeSwitch { get; set; }

        public int radioMode { get; set; }

        public int rlfDelayTime { get; set; }

        public int phsNIInd { get; set; }

        public int tm34T4RSwch { get; set; }

        public int? tm44T4RSwch { get; set; }

        public int ocs { get; set; }

        public int switchUlPRBRandom { get; set; }

        public int oldCellId { get; set; }

        public int switchForNGbrDrx { get; set; }

        public string pciList { get; set; }

        public string rbInterferenceBitMapUl { get; set; }

        public int bandWidthDl { get; set; }

        public int maxUeRbNumUl { get; set; }

        public int matrixType { get; set; }

        public int cqiExpiredTimer { get; set; }

        public string masterECellEqFun { get; set; }

        public int coMPFlagUl { get; set; }

        public int voIPRLFDelayTime { get; set; }

        public int bfmumimoEnableDl { get; set; }

        public int ratTarCarriFre_ratTarCarriFreUTRATDD { get; set; }

        public int pci { get; set; }

        public string cellReservedForOptUse { get; set; }

        public double cellReferenceSignalPower { get; set; }

        public int maxMCSUl { get; set; }

        public int ratTarCarriFre_ratTarCarriFreUTRAFDD { get; set; }

        public int simuLoadSwchDl { get; set; }

        public int ratTarCarriFre_ratTarCarriFreGERAN { get; set; }

        public int pb { get; set; }

        public int sceneCfg { get; set; }

        public double maximumTransmissionPower { get; set; }

        public string addiFreqBand { get; set; }

        public int maxUeRbNumDl { get; set; }

        public double earfcnDl { get; set; }

        public string reservedByEUtranRelation { get; set; }

        public int eai { get; set; }

        public int ratioShared { get; set; }

        public int phyChCPSel { get; set; }

        public string refECellEquipmentFunction { get; set; }

        public int freqBandInd { get; set; }

        public int csfbMethodofGSM { get; set; }

        public string userLabel { get; set; }

        public string alias { get; set; }

        public int allowedAccessClasses { get; set; }

        public int mumimoEnableUl { get; set; }

        public int csfbMethdofCDMA { get; set; }

        public int sfAssignment { get; set; }

        public int cFI { get; set; }

        public int cceAdaptMod { get; set; }

        public int voLTESwch { get; set; }

        public int cutRbNumDl { get; set; }

        public int rd4ForCoverage { get; set; }

        public int minMCSUl { get; set; }

        public int sampleRateCfg { get; set; }

        public int testState { get; set; }

        public int ceuccuSwitch { get; set; }

        public int loadtestSwitch { get; set; }

        public int cutRbNumUl { get; set; }

        public int cellLocalId { get; set; }

        public string ratioOperatorn { get; set; }

        public int commCCENumDl { get; set; }

        public int qosAdpSwchUL { get; set; }

        public int specialSfPatterns { get; set; }

        public int tac { get; set; }

        public int cellSize { get; set; }

        public string refPlmn { get; set; }

        public int addiSpecEmiss { get; set; }

        public int aggregationUl { get; set; }

        public int switchForGbrDrx { get; set; }

        public int ratTarCarriFre_ratTarCarriFreCMA1xRTT { get; set; }

        public double earfcnUl { get; set; }

        public int avoidFreqOffsetNISwch { get; set; }

        public int ueTransMode { get; set; }

        public int mimoModeSwitch { get; set; }

        public int energySavControl { get; set; }

        public int srsRLFSwitch { get; set; }

        public int cellResvInfo { get; set; }

        public int wimaxCoexistSwitch { get; set; }

        public double longitude { get; set; }

        public int ratTarCarriFre_ratTarCarriFreFDD { get; set; }

        public int maxMCSDl { get; set; }

        public int isCompressed { get; set; }

        public string rbInterferenceBitMapDl { get; set; }

        public double offsetAngle { get; set; }

        public int cellCapaLeveInd { get; set; }

        public int cellRadius { get; set; }

        public int timeAlignTimer { get; set; }

        public int ratTarCarriFre_ratTarCarriFreCMAHRPD { get; set; }

        public int antPort1 { get; set; }

        public int EUtranCellFDD { get; set; }

        public double transmissionPower { get; set; }

        public int glCSSSwch { get; set; }

        public int rbSharMode { get; set; }

        public int rlfSwitch { get; set; }

        public int fullConfigSwch { get; set; }

        public int bandIndicator { get; set; }

        public int ueTransModeTDD { get; set; }

        public int bandWidthUl { get; set; }

        public int antDecRankSwch { get; set; }

        public int adminState { get; set; }

        public int upInterfFreqEffThr { get; set; }

        public int nbrBlackListExist { get; set; }

        public int preScheUEAccessSwitchUl { get; set; }

        public int csfbMethdofUMTS { get; set; }

        public int mimoScenarios { get; set; }

        public int flagSwiModeUl { get; set; }

        public string buildPhyLocation { get; set; }

        public int aggregationDl { get; set; }

        public string addiSpecEmissForAddiFreq { get; set; }

        public int cellRSPortNum { get; set; }

        public int qosAdpSwchDL { get; set; }

        public string supercellFlag { get; set; }

        public int magicRadioSwch { get; set; }

        public int qam64DemSpIndUl { get; set; }

        public int? narrowInterferenceSwch { get; set; }

        public string reservedPara10 { get; set; }

        public string reservedPara9 { get; set; }

        public string reservedPara8 { get; set; }

        public string reservedPara7 { get; set; }

        public string reservedPara6 { get; set; }

        public string reservedPara5 { get; set; }

        public string reservedPara4 { get; set; }

        public string reservedPara3 { get; set; }

        public string reservedPara2 { get; set; }

        public string reservedPara1 { get; set; }

        public int? deRohcSch { get; set; }

        public int? bandWidth { get; set; }

        public int? sfBitmapSwchDl { get; set; }

        public int? codeRateSwitchDl { get; set; }

        public string relatedCellLocalId { get; set; }

        public int? codeRateSwitchUl { get; set; }

        public int? magicRadioULDCESwch { get; set; }

        public int? narrowInterferenceLen { get; set; }

        public int? sfBitmapSwchUl { get; set; }

        public int? switchDlPRBRandom { get; set; }

        public int? hiterThreshold { get; set; }

        public int? pucchDTXThre { get; set; }

        public int? prachSupFarCoverSwch { get; set; }

        public string refSignalResCfg { get; set; }

        public int? mbsfnSyncAreaID { get; set; }

        public int? mbmsCCEAdaptMod { get; set; }

        public int? cfiNotSameSwitch { get; set; }

        public int? bfRbExpandSwch { get; set; }

        public int? qamSwch { get; set; }

        public int? forbidRbNum4NStandBWDl { get; set; }

        public int? atmosphericSwch { get; set; }

        public int? earfcn { get; set; }

        public int? oneRBAllocateSwch { get; set; }

        public int? laaCarrierPriSwch { get; set; }

        public int? laaDFSSwch { get; set; }

        public int? cellLAACarrierAtt { get; set; }

        public int? taLimitedReleaseSwch { get; set; }

        public int? configDRXTimerForConnect { get; set; }

        public int? configDRXTimerForHO { get; set; }

        public int? configDRXTimerForRA { get; set; }

        public int? tm83TestMcsThr8TX { get; set; }

        public int? atmosphericQuitPerid { get; set; }

        public int? atmosphericSupresEnterPerid { get; set; }

        public int? atmosphericSeqDemAntNum { get; set; }

        public int? atmosphericLocationSwch { get; set; }

        public int? atmosphericSymBackSwch { get; set; }

        public int? atmosphericSeqDemParaThres { get; set; }

        public int? atmosphericPAPRThres { get; set; }

        public int? atmosphericSupresThres { get; set; }

        public int? atmosphericDecPowrThres { get; set; }

        public int? atmosphericDetecWaySwch { get; set; }

        public int? atmosphericSupresQuitPerid { get; set; }

        public string reservedPara11 { get; set; }

        public string reservedPara12 { get; set; }

        public string reservedPara13 { get; set; }

        public string reservedPara14 { get; set; }

        public string reservedPara15 { get; set; }

        public string reservedPara16 { get; set; }

        public string reservedPara17 { get; set; }

        public string reservedPara18 { get; set; }

        public string reservedPara19 { get; set; }

        public string reservedPara20 { get; set; }

        public string reservedPara21 { get; set; }

        public string reservedPara22 { get; set; }

        public string reservedPara23 { get; set; }

        public string reservedPara24 { get; set; }

        public string reservedPara25 { get; set; }

        public string reservedPara26 { get; set; }

        public string reservedPara27 { get; set; }

        public string reservedPara28 { get; set; }

        public string reservedPara29 { get; set; }

        public string reservedPara30 { get; set; }

        public int? the256QAMCATLimitSwch { get; set; }

        public int? qcellPosiSwch { get; set; }

        public int? mmModeSwch { get; set; }

        public int? pubAndHwPriOvlpRbNumDL { get; set; }

        public int? pubAndHwPriOvlpRbNumUL { get; set; }

        public int? acSendPowdeltaDL { get; set; }

        public int? acSendPowdeltaUL { get; set; }

        public int? tm38McsThr8TX { get; set; }

        public int? ringCoverSwch { get; set; }

        public int? distributedMIMOSwch { get; set; }

        public int? tddCfg0MUMIMOSwchUL { get; set; }

        public int? ringCoverAdjustQuantity { get; set; }

        public int? muMIMO4MultiUESwchUl { get; set; }

        public int? type1RBAllocEnableSwchDl { get; set; }

        public int? highBllrCntThrUL { get; set; }

        public int? highBllrCntThrDL { get; set; }

        public int? highBllrCntThr4VOIPDl { get; set; }

        public int? highBllrCntThr4VOIPUl { get; set; }

        public int? srRBNumAuthMode { get; set; }

        public string rbNumMultiFactor4Mod1 { get; set; }

        public int? freqOffCompenSwchDl { get; set; }

        public int? freqOffRptValidThr { get; set; }

        public int? puschChEstType { get; set; }

        public int? ambrLimtSwch { get; set; }

        public int? speedInd { get; set; }

        public int? filteWndLen4SRMode1 { get; set; }

        public int? iCo4PubAndHwPriSwch { get; set; }

        public int? shortDrxSwch { get; set; }

        public int? crcMergeSwch { get; set; }

        public int? pubAndHwPriOvlpRbStartPosDL { get; set; }

        public int? pubAndHwPriOvlpRbStartPosUL { get; set; }

        public int? magicRadioMaxBandWidth { get; set; }

        public int? tm9EnhancedSDMASwch { get; set; }
        
        public double? rbRatioHighThr4Mod1 { get; set; }

        public double? rbRatioLowThr4Mod1 { get; set; }

        public double? stopPreCompenRatioThr { get; set; }

        public double? startPreCompenRatioThr { get; set; }

        public double? filteFactor4SRMode1 { get; set; }

        public int? case5RecoverThrdUl { get; set; }

        public int? case5CongStarThrdUl { get; set; }

        public int? case5CongJudPrd { get; set; }

        public int? accMCSEnableSwchUl { get; set; }

        public int? case5RecoverThrdDl { get; set; }

        public int? accMCSEnableSwchDl { get; set; }

        public int? case5RatioACSwch { get; set; }

        public int? case5CongStarThrdDl { get; set; }

        public string case5GuaranteedGBR { get; set; }

        public int? nB { get; set; }

        public int? emtcSwch { get; set; }

        public string ratioOperatorGroupn { get; set; }

        public string plmnGroupIndex { get; set; }

        public int? hARQWeightCombSwch { get; set; }

        public int? nCSpeedJudgeSwch { get; set; }

    }
}
