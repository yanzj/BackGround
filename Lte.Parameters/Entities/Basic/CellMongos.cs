using Abp.Domain.Entities;
using Abp.EntityFramework.Dependency;
using Lte.Domain.Regular.Attributes;
using MongoDB.Bson;

namespace Lte.Parameters.Entities.Basic
{
    [TypeDoc("华为网管内小区信息配置")]
    public class CellHuaweiMongo : IEntity<ObjectId>, IHuaweiCellMongo
    {
        public ObjectId Id { get; set; }

        public bool IsTransient()
        {
            return false;
        }

        public string iDate { get; set; }

        public int eNodeB_Id { get; set; }

        public string eNodeBId_Name { get; set; }

        [MemberDoc("该参数表示EUTRAN小区的小区标识，该小区标识和eNodeB ID组成EUTRAN小区标识，EUTRAN小区标识加上PLMN组成ECGI，参见协议TS 3GPP 36.413")]
        public int CellId { get; set; }

        public int LocalCellId { get; set; }

        [MemberDoc("该参数表示小区的双工模式，取值为CELL_FDD表示当前小区为FDD模式，取值为CELL_TDD表示当前小区为TDD模式")]
        public int FddTddInd { get; set; }

        [MemberDoc("该参数表示小区所使用的前导格式。参数的使用细节详见3GPP TS 36.211")]
        public int PreambleFmt { get; set; }

        [MemberDoc("该参数表示是否支持多RRU共小区特性")]
        public int MultiRruCellFlag { get; set; }

        [MemberDoc("该参数表示是否需要配置上行频点")]
        public int UlEarfcnCfgInd { get; set; }

        [MemberDoc("该参数表示小区的下行循环前缀长度，分为普通循环前缀和扩展循环前缀，扩展循环前缀主要用于一些较复杂的环境，如多径效应明显、时延严重等。同一小区，上下行循环前缀长度配置可以不同，同一块基带板上，各小区的上下行循环前缀长度配置要一致，参数的使用细节参见3GPP TS 36.211")]
        public int DlCyclicPrefix { get; set; }
        
        [MemberDoc("该参数表示小区下行带宽。以RB数表示，取值CELL_BW_N25表示小区带宽为25RB，取值CELL_BW_N50表示小区带宽为50RB，等等。参数的使用细节参见3GPP TS 36.104")]
        public int DlBandWidth { get; set; }

        [MemberDoc("该参数表示本小区允许的UE最大发射功率是否配置。如果不配置，则UE的最大发射功率由UE自己的能力决定")]
        public int UePowerMaxCfgInd { get; set; }

        [MemberDoc("该参数表示小区的上行循环前缀长度，分为普通循环前缀和扩展循环前缀，扩展循环前缀主要用于一些较复杂的环境，如多径效应明显、时延严重等。同一小区，上下行循环前缀长度配置可以不同，同一块基带板上，各小区的上下行循环前缀长度配置要一致，参数的使用细节参见3GPP TS 36.211")]
        public int UlCyclicPrefix { get; set; }

        [MemberDoc("该参数表示在满规格删除系统内NRT邻区时，当有多个频点上有可删除邻区存在时，优先删除的邻区是否按照频点优先级进行控制。取值越小优先级越低")]
        public int CellScaleInd { get; set; }

        [MemberDoc("该参数表示小区的主备模式。默认为主模式。配置为备模式时，SFN的跨框辅小区在框间故障时可回退到备用小区继续提供业务。备用小区只支持普通小区，SFN小区和多MPRU聚合小区")]
        public int EuCellStandbyMode { get; set; }

        [MemberDoc("该参数表示小区覆盖的起始位置，作为超100km小区半径特性上行定时滞后量的输入")]
        public int CellRadiusStartLocation { get; set; }

        [MemberDoc("该参数表示高速小区指示。高速铁路场景下配置为高速小区，其它场景下配置为低速小区。5M TDD小区仅支持低速小区，8T8R的TDD小区仅支持低速小区。TDD小区不支持配置超高速模式")]
        public int HighSpeedFlag { get; set; }

        [MemberDoc("用户标签")]
        public string UserLabel { get; set; }

