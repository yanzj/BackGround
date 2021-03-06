﻿angular.module('region.import', ['app.core'])
    .factory('basicImportService',
        function(generalHttpService) {
            return {
                queryENodebExcels: function() {
                    return generalHttpService.getApiData('NewENodebExcels', {});
                },
                queryCellExcels: function() {
                    return generalHttpService.getApiData('NewCellExcels', {});
                },
                queryBtsExcels: function() {
                    return generalHttpService.getApiData('NewBtsExcels', {});
                },
                queryCdmaCellExcels: function() {
                    return generalHttpService.getApiData('NewCdmaCellExcels', {});
                },
                queryCellCount: function() {
                    return generalHttpService.getApiData('DumpLteRru', {});
                },
                queryCdmaCellCount: function() {
                    return generalHttpService.getApiData('DumpCdmaRru', {});
                },
                queryVanishedENodebs: function() {
                    return generalHttpService.getApiData('DumpENodebExcel', {});
                },
                queryVanishedBtss: function() {
                    return generalHttpService.getApiData('DumpBtsExcel', {});
                },
                queryVanishedCells: function() {
                    return generalHttpService.getApiData('DumpCellExcel', {});
                },
                queryVanishedCdmaCells: function() {
                    return generalHttpService.getApiData('DumpCdmaCellExcel', {});
                },
                dumpOneENodebExcel: function(item) {
                    return generalHttpService.postApiData('DumpENodebExcel', item);
                },
                dumpOneBtsExcel: function(item) {
                    return generalHttpService.postApiData('DumpBtsExcel', item);
                },
                dumpOneCellExcel: function(item) {
                    return generalHttpService.postApiData('DumpCellExcel', item);
                },
                dumpOneCdmaCellExcel: function(item) {
                    return generalHttpService.postApiData('DumpCdmaCellExcel', item);
                },
                updateLteCells: function() {
                    return generalHttpService.postApiData('DumpLteRru', {});
                },
                dumpLteRrus: function() {
                    return generalHttpService.putApiData('DumpLteRru', {});
                },
                dumpCdmaRrus: function() {
                    return generalHttpService.putApiData('DumpCdmaRru', {});
                },
                dumpMultipleENodebExcels: function(items) {
                    return generalHttpService.postApiData('NewENodebExcels',
                    {
                        infos: items
                    });
                },
                dumpMultipleBtsExcels: function(items) {
                    return generalHttpService.postApiData('NewBtsExcels',
                    {
                        infos: items
                    });
                },
                dumpMultipleCellExcels: function(items) {
                    return generalHttpService.postApiData('NewCellExcels',
                    {
                        infos: items
                    });
                },
                dumpMultipleCdmaCellExcels: function(items) {
                    return generalHttpService.postApiData('NewCdmaCellExcels',
                    {
                        infos: items
                    });
                },
                vanishENodebIds: function(ids) {
                    return generalHttpService.putApiData('DumpENodebExcel',
                    {
                        eNodebIds: ids
                    });
                },
                vanishBtsIds: function(ids) {
                    return generalHttpService.putApiData('DumpBtsExcel',
                    {
                        eNodebIds: ids
                    });
                },
                vanishCellIds: function(ids) {
                    return generalHttpService.putApiData('DumpCellExcel',
                    {
                        cellIdPairs: ids
                    });
                },
                vanishCdmaCellIds: function(ids) {
                    return generalHttpService.putApiData('DumpCdmaCellExcel',
                    {
                        cellIdPairs: ids
                    });
                },
                dumpOneHotSpot: function(item) {
                    return generalHttpService.postApiData('HotSpot', item);
                },
                queryAllHotSpots: function() {
                    return generalHttpService.getApiData('HotSpot', {});
                },
                queryHotSpotsByType: function(type) {
                    return generalHttpService.getApiData('HotSpot',
                    {
                        type: type
                    });
                },
                queryHotSpotCells: function(name) {
                    return generalHttpService.getApiData('LteRruCell',
                    {
                        rruName: name
                    });
                },
                dumpSingleItem: function() {
                    return generalHttpService.putApiData('DumpNeighbor', {});
                },
                queryStationInfos: function() {
                    return generalHttpService.getApiData('DumpStationInfo', {});
                },
                dumpStationInfo: function () {
                    return generalHttpService.putApiData('DumpStationInfo', {});
                },
                resetStationInfo: function () {
                    return generalHttpService.deleteApiData('StationInfoReset', {});
                },
                queryStationENodebs: function () {
                    return generalHttpService.getApiData('DumpStationENodeb', {});
                },
                dumpStationENodeb: function () {
                    return generalHttpService.putApiData('DumpStationENodeb', {});
                },
                resetStationENodeb: function () {
                    return generalHttpService.deleteApiData('StationENodebReset', {});
                },
                queryStationCells: function () {
                    return generalHttpService.getApiData('DumpStationCell', {});
                },
                dumpStationCell: function () {
                    return generalHttpService.putApiData('DumpStationCell', {});
                },
                resetStationCell: function () {
                    return generalHttpService.deleteApiData('StationCellReset', {});
                },
                queryStationRrus: function () {
                    return generalHttpService.getApiData('DumpStationRru', {});
                },
                dumpStationRru: function () {
                    return generalHttpService.putApiData('DumpStationRru', {});
                },
                resetStationRru: function () {
                    return generalHttpService.deleteApiData('StationRruReset', {});
                },
                queryStationAntennas: function () {
                    return generalHttpService.getApiData('DumpStationAntenna', {});
                },
                dumpStationAntenna: function () {
                    return generalHttpService.putApiData('DumpStationAntenna', {});
                },
                resetStationAntenna: function () {
                    return generalHttpService.deleteApiData('StationAntennaReset', {});
                },
                queryStationDistributions: function () {
                    return generalHttpService.getApiData('DumpStationDistribution', {});
                },
                dumpStationDistribution: function () {
                    return generalHttpService.putApiData('DumpStationDistribution', {});
                },
                resetStationDistribution: function () {
                    return generalHttpService.deleteApiData('StationDistributionReset', {});
                }
            };
        })
    .factory('flowImportService',
        function(generalHttpService) {
            return {
                queryHuaweiFlows: function() {
                    return generalHttpService.getApiData('DumpHuaweiFlow', {});
                },
                queryHuaweiCqis: function() {
                    return generalHttpService.getApiData('DumpHuaweiCqi', {});
                },
                queryHuaweiRssis: function () {
                    return generalHttpService.getApiData('DumpHuaweiRssi', {});
                },
                queryZteFlows: function() {
                    return generalHttpService.getApiData('DumpZteFlow', {});
                },
                queryHourPrbs: function () {
                    return generalHttpService.getApiData('DumpHourPrb', {});
                },
                queryHourUserses: function () {
                    return generalHttpService.getApiData('DumpHourUsers', {});
                },
                queryHourCqis: function () {
                    return generalHttpService.getApiData('DumpHourCqi', {});
                },

                clearDumpHuaweis: function() {
                    return generalHttpService.deleteApiData('DumpHuaweiFlow');
                },
                clearDumpCqiHuaweis: function() {
                    return generalHttpService.deleteApiData('DumpHuaweiCqi');
                },
                clearDumpRssiHuaweis: function () {
                    return generalHttpService.deleteApiData('DumpHuaweiRssi');
                },
                clearDumpZtes: function() {
                    return generalHttpService.deleteApiData('DumpZteFlow');
                },
                clearDumpHourPrbs: function() {
                    return generalHttpService.deleteApiData('DumpHourPrb');
                },
                clearDumpHourUserses: function () {
                    return generalHttpService.deleteApiData('DumpHourUsers');
                },
                clearDumpHourCqis: function () {
                    return generalHttpService.deleteApiData('DumpHourCqi');
                },

                dumpHuaweiItem: function() {
                    return generalHttpService.putApiData('DumpHuaweiFlow', {});
                },
                dumpHuaweiCqiItem: function() {
                    return generalHttpService.putApiData('DumpHuaweiCqi', {});
                },
                dumpHuaweiRssiItem: function () {
                    return generalHttpService.putApiData('DumpHuaweiRssi', {});
                },
                dumpZteItem: function() {
                    return generalHttpService.putApiData('DumpZteFlow', {});
                },
                dumpHourPrb: function () {
                    return generalHttpService.putApiData('DumpHourPrb', {});
                },
                dumpHourUserses: function () {
                    return generalHttpService.putApiData('DumpHourUsers', {});
                },
                dumpHourCqis: function () {
                    return generalHttpService.putApiData('DumpHourCqi', {});
                },

                queryHuaweiStat: function(index) {
                    return generalHttpService.getApiData('DumpHuaweiFlow',
                        {
                            index: index
                        });
                },
                queryFlowDumpHistory: function(begin, end) {
                    return generalHttpService.getApiData('DumpFlow',
                        {
                            begin: begin,
                            end: end
                        });
                },
                queryHourDumpHistory: function (begin, end) {
                    return generalHttpService.getApiData('DumpHour',
                        {
                            begin: begin,
                            end: end
                        });
                },
                dumpTownStats: function(statDate) {
                    return generalHttpService.getApiData('DumpStats',
                        {
                            statDate: statDate
                        });
                },
                dumpTownFlows: function(statDate) {
                    return generalHttpService.getApiData('DumpFlow',
                        {
                            statDate: statDate
                        });
                },
                dumpTownHourStats: function (statDate) {
                    return generalHttpService.getApiData('DumpHour',
                        {
                            statDate: statDate
                        });
                },
                dumpTownHourCqis: function (statDate) {
                    return generalHttpService.getApiData('DumpHourCqi',
                        {
                            statDate: statDate
                        });
                },
                dumpTownCqis: function(statDate) {
                    return generalHttpService.getApiData('DumpTownCqi',
                        {
                            statDate: statDate
                        });
                },
                dumpTownPrbs: function(statDate) {
                    return generalHttpService.getApiData('DumpTownPrb',
                        {
                            statDate: statDate
                        });
                },
                dumpTownDoubleFlows: function(statDate) {
                    return generalHttpService.getApiData('DumpTownDoubleFlow',
                        {
                            statDate: statDate
                        });
                }
            };
        })
    .factory('flowService',
        function(generalHttpService) {
            return {
                queryCellFlowByDateSpan: function(eNodebId, sectorId, begin, end) {
                    return generalHttpService.getApiData('FlowQuery',
                    {
                        eNodebId: eNodebId,
                        sectorId: sectorId,
                        begin: begin,
                        end: end
                    });
                },
                queryCellRrcByDateSpan: function(eNodebId, sectorId, begin, end) {
                    return generalHttpService.getApiData('RrcQuery',
                    {
                        eNodebId: eNodebId,
                        sectorId: sectorId,
                        begin: begin,
                        end: end
                    });
                },
                queryAverageFlowByDateSpan: function(eNodebId, sectorId, begin, end) {
                    return generalHttpService.getApiData('FlowQuery',
                    {
                        eNodebId: eNodebId,
                        sectorId: sectorId,
                        beginDate: begin,
                        endDate: end
                    });
                },
                queryAverageRrcByDateSpan: function(eNodebId, sectorId, begin, end) {
                    return generalHttpService.getApiData('RrcQuery',
                    {
                        eNodebId: eNodebId,
                        sectorId: sectorId,
                        beginDate: begin,
                        endDate: end
                    });
                },
                queryENodebGeoFlowByDateSpan: function(begin, end, frequency) {
                    return generalHttpService.getApiData('ENodebFlow',
                    {
                        begin: begin,
                        end: end,
                        frequency: frequency
                    });
                }
            };
        })
    .factory('alarmImportService',
        function(generalHttpService) {
            return {
                queryDumpHistory: function(begin, end) {
                    return generalHttpService.getApiData('DumpAlarm',
                    {
                        begin: begin,
                        end: end
                    });
                },
                queryCoverageHistory: function(begin, end) {
                    return generalHttpService.getApiData('DumpAlarm',
                        {
                            beginDate: begin,
                            endDate: end
                        });
                },
                updateTownCoverageStats: function(statDate, frequency) {
                    return generalHttpService.getApiData('DumpCoverage',
                    {
                        statDate: statDate,
                        frequency: frequency
                    });
                },
                updateCollegeCoverageStats: function(statDate) {
                    return generalHttpService.getApiData('CollegeMroRsrp',
                        {
                            statTime: statDate
                        });
                },
                updateMarketCoverageStats: function(statDate) {
                    return generalHttpService.getApiData('MarketMroRsrp',
                        {
                            statTime: statDate
                        });
                },
                updateTransportationCoverageStats: function(statDate) {
                    return generalHttpService.getApiData('TransportationMroRsrp',
                        {
                            statTime: statDate
                        });
                },
                queryDumpItems: function() {
                    return generalHttpService.getApiData('DumpAlarm', {});
                },
                queryCoverageDumpItems: function () {
                    return generalHttpService.getApiData('DumpCoverage', {});
                },
                queryZhangshangyouQualityDumpItems: function () {
                    return generalHttpService.getApiData('DumpZhangshangyouQuality', {});
                },
                queryZhangshangyouCoverageDumpItems: function () {
                    return generalHttpService.getApiData('DumpZhangshangyouCoverage', {});
                },
                dumpSingleItem: function() {
                    return generalHttpService.putApiData('DumpAlarm', {});
                },
                dumpSingleCoverageItem: function () {
                    return generalHttpService.putApiData('DumpCoverage', {});
                },
                dumpSingleZhangshangyouQualityItem: function () {
                    return generalHttpService.putApiData('DumpZhangshangyouQuality', {});
                },
                dumpSingleZhangshangyouCoverageItem: function () {
                    return generalHttpService.putApiData('DumpZhangshangyouCoverage', {});
                },
                clearImportItems: function() {
                    return generalHttpService.deleteApiData('DumpAlarm');
                },
                clearCoverageImportItems: function () {
                    return generalHttpService.deleteApiData('DumpCoverage');
                },
                clearZhangshangyouQualityImportItems: function () {
                    return generalHttpService.deleteApiData('DumpZhangshangyouQuality');
                },
                clearZhangshangyouCoverageItems: function () {
                    return generalHttpService.deleteApiData('DumpZhangshangyouCoverage');
                },
                updateHuaweiAlarmInfos: function(cellDef) {
                    return generalHttpService.postApiData('Alarms', cellDef);
                }
            };
        });