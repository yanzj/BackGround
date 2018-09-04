using Abp.EntityFramework;
using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.DataService.College;
using Lte.Evaluations.DataService.Dt;
using Lte.Evaluations.DataService.Dump;
using Lte.Evaluations.DataService.Kpi;
using Lte.Evaluations.DataService.Mr;
using Lte.Evaluations.DataService.RegionKpi;
using Lte.Evaluations.DataService.Switch;
using Lte.MySqlFramework.Abstract.Cdma;
using Lte.MySqlFramework.Abstract.College;
using Lte.MySqlFramework.Abstract.Complain;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Kpi;
using Lte.MySqlFramework.Abstract.Maintainence;
using Lte.MySqlFramework.Abstract.Mr;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Abstract.RegionKpi;
using Lte.MySqlFramework.Abstract.Station;
using Lte.MySqlFramework.Abstract.Test;
using Lte.MySqlFramework.Concrete;
using Lte.MySqlFramework.Concrete.Cdma;
using Lte.MySqlFramework.Concrete.College;
using Lte.MySqlFramework.Concrete.Complain;
using Lte.MySqlFramework.Concrete.Infrastructure;
using Lte.MySqlFramework.Concrete.Kpi;
using Lte.MySqlFramework.Concrete.Maintainence;
using Lte.MySqlFramework.Concrete.Mr;
using Lte.MySqlFramework.Concrete.Region;
using Lte.MySqlFramework.Concrete.RegionKpi;
using Lte.MySqlFramework.Concrete.Station;
using Lte.MySqlFramework.Concrete.Test;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Abstract.Infrastructure;
using Lte.Parameters.Abstract.Kpi;
using Lte.Parameters.Abstract.Switch;
using Lte.Parameters.Concrete;
using Lte.Parameters.Concrete.Basic;
using Lte.Parameters.Concrete.Channel;
using Lte.Parameters.Concrete.College;
using Lte.Parameters.Concrete.Kpi;
using Lte.Parameters.Concrete.Neighbor;
using Lte.Parameters.Concrete.Switch;
using LtePlatform.Models;
using Ninject;

