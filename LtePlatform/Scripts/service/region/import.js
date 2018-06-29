angular.module('region.import', ['app.core'])
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
                }
            };
        })
    .factory('flowImportService',
        function(generalHttpService) {
            return {
                queryHuaweiFlows: function() {
                    return generalHttpService.getApiData('DumpHuaweiFlow', {});
                },
                queryHuaweiCqis: function () {
                    return generalHttpService.getApiData('DumpHuaweiCqi', {});
                },
                queryZteFlows: function() {
                    return generalHttpService.getApiData('DumpZteFlow', {});
                },
                clearDumpHuaweis: function() {
                    return generalHttpService.deleteApiData('DumpHuaweiFlow');
                },
                clearDumpCqiHuaweis: function () {
                    return generalHttpService.deleteApiData('DumpHuaweiCqi');
                },
                clearDumpZtes: function() {
                    return generalHttpService.deleteApiData('DumpZteFlow');
                },
                dumpHuaweiItem: function() {
                    return generalHttpService.putApiData('DumpHuaweiFlow', {});
                },
                dumpHuaweiCqiItem: function () {
                    return generalHttpService.putApiData('DumpHuaweiCqi', {});
                },
                dumpZteItem: function() {
                    return generalHttpService.putApiData('DumpZteFlow', {});
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
                dumpTownStats: function(statDate) {
                    return generalHttpService.getApiData('DumpFlow',
                    {
                        statDate: statDate
                    });
                },
                dumpTownCqis: function (statDate) {
                    return generalHttpService.getApiData('DumpFlow',
                        {
                            statTime: statDate
                        });
                }
            }
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
    .factory('alarmsService',
        function(generalHttpService) {
            return {
                queryENodebAlarmsByDateSpanAndLevel: function(eNodebId, begin, end, level) {
                    return generalHttpService.getApiData('Alarms',
                    {
                        eNodebId: eNodebId,
                        begin: begin,
                        end: end,
                        level: level
                    });
                },
                queryMicroItems: function() {
                    return generalHttpService.getApiData('MicroAmplifier', {});
                },
                queryGridClusters: function(theme) {
                    return generalHttpService.getApiData('GridCluster',
                    {
                        theme: theme
                    });
                },
                queryGridClusterRange: function(theme, west, east, south, north) {
                    return generalHttpService.getApiData('GridCluster',
                    {
                        theme: theme,
                        west: west,
                        east: east,
                        south: south,
                        north: north
                    });
                },
                queryClusterGridKpis: function(points) {
                    return generalHttpService.postApiData('GridCluster', points);
                },
                queryClusterKpi: function(points) {
                    return generalHttpService.putApiData('GridCluster', points);
                },
                queryDpiGridKpi: function(x, y) {
                    return generalHttpService.getApiData('DpiGridKpi',
                    {
                        x: x,
                        y: y
                    });
                }
            };
        })
    .factory('alarmImportService',
        function(generalHttpService, alarmsService) {
            return {
                queryDumpHistory: function(begin, end) {
                    return generalHttpService.getApiData('DumpAlarm',
                    {
                        begin: begin,
                        end: end
                    });
                },
                updateTownCoverageStats: function(statDate) {
                    return generalHttpService.getApiData('DumpCoverage',
                    {
                        statDate: statDate
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
                },
                updateClusterKpi: function(stat, callback) {
                    alarmsService.queryClusterKpi(stat.gridPoints).then(function(result) {
                        stat.rsrp = result.rsrp;
                        stat.weakRate = result.weakCoverageRate;
                        stat.bestLongtitute = result.longtitute;
                        stat.bestLattitute = result.lattitute;
                        stat.x = result.x;
                        stat.y = result.y;
                        if (callback) {
                            callback(stat);
                        }

                    });
                }
            };
        });