        [MemberDoc("该参数表示小区是否是对空覆盖小区")]
        public int AirCellFlag { get; set; }

        [MemberDoc("该参数表示小区上行带宽。以RB数表示，取值CELL_BW_N25表示小区带宽为25RB，取值CELL_BW_N50表示小区带宽为50RB，等等。参数的使用细节参见3GPP TS 36.104")]
        public int UlBandWidth { get; set; }

        [MemberDoc("该参数表示小区参考信号端口数。根据协议约束，小区参考信号端口数支持1、2、4三种配置：取值为1表示配置CRS端口数为1，即逻辑天线Port0；取值为2表示配置CRS端口数为2，即逻辑天线Port0/1；取值为4表示配置CRS端口数为4，即逻辑天线Port0/1/2/3")]
        public int CrsPortNum { get; set; }

        [MemberDoc("该参数表示小区半径，满足一定性能条件下小区所能覆盖的最远距离；当配置超过100km时，超100km小区半径特性生效；仅FDD支持配置超过100km，TDD不支持")]
        public int CellRadius { get; set; }

        [MemberDoc("该参数表示小区是否是CSG（Closed Subscriber Group）小区。CSG小区是一种接入受限小区，它会在系统消息中广播一个指定的CSG ID，只有归属于该CSG的用户才允许接入该小区。枚举值BOOLEAN_FALSE表示小区不是CSG小区，BOOLEAN_TRUE表示小区是CSG小区。目前产品不支持CSG小区。")]
        public int CsgInd { get; set; }

        [MemberDoc("该参数表示服务小区的小区偏移量。用于控制服务小区与邻区触发切换的难易程度，该值越小越容易触发测量报告上报")]
        public int CellSpecificOffset { get; set; }

        [MemberDoc("该参数表示生成前导序列的起始逻辑根序列索引，每个逻辑根序列都对应一个物理根序列，它们的对应关系参见3GPP TS 36.211")]
        public int RootSequenceIdx { get; set; }

        [MemberDoc("物理小区标识，即PCI")]
        public int PhyCellId { get; set; }

        [MemberDoc("该参数表示是否需要配置客户化带宽。只有协议带宽为1.4M、5M、10M、15M、20M才支持客户化带宽")]
        public int CustomizedBandWidthCfgInd { get; set; }

        [MemberDoc("紧急区域标识配置指示")]
        public int EmergencyAreaIdCfgInd { get; set; }

        [MemberDoc("该参数表示运营商共享组索引，该参数在混合共享模式下需要配置，可配值为0~5，表示有效运营商共享组索引。在独立运营商、共载频共享、分载频共享模式下，需要配置为无效值255")]
        public int CnOpSharingGroupId { get; set; }

        [MemberDoc("该参数表示附加频谱散射，限制了接入该小区UE的散射功率，参数的使用细节参见3GPP TS 36.101")]
        public int AdditionalSpectrumEmission { get; set; }

        [MemberDoc("该参数表示在满规格删除系统内NRT邻区时，当有多个频点上有可删除邻区存在时，优先删除的邻区是否按照频点优先级进行控制。取值越小优先级越低")]
        public int FreqPriorityForAnr { get; set; }

        [MemberDoc("该参数表示与服务小区同频的邻区的相关信息是否允许通过ANR维护。取值ALLOWED表示相关信息允许通过ANR维护，取值NOT_ALLOWED表示相关信息不允许通过ANR维护")]
        public int IntraFreqAnrInd { get; set; }

        [MemberDoc("小区发送和接收模式")]
        public int TxRxMode { get; set; }

        [MemberDoc("该参数表示小区下行频点，参数的使用细节参见3GPP TS 36.104。其中255144~262143的使用参见LTE-U Forum Documents: eNB Minumum Requirements for LTE-U SDL V1.0")]
        public int DlEarfcn { get; set; }

        [MemberDoc("该参数表示小区工作模式，配置为DL_ONLY仅用于载波聚合")]
        public int WorkMode { get; set; }

        public int CellAdminState { get; set; }

        public int CellActiveState { get; set; }

        [MemberDoc("该参数表示同频邻区是否是共享邻区。BOOLEAN_TRUE表示服务频点是共享频点，同频下所有邻区添加时需要进一步确认共享运营商信息。BOOLEAN_FALSE表示服务频点不是共享频点，同频邻区不需要进一步确认共享运营商信息")]
        public int IntraFreqRanSharingInd { get; set; }