namespace LtePlatform
{
    public static class KernelBindingOpertions
    {
        public static void AddBindings(this IKernel ninjectKernel)
        {
            ninjectKernel.Bind<EFParametersContext>().ToSelf();

            ninjectKernel.Bind<IDbContextProvider<EFParametersContext>>()
                .To<SimpleDbContextProvider<EFParametersContext>>();

            ninjectKernel.Bind<MySqlContext>().ToSelf();

            ninjectKernel.Bind<IDbContextProvider<MySqlContext>>().To<SimpleDbContextProvider<MySqlContext>>();

            ninjectKernel.Bind<ITownRepository>().To<TownRepository>();

            ninjectKernel.Bind<ICdmaRegionStatRepository>().To<CdmaRegionStatRepository>();

            ninjectKernel.Bind<IOptimzeRegionRepository>().To<OptimizeOptimzeRegionRepository>();

            ninjectKernel.Bind<ICollegeRepository>().To<CollegeRepository>();

            ninjectKernel.Bind<IInfrastructureRepository>().To<InfrastructureRepository>();

            ninjectKernel.Bind<IAlarmRepository>().To<EFAlarmRepository>();

            ninjectKernel.Bind<ICollege3GTestRepository>().To<EFCollege3GTestRepository>();

            ninjectKernel.Bind<ICollege4GTestRepository>().To<EFCollege4GTestRepository>();
            
            ninjectKernel.Bind<IENodebRepository>().To<ENodebRepository>();

            ninjectKernel.Bind<IBtsRepository>().To<BtsRepository>();

            ninjectKernel.Bind<ICellRepository>().To<CellRepository>();

            ninjectKernel.Bind<ICdmaCellRepository>().To<CdmaCellRepository>();

            ninjectKernel.Bind<IIndoorDistributionRepository>().To<IndoorDistributionRepository>();

            ninjectKernel.Bind<IPreciseCoverage4GRepository>().To<PreciseCoverage4GRepository>();

            ninjectKernel.Bind<IPreciseMongoRepository>().To<PreciseMongoRepository>();

            ninjectKernel.Bind<ITopDrop2GCellRepository>().To<TopDrop2GCellRepository>();

            ninjectKernel.Bind<ITopConnection3GRepository>().To<TopConnection3GRepository>();

            ninjectKernel.Bind<ITopConnection2GRepository>().To<TopConnection2GRepository>();

            ninjectKernel.Bind<ITownPreciseCoverage4GStatRepository>().To<TownPreciseCoverage4GStatRepository>();

            ninjectKernel.Bind<IAreaTestDateRepository>().To<AreaTestDateDateRepository>();

            ninjectKernel.Bind<IFileRecordRepository>().To<MasterFileRecordRepository>();

            ninjectKernel.Bind<IRasterInfoRepository>().To<RasterInfoRepository>();

            ninjectKernel.Bind<IRasterTestInfoRepository>().To<RasterTestInfoRepository>();

            ninjectKernel.Bind<ILteNeighborCellRepository>().To<LteNeighborCellRepository>();

            ninjectKernel.Bind<INearestPciCellRepository>().To<NearestPciCellRepository>();

            ninjectKernel.Bind<IWorkItemRepository>().To<WorkItemRepository>();

            ninjectKernel.Bind<IInterferenceMatrixRepository>().To<EFInterferenceMatrixRepository>();

            ninjectKernel.Bind<IInterferenceMongoRepository>().To<InterferenceMongoRepository>();

            ninjectKernel.Bind<ICellHuaweiMongoRepository>().To<CellHuaweiMongoRepository>();

            ninjectKernel.Bind<ICellMeasGroupZteRepository>().To<CellMeasGroupZteRepository>();

            ninjectKernel.Bind<IUeEUtranMeasurementRepository>().To<UeEUtranMeasurementRepository>();

            ninjectKernel.Bind<IIntraFreqHoGroupRepository>().To<IntraFreqHoGroupRepository>();

            ninjectKernel.Bind<IIntraRatHoCommRepository>().To<IntraRatHoCommRepository>();

            ninjectKernel.Bind<IInterRatHoCommRepository>().To<InterRatHoCommRepository>();

            ninjectKernel.Bind<IEutranInterNFreqRepository>().To<EutranInterNFreqRepository>();

            ninjectKernel.Bind<IInterFreqHoGroupRepository>().To<InterFreqHoGroupRepository>();

            ninjectKernel.Bind<IEUtranRelationZteRepository>().To<EUtranRelationZteRepository>();

            ninjectKernel.Bind<IEutranIntraFreqNCellRepository>().To<EutranIntraFreqNCellRepository>();

            ninjectKernel.Bind<IEutranInterFreqNCellRepository>().To<EutranInterFreqNCellRepository>();

            ninjectKernel.Bind<IExternalEUtranCellFDDZteRepository>().To<ExternalEUtranCellFDDZteRepository>();

            ninjectKernel.Bind<IEUtranCellMeasurementZteRepository>().To<EUtranCellMeasurementZteRepository>();

            ninjectKernel.Bind<IEUtranCellFDDZteRepository>().To<EUtranCellFDDZteRepository>();

            ninjectKernel.Bind<IPrachFDDZteRepository>().To<PrachFDDZteRepository>();

            ninjectKernel.Bind<IPowerControlDLZteRepository>().To<PowerControlDLZteRepository>();
            
            ninjectKernel.Bind<IPDSCHCfgRepository>().To<PDSCHCfgRepository>();

            ninjectKernel.Bind<ICellDlpcPdschPaRepository>().To<CellDlpcPdschPaRepository>();

            ninjectKernel.Bind<IFlowHuaweiRepository>().To<FlowHuaweiRepository>();

            ninjectKernel.Bind<IFlowZteRepository>().To<FlowZteRepository>();
            
            ninjectKernel.Bind<IPreciseWorkItemCellRepository>().To<PreciseWorkItemCellRepositroy>();

            ninjectKernel.Bind<ICellPowerService>().To<CellPowerService>();

            ninjectKernel.Bind<IEmergencyCommunicationRepository>().To<EmergencyCommunicationRepository>();
            
            ninjectKernel.Bind<IVipDemandRepository>().To<VipDemandRepository>();

            ninjectKernel.Bind<ICollegeYearRepository>().To<CollegeYearRepository>();

            ninjectKernel.Bind<ICellUlpcCommRepository>().To<CellUlpcCommRepository>();

            ninjectKernel.Bind<IPowerControlULZteRepository>().To<PowerControlULZteRepository>();

            ninjectKernel.Bind<ICdmaRruRepository>().To<CdmaRruRepository>();

            ninjectKernel.Bind<IBranchDemandRepository>().To<BranchDemandRepository>();

            ninjectKernel.Bind<IOnlineSustainRepository>().To<OnlineSustainRepository>();

            ninjectKernel.Bind<IVipProcessRepository>().To<VipProcessRepository>();

            ninjectKernel.Bind<IPlanningSiteRepository>().To<PlanningSiteRepository>();

            ninjectKernel.Bind<IMrsTadvRepository>().To<MrsTadvRepository>();

            ninjectKernel.Bind<IMrsPhrRepository>().To<MrsPhrRepository>();

            ninjectKernel.Bind<IMrsRsrpRepository>().To<MrsRsrpRepository>();

            ninjectKernel.Bind<ITownMrsRsrpRepository>().To<TownMrsRsrpRepository>();

            ninjectKernel.Bind<IMrsSinrUlRepository>().To<MrsSinrUlRepository>();

            ninjectKernel.Bind<IMrsTadvRsrpRepository>().To<MrsTadvRsrpRepository>();

            ninjectKernel.Bind<ITownFlowRepository>().To<TownFlowRepository>();

            ninjectKernel.Bind<IRrcZteRepository>().To<RrcZteRepository>();

            ninjectKernel.Bind<IRrcHuaweiRepository>().To<RrcHuaweiRepository>();

            ninjectKernel.Bind<IQciZteRepository>().To<QciZteRepository>();

            ninjectKernel.Bind<ICqiZteRepository>().To<CqiZteRepository>();

            ninjectKernel.Bind<IQciHuaweiRepository>().To<QciHuaweiRepository>();

            ninjectKernel.Bind<ICqiHuaweiRepository>().To<CqiHuaweiRepository>();

            ninjectKernel.Bind<IDoubleFlowHuaweiRepository>().To<DoubleFlowHuaweiRepository>();

            ninjectKernel.Bind<IDoubleFlowZteRepository>().To<DoubleFlowZteRepository>();

            ninjectKernel.Bind<IPrbZteRepository>().To<PrbZteRepository>();

            ninjectKernel.Bind<IPrbHuaweiRepository>().To<PrbHuaweiRepository>();

            ninjectKernel.Bind<ITownRrcRepository>().To<TownRrcRepository>();

            ninjectKernel.Bind<ITownQciRepository>().To<TownQciRepository>();

            ninjectKernel.Bind<ITownCqiRepository>().To<TownCqiRepository>();

            ninjectKernel.Bind<ITownDoubleFlowRepository>().To<TownDoubleFlowRepository>();

            ninjectKernel.Bind<ITownPrbRepository>().To<TownPrbRepository>();
            
            ninjectKernel.Bind<IAgisDtPointRepository>().To<AgisDtPointRepository>();

            ninjectKernel.Bind<IMrGridRepository>().To<MrGridRepository>();
            
            ninjectKernel.Bind<IStationDictionaryRepository>().To<StationDictionaryRepository>();

            ninjectKernel.Bind<IConstructionInformationRepository>().To<ConstructionInformationRepository>();

            ninjectKernel.Bind<IENodebBaseRepository>().To<ENodebBaseRepository>();
            
            ninjectKernel.Bind<IDistributionRepository>().To<DistributionRepository>();

            ninjectKernel.Bind<IHotSpotENodebRepository>().To<HotSpotENodebRepository>();

            ninjectKernel.Bind<IHotSpotBtsRepository>().To<HotSpotBtsRepository>();

            ninjectKernel.Bind<IHotSpotCellRepository>().To<HotSpotCellRepository>();

            ninjectKernel.Bind<IHotSpotCdmaCellRepository>().To<HotSpotCdmaCellRepository>();

            ninjectKernel.Bind<IMicroItemRepository>().To<MicroItemRepository>();

            ninjectKernel.Bind<IMicroAddressRepository>().To<MicroAddressRepository>();

            ninjectKernel.Bind<IEmergencyProcessRepository>().To<EmergencyProcessRepository>();

            ninjectKernel.Bind<IEmergencyFiberWorkItemRepository>().To<EmergencyFiberWorkItemRepository>();

            ninjectKernel.Bind<IComplainItemRepository>().To<ComplainItemRepository>();

            ninjectKernel.Bind<IComplainProcessRepository>().To<ComplainProcessRepository>();

            ninjectKernel.Bind<ILteRruRepository>().To<LteRruRepository>();

            ninjectKernel.Bind<ITownBoundaryRepository>().To<TownBoundaryRepository>();
            
            ninjectKernel.Bind<ITelecomAgpsRepository>().To<TelecomAgpsRepository>();

            ninjectKernel.Bind<IMobileAgpsRepository>().To<MobileAgpsRepository>();

            ninjectKernel.Bind<IUnicomAgpsRepository>().To<UnicomAgpsRepository>();

            ninjectKernel.Bind<IMrGridKpiRepository>().To<MrGridKpiRepository>();

            ninjectKernel.Bind<IGridClusterRepository>().To<GridClusterRepository>();

            ninjectKernel.Bind<IDpiGridKpiRepository>().To<DpiGridKpiRepository>();

            ninjectKernel.Bind<IMonthKpiRepository>().To<MonthKpiRepository>();

            ninjectKernel.Bind<IDtFileInfoRepository>().To<DtFileInfoRepository>();

            ninjectKernel.Bind<IAreaTestInfoRepository>().To<AreaTestInfoRepository>();
            
            ninjectKernel.Bind<ITopMrsRsrpRepository>().To<TopMrsRsrpRepository>();

            ninjectKernel.Bind<ITopMrsSinrUlRepository>().To<TopMrsSinrUlRepository>();

            ninjectKernel.Bind<ILteProblemRepository>().To<LteProblemRepository>();

            ninjectKernel.Bind<ITownMrsSinrUlRepository>().To<TownMrsSinrUlRepository>();

            ninjectKernel.Bind<ICoverageStatRepository>().To<CoverageStatRepository>();

            ninjectKernel.Bind<ITownCoverageRepository>().To<TownCoverageRepository>();

            ninjectKernel.Bind<IUserLoginSessionInfoRepository>().To<UserLoginSessionInfoRepository>();

            ninjectKernel.Bind<IModuleUsageRecordRepository>().To<ModuleUsageRecordRepository>();

            ninjectKernel.Bind<IStationRruRepository>().To<StationRruRepository>();

            ninjectKernel.Bind<IStationAntennaRepository>().To<StationAntennaRepository>();

            ninjectKernel.Bind<IAlarmWorkItemRepository>().To<AlarmWorkItemRepository>();

            ninjectKernel.Bind<IZhangshangyouQualityRepository>().To<ZhangshangyouQualityRepository>();

            ninjectKernel.Bind<IZhangshangyouCoverageRepository>().To<ZhangshangyouCoverageRepository>();

            ninjectKernel.Bind<ICheckingProjectRepository>().To<CheckingProjectRepository>();

            ninjectKernel.Bind<ICheckingBasicRepository>().To<CheckingBasicRepository>();

            ninjectKernel.Bind<ICheckingDetailsRepository>().To<CheckingDetailsRepository>();

            ninjectKernel.Bind<IHourPrbRepository>().To<HourPrbRepository>();

            ninjectKernel.Bind<ITownHourPrbRepository>().To<TownHourPrbRepository>();

            ninjectKernel.Bind<IHourUsersRepository>().To<HourUsersRepository>();

            ninjectKernel.Bind<ITownHourUsersRepository>().To<TownHourUsersRepository>();

            ninjectKernel.Bind<IHourCqiRepository>().To<HourCqiRepository>();

            ninjectKernel.Bind<ITownHourCqiRepository>().To<TownHourCqiRepository>();

            ninjectKernel.Bind<CdmaRegionStatService>().ToSelf();

            ninjectKernel.Bind<CollegeStatService>().ToSelf();

            ninjectKernel.Bind<ENodebQueryService>().ToSelf();
            
            ninjectKernel.Bind<BtsConstructionService>().ToSelf();

            ninjectKernel.Bind<BtsQueryService>().ToSelf();

            ninjectKernel.Bind<CollegeENodebService>().ToSelf();

            ninjectKernel.Bind<CollegeBtssService>().ToSelf();

            ninjectKernel.Bind<CellService>().ToSelf();

            ninjectKernel.Bind<CdmaCellService>().ToSelf();

            ninjectKernel.Bind<College3GTestService>().ToSelf();

            ninjectKernel.Bind<College4GTestService>().ToSelf();

            ninjectKernel.Bind<CollegeLteDistributionService>().ToSelf();

            ninjectKernel.Bind<CollegePreciseService>().ToSelf();
            
            ninjectKernel.Bind<CollegeCdmaCellViewService>().ToSelf();
            
            ninjectKernel.Bind<CollegeCellViewService>().ToSelf();

            ninjectKernel.Bind<CollegeAlarmService>().ToSelf();
            
            ninjectKernel.Bind<PreciseStatService>().ToSelf();

            ninjectKernel.Bind<TownQueryService>().ToSelf();

            ninjectKernel.Bind<KpiImportService>().ToSelf();

            ninjectKernel.Bind<PreciseRegionStatService>().ToSelf();

            ninjectKernel.Bind<PreciseImportService>().ToSelf();

            ninjectKernel.Bind<AlarmsService>().ToSelf();

            ninjectKernel.Bind<TopDrop2GService>().ToSelf();

            ninjectKernel.Bind<TopConnection3GService>().ToSelf();

            ninjectKernel.Bind<BasicImportService>().ToSelf();

            ninjectKernel.Bind<ENodebDumpService>().ToSelf();

            ninjectKernel.Bind<BtsDumpService>().ToSelf();

            ninjectKernel.Bind<CellDumpService>().ToSelf();

            ninjectKernel.Bind<CdmaCellDumpService>().ToSelf();

            ninjectKernel.Bind<AreaTestDateService>().ToSelf();

            ninjectKernel.Bind<CsvFileInfoService>().ToSelf();

            ninjectKernel.Bind<RasterInfoService>().ToSelf();

            ninjectKernel.Bind<LteNeighborCellService>().ToSelf();

            ninjectKernel.Bind<NearestPciCellService>().ToSelf();

            ninjectKernel.Bind<WorkItemService>().ToSelf();

            ninjectKernel.Bind<InterferenceMatrixService>().ToSelf();
            
            ninjectKernel.Bind<InterferenceNeighborService>().ToSelf();

            ninjectKernel.Bind<InterferenceMongoService>().ToSelf();

            ninjectKernel.Bind<CellHuaweiMongoService>().ToSelf();

            ninjectKernel.Bind<IntraFreqHoService>().ToSelf();

            ninjectKernel.Bind<InterFreqHoService>().ToSelf();

            ninjectKernel.Bind<NeighborCellMongoService>().ToSelf();
            
            ninjectKernel.Bind<FlowService>().ToSelf();

            ninjectKernel.Bind<FlowQueryService>().ToSelf();

            ninjectKernel.Bind<RrcQueryService>().ToSelf();

            ninjectKernel.Bind<PreciseWorkItemService>().ToSelf();
            
            ninjectKernel.Bind<EmergencyCommunicationService>().ToSelf();

            ninjectKernel.Bind<VipDemandService>().ToSelf();

            ninjectKernel.Bind<EmergencyFiberService>().ToSelf();

            ninjectKernel.Bind<UlOpenLoopPcService>().ToSelf();

            ninjectKernel.Bind<ComplainService>().ToSelf();

            ninjectKernel.Bind<MicroAmplifierService>().ToSelf();

            ninjectKernel.Bind<PlanningQueryService>().ToSelf();

            ninjectKernel.Bind<MrsService>().ToSelf();

            ninjectKernel.Bind<TownFlowService>().ToSelf();

            ninjectKernel.Bind<HotSpotService>().ToSelf();

            ninjectKernel.Bind<GridClusterService>().ToSelf();

            ninjectKernel.Bind<DpiGridKpiService>().ToSelf();

            ninjectKernel.Bind<TownSupportService>().ToSelf();

            ninjectKernel.Bind<AgpsService>().ToSelf();

            ninjectKernel.Bind<MrGridService>().ToSelf();

            ninjectKernel.Bind<TownTestInfoService>().ToSelf();

            ninjectKernel.Bind<RrcRegionStatService>().ToSelf();

            ninjectKernel.Bind<TownBoundaryService>().ToSelf();

            ninjectKernel.Bind<CqiQueryService>().ToSelf();

            ninjectKernel.Bind<MonthKpiService>().ToSelf();

            ninjectKernel.Bind<MrsRegionStatService>().ToSelf();

            ninjectKernel.Bind<TopMrsRsrpService>().ToSelf();

            ninjectKernel.Bind<LteProblemService>().ToSelf();

            ninjectKernel.Bind<BandCellService>().ToSelf();

            ninjectKernel.Bind<CellRruService>().ToSelf();

            ninjectKernel.Bind<ENodebFlowService>().ToSelf();

            ninjectKernel.Bind<TownKpiService>().ToSelf();

            ninjectKernel.Bind<CoverageStatService>().ToSelf();

            ninjectKernel.Bind<TopCoverageStatService>().ToSelf();

            ninjectKernel.Bind<TownCoverageService>().ToSelf();

            ninjectKernel.Bind<CoverageRegionStatService>().ToSelf();

            ninjectKernel.Bind<PrbQueryService>().ToSelf();

            ninjectKernel.Bind<UserRecordService>().ToSelf();

            ninjectKernel.Bind<CqiReportService>().ToSelf();

            ninjectKernel.Bind<StationInfoService>().ToSelf();

            ninjectKernel.Bind<AlarmWorkItemService>().ToSelf();

            ninjectKernel.Bind<ENodebBaseService>().ToSelf();

            ninjectKernel.Bind<IndoorDistributionService>().ToSelf();

            ninjectKernel.Bind<ConstructionInformationService>().ToSelf();

            ninjectKernel.Bind<StationRruService>().ToSelf();

            ninjectKernel.Bind<StationAntennaService>().ToSelf();

            ninjectKernel.Bind<ZhangshangyouQualityRepository>().ToSelf();

            ninjectKernel.Bind<ZhangshangyouCoverageRepository>().ToSelf();

            ninjectKernel.Bind<CheckingService>().ToSelf();

            ninjectKernel.Bind<MrGridKpiService>().ToSelf();

            ninjectKernel.Bind<MrsRsrpImportService>().ToSelf();

            ninjectKernel.Bind<MrsSinrUlImportService>().ToSelf();

            ninjectKernel.Bind<DoubleFlowRegionStatService>().ToSelf();

            ninjectKernel.Bind<DoubleFlowQueryService>().ToSelf();

            ninjectKernel.Bind<HourKpiService>().ToSelf();

            ninjectKernel.Bind<RegionTownFlowService>().ToSelf();

            ninjectKernel.Bind<TownRrcService>().ToSelf();

            ninjectKernel.Bind<TownQciService>().ToSelf();

            ninjectKernel.Bind<TownCqiService>().ToSelf();

            ninjectKernel.Bind<TownPrbService>().ToSelf();

            ninjectKernel.Bind<TownDoubleFlowService>().ToSelf();

            ninjectKernel.Bind<TownHourKpiService>().ToSelf();

            ninjectKernel.Bind<TownHourPrbService>().ToSelf();

            ninjectKernel.Bind<HourPrbService>().ToSelf();

            ninjectKernel.Bind<HourUsersService>().ToSelf();

            ninjectKernel.Bind<TownHourUsersService>().ToSelf();

            ninjectKernel.Bind<HourPrbRegionStatService>().ToSelf();

            ninjectKernel.Bind<HourUsersRegionStatService>().ToSelf();

            ninjectKernel.Bind<SinrUlRegionStatService>().ToSelf();

            ninjectKernel.Bind<HourPrbQueryService>().ToSelf();

            ninjectKernel.Bind<StationImportService>().ToSelf();

            ninjectKernel.Bind<HourUsersQueryService>().ToSelf();

            ninjectKernel.Bind<HourCqiService>().ToSelf();

            ninjectKernel.Bind<TownHourCqiService>().ToSelf();

        }
    }
}
