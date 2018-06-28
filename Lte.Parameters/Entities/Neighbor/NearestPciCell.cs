using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Lte.Domain.Common;
using Lte.Domain.Common.Types;
using Lte.Domain.Regular.Attributes;
using MongoDB.Bson;

namespace Lte.Parameters.Entities.Neighbor
{
    public class EUtranRelationZte : IEntity<ObjectId>, IZteMongo
    {
        public int eNodeB_Id { get; set; }

        public string eNodeB_Name { get; set; }

        public string lastModifedTime { get; set; }

        public string iDate { get; set; }

        public string parentLDN { get; set; }

        public string description { get; set; }

        public int resPRBDown { get; set; }

        public int resPRBUp { get; set; }

        public int overlapCoverage { get; set; }

        public int shareCover { get; set; }

        public int numRRCCntNumCov { get; set; }

        public int lbIntraMeasureOffset { get; set; }

        public int isX2HOAllowed { get; set; }

        public string userLabel { get; set; }

        public int coperType { get; set; }

        public int isAnrCreated { get; set; }

        public int s1DataFwdFlag { get; set; }

        public int isHOAllowed { get; set; }

        public int switchonTimeWindow { get; set; }

        public int nCelPriority { get; set; }

        public int EUtranRelation { get; set; }

        public int isESCoveredBy { get; set; }

        public int stateInd { get; set; }

        public string refEUtranCellFDD { get; set; }

        public int cellIndivOffset { get; set; }

        public int isRemoveAllowed { get; set; }

        public int qofStCell { get; set; }

        public int esSwitch { get; set; }

        public int coverESCell { get; set; }

        public string refExternalEUtranCellFDD { get; set; }

        public string supercellFlag { get; set; }

        public string refExternalEUtranCellTDD { get; set; }

        public int? coperModSwch { get; set; }

        public int? noSupMobilitySwch { get; set; }

        public int? supportMRO { get; set; }

        public ObjectId Id { get; set; }

        public bool IsTransient()
        {
            return false;
        }
    }

    [TypeDoc("来自MongoDB的LTE邻区关系视图")]
    [AutoMapFrom(typeof(EUtranRelationZte), typeof(ExternalEUtranCellFDDZte), typeof(EutranIntraFreqNCell), typeof(EutranInterFreqNCell))]
    public class NeighborCellMongo
    {
        [MemberDoc("小区编号（对于LTE来说就是基站编号）")]
        [AutoMapPropertyResolve("eNodeB_Id", typeof(EUtranRelationZte))]
        [AutoMapPropertyResolve("eNodeB_Id", typeof(ExternalEUtranCellFDDZte))]
        [AutoMapPropertyResolve("eNodeB_Id", typeof(EutranIntraFreqNCell))]
        [AutoMapPropertyResolve("eNodeB_Id", typeof(EutranInterFreqNCell))]
        public int CellId { get; set; }

        [MemberDoc("扇区编号")]
        public byte SectorId { get; set; }

        [MemberDoc("邻区小区编号")]
        [AutoMapPropertyResolve("eNBId", typeof(ExternalEUtranCellFDDZte))]
        [AutoMapPropertyResolve("eNodeBId", typeof(EutranIntraFreqNCell))]
        [AutoMapPropertyResolve("eNodeBId", typeof(EutranInterFreqNCell))]
        public int NeighborCellId { get; set; }

        [MemberDoc("邻区扇区编号")]
        [AutoMapPropertyResolve("cellLocalId", typeof(ExternalEUtranCellFDDZte))]
        [AutoMapPropertyResolve("CellId", typeof(EutranIntraFreqNCell))]
        [AutoMapPropertyResolve("CellId", typeof(EutranInterFreqNCell))]
        public byte NeighborSectorId { get; set; }

        [MemberDoc("邻区名称")]
        [AutoMapPropertyResolve("userLabel", typeof(ExternalEUtranCellFDDZte))]
        [AutoMapPropertyResolve("NeighbourCellName", typeof(EutranIntraFreqNCell))]
        [AutoMapPropertyResolve("NeighbourCellName", typeof(EutranInterFreqNCell))]
        public string NeighborCellName { get; set; }

        [MemberDoc("PCI，便于查询邻区")]
        [AutoMapPropertyResolve("pci", typeof(ExternalEUtranCellFDDZte))]
        public short NeighborPci { get; set; }

