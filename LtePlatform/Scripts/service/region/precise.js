angular.module('region.precise', ['app.core'])
    .factory('workitemService',
        function(generalHttpService) {
            return {
                queryWithPaging: function(state, type) {
                    return generalHttpService.getApiDataWithHeading('WorkItem',
                    {
                        'statCondition': state,
                        'typeCondition': type
                    });
                },
                queryWithPagingByDistrict: function(state, type, district) {
                    return generalHttpService.getApiDataWithHeading('WorkItem',
                    {
                        statCondition: state,
                        typeCondition: type,
                        district: district
                    });
                },
                querySingleItem: function(serialNumber) {
                    return generalHttpService.getApiData('WorkItem',
                    {
                        serialNumber: serialNumber
                    });
                },
                signIn: function(serialNumber) {
                    return generalHttpService.getApiDataWithHeading('WorkItem',
                    {
                        signinNumber: serialNumber
                    });
                },
                queryChartData: function(chartType) {
                    return generalHttpService.getApiDataWithHeading('WorkItem',
                    {
                        chartType: chartType
                    });
                },
                updateSectorIds: function() {
                    return generalHttpService.putApiData('WorkItem', {});
                },
                feedback: function(message, serialNumber) {
                    return generalHttpService.postApiDataWithHeading('WorkItem',
                    {
                        message: message,
                        serialNumber: serialNumber
                    });
                },
                finish: function(comments, finishNumber) {
                    return generalHttpService.getApiDataWithHeading('WorkItem',
                    {
                        finishNumber: finishNumber,
                        comments: comments
                    });
                },
                queryByENodebId: function(eNodebId) {
                    return generalHttpService.getApiDataWithHeading('WorkItem',
                    {
                        eNodebId: eNodebId
                    });
                },
                queryByCellId: function(eNodebId, sectorId) {
                    return generalHttpService.getApiDataWithHeading('WorkItem',
                    {
                        eNodebId: eNodebId,
                        sectorId: sectorId
                    });
                },
                queryCurrentMonth: function() {
                    return generalHttpService.getApiData('WorkItemCurrentMonth', {});
                },
                constructPreciseItem: function(cell, begin, end) {
                    return generalHttpService.postApiDataWithHeading('PreciseWorkItem',
                    {
                        view: cell,
                        begin: begin,
                        end: end
                    });
                }
            };
        })
    .factory('dumpWorkItemService',
        function(generalHttpService) {
            return {
                dumpSingleItem: function() {
                    return generalHttpService.putApiData('DumpWorkItem', {});
                },
                clearImportItems: function() {
                    return generalHttpService.deleteApiData('DumpWorkItem');
                },
                queryTotalDumpItems: function() {
                    return generalHttpService.getApiData('DumpWorkItem', {});
                },
                dumpSingleSpecialItem: function () {
                    return generalHttpService.putApiData('DumpSpecialWorkItem', {});
                },
                clearImportSpecialItems: function () {
                    return generalHttpService.deleteApiData('DumpSpecialWorkItem');
                },
                queryTotalSpecialDumpItems: function () {
                    return generalHttpService.getApiData('DumpSpecialWorkItem', {});
                }
            };
        })
    .factory('preciseWorkItemService',
        function(generalHttpService) {
            return {
                queryByDateSpanDistrict: function(begin, end, district) {
                    return generalHttpService.getApiData('PreciseWorkItem',
                    {
                        begin: begin,
                        end: end,
                        district: district
                    });
                },
                queryByDateSpan: function(begin, end) {
                    return generalHttpService.getApiData('PreciseWorkItem',
                    {
                        begin: begin,
                        end: end
                    });
                },
                queryBySerial: function(number) {
                    return generalHttpService.getApiDataWithHeading('PreciseWorkItem',
                    {
                        number: number
                    });
                },
                updateInterferenceNeighbor: function(number, items) {
                    return generalHttpService.postApiDataWithHeading('InterferenceNeighborWorkItem',
                    {
                        workItemNumber: number,
                        items: items
                    });
                },
                updateInterferenceVictim: function(number, items) {
                    return generalHttpService.postApiDataWithHeading('InterferenceVictimWorkItem',
                    {
                        workItemNumber: number,
                        items: items
                    });
                },
                updateCoverage: function(number, items) {
                    return generalHttpService.postApiDataWithHeading('CoverageWorkItem',
                    {
                        workItemNumber: number,
                        items: items
                    });
                }
            };
        })
    .factory('preciseImportService', //////////////////////////////////////////////////////////
        function(generalHttpService) {
            return {
                queryDumpHistroy: function(beginDate, endDate) {
                    return generalHttpService.getApiData('PreciseImport',
                    {
                        begin: beginDate,
                        end: endDate
                    });
                },
                queryDumpRsrpHistroy: function (beginDate, endDate) {
                    return generalHttpService.getApiData('MrsRsrpImport',
                    {
                        begin: beginDate,
                        end: endDate
                    });
                },
                queryDumpSinrHistroy: function (beginDate, endDate) {
                    return generalHttpService.getApiData('MrsSinrUlImport',
                        {
                            begin: beginDate,
                            end: endDate
                        });
                },
                queryDumpTaHistroy: function (beginDate, endDate) {
                    return generalHttpService.getApiData('MrsTadvImport',
                        {
                            begin: beginDate,
                            end: endDate
                        });
                },

                queryTotalDumpItems: function() {
                    return generalHttpService.getApiData('PreciseImport', {});
                },
                queryTotalMrsRsrpItems: function () {
                    return generalHttpService.getApiData('MrsRsrpImport', {});
                },
                queryTotalMrsSinrUlItems: function () {
                    return generalHttpService.getApiData('MrsSinrUlImport', {});
                },
                queryTotalMrsTadvItems: function () {
                    return generalHttpService.getApiData('MrsTadvImport', {});
                },

                queryTownPreciseViews: function(statTime, frequency) {
                    return generalHttpService.getApiData('TownPreciseImport',
                    {
                        statTime: statTime,
                        frequency: frequency
                    });
                },
                queryCollegePreciseViews: function(statTime) {
                    return generalHttpService.getApiData('TownPreciseImport',
                        {
                            statDate: statTime
                        });
                },
                queryMarketPreciseViews: function(statTime) {
                    return generalHttpService.getApiData('MarketPrecise',
                        {
                            statDate: statTime
                        });
                },
                queryTownMrsStats: function (statTime, frequency) {
                    return generalHttpService.getApiData('MrsRsrpImport',
                        {
                            statTime: statTime,
                            frequency: frequency
                        });
                },
                queryCollegeMrsStats: function(statTime) {
                    return generalHttpService.getApiData('MrsRsrpImport',
                        {
                            statDate: statTime
                        });
                },
                queryMarketMrsStats: function(statTime) {
                    return generalHttpService.getApiData('MarketMrsRsrp',
                        {
                            statDate: statTime
                        });
                },
                queryTownSinrUlStats: function (statTime, frequency) {
                    return generalHttpService.getApiData('MrsSinrUlImport',
                        {
                            statTime: statTime,
                            frequency: frequency
                        });
                },
                queryCollegeSinrUlStats: function(statTime) {
                    return generalHttpService.getApiData('MrsSinrUlImport',
                        {
                            statDate: statTime
                        });
                },
                queryMarketSinrUlStats: function(statTime) {
                    return generalHttpService.getApiData('MarketMrsSinrUl',
                        {
                            statDate: statTime
                        });
                },
                queryTownTaStats: function (statTime, frequency) {
                    return generalHttpService.getApiData('MrsTadvImport',
                        {
                            statTime: statTime,
                            frequency: frequency
                        });
                },
                queryCollegeTaStats: function(statTime) {
                    return generalHttpService.getApiData('MrsTadvImport',
                        {
                            statDate: statTime
                        });
                },

                queryTopMrsStats: function (statTime) {
                    return generalHttpService.getApiData('MrsRsrpImport',
                        {
                            topDate: statTime
                        });
                },
                queryTopMrsSinrUlStats: function (statTime) {
                    return generalHttpService.getApiData('MrsSinrUlImport',
                        {
                            topDate: statTime
                        });
                },
                queryTopMrsTadvStats: function (statTime) {
                    return generalHttpService.getApiData('MrsTadvImport',
                        {
                            topDate: statTime
                        });
                },

                clearImportItems: function() {
                    return generalHttpService.deleteApiData('PreciseImport', {});
                },
                clearMrsRsrpItems: function () {
                    return generalHttpService.deleteApiData('MrsRsrpImport', {});
                },
                clearMrsSinrUlItems: function () {
                    return generalHttpService.deleteApiData('MrsSinrUlImport', {});
                },
                clearMrsTadvItems: function () {
                    return generalHttpService.deleteApiData('MrsTadvImport', {});
                },

                dumpTownItems: function(
                    views, collegeStats, marketStats, views800, views1800, views2100
                ) {
                    return generalHttpService.postApiData('TownPreciseImport',
                    {
                        views: views,
                        collegeStats: collegeStats,
                        marketStats: marketStats,
                        views800: views800,
                        views1800: views1800,
                        views2100: views2100
                    });
                },
                dumpTownRsrpItems: function( 
                    mrsStats, collegeMrsStats, marketMrsStats, mrsStats800, mrsStats1800, mrsStats2100
                ) {
                    return generalHttpService.postApiData('MrsRsrpImport',
                        {
                            mrsRsrps: mrsStats,
                            collegeMrsRsrps: collegeMrsStats,
                            marketMrsRsrps: marketMrsStats,
                            mrsRsrps800: mrsStats800,
                            mrsRsrps1800: mrsStats1800,
                            mrsRsrps2100: mrsStats2100
                        });
                },
                dumpTownSinrItems: function (
                    mrsSinrUls, collegeSinrUls, marketSinrUls, mrsSinrUls800, mrsSinrUls1800, mrsSinrUls2100
                ) {
                    return generalHttpService.postApiData('MrsSinrUlImport',
                    {
                        mrsSinrUls: mrsSinrUls,
                        collegeMrsSinrUls: collegeSinrUls,
                        marketMrsSinrUls: marketSinrUls,
                        mrsSinrUls800: mrsSinrUls800,
                        mrsSinrUls1800: mrsSinrUls1800,
                        mrsSinrUls2100: mrsSinrUls2100
                    });
                },
                dumpTownTaItems: function (
                    mrsTadvs, collegeTadvs, mrsTadvs800, mrsTadvs1800, mrsTadvs2100
                ) {
                    return generalHttpService.postApiData('MrsTadvImport',
                        {
                            mrsTadvs: mrsTadvs,
                            collegeMrsTadvs: collegeTadvs,
                            mrsTadvs800: mrsTadvs800,
                            mrsTadvs1800: mrsTadvs1800,
                            mrsTadvs2100: mrsTadvs2100
                        });
                },

                dumpTownAgpsItems: function(views) {
                    return generalHttpService.postApiData('MrGrid',
                    {
                        views: views
                    });
                },
                dumpSingleItem: function() {
                    return generalHttpService.putApiData('PreciseImport', {});
                },
                dumpSingleMrsRsrpItem: function () {
                    return generalHttpService.putApiData('MrsRsrpImport', {});
                },
                dumpSingleMrsSinrUlItem: function () {
                    return generalHttpService.putApiData('MrsSinrUlImport', {});
                },
                dumpSingleMrsTadvItem: function () {
                    return generalHttpService.putApiData('MrsTadvImport', {});
                },
                updateMongoItems: function(statDate) {
                    return generalHttpService.getApiData('PreciseMongo',
                    {
                        statDate: statDate
                    });
                }
            };
        })
    .factory('cellPreciseService',
        function(generalHttpService) {
            return {
                queryDataSpanKpi: function(begin, end, cellId, sectorId) {
                    return generalHttpService.getApiData('PreciseStat',
                    {
                        'begin': begin,
                        'end': end,
                        'cellId': cellId,
                        'sectorId': sectorId
                    });
                },
                queryOneWeekKpi: function(cellId, sectorId) {
                    return generalHttpService.getApiData('PreciseStat',
                    {
                        'cellId': cellId,
                        'sectorId': sectorId
                    });
                }
            };
        })
    .factory('appRegionService',
        function(generalHttpService) {
            return {
                initializeCities: function() {
                    return generalHttpService.getApiData('CityList', {});
                },
                queryDistricts: function(cityName) {
                    return generalHttpService.getApiData('CityList',
                    {
                        city: cityName
                    });
                },
                queryTowns: function(cityName, districtName) {
                    return generalHttpService.getApiData('CityList',
                    {
                        city: cityName,
                        district: districtName
                    });
                },
                queryTown: function(city, district, town) {
                    return generalHttpService.getApiData('Town',
                    {
                        city: city,
                        district: district,
                        town: town
                    });
                },
                queryTownBoundaries: function(city, district, town) {
                    return generalHttpService.getApiData('TownBoundary',
                    {
                        city: city,
                        district: district,
                        town: town
                    });
                },
                isInTownBoundary: function(longtitute, lattitute, city, district, town) {
                    return generalHttpService.getApiData('TownBoundary',
                    {
                        longtitute: longtitute,
                        lattitute: lattitute,
                        city: city,
                        district: district,
                        town: town
                    });
                },
                queryAreaBoundaries: function() {
                    return generalHttpService.getApiData('AreaBoundary', {});
                },
                queryENodebTown: function(eNodebId) {
                    return generalHttpService.getApiData('Town',
                    {
                        eNodebId: eNodebId
                    });
                },
                accumulateCityStat: function(stats, cityName) {
                    var cityStat = {
                        district: cityName,
                        totalLteENodebs: 0,
                        totalLteCells: 0,
                        totalNbIotCells: 0,
                        totalCdmaBts: 0,
                        totalCdmaCells: 0
                    };
                    angular.forEach(stats,
                        function(stat) {
                            cityStat.totalLteENodebs += stat.totalLteENodebs;
                            cityStat.totalLteCells += stat.totalLteCells;
                            cityStat.totalNbIotCells += stat.totalNbIotCells;
                            cityStat.totalCdmaBts += stat.totalCdmaBts;
                            cityStat.totalCdmaCells += stat.totalCdmaCells;
                        });
                    stats.push(cityStat);
                },
                getTownFlowStats: function(statDate, frequency) { ///////////////////////////////////////////////////////////
                    return generalHttpService.getApiData('TownFlow',
                    {
                        statDate: statDate,
                        frequency: frequency
                    });
                },
                getCurrentDateTownFlowStats: function (statDate, frequency) { //////////////////////////////////////////
                    return generalHttpService.getApiData('TownFlow',
                        {
                            currentDate: statDate,
                            frequency: frequency
                        });
                },
                getCurrentDateTownCqiStats: function (statDate, frequency) { ////////////////////////////////////////////////
                    return generalHttpService.getApiData('TownCqi',
                        {
                            currentDate: statDate,
                            frequency: frequency
                        });
                },
                getCurrentDateTownPrbStats: function (statDate, frequency) { ////////////////////////////////////////////////
                    return generalHttpService.getApiData('TownPrb',
                        {
                            currentDate: statDate,
                            frequency: frequency
                        });
                },
                getCurrentDateTownHourCqiStats: function (statDate, frequency) { ////////////////////////////////////////////
                    return generalHttpService.getApiData('TownHourCqi',
                        {
                            currentDate: statDate,
                            frequency: frequency
                        });
                },
                updateTownFlowStat: function(stat) { ///////////////////////////////////////////////////////////////////
                    return generalHttpService.postApiData('TownFlow', stat);
                },
                updateTownCqiStat: function (stat) { //////////////////////////////////////////////////////////////////////
                    return generalHttpService.postApiData('TownCqi', stat);
                },
                updateTownPrbStat: function (stat) { //////////////////////////////////////////////////////////////////////
                    return generalHttpService.postApiData('TownPrb', stat);
                },
                updateTownHourCqiStat: function (stat) { /////////////////////////////////////////////////////////////////////////
                    return generalHttpService.postApiData('TownHourCqi', stat);
                }
            };
        });