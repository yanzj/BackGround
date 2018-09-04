using System;
using System.Collections.Generic;
using Lte.Domain.Common.Wireless.Alarm;
using Lte.Domain.Common.Wireless.Antenna;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Common.Wireless.Complain;
using Lte.Domain.Common.Wireless.Distribution;
using Lte.Domain.Common.Wireless.ENodeb;
using Lte.Domain.Common.Wireless.Kpi;
using Lte.Domain.Common.Wireless.Region;
using Lte.Domain.Common.Wireless.Station;
using Lte.Domain.Common.Wireless.Work;

namespace Lte.Domain.Common.Wireless
{
    public static class WirelessConstants
    {
        public static readonly Dictionary<string, Tuple<object, string>[]> EnumDictionary = new Dictionary
            <string, Tuple<object, string>[]>
            {
                {
                    "AlarmType", AlarmTypeTuples.GetTuples()
                },
                {
                    "AlarmLevel", AlarmLevelTuples.GetTuples()
                },
                {
                    "AlarmCategory", AlarmCategoryTuples.GetTuples()
                },
                {
                    "AntennaPortsConfigure", AntennaPortsConfigureTuples.GetTuples()
                },
                {
                    "DemandLevel", DemandLevelTuples.GetTuples()
                },
                {
                    "VehicleType", VehicleTypeTuples.GetTuples()
                },
                {
                    "OrderPreciseStatPolicy", OrderPreciseStatPolicyTuples.GetTuples()
                },
                {
                    "OrderDownSwitchPolicy", OrderDownSwitchPolicyTuples.GetTuples()
                },
                {
                    "OrderCqiPolicy", OrderCqiPolicyTuples.GetTuples()
                },
                {
                    "OrderMrsRsrpPolicy", OrderMrsRsrpPolicyTuples.GetTuples()
                },
                {
                    "OrderTopConnection3GPolicy", OrderTopConnection3GPolicyTuples.GetTuples()
                },
                {
                    "OrderTopDrop2GPolicy", OrderTopDrop2GPolicyTuples.GetTuples()
                },
                {
                    "OrderPrbStatPolicy", OrderPrbStatPolicyTuples.GetTuples()
                },
                {
                    "OrderTopFlowPolicy", OrderTopFlowPolicyTuples.GetTuples()
                },
                {
                    "OrderFeelingRatePolicy", OrderFeelingRatePolicyTuples.GetTuples()
                },
                {
                    "NetworkType", NetworkTypeTuples.GetTuples()
                },
                {
                    "MarketTheme", MarketThemeTuples.GetTuples()
                },
                {
                    "EmergencyState", EmergencyStateTuples.GetTuples()
                },
                {
                    "ComplainSource", ComplainSourceTuples.GetTuples()
                },
                {
                    "CustomerType", CustomerTypeTuples.GetTuples()
                },
                {
                    "ComplainReason", ComplainReasonTuples.GetTuples()
                },
                {
                    "ComplainSubReason", ComplainSubReasonTuples.GetTuples()
                },
                {
                    "ComplainScene", ComplainSceneTuples.GetTuples()
                },
                {
                    "ComplainCategory", ComplainCategoryTuples.GetTuples()
                },
                {
                    "WorkItemType", WorkItemTypeTuples.GetTuples()
                },
                {
                    "WorkItemSubtype", WorkItemSubtypeTuples.GetTuples()
                },
                {
                    "WorkItemState", WorkItemStateTuples.GetTuples()
                },
                {
                    "WorkItemCause", WorkItemCauseTuples.GetTuples()
                },
                {
                    "SolveFunction", SolveFunctionTuples.GetTuples()
                },
                {
                    "VipState", VipStateTuples.GetTuples()
                },
                {
                    "ComplainState", ComplainStateTuples.GetTuples()
                },
                {
                    "AntennaFactory", AntennaFactoryTuples.GetTuples()
                },
                {
                    "InfrastructureType", InfrastructureTypeTuples.GetTuples()
                },
                {
                    "HotspotType", HotspotTypeTuples.GetTuples()
                },
                {
                    "ElectricFunction", ElectricFunctionTuples.GetTuples()
                },
                {
                    "ElectricType", ElectricTypeTuples.GetTuples()
                },
                {
                    "BatteryType", BatteryTypeTuples.GetTuples()
                },
                {
                    "OperatorUsage", OperatorUsageTuples.GetTuples()
                },
                {
                    "Operator", OperatorTuples.GetTuples()
                },
                {
                    "TowerType", TowerTypeTuples.GetTuples()
                },
                {
                    "ENodebFactory", ENodebFactoryTuples.GetTuples()
                },
                {
                    "Duplexing", DuplexingTuples.GetTuples()
                },
                {
                    "OmcState", OmcStateTuples.GetTuples()
                },
                {
                    "ENodebType", ENodebTypeTuples.GetTuples()
                },
                {
                    "ENodebClass", ENodebClassTuples.GetTuples()
                },
                {
                    "CellCoverage", CellCoverageTuples.GetTuples()
                },
                {
                    "AntennaType", AntennaTypeTuples.GetTuples()
                },
                {
                    "RemoteType", RemoteTypeTuples.GetTuples()
                },
                {
                    "ElectricSource", ElectricSourceTuples.GetTuples()
                },
                {
                    "ShareFunction", ShareFunctionTuples.GetTuples()
                },
                {
                    "AntennaDirection", AntennaDirectionTuples.GetTuples()
                },
                {
                    "AntennaPolar", AntennaPolarTuples.GetTuples()
                },
                {
                    "AntennaBeauty", AntennaBeautyTuples.GetTuples()
                },
                {
                    "CoverageArea", CoverageAreaTuples.GetTuples()
                },
                {
                    "CoverageRoad", CoverageRoadTuples.GetTuples()
                },
                {
                    "CoverageHotspot", CoverageHotspotTuples.GetTuples()
                },
                {
                    "BoundaryType", BoundaryTypeTuples.GetTuples()
                },
                {
                    "IndoorCategory", IndoorCategoryTuples.GetTuples()
                },
                {
                    "IndoorNetwork", IndoorNetworkTuples.GetTuples()
                },
                {
                    "CombinerFunction", CombinerFunctionTuples.GetTuples()
                },
                {
                    "OldCombiner", OldCombinerTuples.GetTuples()
                },
                {
                    "DistributionChannel", DistributionChannelTuples.GetTuples()
                },
                {
                    "TestType", TestTypeTuples.GetTuples()
                },
                {
                    "OrderDoubleFlowPolicy", OrderDoubleFlowPolicyTuples.GetTuples()
                },
                {
                    "OrderUsersStatPolicy", OrderUsersStatPolicyTuples.GetTuples()
                }
            };
    }
}