        [MemberDoc("是否为ANR创建")]
        [AutoMapPropertyResolve("isAnrCreated", typeof(EUtranRelationZte), typeof(IntToBoolTransform))]
        [AutoMapPropertyResolve("AnrFlag", typeof(EutranIntraFreqNCell), typeof(PositiveBoolTransform))]
        [AutoMapPropertyResolve("AnrFlag", typeof(EutranInterFreqNCell), typeof(PositiveBoolTransform))]
        public bool IsAnrCreated { get; set; }

        [MemberDoc("是否允许切换")]
        [AutoMapPropertyResolve("isHOAllowed", typeof(EUtranRelationZte), typeof(IntToBoolTransform))]
        [AutoMapPropertyResolve("NoHoFlag", typeof(EutranIntraFreqNCell), typeof(ZeroBoolTransform))]
        [AutoMapPropertyResolve("NoHoFlag", typeof(EutranInterFreqNCell), typeof(ZeroBoolTransform))]
        public bool HandoffAllowed { get; set; }

        [MemberDoc("是否可以被ANR删除")]
        [AutoMapPropertyResolve("isRemoveAllowed", typeof(EUtranRelationZte), typeof(IntToBoolTransform))]
        [AutoMapPropertyResolve("NoRmvFlag", typeof(EutranIntraFreqNCell), typeof(ZeroBoolTransform))]
        [AutoMapPropertyResolve("NoRmvFlag", typeof(EutranInterFreqNCell), typeof(ZeroBoolTransform))]
        public bool RemovedAllowed { get; set; }

        [MemberDoc("小区测量优先级是否为高")]
        [AutoMapPropertyResolve("CellMeasPriority", typeof(EutranIntraFreqNCell))]
        [AutoMapPropertyResolve("CellMeasPriority", typeof(EutranInterFreqNCell))]
        [AutoMapPropertyResolve("nCelPriority", typeof(EUtranRelationZte))]
        public int CellPriority { get; set; }
    }

    public class ExternalEUtranCellFDDZte : IEntity<ObjectId>, IZteMongo
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

        public int? esCellNum { get; set; }

        public double earfcnDl { get; set; }

        public int cellType { get; set; }

        public int pci { get; set; }

        public string userLabel { get; set; }

        public int antPort1 { get; set; }

        public int cellLocalId { get; set; }

        public int plmnIdList_mcc { get; set; }

        public int switchSurportTrunking { get; set; }

        public int tac { get; set; }

        public int plmnIdList_mnc { get; set; }

        public string reservedByEUtranRelation { get; set; }

        public int bandWidthUl { get; set; }

        public int mcc { get; set; }

        public int eNBId { get; set; }

        public double earfcnUl { get; set; }

        public int freqBandInd { get; set; }

        public int voLTESwch { get; set; }

        public int coMPFlagUl { get; set; }

        public int bandWidthDl { get; set; }

        public int mnc { get; set; }

        public string addiFreqBand { get; set; }

        public int ExternalEUtranCellFDD { get; set; }

        public int? freqBandPriInd { get; set; }

        public int? freqBandPriSwch { get; set; }
    }

    public class EutranIntraFreqNCell : IEntity<ObjectId>, IHuaweiNeighborMongo
    {
        public ObjectId Id { get; set; }

        public bool IsTransient()
        {
            return false;
        }

        public string iDate { get; set; }

        public int eNodeB_Id { get; set; }

        public string eNodeBId_Name { get; set; }

        public int CtrlMode { get; set; }

        public int CellMeasPriority { get; set; }

        public string NeighbourCellName { get; set; }

        public int LocalCellId { get; set; }

        public int AttachCellSwitch { get; set; }

        public int NoHoFlag { get; set; }

        public int CellId { get; set; }

        public string LocalCellName { get; set; }

        public int CellRangeExpansion { get; set; }

        public int Mnc { get; set; }

        public int Mcc { get; set; }

        public int NCellClassLabel { get; set; }

        public int CellQoffset { get; set; }

        public int NoRmvFlag { get; set; }

        public int eNodeBId { get; set; }

        public int CellIndividualOffset { get; set; }

        public int AnrFlag { get; set; }

        public int? VectorCellFlag { get; set; }

        public int? HighSpeedCellIndOffset { get; set; }
    }
}