        public BsonDocument[] CellSlaveBand { get; set; }

        [MemberDoc("该参数表示小区CPRI压缩类型。CPRI压缩主要用于在RRU级联场景下，在不改变CPRI光口速率的情况下，提升级联规格")]
        public int CPRICompression { get; set; }

        public string CellName { get; set; }

        public int objId { get; set; }

        [MemberDoc("该参数表示小区所属的频带，参数的使用细节参见3GPP TS 36.104。其中252~255的使用参见LTE-U Forum Documents: eNB Minumum Requirements for LTE-U SDL V1.0")]
        public int FreqBand { get; set; }

        [MemberDoc("该参数表示服务小区频点的特定频率偏置。在测量控制中下发，用于控制服务小区与邻区触发切换的难易程度")]
        public int QoffsetFreq { get; set; }

        [MemberDoc("该参数表示SFN小区包含的物理小区总数目,宏站指SFN小区下扇区设备的个数，Lampsite指SFN小区下扇区设备组的个数，宏和Lampsite混合时指SFN小区扇区设备和扇区设备组的个数之和")]
        public int? SectorEqmNum { get; set; }

        [MemberDoc("该参数指示多RRU小区的模式")]
        public int? MultiRruCellMode { get; set; }

        [MemberDoc("该参数表示MPRU聚合小区的CPRI_E接口的CPRI数据压缩比率。CPRI_E接口的数据带宽压缩功能用于节省RHUB与MPRU之间的网线承载的LTE CPRI数据占用带宽，提升网线可承载的各制式载波数量规格")]
        public int? CpriEthCompressionRatio { get; set; }

        [MemberDoc("该参数用于设置多小区共RRU模式。该参数仅适用于TDD")]
        public int? MultiCellShareMode { get; set; }

        [MemberDoc("该参数用于维护和查询SFN辅小区归属的主小区信息。该参数仅适用于FDD")]
        public string SfnMasterCellLabel { get; set; }

        [MemberDoc("该参数用于指示本小区是否属于专有小区。 根据运营商策略进行配置。当取值为SPECSERCELL时，表示该小区是专有业务小区，会将“未识别SPID的用户”或者“已识别SPID用户并且用户SPID配置的专有用户标识配置为NONE的用户”迁移到普通小区，同时该功能受License控制；当取值为NONE时，表示该小区是普通小区")]
        public int? SpecifiedCellFlag { get; set; }

        public int? NbCellFlag { get; set; }

        public int? CoverageLevelType { get; set; }
    }

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
    }

    public class PrachFDDZte : IEntity<ObjectId>, IZteMongo
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

        public int preambleTransMax { get; set; }

        public int highSpeedFlag { get; set; }

        public int numContRA { get; set; }

        public int groupBEnable { get; set; }

        public int powerRampingStep { get; set; }

        public int prachFreqOffsetFlag { get; set; }

        public int prachUltiCapSwch { get; set; }

        public int macContResTimer { get; set; }

        public int raResponseWindowSize { get; set; }

        public int pathlossThrd { get; set; }

        public int ueAvgSpeed { get; set; }

        public int PrachFDD { get; set; }

        public int preambLifeTime { get; set; }

        public int numContFreeRA { get; set; }

        public int preambleIniReceivedPower { get; set; }

        public int macNonContenPreamble { get; set; }

        public int rootSequenceIndex { get; set; }

        public int prachFreqOffset { get; set; }

        public int messagePowerOffsetGroupB { get; set; }

        public int prachConfigIndex { get; set; }

        public int sizeOfRAPreamblesGroupA { get; set; }

        public double raCollProb { get; set; }

        public int maxHarqMsg3Tx { get; set; }

        public int messageSizeGroupA { get; set; }

        public int ncs { get; set; }

        public int ueSpeedThrd { get; set; }

        public int numberOfRAPreambles { get; set; }

        public int? prachFMRecTAThresh { get; set; }

        public int? prachSupFarCoverSwch { get; set; }

        public int? prachFMRecOnTASwch { get; set; }
    }
}
