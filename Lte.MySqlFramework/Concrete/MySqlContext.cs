using Abp.EntityFramework;
using Lte.MySqlFramework.Entities;
using System.Data.Entity;
using Abp.EntityFramework.Entities;
using MySql.Data.Entity;

namespace Lte.MySqlFramework.Concrete
{
    //实施数据库迁移前，请解除注释；迁移完成后，请再次注释编译后发布，否则在IIS上程序会报错
    //[DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MySqlContext : AbpDbContext
    {
        public MySqlContext() : base("MySqlContext")
        {
            
        }
        
        public DbSet<FlowHuawei> FlowHuaweises { get; set; }

        public DbSet<FlowZte> FlowZtes { get; set; }
        
        public DbSet<PreciseWorkItemCell> PreciseWorkItemCells { get; set; }

        public DbSet<EmergencyCommunication> EmergencyCommunications { get; set; }
        
        public DbSet<VipDemand> VipDemands { get; set; }

        public DbSet<EmergencyProcess> EmergencyProcesses { get; set; }

        public DbSet<EmergencyFiberWorkItem> EmergencyFiberWorkItems { get; set; }

        public DbSet<CollegeYearInfo> CollegeYearInfos { get; set; }

        public DbSet<ComplainItem> ComplainItems { get; set; }

        public DbSet<CdmaRegionStat> CdmaRegionStats { get; set; }

        public DbSet<TopDrop2GCell> TopDrop2GStats { get; set; }

        public DbSet<TopConnection2GCell> TopConnection2GStats { get; set; }

        public DbSet<TopConnection3GCell> TopConnection3GStats { get; set; }

        public DbSet<CdmaRru> CdmaRrus { get; set; }

        public DbSet<BranchDemand> BranchDemands { get; set; }

        public DbSet<OnlineSustain> OnlineSustains { get; set; }

        public DbSet<VipProcess> VipProcesses { get; set; }

        public DbSet<ComplainProcess> ComplainProcesses { get; set; }

        public DbSet<LteRru> LteRrus { get; set; }

        public DbSet<College3GTestResults> College3GTestResultses { get; set; }

        public DbSet<College4GTestResults> College4GTestResultses { get; set; }
        
        public DbSet<PlanningSite> PlanningSites { get; set; }

        public DbSet<TownFlowStat> TownFlowStats { get; set; }

        public DbSet<TownRrcStat> TownRrcStats { get; set; }

        public DbSet<RrcZte> RrcZtes { get; set; }

        public DbSet<RrcHuawei> RrcHuaweis { get; set; }

        public DbSet<AgisDtPoint> AgisDtPoints { get; set; }

        public DbSet<MrGrid> MrGrids { get; set; }

        public DbSet<StationDictionary> StationDictionaries { get; set; }

        public DbSet<DistributionSystem> DistributionSystems { get; set; }

        public DbSet<HotSpotENodebId> HotSpotENodebIds { get; set; }

        public DbSet<HotSpotCellId> HotSpotCellIds { get; set; }

        public DbSet<HotSpotBtsId> HotSpotBtsIds { get; set; }

        public DbSet<HotSpotCdmaCellId> HotSpotCdmaCellIds { get; set; }

        public DbSet<TownBoundary> TownBoundaries { get; set; }

        public DbSet<AreaTestInfo> AreaTestInfos { get; set; }

        public DbSet<ENodebBase> Enodeb_Bases { get; set; }

        public DbSet<ConstructionInformation> Construction_Informations { get; set; }

        public DbSet<MicroItem> MicroItems { get; set; }

        public DbSet<MicroAddress> MicroAddresses { get; set; }

        public DbSet<WorkItem> WorkItems { get; set; }

        public DbSet<MrGridKpi> MrGridKpis { get; set; }

        public DbSet<GridCluster> GridClusters { get; set; }

        public DbSet<DpiGridKpi> DpiGridKpis { get; set; }

        public DbSet<MonthKpiStat> AgpsCoverageTowns { get; set; }

        public DbSet<RasterInfo> RasterInfos { get; set; }

        public DbSet<RasterTestInfo> RasterTestInfos { get; set; }
        
        public DbSet<CsvFilesInfo> CsvFilesInfos { get; set; }

        public DbSet<AreaTestDate> AreaTestDates { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<OptimizeRegion> OptimizeRegions { get; set; }

        public DbSet<CollegeInfo> CollegeInfos { get; set; }

        public DbSet<Cell> Cells { get; set; }

        public DbSet<CdmaCell> CdmaCells { get; set; }

        public DbSet<LteNeighborCell> LteNeighborCells { get; set; }

        public DbSet<NearestPciCell> NearestPciCells { get; set; }

        public DbSet<InfrastructureInfo> InfrastructureInfos { get; set; }

        public DbSet<IndoorDistribution> IndoorDistributions { get; set; }

        public DbSet<ENodeb> ENodebs { get; set; }

        public DbSet<CdmaBts> Btss { get; set; }

        public DbSet<QciHuawei> QciHuaweis { get; set; }

        public DbSet<CqiHuawei> CqiHuaweis { get; set; }

        public DbSet<QciZte> QciZtes { get; set; }

        public DbSet<CqiZte> CqiZtes { get; set; }

        public DbSet<TownQciStat> TownQciStats { get; set; }

        public DbSet<TownCqiStat> TownCqiStats { get; set; }

        public DbSet<PrbHuawei> PrbHuaweis { get; set; }

        public DbSet<PrbZte> PrbZtes { get; set; }

        public DbSet<TownPrbStat> TownPrbStats { get; set; }

        public DbSet<TownMrsRsrp> TownMrsRsrps { get; set; }

        public DbSet<TopMrsRsrp> TopMrsRsrps { get; set; }

        public DbSet<TownMrsSinrUl> TownMrsSinrUls { get; set; }

        public DbSet<TopMrsSinrUl> TopMrsSinrUls { get; set; }

        public DbSet<LteProblem> LteProblems { get; set; }

        public DbSet<CoverageStat> CoverageStats { get; set; }

        public DbSet<TownCoverageStat> TownCoverageStats { get; set; }

        public DbSet<UserLoginSessionInfo> UserLoginSessionInfos { get; set; }

        public DbSet<ModuleUsageRecord> ModuleUsageRecords { get; set; }

        public DbSet<DoubleFlowHuawei> DoubleFlowHuaweis { get; set; }

        public DbSet<DoubleFlowZte> DoubleFlowZtes { get; set; }

        public DbSet<TownDoubleFlow> TownDoubleFlows { get; set; }

        public DbSet<StationRru> StationRrus { get; set; }

        public DbSet<StationAntenna> StationAntennas { get; set; }

        public DbSet<AlarmWorkItem> AlarmWorkItems { get; set; }

        public DbSet<ZhangshangyouQuality> ZhangshangyouQualities { get; set; }

        public DbSet<ZhangshangyouCoverage> ZhangshangyouCoverages { get; set; }

        public DbSet<CheckingProject> CheckingProjects { get; set; }

        public DbSet<CheckingBasic> CheckingBasics { get; set; }

        public DbSet<CheckingDetails> CheckingDetailses { get; set; }

    }
}
